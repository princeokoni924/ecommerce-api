using System.Linq;
using Core.Contract;
using Core.Contract.ICartServices;
using Core.Contract.IGeneric;
using Core.Entities;
using Core.Redis;
using Microsoft.Extensions.Configuration;
using Stripe;


namespace Infrastructure.Services
{
    public class PaymentService(
     IConfiguration Iconfig,
     IShoppingCartServices _shopservice,
     IGenericRepo<Core.Entities.Product> _proRepo,
     IGenericRepo<DeliveryMethod> _dmRepo) : IPaymentService
    {
        
           
        public async Task<ShoppingCart?> CreateOrUpdatePaymentItent(string cartId)
        {
           // set up stripe configuration
           StripeConfiguration.ApiKey = Iconfig["StripeSettings:secratKey"];
           var cart = await _shopservice.GetShoppingCartAsync(cartId);
              if(cart == null)
               {
                return null;
                };
                var deliveryPrice = 0m;
                if(cart.DeliveryMethodId.HasValue)
                {
                    var deliveryMethod = await _dmRepo.GetDataByIdAsync((int)cart.DeliveryMethodId);
                    if(deliveryMethod != null)
                    {
                        deliveryPrice = deliveryMethod.Price;
                    }else{
                        return null;
                    }
                }

                // update and validate the item in the cart
                foreach(var item in cart.Items)
                {
                  var productItemFromDatabase = await _proRepo
                  .GetDataByIdAsync(item.ProductId);
                  if(productItemFromDatabase == null)
                  {
                      return null;
                  }

                  if(item.Price != productItemFromDatabase.Price)
                  {
                      item.Price = productItemFromDatabase.Price;
                  }
                }

                var service = new PaymentIntentService();
                     PaymentIntent? intent = null;


                     var amountInKobo = (long)cart.Items.Sum(i=>i
                     .Quantity*(i.Price*100)+ deliveryPrice*100);
                            // Validate the minimum amount (Stripe requires at least $0.50 USD)
                             var exchangeRate = 1678; // 1 USD to NGN
          
                             //var amountInUSD = amountInKobo / (100* exchangeRate); // convert the amount to USD
                             var mininumAmountInKobo = (long) (0.5m * exchangeRate* 100); // 50 cents in kobo
                             if(amountInKobo < mininumAmountInKobo)
                             {
                                 throw new Exception("The total amount must convert to at least $0.50.");
                             }

                               
                // if payment intent is not exist in the cart
                    if(string.IsNullOrEmpty(cart.PaymentIntentId))
                    {
                               // create new payment intent
                                var newPaymentIntent = new PaymentIntentCreateOptions
                                {
                                    Amount = amountInKobo,
                                    Currency = "NGN",
                                };
                       
                            intent = await service.CreateAsync(newPaymentIntent);

                        // setting cart to store the payment intent id and client secret code
                        cart.PaymentIntentId = intent.Id; // the id is use to identify the payment intent
                        cart.ClientSecretCode = intent.ClientSecret; // the secret is use to communicate with stripe

                        }else{
                            // update existing payment intent
                            var updatePaymentIntent = new PaymentIntentUpdateOptions
                            {

                                Amount = amountInKobo,
                                // Amount = (long)cart.Items.
                                // Sum(i=>i.Quantity*(i.Price*100))
                                // + (long)deliveryPrice*100,// the amount to be paid
                            }; 
                            // set intent to the updated payment intent
                           intent = await service.UpdateAsync(cart.PaymentIntentId, updatePaymentIntent);
                        }
                        // save the cart to the redis
                        await _shopservice.SetShoppingCartAsync(cart); 
                        return cart; // return the updated cart
                    }

        public Task<string> RefundPayment(string paymentIntentId)
        {
            throw new NotImplementedException();
        }
    }

    
    }

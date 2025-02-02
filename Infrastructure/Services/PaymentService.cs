// using System.Linq;
// using Core.Contract;
// using Core.Contract.ICartServices;
// using Core.Contract.IGeneric;
// using Core.Entities;
// using Core.Redis;
// using Microsoft.Extensions.Configuration;
// using Stripe;
// using Stripe.Tax;
// using Stripe.V2;


// namespace Infrastructure.Services
// {
//     public class PaymentService(
//      IConfiguration Iconfig,
//      IShoppingCartServices _shopservice,
//      IGenericRepo<Core.Entities.Product> _proRepo,
//      IGenericRepo<DeliveryMethod> _dmRepo) : IPaymentService
//     {
//         public async Task<ShoppingCart?> CreateOrUpdatePaymentItent(string cartId)
//         {
//            // set up stripe configuration key
//            StripeConfiguration.ApiKey = Iconfig["StripeSettings:secratKey"];
//          #region compare this code with the code in the future
//         //  // validate the cart service
//          var cart = await _shopservice.GetShoppingCartAsync(cartId);
         
//          // if cart is not null create a new payment intent
//          if(cart == null){
//                 return null;
//          };
//          var deliveryPrice = 0m;

//             // check to see if the delivery method id is set to value
//             if(cart.DeliveryMethodId.HasValue){
//                 // if the value exist get the delivery method
//                 var deliveryMethod = await _dmRepo.GetDataByIdAsync((int)cart.DeliveryMethodId);
//                 // if the delivery method is not null get the price
//                 if(deliveryMethod != null){
//                     deliveryPrice = deliveryMethod.Price;
//                 }else{
//                     return null;
//                 };
//             }
//                 // validate and update the item in the cart
//                 foreach(var item in cart.Items){
//                     var productItemFromDatabase = await _proRepo.GetDataByIdAsync(item.ProductId);
//                     // if the product item is not null then update the price
//                     if(productItemFromDatabase != null){
//                         if(item.Price != productItemFromDatabase.Price){
//                             // set the price to the product item price
//                             item.Price = productItemFromDatabase.Price;
//                         }
//                     }
//                     else{
//                         return null;
//                     }
//                 }

//                 // create a new payment intent service from stripe
//                 var service = new PaymentIntentService();
//                  // if payment intent is null the create a new payment intent
//                  PaymentIntent? intent = null;
//                  // if the payment intent is not exist or empty in the cart
//                  if(string.IsNullOrEmpty(cart.PaymentIntentId))
//                  {
//                     // create new payment intent
//                     var createPaymentIntentOption = new PaymentIntentCreateOptions
//                     {
//                         // calculate the amount to be paid
//                       Amount = (long)cart.Items.Sum(i=>i.Quantity*(i.Price*100)+ (long)deliveryPrice*100),
//                         Currency = "usd",
//                         PaymentMethodTypes = ["card"]
//                     };
//                     // create the payment intent
//                     intent = await service.CreateAsync(createPaymentIntentOption);
//                     // create cart to store the payment intent id and client secret code
//                     cart.PaymentIntentId = intent.Id;
//                     cart.ClientSecretCode = intent.ClientSecret; 
//                  }else{
//                     // update existing payment intent
//                     var updatePaymentIntentOptions = new PaymentIntentUpdateOptions
//                     {
//                         Amount = (long)cart
//                         .Items
//                         .Sum(i=>i
//                         .Quantity*(i
//                         .Price*100)+ (long)deliveryPrice*100)
//                     };
//                     // update the payment intent
//                     intent = await service.UpdateAsync(cart.PaymentIntentId, updatePaymentIntentOptions);
//                  // set the cart to the redis
//                  await _shopservice.SetShoppingCartAsync(cart);
//              }
//              // return the updated cart
//              return cart;
// #endregion 
//            #region  reuse this code in the future
           
          

//         //    var cart = await _shopservice.GetShoppingCartAsync(cartId);
//         //       if(cart == null)
//         //        {
//         //         return null;
//         //         };
//         //         var deliveryPrice = 0m;
//         //         if(cart.DeliveryMethodId.HasValue)
//         //         {
//         //             var deliveryMethod = await _dmRepo.GetDataByIdAsync((int)cart.DeliveryMethodId);
//         //             if(deliveryMethod != null)
//         //             {
//         //                 deliveryPrice = deliveryMethod.Price;
//         //             }else{
//         //                 return null;
//         //             }
//         //         }

//         //         // update and validate the item in the cart
//         //         foreach(var item in cart.Items)
//         //         {
//         //           var productItemFromDatabase = await _proRepo
//         //           .GetDataByIdAsync(item.ProductId);
//         //           if(productItemFromDatabase == null)
//         //           {
//         //               return null;
//         //           }
//         //                 // if pric
//         //           if(item.Price != productItemFromDatabase.Price)
//         //           {
//         //               item.Price = productItemFromDatabase.Price;
//         //           }
//         //         }
//         //               // calculate the amount to be paid
//         //               var calculateAmount = (long)cart.Items.Sum(i=>i
//         //               .Quantity*(i.Price*100)+ (long) deliveryPrice*100);
                            
//         //                          // create a new payment intent service from stripe
//         //                          var service = new PaymentIntentService();
//         //                          PaymentIntent? intent = null;    
                               
//         //         // if payment intent is not exist in the cart
//         //             if(string.IsNullOrEmpty(cart.PaymentIntentId))
//         //             {
//         //                        // create new payment intent
//         //                         var newPaymentIntent = new PaymentIntentCreateOptions
//         //                         {
//         //                             Amount = calculateAmount,
//         //                             Currency = "usd",
//         //                             PaymentMethodTypes = ["card"]
//         //                         };
                       
//         //                     intent = await service.CreateAsync(newPaymentIntent);

//         //                 // setting cart to store the payment intent id and client secret code
//         //                 cart.PaymentIntentId = intent.Id; // the id is use to identify the payment intent
//         //                 cart.ClientSecretCode = intent.ClientSecret; // the secret is use to communicate with stripe

//         //                 }else{
//         //                     // update existing payment intent
//         //                     var updatePaymentIntent = new PaymentIntentUpdateOptions
//         //                     {
//         //                             // assign the amount to the updated payment intent
//         //                               Amount = calculateAmount
                                 
//         //                     }; 
//         //                     // set intent to the updated payment intent
//         //                    intent = await service.UpdateAsync(cart.PaymentIntentId, updatePaymentIntent);
//         //                 }
//         //                 // save the cart to the redis
//         //                 await _shopservice.SetShoppingCartAsync(cart); 
//         //                 return cart; // return the updated cart

//          #endregion
//                     }
//         public Task<string> RefundPayment(string paymentIntentId)
//         {
//             throw new NotImplementedException();
//         }
//     }

    
//     }

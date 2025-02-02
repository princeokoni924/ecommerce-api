using System.Text;
using Core.Contract;
using Core.Contract.ICartServices;
using Core.Contract.IGeneric;
using Core.Entities;
using Core.Redis;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Flutterwave.Net;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class FlutterwavePaymentService(
    IConfiguration _Iconfig,
    IShoppingCartServices _shoppingService,
    IGenericRepo<Product> _proRepo,
    IGenericRepo<DeliveryMethod> _dmRepo,
    ILogger<FlutterwavePaymentService> _logger
     ) : IFlutterWavePaymentService
    {
        private readonly HttpClient _http = new();
          
        public async Task<ShoppingCart> InitializePayment(string cartId)
        {
           _logger.LogInformation($"Initializing payment for cart with id {cartId}", cartId);
            // get the api key
            var apiKey = _Iconfig["FlutterwaveSettings:secratKey"];
            if(string.IsNullOrEmpty(apiKey)){
                _logger.LogError("Flutterwave secret key is missing some configuration");
                throw new Exception("Flutterwave secret key is missing some configuration");
            }
            // get the shopping cart
            var cart = await _shoppingService.GetShoppingCartAsync(cartId);
            // if the cart is null return null
            if(cart == null){
             _logger.LogError($"Cart with id {cartId} not found", cartId);
             throw new Exception($"Cart with id {cartId} not found");
            }
            var deliveryPrice = 0m;
            // check to see if the delivery method id is set to value
            if(cart.DeliveryMethodId.HasValue){
                // if the value exist get the delivery method
               var deliveryMethod = await _dmRepo.GetDataByIdAsync(cart.DeliveryMethodId.Value);
                // if the delivery method is not null get the price
                if(deliveryMethod != null){
                    deliveryPrice = deliveryMethod.Price;
                }else{
                    _logger.LogError($"Delivery method with id {cart.DeliveryMethodId.Value} not found.");
                    throw new Exception($"Delivery method with id {cart.DeliveryMethodId.Value} not found.");
                }
            }

            // validate and update the item in the cart
          foreach(var item in cart.Items){
            var productItemFromDatabase = await _proRepo.GetDataByIdAsync(item.ProductId);
            if(productItemFromDatabase != null){
                if(item.Price != productItemFromDatabase.Price){
                    item.Price = productItemFromDatabase.Price;
                }
            }else{
                _logger.LogError($"Product with id {item.ProductId} not found.");
                throw new Exception($"Product with id {item.ProductId} not found.");
            }
          }

          // calculate the total price
          var totalAmount =(long)cart.Items.Sum(item=>item.Price *item.Quantity) + (long)deliveryPrice * 100;
          // create a payment request
          var paymentRequest = new{
            tx_ref = cart.PaymentIntentId,
            amount = totalAmount,
            currency = "NGN",
            redirect_url = _Iconfig["https:flutterwave.com/payment-success"],
            payment_options = "card",
            customer = new{
                email =   cart.BuyerEmail,
                phonenumber =  cart.BuyerPhone,
                name = cart.BuyerName
            },
          };
             string responseContent = string.Empty;
            if(string.IsNullOrEmpty(apiKey)){
            // create a new flutterwave payment intent
            
            var url = "https://api.flutterwave.com/v3/payments";
            var content = new StringContent(JsonConvert.SerializeObject(paymentRequest), Encoding.UTF8, "application/json");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var response = await _http.PostAsync(url, content);
            responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Flutterwave Api response {responseContent}");

                if(!response.IsSuccessStatusCode){
                    _logger.LogError($"Error occured while creating payment intent {responseContent}");
                    throw new Exception($"Error occured while creating payment intent {responseContent}");
                }
                    // parse a response
                var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (responseObject == null | responseObject.Data == null | responseObject.Data.tx_ref == null){
                    _logger.LogError($"Error occured while parsing response {responseContent}");
                    throw new Exception($"Error occured while parsing response {responseContent}");
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                cart.PaymentIntentId = responseObject.Data.tx_ref;
                cart.ClientSecretCode = responseObject.Data.link;
            }else{
                responseContent = JsonConvert.SerializeObject(new {message = "Existing payment intent in use."});
            }

            // save the cart
             await _shoppingService.SetShoppingCartAsync(cart);
             return cart;
        }

        public Task<string> RefundPayment(string paymentIntentId)
        {
            throw new NotImplementedException();
        }

        Task<bool> IFlutterWavePaymentService.VerifyPayment(string transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
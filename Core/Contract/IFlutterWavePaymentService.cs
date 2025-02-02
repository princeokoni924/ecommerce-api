using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Redis;

namespace Core.Contract
{
    public interface IFlutterWavePaymentService
    {
        Task<ShoppingCart>InitializePayment(string cartId);
        Task<bool>VerifyPayment(string transactionId);
        Task<string>RefundPayment(string paymentIntentId);
    }
}
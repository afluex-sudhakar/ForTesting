using Razorpay.Api;
using RazorPayDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RazorPayDemo.Controllers
{
    public class PaymentController : Controller
    {
        public static object PaymentGateWayDetails { get; private set; }

        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult CreateOrder(PaymentInitiateModel model)
        {
            // Generate random receipt number for order
            Random randomObj = new Random();
            string transactionId = randomObj.Next(10000000, 100000000).ToString();
            
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_P757MN4t5EJyzq","MEoBKToZvhQJwFz3zSpCvdtb");
            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(Common._Key, Common._Secreate);
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", model.Amount * 100);  // Amount will in paise
            options.Add("receipt", transactionId);
            options.Add("currency", "INR");
            options.Add("payment_capture", "0"); // 1 - automatic  , 0 - manual
            //options.Add("notes", "This is for testing.");
            
            Order orderResponse = client.Order.Create(options);
            string orderId = orderResponse["id"].ToString();
            model.orderId = orderId;
            // Create order model for return on view
            RazorPayOrder orderModel = new RazorPayOrder
            {
                OrderId = orderResponse.Attributes["id"],
                RazorPayAPIKey = Common._Key,
                Amount = model.Amount * 100,
                Currency = "INR",
                Name = model.Name,
                Email = model.Email,
                Mobile = model.Mobile,
            };
            // Return on PaymentPage with Order data
            model.Pk_UserId = "1";
            model.TransactionType = "Wallet Web";
            model.Type = "Card";
            DataSet ds = model.SaveOrderDetails();
            return View("Payment", orderModel);
        }

        public class RazorPayOrder
        {
            public string OrderId { get; set; }
            public string RazorPayAPIKey { get; set; }
            public int Amount { get; set; }
            public string Currency { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public string Address { get; set; }
        }



        //public static HttpWebRequest GetCreateOrderURL()
        //{
        //    var url = PaymentGateWayDetails.CreateOrder;
        //    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"" + url);
        //    webRequest.ContentType = "application/json";
        //    webRequest.Method = "POST";
        //    return webRequest;
        //}

        public ActionResult Payment()
        {
            RazorPayOrder model = new RazorPayOrder();
            model.Amount = Convert.ToInt32(Session["Amount"]);
            model.OrderId = Session["OrderId"].ToString();
            model.Name = Session["CustomerName"].ToString();
            model.Mobile = Session["Contact"].ToString();
            model.Email = Session["EmailId"].ToString();
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Complete(PaymentInitiateModel model)
        {
            // Payment data comes in url so we have to get it from url
            // This id is razorpay unique payment id which can be use to get the payment details from razorpay server
            string paymentId = HttpContext.Request.Form["rzp_paymentid"].ToString();
            // This is orderId
            string orderId = HttpContext.Request.Form["rzp_orderid"].ToString();


            //Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_P757MN4t5EJyzq", "MEoBKToZvhQJwFz3zSpCvdtb");

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(Common._Key,Common._Secreate);

            
            Payment payment = client.Payment.Fetch(paymentId);
            // This code is for capture the payment 
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];
            //// Check payment made successfully
            model.Pk_UserId = "1";
            DataSet ds = model.SaveFetchPaymentResponse();
            if (paymentCaptured.Attributes["status"] == "captured")
            {
                // Create these action method
                //ViewBag.Message = "Paid successfully";
                //ViewBag.OrderId = paymentCaptured.Attributes["id"];
                //return View("Result");
                return RedirectToAction("Success");
            }
            else
            {
                //ViewBag.Message = "Payment failed, something went wrong";
                //return View("Result");
                return RedirectToAction("Failed");
            } 
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Failed()
        {
            return View();
        }


    }
}
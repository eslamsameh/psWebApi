using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using psBackEnd.Models;
using System.Runtime.Serialization;

namespace PlayStationBackEnd.Controllers
{

    public class PaymentController : ApiController
    {
        psDataBaseEntities db = new psDataBaseEntities();
      
       public PaymentController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        [Authorize]
        [HttpPost]
        [Route("api/payment/GetRemaningOfCustomer")]
        public HttpResponseMessage GetRemaningOfCustomer(payment p)
        {

            var customer = db.payments.Where(x => x.customerID == p.customerID).Select(x => new { x.payed , x.amount }).ToList();
           return Request.CreateResponse(HttpStatusCode.OK, new { status = "success", customer = customer });
        }


        [Authorize]
        [HttpPost]
        [Route("api/payment/GetPaymentByCustomer")]
        public HttpResponseMessage GetPaymentByCustomer(payment p)
        {

            var customer = db.payments.Where(x => x.customerID == p.customerID).Select(x => new { x.RemeningAmount ,x.admin.userName, x.date,x.startTime,x.endTime,x.game.gameName,x.device.deviceName,x.customer.customerName,x.payed,x.amount }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "success", customer = customer });
        }

        [Authorize]
        [HttpGet]
        [Route("api/payment/GetPaymentsNotFinished")]
        public HttpResponseMessage GetPaymentsNotFinished()
        {

            var customer = db.payments.Where(x => x.finished==0).Select(x => new { x.customer.customerName, x.device.deviceName, x.admin.userName,  x.startTime, x.endTime, x.game.gameName, x.customerID,  x.adminID, x.deviceID, x.gameID, x.paymentID }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "success", customer = customer });
        }

        [Authorize]
        [HttpGet]
        [Route("api/payment/TopCustomer")]
        public HttpResponseMessage TopCustomer()
        {

            var customer = db.customers.OrderByDescending(u => u.payments.Count).Take(1);
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "success", customer = customer });
        }

        [Authorize]
        [HttpPost]
        [Route("api/payment/addNewPayment")]
        public HttpResponseMessage addNewPayment(payment p)
        {
            try
            {
                if(p.deviceID ==null || p.customerID == null || p.startTime == null || p.gameID == null ||p.singleOrMulti==null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = "please Enter the Required Data" });

                }
                else
                {
                    db.payments.Add(p);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" });
                }
               
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Failure" });

            }
        }
       
      
        [HttpPost]
        [Authorize]
        [Route("api/payment/addPayment")]
        public HttpResponseMessage addPayment(payment p)
        {
            try
            {
                db.payments.Add(p);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "success" });
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "failure" });

            }
        }
        [Authorize]
        [HttpPost]
        [Route("api/payment/EditPayment")]
        public HttpResponseMessage EditPayment(payment p)
        {
            try
            {
                var payment = db.payments.First(x => x.paymentID == p.paymentID);
                payment.amount = p.amount;
                payment.amountAfterDiscount = p.amountAfterDiscount;
                payment.amountBeforeDiscount = p.amountBeforeDiscount;
                payment.date = p.date;
                payment.haveDiscount = p.haveDiscount;
                payment.haveRemeningAmount = p.haveRemeningAmount;
                payment.payed = p.payed;
                payment.RemeningAmount = p.RemeningAmount;
                payment.wasAmount = p.wasAmount;
                payment.startTime = p.startTime;
                payment.endTime = p.endTime;
                payment.gameID = p.gameID;
                payment.deviceID = p.deviceID;
                payment.customerID = p.customerID;
                payment.finished = p.finished;
                payment.singleOrMulti = p.singleOrMulti;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" });

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Failure" });

            }
        }
        [Authorize]
        [HttpPost]
        [Route("api/payment/deletePayment")]
        public HttpResponseMessage deletePayment(payment p)
        {
            try
            {
                var payment = db.payments.First(x => x.paymentID == p.paymentID);
                db.payments.Remove(payment);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" });

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Failure" });

            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/payment/GetCurrentPayment")]
        public HttpResponseMessage GetCurrentPayment(payment p)
        {
            try
            {
                var payment = db.payments.Where(x => x.paymentID == p.paymentID).Select(x => new { x.customer.customerName,x.customerID, x.paymentID, x.gameID, x.game.gameName, x.payed, x.amount, x.deviceID, x.device.deviceName, x.startTime, x.endTime, x.RemeningAmount ,x.finished,x.date,x.singleOrMulti,x.device.status });
               
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" , payment = payment });

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Failure" });

            }
        }




    }
  
}

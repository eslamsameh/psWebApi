using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using psBackEnd.Models;
using MoreLinq;
using System.Runtime.Serialization;

namespace psBackEnd.Controllers
{
    public class reportController : ApiController
    {
        psDataBaseEntities db = new psDataBaseEntities();
        public reportController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        [Authorize]
        [HttpPost]
        [Route("api/report/AllPaymentByTwoDate")]
        public HttpResponseMessage SearchPaymentByTwoDate(report r)
        {
            try
            {

                var payment = db.payments.Where(x => x.date >= r.starttime && x.date <= r.endtime && x.finished == 1).Select(x => new { x.amount, x.payed, x.RemeningAmount }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", payment = payment });

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Failure" });

            }

        }


        [Authorize]
        [HttpPost]
        [Route("api/admin/NumberOfPageTwoDate")]

        public HttpResponseMessage NumberOfPageTwoDate(report r)
        {
            var payment = db.payments.Where(x => x.date >= r.starttime && x.date <= r.endtime && x.finished == 1).Select(x => new { x.paymentID }).ToList();
            double count = payment.Count;
            if (count > 10)
            {
                double result = (count / 10);
                if (result % 1 == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = "success", NumberOfPages = result });

                }
                else
                {
                    result = (int)result;
                    result += 1;

                    return Request.CreateResponse(HttpStatusCode.OK, new { status = "success", NumberOfPages = result });

                }


            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "success", NumberOfPages = 1 });

            }
        }


        [Authorize]
        [HttpPost]
        [Route("api/admin/GotoPageTwoDate")]

        public HttpResponseMessage GotoPageTwoDate(report r)
        {
            try
            {
                var products = db.payments.Where(x => x.date >= r.starttime && x.date <= r.endtime && x.finished == 1).Select(x => new { x.admin.userName, x.amount, x.game.gameName, x.payed, x.RemeningAmount, x.singleOrMulti, x.startTime, x.endTime, x.customer.customerName }).ToList();
                var data = products.Batch(10, seq => seq.ToList()).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, new { status = "success", data = data[r.Pagenumber] });
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Failure" });

            }
        }





        [Authorize]
        [HttpPost]
        [Route("api/report/AllPaymentByOneDate")]
        public HttpResponseMessage AllPaymentByOneDate(payment p)
        {
            try
            {

                var payment = db.payments.Where(x => x.date == p.date && x.finished == 1).Select(x => new { x.amount, x.payed, x.RemeningAmount }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", payment = payment });

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Failure" });

            }
        }



        [Authorize]
        [HttpPost]
        [Route("api/admin/NumberOfPageSingleDate")]

        public HttpResponseMessage NumberOfPageSingleDate(payment p)
        {
            var payment = db.payments.Where(x => x.date == p.date && x.finished == 1).Select(x => new { x.paymentID }).ToList();
            double count = payment.Count;
            if (count > 10)
            {
                double result = (count / 10);
                if (result % 1 == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = "success", NumberOfPages = result });

                }
                else
                {
                    result = (int)result;
                    result += 1;

                    return Request.CreateResponse(HttpStatusCode.OK, new { status = "success", NumberOfPages = result });

                }


            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "success", NumberOfPages = 1 });

            }
        }


        [Authorize]
        [HttpPost]
        [Route("api/admin/GotoPageSingleDate")]

        public HttpResponseMessage GotoPageSingleDate(report r)
        {
            var products = db.payments.Where(x => x.date == r.starttime && x.finished == 1).Select(x => new { x.admin.userName, x.amount, x.game.gameName, x.payed, x.RemeningAmount, x.singleOrMulti, x.startTime, x.endTime, x.customer.customerName }).ToList();
            var data = products.Batch(10, seq => seq.ToList());

            return Request.CreateResponse(HttpStatusCode.OK, new { status = "success", data = data.ToArray()[r.Pagenumber] });
        }

    }
    public class report
    {
        public DateTime starttime { get; set; }
        public DateTime endtime { get; set; }
        public int Pagenumber { get; set; }


    }
}

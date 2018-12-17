using psBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace psBackEnd.Controllers
{
    
    public class customerController : ApiController
    {
        psDataBaseEntities db = new psDataBaseEntities();
        [Authorize]
        [HttpGet]
        [Route("api/customer/GetAllCustomer")]
        public HttpResponseMessage GetAllCustomer()
        {
            var customer = db.customers.Select(x => new { x.customerID, x.customerName }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, new { statues = "success", customer = customer });
        }
        [Authorize]
        [HttpGet]
        [Route("api/customer/GetCountOfCustomers")]
        public HttpResponseMessage GetCountOfCustomers()
        {
            var customer = db.customers.Select(x => x.customerID).ToList();
            var CountOfCustomer = customer.Count;
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", CountOfCustomer = CountOfCustomer });
        }
        [Authorize]
        [HttpPost]
        [Route("api/customer/addNewCustomer")]
        public HttpResponseMessage AddNewCustomer(customer c)
        {
            try
            {
                db.customers.Add(c);
                db.SaveChanges();
                int customerID = c.customerID;
                return Request.CreateResponse(HttpStatusCode.OK, new { statues = "success", customerID= customerID });
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { statues = "failure" });
            }
        }
        [Authorize]
        [HttpPost]
        [Route("api/customer/DeleteCustomer")]
        public HttpResponseMessage DeleteCustomer(customer c)
        {
            try
            {
                var customer = db.customers.First(x => x.customerID == c.customerID);
                db.customers.Remove(customer);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { statues = "success" });

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { statues = "failure" });

            }
        }
        [Authorize]
        [HttpPost]
        [Route("api/customer/UpdateCustomer")]
        public HttpResponseMessage UpdateCustomer(customer c)
        {
            try
            {
                var customer = db.customers.First(x => x.customerID == c.customerID);
                customer.customerName = c.customerName;
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
        [Route("api/customer/GetSingleCustomer")]
        public HttpResponseMessage GetSingleCustomer(customer c)
        {
            try
            {
                var customer = db.customers.Where(x => x.customerID == c.customerID).Select(x => new { x.customerID, x.customerName });
               
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "success" , customer= customer });
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "failure" });

            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/customer/SearchByCustomerName")]
        public HttpResponseMessage SearchByCustomerName(customer c)
        {
            try
            {
                if (c.customerName == "")
                {
                   
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = "Please enter Customer Name",  });
                }

                else
                {
                    var customer = db.customers.Where(x => x.customerName.Contains(c.customerName)).Select(x => new { x.customerID, x.customerName });

                    return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", customer = customer });
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Not Found" });

            }
        }

    }
}

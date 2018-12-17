using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using psBackEnd.Models;

namespace psBackEnd.Controllers
{
    public class reservationController : ApiController
    {
        

        psDataBaseEntities db = new psDataBaseEntities();
        [Authorize]
        [HttpPost]
        [Route("api/reservation/AddNewResvation")]
        public HttpResponseMessage AddNewResvation(reservation r)
        {
            db.reservations.Add(r);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" });

        }
        [Authorize]
        [HttpPost]
        [Route("api/reservation/EditReservation")]
        public HttpResponseMessage EditReservation(reservation r)
        {
            var reservation = db.reservations.First(x => x.reservationID == r.reservationID);
            reservation.gameID = reservation.gameID;
            reservation.endTime = r.endTime;
            reservation.startTime = r.startTime;
            reservation.deviceID = reservation.deviceID;
            reservation.customerID = reservation.customerID;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" });
        }
        [Authorize]
        [HttpPost]
        [Route("api/reservation/DeleteReservation")]
        public HttpResponseMessage DeleteReservation (reservation r)
        {
            var reservation = db.reservations.First(x => x.reservationID == r.reservationID);
            db.reservations.Remove(reservation);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" });

        }


        [Authorize]
        [HttpGet]
        [Route("api/reservation/GetReservation")]
        public HttpResponseMessage GetReservation()
        {
            var reservation = db.reservations.Where(x => x.start == 0).Select(x => new { x.adminID, x.customer.customerName, x.date, x.device.deviceName, x.reservationID, x.startTime, x.endTime, x.gameID });
            
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" });

        }

        [Authorize]
        [HttpPost]
        [Route("api/reservation/GetCurrentReservation")]
        public HttpResponseMessage GetCurrentReservation(reservation r)
        {
            var reservation = db.reservations.Where(x=>x.reservationID == r.reservationID).Select(x => new { x.adminID, x.customer.customerName, x.date, x.device.deviceName, x.reservationID, x.startTime, x.endTime, x.gameID });

            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" });

        }

        [Authorize]
        [HttpPost]
        [Route("api/reservation/AfterPaymentFinsh")]
        public HttpResponseMessage AfterPaymentFinsh(reservation r)
        {
            try
            {
                var reservation = db.reservations.Where(x => x.deviceID == r.deviceID && x.start == 0).Select(x => new { x.customerID, x.customer.customerName, x.gameID, x.device.deviceName, x.deviceID, x.startTime, x.endTime, x.date,x.singelOrMulti }).First();

                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" });
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Failure" });

            }

        }



    }
}

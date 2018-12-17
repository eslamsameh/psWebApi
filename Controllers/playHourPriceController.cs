using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using psBackEnd.Models;

namespace psBackEnd.Controllers
{
    public class playHourPriceController : ApiController
    {
        psDataBaseEntities db = new psDataBaseEntities();
        
        [Authorize]
        [HttpPost]
        [Route("api/playHourPrice/addNewplayHourPrice")]
        public HttpResponseMessage addNewplayHourPrice(playHourPrice p)
        {
            db.playHourPrices.Add(p);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" });

        }
        [Authorize]
        [HttpPost]
        [Route("api/playHourPrice/editplayHourPrice")]
        public HttpResponseMessage editplayHourPrice(playHourPrice p)
        {
            var playHour = db.playHourPrices.First(x => x.playHourePriceID == p.playHourePriceID);
            playHour.hourPlayMulti = p.hourPlayMulti;
            playHour.hourPlaysSingle = p.hourPlaysSingle;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" });
        }
        [Authorize]
        [HttpPost]
        [Route("api/playHourPrice/deleteplayHourPrice")]
        public HttpResponseMessage deleteplayHourPrice(playHourPrice p)
        {
            var playHour = db.playHourPrices.First(x => x.playHourePriceID == p.playHourePriceID);
            db.playHourPrices.Remove(playHour);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" });
        }
        [Authorize]
        [HttpGet]
        [Route("api/playHourPrice/GetplayHourPrice")]
        public HttpResponseMessage GetplayHourPrice()
        {
            try
            {
                var playHour = db.playHourPrices.Select(x => new { x.playHourePriceID, x.hourPlayMulti, x.hourPlaysSingle }).First();
                return Request.CreateResponse(HttpStatusCode.OK, new { status= "Success", playHour = playHour });
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Failure" });

            }
        }
    }
}

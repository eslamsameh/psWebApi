using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using psBackEnd.Models;

namespace psBackEnd.Controllers
{
    public class drinkController : ApiController
    {
        psDataBaseEntities db = new psDataBaseEntities();
        [Authorize]
        [HttpPost]
        [Route("api/drink/addNewDrink")]
        public HttpResponseMessage addNewDrink(drink d)
        {
            db.drinks.Add(d);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" });

        }
        [Authorize]
        [HttpPost]
        [Route("api/drink/EditDrink")]
        public HttpResponseMessage EditDrink(drink d)
        {
            try
            {

                var drink = db.drinks.First(x => x.drinkID == d.drinkID);
                drink.drinkNameID = d.drinkNameID;
                //drink.drinksName.drinkName = d.drinksName.drinkName;
                drink.endDiscount = d.endDiscount;
                drink.haveDiscount = d.haveDiscount;
                drink.paymentID = d.paymentID;
                drink.priceAfterDiscount = d.priceAfterDiscount;
                drink.startDiscount = d.startDiscount;
                drink.wasPrice = d.wasPrice;
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
        [Route("api/drink/DeleteDrink")]
        public HttpResponseMessage DeleteDrink(drink d)
        {
            try
            {
                var drink = db.drinks.First(x => x.drinkID == d.drinkID);
                db.drinks.Remove(drink);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" });
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Failure" });

            }

        }
        [Authorize]
        [HttpGet]
        [Route("api/drink/GetAllDrinksName")]

        public HttpResponseMessage GetAllDrinksName()
        {
            var drinksNames = db.drinksNames.Select(x => new { x.drinkNameID,x.drinkName, x.drinkPrice }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", drinksNames = drinksNames });

        }

        [Authorize]
        [HttpPost]
        [Route("api/drink/addNewDrinkName")]

        public HttpResponseMessage addNewDrinkName(drinksName d)
        {
            db.drinksNames.Add(d);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" });

        }

        [Authorize]
        [HttpPost]
        [Route("api/drink/EditDrinkName")]

        public HttpResponseMessage EditDrinkName(drinksName d)
        {
            var drinkName = db.drinksNames.First(x => x.drinkNameID == d.drinkNameID);
            drinkName.drinkName = d.drinkName;
            drinkName.drinkPrice = d.drinkPrice;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" });

        }

        [Authorize]
        [HttpPost]
        [Route("api/drink/SingleDrinkName")]

        public HttpResponseMessage SingleDrinkName(drinksName d)
        {
            var drinkName = db.drinksNames.Where(x => x.drinkNameID == d.drinkNameID).Select(x => new { x.drinkName, x.drinkNameID, x.drinkPrice });
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" , drinkName= drinkName });

        }
        [Authorize]
        [HttpPost]
        [Route("api/drink/DeleteDrinkName")]

        public HttpResponseMessage DeleteDrinkName(drinksName d)
        {
            var drinkName = db.drinksNames.First(x => x.drinkNameID == d.drinkNameID);
            db.drinksNames.Remove(drinkName);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" });

        }

        [Authorize]
        [HttpPost]
        [Route("api/drink/GetDrinksByPaymentID")]

        public HttpResponseMessage GetDrinksByPaymentID(drink d )
        {
            var drinkName = db.drinks.Where(x => x.paymentID == d.paymentID).Select(x => new { x.drinkID, x.drinkNameID,x.drinksName.drinkPrice,x.drinksName.drinkName}).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success" , drinkName = drinkName });

        }
    }
}

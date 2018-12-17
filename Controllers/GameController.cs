using psBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace psBackEnd.Controllers
{
    public class GameController : ApiController
    {
        psDataBaseEntities db = new psDataBaseEntities();

        [Authorize]
        [HttpGet]
        [Route("api/Game/GetAllGames")]
        public HttpResponseMessage GetAllGames()
        {
            var game = db.games.Select(x => new { x.gameName,x.gameID });
            return Request.CreateResponse(HttpStatusCode.OK, new { statues = "success", game = game });
        }
        [Authorize]
        [HttpPost]
        [Route("api/Game/addNewGame")]
        public HttpResponseMessage addNewGame(game g)
        {
            try
            {
                db.games.Add(g);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { statues = "success" });

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { statues = "Failure" });

            }
        }
        [Authorize]
        [HttpPost]
        [Route("api/Game/UpdateGame")]
        public HttpResponseMessage UpdateGame(game g)
        {
            try
            {
                var game = db.games.First(x => x.gameID == g.gameID);
                game.gameName = g.gameName;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { statues = "success" });
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { statues = "Failure" });

            }
        }
        [Authorize]
        [HttpPost]
        [Route("api/Game/GetCurrentGameSelected")]
        public HttpResponseMessage GetCurrentGameSelected(game g)
        {
            try
            {
                var game = db.games.Where(x => x.gameID == g.gameID).Select(x => new { x.gameID, x.gameName });
               
                return Request.CreateResponse(HttpStatusCode.OK, new { statues = "success" , game = game });
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { statues = "Failure" });

            }
        }
        [Authorize]
        [HttpPost]
        [Route("api/Game/DeleteGame")]
        public HttpResponseMessage DeleteGame(game g)
        {
            try
            {
                var game = db.games.First(x => x.gameID == g.gameID);
                db.games.Remove(game);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { statues = "success" });
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { statues = "Failure" });

            }
        }

    }
}

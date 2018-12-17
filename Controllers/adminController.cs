using psBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace psBackEnd.Controllers
{
    public class adminController : ApiController
    {
        psDataBaseEntities db = new psDataBaseEntities();
        [Authorize]
        [HttpGet]
        [Route("api/admin/authintication")]
        public IHttpActionResult GetForAuthinticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var adminId = identity.FindFirst("adminId").Value;
            var role = identity.FindFirst("role").Value;
           var adminName= identity.FindFirst("adminName").Value;
            var str = new { adminId, role, adminName };
            return Ok(str);
        }
        [Authorize]
        [HttpPost]
        [Route ("api/admin/addNewUserAdmin")]
        public HttpResponseMessage AddNewUserAdmin(admin ad)
        {
            try
            {
                db.admins.Add(ad);
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
        [Route("api/admin/GetCurrentAdmin")]
        public HttpResponseMessage GetCurrentAdmin(admin ad)
        {
            var admin = db.admins.Where(x => x.adminID == ad.adminID).Select(x => new { x.address, x.age, x.email, x.password, x.phone, x.userName, x.role });

            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "sucess", admin= admin });

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "failure" });

            }

        }


        [Authorize]
        [HttpPost]
        [Route("api/admin/GetCurrentAdminName")]
        public HttpResponseMessage GetCurrentAdminName(admin ad)
        {
            var admin = db.admins.Where(x => x.adminID == ad.adminID).Select(x => new {  x.userName });

            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "sucess", admin = admin });

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "failure" });

            }

        }


        [Authorize]
        [HttpPost]
        [Route("api/admin/UpdateAdmin")]
        public HttpResponseMessage UpdateAdmin(admin ad)
        {
            var user = db.admins.First(x => x.adminID == ad.adminID);
            user.userName = ad.userName;
            if (ad.password != null) { user.password = ad.password; }
            user.age = ad.age;
            user.email = ad.email;
            user.address = ad.address;
            user.phone = ad.phone;
            user.role = ad.role;
            user.userName = ad.userName;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "success"});

        }
        [Authorize]
        [HttpPost]
        [Route("api/admin/DeleteAdmin")]
        public HttpResponseMessage DeleteAdmin(admin ad)
        {
            try
            {
                var userid = db.admins.First(x => x.adminID == ad.adminID);
                db.admins.Remove(userid);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "success" });

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "failure" });

            }
        }
        [Authorize]
        [HttpGet]
        [Route("api/admin/GetAllAdmins")]
        public HttpResponseMessage GetAllAdmins( )
        {
            try
            {
                var admins = db.admins.Select(x => new { x.adminID, x.userName, x.email, x.role });
               
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "success",admins=admins });

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "failure" });

            }
        }
    }
}

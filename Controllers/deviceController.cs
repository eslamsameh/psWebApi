using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using psBackEnd.Models;

namespace psBackEnd.Controllers
{
    public class deviceController : ApiController
    {
        psDataBaseEntities db = new psDataBaseEntities();
        [Authorize]
        [HttpGet]
        [Route("api/admin/GetAllDevices")]
        public HttpResponseMessage GetAllDevices()
        {
            try
            {
                var devices = db.devices.Select(x => new { x.deviceID, x.deviceName, x.status });
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "success", devices = devices });

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "failure"});
            }
           
        }
        [Authorize]
        [HttpPost]
        [Route("api/admin/GetSingleDevice")]
        public HttpResponseMessage GetSingleDevice(device d)
        {
            try
            {
                var devices = db.devices.Where(x => x.deviceID == d.deviceID).Select(x => new { x.deviceID,x.deviceName,x.status});
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "success", devices = devices });

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "failure" });
            }

        }
        [Authorize]
        [HttpPost]
        [Route("api/admin/AddNewDevice")]
        public HttpResponseMessage AddNewDevice(device de)
        {
            db.devices.Add(de);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, new { status = "success"});
        }
        [Authorize]
        [HttpPost]
        [Route("api/admin/RemoveDevice")]
        public HttpResponseMessage RemoveDevice(device de)
        {
            try
            {
                var deviceId = db.devices.First(x => x.deviceID == de.deviceID);
                db.devices.Remove(deviceId);
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
        [Route("api/admin/GetDeviceNonActive")]
        public HttpResponseMessage GetDeviceNonActive()
        {
            try
            {
                var device = db.devices.Where(x => x.status == 0).Select(x => new { x.deviceID, x.deviceName, x.status });
               
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "success" , device = device });
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "failure" });
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/admin/UpdateDevice")]
        public HttpResponseMessage UpdateDevice(device d)
        {
            try
            {
                var device = db.devices.First(x => x.deviceID == d.deviceID);
                device.deviceName = d.deviceName;
                device.status = d.status;
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
       [Route("api/devices/PercentageCalculationForDeviceActicve")]
        public HttpResponseMessage PercentageCalculationForDeviceActicve()
        {
            var devicesActive = db.devices.Where(x => x.status == 1).Count();
            var devices = db.devices.Select(x => x.status).Count();
            var Calc= (int)Math.Round((double)(100 * devicesActive) / devices);
           return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", data = Calc });
        }

    }
}

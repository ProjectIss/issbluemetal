using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace issBlueMetal.API
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Hotel")]
    public class HotelController : ApiController
    {
        [Route("GetHotel")]
        [HttpGet]
        public string GetHotel()
        {
            return "Hello";
        }
        [Route("SaveData")]
        [HttpGet]
        public string SaveData(string CreateOrder)
        {
            return "Hello";
        }
    }
}

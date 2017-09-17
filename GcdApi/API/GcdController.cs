using GcdApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GcdApi.API
{
    public class GcdController : ApiController
    {
        [HttpGet]
        [Route("api/get")]
        public IHttpActionResult Get(int page)
        {
            GcdData data = new Utility().GetServies(page);

            return Ok(data);
        }

        [HttpGet]
        [Route("api/_search/")]
        public IHttpActionResult _Search(string q, int page)
        {
            GcdData data = new Utility().GetServies(q, page);

            return Ok(data);
        }

        [HttpGet]
        [Route("api/_search")]
        public IHttpActionResult _Search(decimal lon, decimal lat, decimal maxDistance)
        {
            GcdData data = new Utility().GetServiesByLocation(lon, lat, maxDistance);

            return Ok(data);
        }

        [HttpGet]
        [Route("api/_search")]
        public IHttpActionResult _Search(string q, decimal lon, decimal lat, decimal maxDistance)
        {
            GcdData data = new Utility().GetServiesByLocation(q, lon, lat, maxDistance);

            return Ok(data);
        }
    }
}

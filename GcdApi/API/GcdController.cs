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
        public IHttpActionResult Get()
        {
            GcdData data = new Utility().GetServies();

            return Ok(data);
        }

        [HttpGet]
        [Route("api/_search/{q}")]
        public IHttpActionResult _Search(string q)
        {
            GcdData data = new Utility().GetServies(q);

            return Ok(data);
        }
    }
}

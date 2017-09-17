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
        [Route("api/Get/{pageNumber}")]
        public IHttpActionResult Get(int pageNumber)
        {
            GcdData data = new Utility().GetServies(pageNumber);

            return Ok(data);
        }

        [HttpGet]
        [Route("api/_search/{q}/{pageNumber}")]
        public IHttpActionResult _Search(string q, int pageNumber)
        {
            GcdData data = new Utility().GetServies(q, pageNumber);

            return Ok(data);
        }
    }
}

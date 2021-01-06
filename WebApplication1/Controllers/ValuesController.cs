using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebApplication1.ClassLib;

namespace WebApplication1.Controllers
{
    public class UserObj
    {
        public string MyFitnessPalUserName { get; set; }
    }

    public class ValuesController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        //GET api/values
        public HttpResponseMessage Get([FromBody] UserObj user)
        {
            string userid = user.MyFitnessPalUserName;
            datalayer dl = new datalayer();
            var res = dl.getfitnesspaldata(userid);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(res.ToString(), Encoding.UTF8, "application/json");
            return response;
        }

    }
}

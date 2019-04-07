using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConsumerApi.Controllers
{
    [Route("api/[controller]")]
    public class ConsumerController : Controller
    {
        [HttpPost("{message}")]
        public async Task<string> Post(string message)
        {
            var uri = new Uri(string.Format("http://localhost:50892/RestService.svc/EnfileiraDados/{0}", message));

            using (var client = new HttpClient())
            {
                var data = JsonConvert.SerializeObject(message);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                    return Ok().ToString();
            }

            return BadRequest().ToString();
        }
    }
}
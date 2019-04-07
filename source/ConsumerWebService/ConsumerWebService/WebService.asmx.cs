using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Services;


namespace ConsumerWebService
{
    /// <summary>
    /// Descrição resumida de WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string EnfileraMensagem(string message)
        {
            var result = string.Empty;
            var uri = new Uri(string.Format("http://localhost:50892/RestService.svc/EnfileiraDados/{0}", message));

            using (var client = new HttpClient())
            {
                var data = JsonConvert.SerializeObject(message);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(uri, content).Result;

                if (response.IsSuccessStatusCode)
                    return HttpStatusCode.OK.ToString();
            }

            return HttpStatusCode.BadRequest.ToString();
        }
    }
}

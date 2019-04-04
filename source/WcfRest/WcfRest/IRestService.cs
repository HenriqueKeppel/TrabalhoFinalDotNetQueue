using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfRest
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "IRestService" no arquivo de código e configuração ao mesmo tempo.
    [ServiceContract]
    public interface IRestService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
                 ResponseFormat = WebMessageFormat.Json,
                 RequestFormat = WebMessageFormat.Json,
                 BodyStyle = WebMessageBodyStyle.Wrapped,
                 UriTemplate = "EnfileiraDados/{dados}")]
        string Enfileira(string dados);
    }
}

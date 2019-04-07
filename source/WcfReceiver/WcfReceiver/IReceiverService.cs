using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfReceiver
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "IReceiverService" no arquivo de código e configuração ao mesmo tempo.
    [ServiceContract]
    public interface IReceiverService
    {
        [OperationContract]
        void Receiver();
    }
}

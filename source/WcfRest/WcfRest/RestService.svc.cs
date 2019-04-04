using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfRest
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da classe "RestService" no arquivo de código, svc e configuração ao mesmo tempo.
    // OBSERVAÇÃO: Para iniciar o cliente de teste do WCF para testar esse serviço, selecione RestService.svc ou RestService.svc.cs no Gerenciador de Soluções e inicie a depuração.
    public class RestService : IRestService
    {
        public string Enfileira(string dados)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "messageQueue",
                                             durable: false,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

                        var body = Encoding.UTF8.GetBytes(dados);

                        channel.BasicPublish(exchange: "",
                                             routingKey: "messageQueue",
                                             basicProperties: null,
                                             body: body);
                    }
                }
            }
            catch (Exception ex)
            {
                return "BadRequest: " + ex.Message;
            }
            return "OK";
        }
    }
}

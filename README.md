# TrabalhoFinalDotNetQueue
Trabalho final desenvolvido para a disciplina Arquitetura com .Net. Curso: Arquitetura de Softwares Distribuídos - Oferta 10.

## Resumo

O trabalho consiste em criar implementações para resolver o problema descrito através do diagrama abaixo:

<img src="https://github.com/HenriqueKeppel/TrabalhoFinalDotNetQueue/issues/1#issue-430096247">

Para esta solução, foram criados 4 aplicações, descritas abaixo:

- WcfRest
  Trata-se de um WCF configurado para REST que tem como função inserir na Fila (rabbitMQ) mensagens enviadas a ele.
- WcfReceiver -> ReceiverConsole
  Trata-de um WCFque tem como função remover itens da Fila e inseri-los no banco de dados. Também é composto por um console listener
  que executa a mesma função, porem, o console não precisa ser acionado para desenfileirar a mensagem. Por se tratar de um listener,
  o proprio console fica escutando a fila e assim que uma nova mensagem é recebida, o mesmo já a trata.
- ConsumerApi
  Trata-se de uma API desenvolvido com .Net Core que tem como objetivo consumir o WcfRest para enfileiramento de mensagens.  
  É apenas um cliente para demonstrar a integração entre diversos sistemas diferentes.
- ConsumerWebService
  Trata-se de um Web Services que tem como objetivo consumir o WcfRest para enfileiramento de mensagens.
  É apenas um cliente para demonstrar a integração entre diversos sistemas diferentes
  
Para compor esta solução, foi utilizado banco de dados Mysql para persistencia e a aplicação RabbitMQ para enfileiramento de mensagens.<br>
Para este projeto não foram criados padrões de projeto nem arquivos de configuração, levando-se em conta que o objeto é demonstrar
o conhecimento da comunicação entre camadas e uso de fila para troca de mensagens.

## Funcionamento

Primeiramente, é necessário realizar a instalação do servidor de fila, o RabbitMQ pode ser baixado através do link:
<a href="https://www.rabbitmq.com/download.html">RabbitMQ</a>

O mesmo deve funcionar localmente pelo endereço localhost e utiliza por padrão a porta: 15672.
Caso o mesmo esteja instalado em um local diferente da estação que irá executar o sistema, as configurações para os serviços WcfRest 
e WcfReceiver devem ser realizadas conforme descrito abaixo:
- WcfRest
  Arquivo: RestService.svc.cs -> alterar a linha 19 inserindo o servidor e porta que devem ser utilizados:<br>
  var factory = new ConnectionFactory() { HostName = "localhost" };
  
- WcfReceiver.ReceiverConsole
  Arquivo: Program.cs -> alterar a linha 12 inserindo o servidor e porta que devem ser utilizados:<br>
  var factory = new ConnectionFactory() { HostName = "localhost" };
  
É necessário tambem que a estação contenha um data base MySql Server, utilizando configurações padrão.
Os scripts para criação do banco de dados se encontram na pasta: dataBase-scripts. Basta executa-los 
no MySql e o banco de dados estará pronto para ser utilizado.
Caso seja necessário alterar o endereço do servidor, o mesmo deve ser feito alterando o arquivo
AdoMessage.cs no diretório /WcfReveiver/ReceiverConsole, linha 9, conforme abaixo:<br>
private static string _connectionString = "Server=localhost;Database=dotnetqueue;Uid=root;Pwd=";

## Execução

Estando já configurados e em execução os serviços RabbitMQ e Mysql server, é necessário executar os serviços WcfRest para
que o mesmo receba mensagens e o serviço WcfReceiver.ReceiverConsole, que fica em standby escutando a fila e processando
mensagens quando as mesmas chegam (o WcfReceiver não esta executando corretamente no momento, apenas o ReceiverConsole).

Para enviar mensagens, utilizamos os serviços abaixo:
Para enviar mensagens através da ConsumerApi, basta inserir o endereço abaixo no Postman ou qualquer outro aplicativo
de envio de serviços rest:
http://localhost:62124/api/consumer/<mensagem_a_ser_gravada>

Para enviar mensagens através do ConsumerWebService, basta executa-lo e consumi-lo no proprio navegador ou algum aplicativo
de consumo de serviços SOAP.

Finalmente, para visualizar se as mensagens estão sendo gravadas corretamente, basta executar a query abaixo em algum terminal do MySql:

use dotnetqueue;<br>
select *from message;

07/04/2019 - Henrique Keppel.


  
  

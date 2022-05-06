<Query Kind="Statements">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
</Query>

var exchangeName = "TestExchange";
var queueName = "TestQueueName";
var routingKey = "TestRoutingKey";
var result = "";

ArchPM.FluentRabbitMQ.FluentRabbit.Instance
.Trace(i => { Debug.WriteLine($"{i.Method} {i.Message}"); })
.Connect()
.CreateExchange(exchangeName)
.CreateQueue(queueName)
.Bind(i => { i.ExchangeName = exchangeName; i.QueueName = queueName; i.RoutingKey = routingKey; })
.Subscribe(queueName, callback => { result = Encoding.ASCII.GetString(callback.Body); })
.Publish("tugbay atilla", p => {p.ExchangeName = exchangeName; p.RoutingKey = routingKey; p.PayloadFormat = ArchPM.FluentRabbitMQ.PayloadFormat.String;})
.WaitUntil (() => !string.IsNullOrWhiteSpace(result))
.ConfigureDown()
.Dispose();

result.Dump("Result");
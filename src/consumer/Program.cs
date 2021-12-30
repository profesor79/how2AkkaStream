// See https://aka.ms/new-console-template for more information
using Akka.Actor;
using common;
using Serilog;

SerilogBase.SetLoggerConfiguration("consumer");
Console.WriteLine("Hello, World!");


var c = new Configuration();

var st = new StreamConsumer();
Console.WriteLine("waiting to finish");
Log.Debug ("starting this thing");
var actorSystem = ActorSystem.Create("system", AkkaConfiguration.GetConfig("consumer"));
st.Start(c, actorSystem);

Thread.Sleep(5000);
Console.WriteLine("finish");
 actorSystem.WhenTerminated.Wait();

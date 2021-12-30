// See https://aka.ms/new-console-template for more information
//using Akka;
using Akka.Actor;
using Akka.Streams;
using Akka.Streams.Dsl;



Console.WriteLine("Hello, World!");
var system = ActorSystem.Create("system");
var materializer = system.Materializer();

Source<int, Akka.NotUsed> source = Source.From(Enumerable.Range(1, 100));
source.RunForeach(i => Console.WriteLine(i.ToString()), materializer);



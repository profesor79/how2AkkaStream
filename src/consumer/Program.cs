// See https://aka.ms/new-console-template for more information
using consumer;

Console.WriteLine("Hello, World!");


var c = new Configuration("localhost", 35000);

var st = new StreamConsumer(c);
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var c = new Configuration("localhost", 35000);
var producer = new Producer(c);

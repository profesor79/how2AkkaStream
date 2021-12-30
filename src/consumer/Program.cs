// See https://aka.ms/new-console-template for more information
using common;


SerilogBase.SetLoggerConfiguration("consumer");
Console.WriteLine("Hello, World!");


var c = new Configuration("localhost", 35000);

var st = new StreamConsumer();
st.Start(c);
Console.WriteLine("waiting to finish");
Console.ReadLine(); ;
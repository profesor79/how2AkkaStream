// See https://aka.ms/new-console-template for more information
using common;

Console.WriteLine("Hello, World!");
var c = new Configuration();


while (true) {
    try
    {
        var producer = new Producer(c);
    }
    catch (Exception t)
    {
        Console.WriteLine(t.Message);

    }
}
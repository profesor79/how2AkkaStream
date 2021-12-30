// See https://aka.ms/new-console-template for more information
using common;

Console.WriteLine("Hello, World!");
var c = new Configuration();



    try
    {
        for (int i = 0; i < 2; i++)
        {
            var producer = new Producer(c,i);
        }
        
    }
    catch (Exception t)
    {
        Console.WriteLine(t.Message);

    }

    Console.ReadLine();

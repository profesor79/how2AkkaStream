
using common;
using System.Net;
using System.Net.Sockets;

public class Producer
{

    public Producer(Configuration conf, int id)
    {
        Conf = conf;
        Id = id;
        Connect();
        Task.Factory.StartNew(() => SendData());
    }
    public void SendData()
    {

        var message = $"{Id}---\n23451aaaaeeeeeeeeeeeaaaaaaaaaaaa2345";
        Byte[] data = System.Text.Encoding.ASCII.GetBytes($"{DateTime.UtcNow.ToString("HH:mm:ss.fff")}  {message}");
        NetworkStream stream = _tcpClient.GetStream();
        stream.WriteTimeout = 5000;
        
        var canWrite = true;
        while (canWrite)
        {
            try
            {
     // Send the message to the connected TcpServer.
            stream.Write(data, 0, data.Length);
           // Thread.Sleep(1);
            Console.Write(Id);
              //  Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception)
            {
                canWrite = false;
                
            }
               
       
        }

    }
    public void Connect()
    {

        _tcpClient = new TcpClient();

        _tcpClient.Connect(Conf.HostName, Conf.Port);
    }
    TcpClient _tcpClient;
    public Configuration Conf { get; }
    public int Id { get; }
}

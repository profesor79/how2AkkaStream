
using System.Net;
using System.Net.Sockets;

public class Producer
{

    public Producer(Configuration conf)
    {
        Conf = conf;
        Connect();
        SendData();
    }
    public void SendData()
    {

        var message = "234512345";
        Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
        while (true)
        {

            // Get a client stream for reading and writing.
            //  Stream stream = client.GetStream();

            NetworkStream stream = _tcpClient.GetStream();
            // Send the message to the connected TcpServer.
            stream.Write(data, 0, data.Length);

            Console.Write(".");
        }

    }
    public void Connect()
    {

        _tcpClient = new TcpClient();

        _tcpClient.Connect(Conf.HostName, Conf.Port);
    }
    TcpClient _tcpClient;
    public Configuration Conf { get; }
}


public class Configuration
{

    public int Port { get; set; }



    public string HostName { get; set; }

    public Configuration(string hostName, int port)
    {
        Port = port;
        HostName = hostName;
    }
}
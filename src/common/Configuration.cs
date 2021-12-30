namespace common
{

    public class Configuration
    {

        public int Port { get; set; }



        public string HostName { get; set; }

        public Configuration(string hostName="localhost", int port=35000)
        {
            Port = port;
            HostName = hostName;
        }
    }
}

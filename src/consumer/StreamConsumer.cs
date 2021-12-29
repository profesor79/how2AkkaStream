namespace consumer
{
    internal class StreamConsumer
    {
        public StreamConsumer(Configuration c)
        {
            Configuration = c;
        }

        public Configuration Configuration { get; }
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
}

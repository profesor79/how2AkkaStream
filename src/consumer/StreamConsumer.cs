
using Akka;
using Akka.Actor;
using Akka.IO;
using Akka.Streams.Dsl;
using System.Net;
using Akka.Streams;
using System.Net.Sockets;
using System.Text;
using Tcp = Akka.Streams.Dsl.Tcp;

namespace common
{
    internal class StreamConsumer
    {
        public async void Start(Configuration c)
        {
            Configuration = c;

            var actorSystem = ActorSystem.Create("system");
            IMaterializer materializer = actorSystem.Materializer();
                IPAddress localAddr = IPAddress.Any;
            // define an incoming request processing logic
            Flow<ByteString, ByteString, NotUsed> echo = Flow.Create<ByteString>();

        //    Akka.Streams.Dsl.Tcp.ServerBinding binding = await actorSystem.TcpStream()
         //       .BindAndHandle(echo, materializer, c.HostName, c.Port);

        //    Console.WriteLine($"Server listening at {binding.LocalAddress}");


            Source<Tcp.IncomingConnection, Task<Tcp.ServerBinding>> connections =
    actorSystem.TcpStream().Bind(c.HostName, c.Port);


            _ = connections.RunForeach(connection =>
              {
                  Console.WriteLine($"New connection from: {connection.RemoteAddress}");

                  var echo = Flow.Create<ByteString>()
                      .Via(Framing.Delimiter(
                          ByteString.FromString("\n"),
                          maximumFrameLength: 512,
                          allowTruncation: true))
                      .Select(c => c.ToString())

                      .Select(c =>
                      {
                          Console.WriteLine(c);
                          return c;
                      })
                      .Select(ByteString.FromString);

                  connection.HandleWith(echo, materializer);

                  var closed = Flow.FromSinkAndSource(Sink.Cancelled<ByteString>(), Source.Empty<ByteString>());
                //  connection.HandleWith(closed, materializer);

            }, materializer);


           

            // close server after everything is done
          //  await binding.Unbind();
        }

        public Configuration Configuration; 



        }








    
}

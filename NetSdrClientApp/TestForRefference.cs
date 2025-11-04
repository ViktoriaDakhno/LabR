using EchoServer;

namespace NetSdrClientApp
{
    public class TestForRefference
    {
        public void CreateServer()
        {
            var server = new EchoServer.EchoServer(5000);
        }
    }
}

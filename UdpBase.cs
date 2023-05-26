using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RandomCordinateGenerator
{
    public class UdpBase
    {
        Socket client;
        Task task;
        public event Action<byte[]> ReceiveAction;
        private IPEndPoint dstEp;

        public string MyIpAddress { get; set; } = "127.0.0.1";
        public string DstIpAddress { get; set; } = "127.0.0.1";
        public int MyPort { get; set; } = 9091;
        public int DstPort { get; set; } = 9090;


        public UdpBase()
        {
            dstEp = new IPEndPoint(IPAddress.Parse(DstIpAddress), DstPort);
        }

        public UdpBase(string myIPAddr, string dstIPAddr, int myPort, int dstPort) : this()
        {
            MyIpAddress = myIPAddr;
            DstIpAddress = dstIPAddr;
            MyPort = myPort;
            DstPort = dstPort;
        }


        public void SendTo(string msg)
        {
            using (client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                // string --> byte[]
                byte[] _data = Encoding.Default.GetBytes(msg);

                // SendTo()
                client.SendTo(_data, _data.Length, SocketFlags.None, dstEp);

            }
        }

        public void ReceiveFrom()
        {
            IPEndPoint localPoint = new IPEndPoint(IPAddress.Parse(MyIpAddress), MyPort);
            EndPoint localEndPoint = (EndPoint)(localPoint);
            byte[] _data = new byte[1024];

            if (task == null)
            {
                task = Task.Run(() =>
                {
                    using (client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                    {
                        client.Bind(localEndPoint);

                        while (true)
                        {
                            Console.WriteLine(localEndPoint);
                            client.ReceiveFrom(_data, ref localEndPoint);
                            ReceiveAction.Invoke(_data);

                            Array.Clear(_data, 0, _data.Length);
                        }
                    }
                });
            }
        }


    }
}

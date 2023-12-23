using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Net.IO
{
    internal class PacketReader : BinaryReader
    {
        private NetworkStream _networkStream;

        public PacketReader(NetworkStream ns) : base(ns)
        {
            _networkStream = ns;
        }
        public string ReadMessage()
        {
            byte[] msgbuffer;
            var lenght = ReadInt32();
            msgbuffer = new byte[lenght];
            _networkStream.Read(msgbuffer, 0, lenght);

            var msg = Encoding.ASCII.GetString(msgbuffer);

            return msg;
        }
    }
}

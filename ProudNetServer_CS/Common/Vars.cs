using Nettention.Proud;
using Guid = System.Guid;

namespace Common
{

    public static class Vars
    {
        public const string SERVER_IP = "127.0.0.1";
        public const ushort SERVER_PORT = 17326;
        public const ushort WEB_SERVER_PORT = SERVER_PORT - 1000;
        public static Guid PROTOCOL_VERSION = new Guid("{ 0xafa3c0c, 0x77d7, 0x4b74, { 0x9d, 0xdb, 0x1c, 0xb3, 0xd2, 0x5e, 0x1e, 0x64 } }");
    }
}
using ChatRoom.Contracts.Model;
using System.Net.Sockets;

namespace Server.Contracts.Service
{
    //اینترفیس های لازم در سمت سرور
    public interface ITcpSocketServer
    {
        //سرور روی آی ی و پورت لازم شنود می کند و منتظر دریافت کانکشن می ماند
        TcpListener CreateListener(int port);
        //انتظار برای پذیرش کانکشن
        TcpClient WaitForConnection(TcpListener listener);
        //ایجاد استریم در سمت سرور
        NetworkStream CreateNetworkStream(TcpClient client);
        //دریافت پیام کلاینت و ارسال پیام به کلاینت در صوورت نیاز
        void ReceiveAndWriteMessage(TcpClient client, NetworkStream stream, Room room);
    }

}

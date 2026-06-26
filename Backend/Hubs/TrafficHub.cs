using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Backend.Hubs
{
    public class TrafficHub : Hub
    {
        // Hub này đóng vai trò phòng chat chung.
        // Khi Server gọi gửi tin, tất cả Client kết nối (WinForm, Mobile) sẽ nhận được ngay.
        public async Task SendStatusUpdate(object status)
        {
            await Clients.All.SendAsync("ReceiveStatus", status);
        }
    }
}
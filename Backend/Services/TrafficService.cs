using Backend.Data;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Services
{
    public class TrafficService
    {
        private readonly AppDbContext _context;

        public TrafficService(AppDbContext context)
        {
            _context = context;
        }

        // Thành viên Backend có thể viết thêm các hàm xử lý logic phán đoán hoặc thống kê log tại đây
    }
}
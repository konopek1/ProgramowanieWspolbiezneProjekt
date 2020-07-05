using System.Drawing;

namespace WindowsFormsApp1.com.WorkShops
{
    public class WorkShopLakier : WorkShop
    {
        public WorkShopLakier(Damage type, Worker worker, Painter painter, Point pos) : base(type, worker, painter, pos)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KLTN_FreshShop.Models
{
    public class GioHang
    {
        public SP Spham { get; set; }
        public int SoLuong { get; set; }
        public GioHang()
        {

        }
        public GioHang(SP spham, int soluong)
        {
            this.Spham = spham;
            this.SoLuong = soluong;
        }
    }
}
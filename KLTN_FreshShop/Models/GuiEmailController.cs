using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace KLTN_FreshShop.Models
{
    public class GuiEmail
    {
        public string Emailnguoigui { get; set; }
        public string Emailnguoinhan { get; set; }
        public string chude { get; set; }
        public string noidung { get; set; }
        public string mathau { get; set; }

        public void guithu()
        {
            MailMessage ms = new MailMessage();
            ms.From = new MailAddress(Emailnguoigui);
            ms.To.Add(new MailAddress(Emailnguoinhan));
            ms.Subject = chude;
            ms.IsBodyHtml = true;
            ms.Body = string.Format(noidung);
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(Emailnguoigui, mathau);
                smtp.Timeout = 200000;
            }
            smtp.Send(ms);
        }
    }
}
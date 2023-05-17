using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using static System.Net.WebRequestMethods;
using System.Xml.Linq;

namespace BLL.Services
{
    public class MailService
    {
        static string fromAddress = "desidukaan.official@gmail.com";
        static string emailPassword = "vbwdeqhtklkdfgsl";
        static SmtpClient smtpClient = new SmtpClient()
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential(fromAddress, emailPassword)
        };
        public static void SendMail(string toAddress, string subject, string body)
        {
            try
            {
                smtpClient.Send(fromAddress, toAddress, subject, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static void CustomerRegistration(string name, string toAddress)
        {
            string subject = "Paradise Mart Registration";
            string body = $@"Hi {name},
Welcome to Paradise Mart. Start your shopping today!
Happy Shopping!!!";
            SendMail(toAddress, subject, body);
        }
        public static void SellerRegistration(string name, string toAddress)
        {
            string subject = "Paradise Mart Seller Registration";
            string body = $@"Hi {name},
Welcome to Paradise Mart. Start your business today. Follow the terms and conditions.
Good luck!!!";
            SendMail(toAddress, subject, body);
        }
        public static void AdminRegistration(string name, string toAddress)
        {
            string subject = "Paradise Mart Admin Registration";
            string body = $@"Hi {name},
Welcome to Paradise Mart. Hope that you will provide a good service to the company.
Follow the terms and conditions properly.
Good luck!!!";
            SendMail(toAddress, subject, body);
        }
        public static string ForgotPassword(string name, string toAddress)
        {
            Random rand = new Random();
            string otp = rand.Next(1000000, 10000000).ToString();
            string subject = "Paradise Mart - Forgot Password";
            string body = $@"Hi {name},
Your OTP is {otp}.";
            SendMail(toAddress, subject, body);
            return otp;
        }
    }
}

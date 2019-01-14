using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Dapper;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;

namespace stackTraceYönetimi
{
    class Program
    {
       static SqlConnection con;

        static void Main(string[] args)
        {
            app();
            Console.ReadLine();
        }

        //public void mailGonder(string msg)
        //{
        //    MailMessage ePosta = new MailMessage();
        //    ePosta.From = new MailAddress("hata@zirvedekibeyinler.net");
        //    ePosta.To.Add("ercan@trio.gen.tr");
        //    ePosta.Subject = "Program Patladi";
        //    ePosta.Body = msg;

        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Credentials = new System.Net.NetworkCredential("posta@gmail.com", "sifre");
        //    smtp.Port = 587;
        //    smtp.Host = "smtp.gmail.com";
        //    smtp.EnableSsl = true;
        //    object userState = ePosta;
        //    bool kontrol = true;
        //    try
        //    {
        //        smtp.SendAsync(ePosta, (object)ePosta);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public static void app()
        {
            try
            {
                string str = "qwe";
                int i = Convert.ToInt32(str);
            }
            catch (Exception ex)
            {
                con = new SqlConnection("Data Source=10.10.22.199;Initial Catalog=test;User ID=test2;Password=test2");
                //Hatanin satir numarasina erismek icin true degeri verilir.//
                StackTrace stack = new StackTrace(true);
                foreach (StackFrame frame in stack.GetFrames())
                {
                    if (!string.IsNullOrEmpty(frame.GetFileName()))
                    {
                        con.Execute("Insert into HataLoglari(DosyaAdi,MethodAdi,LineNumber,ColumnNumber,Message) Values(@DosyaAdi,@MethodAdi,@LineNumber,@ColumnNumber,@Message)",
                            new { @DosyaAdi =Path.GetFileName(frame.GetFileName()),
                                @MethodAdi = frame.GetMethod().ToString(),
                                @LineNumber = frame.GetFileLineNumber(),
                                @ColumnNumber = frame.GetFileColumnNumber(),
                                @Message = ex.Message
                                });

                        Console.WriteLine($"Dosya Adi:{frame.GetFileName()}");
                        Console.WriteLine($"Line Number:{frame.GetFileLineNumber()}");
                        Console.WriteLine($"Column Number:{frame.GetFileColumnNumber()}");
                        Console.WriteLine($"Method Name:{frame.GetMethod()}");

                    }
                }

            }
        }
    }
}

using System;
using System.IO;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using S22.Imap;

namespace S22.Mail.UnitTest
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestMIME822()
        {
            MailMessage mailMessage = GenerateMailMessage();

            string mime822 = mailMessage.ToMIME822();
            
            mailMessage = MessageBuilder.FromMIME822(mime822);

            PrintMailMessage(mailMessage);
        }

        [Test]
        public void TestJson()
        {
            MailMessage mailMessage = GenerateMailMessage();
            string str = JsonConvert.SerializeObject(mailMessage);
            Console.WriteLine(str);
        }

        [Test]
        public void Test3()
        {
            MailMessage mailMessage = GenerateMailMessage();

            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream s = new MemoryStream())
            {
                // Serialize MailMessage to memory stream
                formatter.Serialize(s, (SerializableMailMessage) mailMessage);

                // Rewind stream and deserialize MailMessage
                s.Seek(0, SeekOrigin.Begin);
                MailMessage newMailMessage = formatter.Deserialize(s) as SerializableMailMessage;
                PrintMailMessage(newMailMessage);
            }
        }

        private static MailMessage GenerateMailMessage()
        {
            MailAddress from = new MailAddress("244657538@qq.com", "Chuck Lu, 子非鱼");
            MailAddress to = new MailAddress("778346154@qq.com","Jun Zhou");
            MailMessage mailMessage = new MailMessage(from, to)
            {
                HeadersEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8
            };

            mailMessage.CC.Add("614982784@qq.com");
            mailMessage.CC.Add("1263028489@qq.com");
            mailMessage.Bcc.Add("1595442830@qq.com");
            mailMessage.Subject = "Hello World 世界你好";
            mailMessage.Body = "This is just a test 这只是一个测试";
            return mailMessage;
        }

        private static void PrintMailMessage(MailMessage mailMessage)
        {
            Console.WriteLine($"From: {mailMessage.From}");
            Console.WriteLine($"Subject: {mailMessage.Subject}");
            Console.WriteLine($"To: {mailMessage.To}");
            Console.WriteLine($"CC: {mailMessage.CC}");
            Console.WriteLine($"Bcc: {mailMessage.Bcc}");
            Console.WriteLine($"Body: {mailMessage.Body}");
        }
    }
}

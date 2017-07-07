﻿using System;
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
            string str = mailMessage.ToMIME822();
            Console.WriteLine(str);
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
                Console.WriteLine(newMailMessage.From.Address);
                Console.WriteLine(newMailMessage.From.DisplayName);
                Console.WriteLine(newMailMessage.Subject);
                Console.WriteLine(newMailMessage.Body);
            }
        }

        private static MailMessage GenerateMailMessage()
        {
            MailAddress from = new MailAddress("244657538@qq.com", "Chuck LU");
            MailAddress to = new MailAddress("778346154@qq.com");
            MailMessage mailMessage = new MailMessage(from, to)
            {
                HeadersEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8
            };

            mailMessage.CC.Add("614982784@qq.com");
            mailMessage.Bcc.Add("1595442830@qq.com");
            mailMessage.Subject = "Hello World";
            mailMessage.Body = "This is just a test";
            return mailMessage;
        }
    }
}

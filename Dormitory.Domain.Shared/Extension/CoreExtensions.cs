using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace Dormitory.Domain.Shared.Extension
{
    public static class CoreExtensions
    {
        public static string Encrypt(string toEncrypt)
        {
            string str_md5 = "";
            byte[] mang = Encoding.UTF8.GetBytes(toEncrypt);

            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);

            foreach (byte b in mang)
            {
                str_md5 += b.ToString("X2");
            }

            return str_md5;
        }
    }
}

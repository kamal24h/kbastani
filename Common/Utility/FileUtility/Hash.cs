using System;
using System.Security.Cryptography;
using System.Text;

namespace LuxShop.CommonUtility.FileUtility
{
    public static class Hash
    {
        //Encrypt using MD5   
        public static string EncodePasswordMd5(this string pass)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)   
            md5 = new MD5CryptoServiceProvider();
            originalBytes = Encoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string   
            return BitConverter.ToString(encodedBytes);
        }
    }
}

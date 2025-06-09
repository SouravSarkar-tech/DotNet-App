using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class Encryption
{
    public static string Decrypt(string strText)
    {
        return DecryptText(strText, "&%#@?,:*");
    }

    private static string DecryptText(string strText, string strDecrKey)
    {
        byte[] rgbKey = new byte[0];
        byte[] rgbIV = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
        byte[] buffer = new byte[strText.Length];
        try
        {
            rgbKey = Encoding.UTF8.GetBytes(strDecrKey.Substring(0, 8));
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            buffer = Convert.FromBase64String(strText);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            return Encoding.UTF8.GetString(stream.ToArray());
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
    }

    public static string Encrypt(string strText)
    {
        return EncryptText(strText, "&%#@?,:*");
    }

    private static string EncryptText(string strText, string strEncrKey)
    {
        byte[] rgbKey = new byte[0];
        byte[] rgbIV = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
        try
        {
            rgbKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(strText);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            return Convert.ToBase64String(stream.ToArray());
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
    }
}
/*
 * Created by SharpDevelop.
 * User: daniel.thaller
 * Date: 02.11.2021
 * Time: 09:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;  
using System.Security.Cryptography;  
using System.Text; 

namespace Bibliotheken
{
	/// <summary>
	/// Sammlung von Funktionen im Zusammenhang mit Kryptografie
	/// </summary>
	public static class Crypto
	{
		/// <summary>
		/// Verschlüsselung eines Strings
		/// </summary>
		/// <param name="CryptoKey">Key / String mit welchem die Werte verschlüsselt werden sollen</param>
		/// <param name="Text">zu verschlüsselnder TEXT / String</param>
		/// <returns>verschlüsselte Daten als String</returns>
        public static string EncryptString(string CryptoKey, string Text)  
        {  
			try 
			{
				byte[] iv = new byte[16];
	            byte[] array;  
	  
	            using (Aes aes = Aes.Create())  
	            {  
	                aes.Key = Encoding.UTF8.GetBytes(CryptoKey);  
	                aes.IV = iv;  
	  
	                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);  
	  
	                using (MemoryStream memoryStream = new MemoryStream())  
	                {  
	                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))  
	                    {  
	                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))  
	                        {  
	                            streamWriter.Write(Text);  
	                        }  
	  
	                        array = memoryStream.ToArray();  
	                    }  
	                }  
	            }  
	            return Convert.ToBase64String(array);
	        }
			catch (Exception)
			{
				return null;
			}            
        }  
  
		/// <summary>
		/// Entschlüsselung eines Strings
		/// </summary>
		/// <param name="CryptoKey">Key / String mit welchem die Werte entschlüsselt werden sollen</param>
		/// <param name="Text">verschlüsselter TEXT / String</param>
		/// <returns>unverschlüsselte Daten als String</returns>
        public static string DecryptString(string CryptoKey, string Text)  
        {  
			try 
			{
	        	byte[] iv = new byte[16];
	            byte[] buffer = Convert.FromBase64String(Text);  
	            using (Aes aes = Aes.Create())  
	            {  
	                aes.Key = Encoding.UTF8.GetBytes(CryptoKey);  
	                aes.IV = iv;  
	                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);  
	  
	                using (MemoryStream memoryStream = new MemoryStream(buffer))  
	                {  
	                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))  
	                    {  
	                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))  
	                        {  
	                            return streamReader.ReadToEnd();  
	                        }  
	                    }  
	                }  
	            }  
			}
			catch (Exception)
			{
				return null;
			}
        }  
	}
}

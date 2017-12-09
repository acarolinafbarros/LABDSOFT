using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace GAM.Security
{
    public class Encryptor<T> : IEncryptor<T>
    {
        private readonly IDataProtector _protector;

        public Encryptor(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector(GetType().FullName);
        }

        public IList<string> EncryptListStrings(IList<string> listStrings)
        {
            IList<string> encryptedList = new List<string>();

            foreach (string s in listStrings)
            {
                string new_s = EncryptString(s);

                encryptedList.Add(new_s);
            }
            return encryptedList;
        }

        public string EncryptObject(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            return EncryptString(json);
        }

        public string EncryptString(string plaintext)
        {
            return _protector.Protect(plaintext);
        }

        public IList<string> TryDecryptListStrings(IList<string> listStrings)
        {
            IList<string> decryptedList = new List<string>();

            foreach(string s in listStrings)
            {
                if(TryDecryptString(s, out string decryptedString))
                {
                    decryptedList.Add(decryptedString);
                }
            }
            return decryptedList;
        }

        public bool TryDecryptObject(string encryptedText, out T obj)
        {
            if (TryDecryptString(encryptedText, out var json))
            {
                obj = JsonConvert.DeserializeObject<T>(json);

                return true;
            }

            obj = default(T);

            return false;
        }

        public bool TryDecryptString(string encryptedText, out string decryptedText)
        {
            try
            {
                decryptedText = _protector.Unprotect(encryptedText);

                return true;
            }
            catch (CryptographicException)
            {
                decryptedText = null;

                return false;
            }
        }
    }
}

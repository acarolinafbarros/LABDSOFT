using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAM.Security
{
    public interface IEncryptor<T>
    {
        IList<string> EncryptListStrings(IList<string> listStrings);
        string EncryptObject(T obj);
        string EncryptString(string str);
        IList<string> TryDecryptListStrings(IList<string> listStrings);
        bool TryDecryptObject(string encryptedText, out T obj);
        bool TryDecryptString(string encryptedText, out string decryptedText);
    }
}

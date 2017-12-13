using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamTests
{
    public class EncryptorTestInstance : IDataProtectionProvider
    {
        public IDataProtector CreateProtector(string purpose)
        {
            return new DummyDataProtector();
        }

        private class DummyDataProtector : IDataProtector
        {
            public IDataProtector CreateProtector(string purpose)
            {
                return this;
            }

            public byte[] Protect(byte[] userData)
            {
                return userData;
            }

            public byte[] Unprotect(byte[] protectedData)
            {
                return protectedData;
            }
        }
    }
}

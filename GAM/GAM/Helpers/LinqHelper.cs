using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAM.Helpers
{
    public static class LinqHelper
    {
        public static T GetFirstOrDefault<T>(this T value, T alternate)
        {
            if (value.Equals(default(T))) return alternate;
            return value;
        }

    }
}

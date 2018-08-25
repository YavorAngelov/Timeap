using System;

namespace Timeap.Common.Validation
{
    public static class Validator
    {
        public static void EnsureNotNull(object obj, string message = "")
        {
            if (obj == null)
            {
                throw new ArgumentException(message);
            }
        }

        public static void EnsureStringIsNotNullOrEmpty(string str, string message = "")
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException(message);
            }
        }
    }
}

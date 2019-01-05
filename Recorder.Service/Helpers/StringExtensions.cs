using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recorder.Service.Helpers
{
    public static class StringExtensions
    {
        public static bool isIPAddress(this string input)
        {
            if (String.IsNullOrWhiteSpace(input))
                return false;

            string[] splitValues = input.Split('.');
            if (splitValues.Length != 4)
                return false;

            if (splitValues.All(b => byte.TryParse(b, out _)))
                return true;

            return false;
        }
    }
}

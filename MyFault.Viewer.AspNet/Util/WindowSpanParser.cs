using System;
using System.Collections.Specialized;
using System.Linq;

namespace MyFault.Viewer.AspNet.Util
{
    public class WindowSpanParser
    {
        public static TimeSpan ParseWindowSpan(NameValueCollection requestQueryString)
        {
            string queryStringValue = requestQueryString["window"];
            char qualifier = queryStringValue.ToLower().Last();
            string value = queryStringValue.Substring(0, queryStringValue.Length - 1);
            int parsedValue = Int32.Parse(value);
            TimeSpan returnSpan = TimeSpan.MinValue;
            switch (qualifier)
            {
                case 's':
                    returnSpan = new TimeSpan(0, 0, parsedValue);
                    break;
                case 'm':
                    returnSpan = new TimeSpan(0, parsedValue, 0);
                    break;
                case 'h':
                    returnSpan = new TimeSpan(parsedValue, 0, 0);
                    break;
                case 'd':
                    returnSpan = new TimeSpan(parsedValue, 0, 0, 0);
                    break;
                case 'y':
                    returnSpan = new TimeSpan(365 * parsedValue, 0, 0, 0);
                    break;
            }

            return returnSpan;
        }
    }
}
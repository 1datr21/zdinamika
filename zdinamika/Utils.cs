using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdinamika
{
    class Utils
    {
        public static DateTime NumStr2DT(string numstr)// юникс-время в виде строки в DateTime
        {
            Int64 unixTime = Convert.ToInt64(numstr);

            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddMilliseconds(unixTime);

        }
    }
}

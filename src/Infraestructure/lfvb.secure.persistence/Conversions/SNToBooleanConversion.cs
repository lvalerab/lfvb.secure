using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Conversions
{
    public class SNToBooleanConversion
    {
        public static bool toBoolean(string v)
        {
            return v.ToUpper() == "S" ? true : false;
        }

        public static string toString(bool v)
        {
            return v ? "S" : "N";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.persistence.Conversions
{
    public class GuidConversion
    {
        public static Guid toGuid(string v)
        {
            return Guid.Parse(v);
        }

        public static string toString(Guid v)
        {
            return v.ToString();
        }
    }
}

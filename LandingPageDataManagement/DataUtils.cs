using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingPageDataManagement
{
    public class DataUtils
    {
        public static string GetJSON(Dictionary<string, List<int>> dict)
        {
            var entries = dict.Select(d =>
                string.Format("\"{0}\": [{1}]", d.Key, string.Join(",", d.Value)));
            return "{" + string.Join(",", entries) + "}";
        }
    }
}

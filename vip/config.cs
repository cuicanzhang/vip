using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vip
{
    class config
    {
        public static string DatabaseFile = "vip.sqlite";
        public static string DataSource
        {
            get
            {
                return string.Format("data source={0}", DatabaseFile);
            }
        }

        public static string adminID { set; get; }
        public static string vipCount { set; get; }
        public static string vipBirthdayCount { set; get; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebSiteTest.Test
{
    public class Projects : List<Project>
    {
        public new object[] ToArray()
        {
            object[] retVal = this
                          .Select(r => r.Url.ToString())
                          .ToArray();

            return retVal;
        }
    }
}

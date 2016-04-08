using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSiteTest.Test.Commands;

namespace WebSiteTest.Parameters
{
    public class TestParameters
    {
        public Test.Projects Projects { get; set; }
        public TestCommands Commands =new TestCommands();
    }
}

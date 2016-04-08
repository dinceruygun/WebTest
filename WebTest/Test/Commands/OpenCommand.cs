using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;

namespace WebSiteTest.Test.Commands
{
    public class OpenCommand: ICommand
    {
        private string CommandName => "Open";

        public override string GetCommandName()
        {
            return this.CommandName;
        }

        public override void RunTest(IE browser, Project project)
        {
            browser.GoTo($"{project.Url}{base.Target}");
        }
    }
}

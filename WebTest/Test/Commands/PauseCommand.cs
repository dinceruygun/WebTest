using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatiN.Core;

namespace WebSiteTest.Test.Commands
{
    public class PauseCommand: ICommand
    {
        private string CommandName => "Pause";

        public override string GetCommandName()
        {
            return this.CommandName;
        }

        public override void RunTest(IE browser, Project project)
        {
            System.Threading.Thread.Sleep(int.Parse(base.Value));
        }
    }
}

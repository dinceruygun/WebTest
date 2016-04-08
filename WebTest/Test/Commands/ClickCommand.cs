using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using WatiN.Core.Constraints;

namespace WebSiteTest.Test.Commands
{
    public class ClickCommand:ICommand
    {
        private string CommandName => "Click";

        public override string GetCommandName()
        {
            return this.CommandName;
        }

        public override void RunTest(IE browser, Project project)
        {
            AttributeConstraint att = base.GetFindAttribute(base.Target);

            if (att == null) return;

            switch (att.AttributeName)
            {
                case "value":
                    browser.Button(att).Click();
                    break;
                default:
                    browser.Element(att).Click();
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using WatiN.Core.Constraints;

namespace WebSiteTest.Test.Commands
{
    public class TypeCommand : ICommand
    {
        private string CommandName => "Type";

        public override string GetCommandName()
        {
            return this.CommandName;
        }

        public override void RunTest(IE browser, Project project)
        {
            AttributeConstraint att = base.GetFindAttribute(base.Target);

            if (att != null)
                browser.TextField(att).TypeText(base.Value);
        }
    }
}

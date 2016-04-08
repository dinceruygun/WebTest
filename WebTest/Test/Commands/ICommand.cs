using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using WatiN.Core.Constraints;

namespace WebSiteTest.Test.Commands
{
    // ReSharper disable once InconsistentNaming
    public abstract class ICommand: Command
    {

        public abstract string GetCommandName();
        public abstract void RunTest(IE browser, Project project);

        protected ICommand()
        {
            Name = GetCommandName();
        }

        protected AttributeConstraint GetFindAttribute(string target)
        {
            if (target == null) return null;

            if (target.IndexOf("=", StringComparison.Ordinal) < 0)
            {
                return null;
            }
            else
                switch (target.Split('=')[0])
                {
                    case "id":
                        return Find.ById(target.Split('=')[1]);
                    case "name":
                        return Find.ByName(target.Split('=')[1]);
                    case "class":
                        return Find.ByClass(target.Split('=')[1]);
                    case "value":
                        return Find.ByValue(target.Split('=')[1]);
                    case "src":
                        return Find.BySrc(target.Split('=')[1]);
                    default:
                        return null;
                }
        }
    }
}

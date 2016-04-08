using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebSiteTest.Test.Commands
{
    public static class CommandFactory
    {
        public static List<T> GetCommands<T>()
        {
            var interfaceType = typeof (T);

            var classes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => t.BaseType?.Name == interfaceType.Name).ToList();


            var typeList = classes
                .Select(t => (T) Activator.CreateInstance(t))
                .Where(e => e != null)
                .ToList();


            return typeList;
        }
    }
}

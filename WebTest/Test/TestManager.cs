﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSiteTest.Test.Commands;
using WatiN.Core;

namespace WebSiteTest.Test
{
    public static class TestManager
    {
        private static IE _browser;
        private static List<ICommand> _commands;
        private static readonly object Locker = new object();
        private static bool _runTestStatus = false;

        public static List<ICommand> Commands
        {
            get
            {
                if (_commands == null)
                {
                    lock (Locker)
                    {
                        _commands = CommandFactory.GetCommands<ICommand>();
                    }
                }

                return _commands; 
            }
        }

        public static void SetBrowser(IE browser)
        {
            _browser = browser;
        }

        public static IE Browser
        {
            get { return _browser; }
        }

        public static bool RunTestStatus
        {
            get { return _runTestStatus; }
        }

        public static void RunTest(Project project, ICommand cmd)
        {
            try
            {
                _runTestStatus = true;
                cmd.RunTest(Browser, project);
                _runTestStatus = false;
            }
            catch (Exception)
            {
                _runTestStatus = false;
            }
        }
    }
}

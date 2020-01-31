using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Orange.HRM.Common.Helper;
using System;

namespace Orange.HRM.Common.NUnit.Attributtes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class KillNotepadProcessAttribute : Attribute, ITestAction
    {
        public void BeforeTest(ITest test)
        {
            ProcessUtils.KillProcesses("notepad");
        }

        public void AfterTest(ITest test)
        {
            ProcessUtils.KillProcesses("notepad");
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }
    }
}

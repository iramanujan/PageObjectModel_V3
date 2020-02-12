using Orange.HRM.Common.Handler.Browser;
using Orange.HRM.TestHarness.Steps.BaseSetup;
using System;
using System.Collections.Generic;

namespace Orange.HRM.TestHarness.Utils
{
    class CreateObjectUtils
    {
        public static OrangeHrmSteps CreateOrangeHrmSteps<OrangeHrmSteps>(Browser browser, params object[] additionalArgs)
            where OrangeHrmSteps : OrangeHrmBaseSteps
        {
            var args = MergeArguments(new object[] { browser, }, additionalArgs);
            return CreateInstance<OrangeHrmSteps>(args);
        }

        public static T CreateInstance<T>(object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }

        private static object[] MergeArguments(object[] requiredArgs, params object[] optionalArgs)
        {
            var result = new List<object>();
            if (requiredArgs != null && requiredArgs.Length != 0)
            {
                result.AddRange(requiredArgs);
            }

            if (optionalArgs != null && optionalArgs.Length != 0)
            {
                result.AddRange(optionalArgs);
            }
            return result.ToArray();
        }
    }
}

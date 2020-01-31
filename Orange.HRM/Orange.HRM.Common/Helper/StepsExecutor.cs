﻿using Orange.HRM.Common.Handler.Log;
using NUnit.Framework;
using Orange.HRM.Common.Configuration;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using NInternal = NUnit.Framework.Internal;


namespace Orange.HRM.Common.Helper
{
    public static class StepsExecutor
    {
        private static string ScreenshotImagPath = "";
        private static readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        public static Exception ExecuteSafely(Action steps)
        {
            return Execute(steps,false, false);
        }
        public static Exception ExecuteSafelyCheckScreenshotOnFailure(Action steps, bool onErrorTakeScreenshot)
        {
            return Execute(steps, onErrorTakeScreenshot, false);
        }

        private static Exception Execute(Action steps, bool onErrorTakeScreenshot, bool rethrowExceptionAfterReporting)
        {
            using (new NInternal.TestExecutionContext.IsolatedContext())
            {
                try
                {
                    steps();
                }
                catch (Exception exception)
                {
                    LogException(exception);
                    if (onErrorTakeScreenshot)
                    {
                        MakeAndSaveScreenshot();
                    }
                    if (rethrowExceptionAfterReporting)
                    {
                        throw;
                    }
                    return exception;
                }
                return null;
            }
        }

        private static void LogException(Exception exception)
        {
            var className = TestContext.CurrentContext.Test.ClassName;
            var testName = TestContext.CurrentContext.Test.MethodName;
            Logger.Error("The exception Message: " + exception.Message);
            Logger.Error("The exception Stack Trace:{0}{1} ", Environment.NewLine, exception.StackTrace);
            Logger.Error("ERROR: Failed to complete steps for method {0}.{1}", className, testName);
        }


        private static string MakeAndSaveScreenshot()
        {
           ScreenshotImagPath = appConfigMember.AutomationReportPath + "\\" + GenerateScreenshotName(TestContext.CurrentContext.Test.Name.Replace(" ", string.Empty)) + ".png";
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                bitmap.Save(ScreenshotImagPath, ImageFormat.Png);
            }
            return ScreenshotImagPath;
        }

        private static string GenerateScreenshotName(string fileName)
        {
            return fileName + string.Format("{0:_yyyy_MM_dd_hh_mm_ss}", DateTime.Now);
        }
    
    }
}

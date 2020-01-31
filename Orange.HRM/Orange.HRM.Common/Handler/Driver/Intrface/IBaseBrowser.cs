using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Orange.HRM.Common.Handler.Driver.Intrface
{
    interface IBaseBrowser
    {

        string Url { get; set; }

        string Title { get; }

        string PageSource { get; }

        string CurrentWindowHandle { get; }

        ReadOnlyCollection<string> WindowHandles { get; }

        void Close();

        IOptions Manage();

        INavigation Navigate();

        void Quit();

        void SwitchToWindowHandle(string windowHandle);

        IBaseBrowser Refresh();

    }
}

using OpenQA.Selenium;
using Orange.HRM.Common.Configuration;
using Orange.HRM.Common.Location.Upload;
using System;

namespace Orange.HRM.Common.Handler.Driver.Base
{
    public abstract class BaseRemoteDriverFactory
    {
        protected abstract ICapabilities Capabilities { get; }

        protected static readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        protected Lazy<string> downloadLocation;
        protected Lazy<UploadLocation> uploadLocation;

        protected string browserName = appConfigMember.Browser.ToString();
        protected string gridUrl = appConfigMember.GridUrl.ToString();
        protected int commandTimeout = appConfigMember.CommandTimeout;
        protected string gridHost = appConfigMember.GridHost.ToString();

    }
}

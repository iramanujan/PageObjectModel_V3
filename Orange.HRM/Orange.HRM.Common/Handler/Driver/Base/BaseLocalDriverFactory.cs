using Orange.HRM.Common.Configuration;
using Orange.HRM.Common.Location.Download;
using Orange.HRM.Common.Location.Upload;
using System;

namespace Orange.HRM.Common.Handler.Driver.Base
{
    public class BaseLocalDriverFactory
    {
        protected static readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        protected Lazy<string> downloadLocation = new Lazy<string>(() => DownloadLocation.CreateWebDriverDirectory(appConfigMember.Browser.ToString() + appConfigMember.ExecutionType.ToString(), appConfigMember.RootDownloadLocation));
        protected Lazy<UploadLocation> uploadLocation = new Lazy<UploadLocation>(() => UploadLocation.Create(appConfigMember.Browser.ToString() + appConfigMember.ExecutionType.ToString(), true, appConfigMember.RootUploadLocation));
    }
}

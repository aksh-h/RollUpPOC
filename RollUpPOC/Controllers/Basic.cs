using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace RollUpPOC.Controllers
{
    public class Basic
    {
        public static ProjectHttpClient _projectClient;
        public static void InitClients(VssConnection srcConnection) => _projectClient = srcConnection.GetClient<ProjectHttpClient>();
        public static void ConnectWithPAT(string srcURL, string srcPAT)
        {
            try
            {
                VssConnection srcConnection = new VssConnection(new Uri(srcURL), new VssBasicCredential(string.Empty, srcPAT));
                InitClients(srcConnection);
            }
            catch (Exception ex)
            {
            }
        }
    }
}

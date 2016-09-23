using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Demo.Models.Viewmodel;
using EPiServer;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.People.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Demo.Controllers.Admin
{
    [Authorize(Roles = "Administrators, WebAdmins")]
    [EPiServer.PlugIn.GuiPlugIn(
        Area = EPiServer.PlugIn.PlugInArea.AdminMenu,
        Url = "/GoogleAuth/Index",
        DisplayName = "Google authentication")]
    public class GoogleAuthController : Controller
    {
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetadata()).AuthorizeAsync(cancellationToken);

            if (result.Credential != null)
            {
                var peopleService = new PeopleService(new BaseClientService.Initializer
                {
                    ApplicationName = "People API .NET Quickstart",
                    HttpClientInitializer = result.Credential,
                });

                var response = peopleService.People.Get("people/me").Execute();

                var viewModel = new GoogleAuthViewModel();
                viewModel.Email = response.EmailAddresses.First().Value;
                viewModel.Name = response.Names.First().DisplayName;

                return View("~/Views/Admin/GoogleAuth/Index.cshtml", viewModel);
            }

            return View("~/Views/Admin/GoogleAuth/Index.cshtml", null);
        }

        public async Task<ActionResult> Clear(CancellationToken cancellationToken)
        {
            var fileDataStore = new FileDataStore(AppDomain.CurrentDomain.BaseDirectory + "/App_Data/People.Api.Auth.Store", true);

            await fileDataStore.ClearAsync();

            return View("~/Views/Admin/GoogleAuth/Index.cshtml", null);
        }

        public async Task<ActionResult> Auth(CancellationToken cancellationToken)
        {
            var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetadata()).AuthorizeAsync(cancellationToken);

            if (result.Credential != null)
            {
                return View("~/Views/Admin/GoogleAuth/Auth.cshtml");
            }
            else
            {
                return new RedirectResult(result.RedirectUri);
            }
        }

        public ActionResult ClearCache()
        {
            CacheManager.Remove("contacts-cache-key");

            return RedirectToAction("Index");
        }
    }
    public class AuthCallbackController : Google.Apis.Auth.OAuth2.Mvc.Controllers.AuthCallbackController
    {
        protected override Google.Apis.Auth.OAuth2.Mvc.FlowMetadata FlowData
        {
            get { return new AppFlowMetadata(); }
        }
    }
}
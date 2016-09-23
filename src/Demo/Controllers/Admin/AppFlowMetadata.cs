using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.People.v1;
using Google.Apis.Util.Store;

namespace Demo.Controllers.Admin
{
    public class AppFlowMetadata : FlowMetadata
    {
        private static readonly IAuthorizationCodeFlow flow =
            new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "814226254437-6nb8trdltc3mks987af4s4g9qndst781.apps.googleusercontent.com",
                    ClientSecret = "4IQJQgN2JxyIwQDr1xAtDLJp"
                },
                Scopes = new[] { PeopleService.Scope.ContactsReadonly, PeopleService.Scope.UserinfoProfile, PeopleService.Scope.UserEmailsRead, PeopleService.Scope.PlusLogin },
                DataStore = new FileDataStore(AppDomain.CurrentDomain.BaseDirectory + "/App_Data/People.Api.Auth.Store", true)
            });

        public override string GetUserId(Controller controller)
        {
            return "user";
        }

        public override IAuthorizationCodeFlow Flow
        {
            get { return flow; }
        }
    }
}
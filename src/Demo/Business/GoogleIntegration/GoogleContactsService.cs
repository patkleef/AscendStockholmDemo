using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Business.Configuration;
using EPiServer;
using EPiServer.Framework.Cache;
using EPiServer.ServiceLocation;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.People.v1;
using Google.Apis.People.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Demo.Business.GoogleIntegration
{
    /// <summary>
    /// Google contacts service
    /// Integration with Google People APi
    /// </summary>
    [ServiceConfiguration(ServiceType = typeof(GoogleContactsService))]
    public class GoogleContactsService
    {
        private readonly string _cacheKey = "contacts-cache-key";
        private readonly string _fileDataStorePath;

        private IEnumerable<Person> _persons;

        public GoogleContactsService()
        {
            _fileDataStorePath = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/People.Api.Auth.Store";
            _persons = new List<Person>();
        }

        private async Task<UserCredential> GetUserCredential()
        {
            var fileDataStore = new FileDataStore(_fileDataStorePath, true);
            var tokenResponse = await fileDataStore.GetAsync<TokenResponse>("user");

            var flow = new Google.Apis.Auth.OAuth2.Flows.AuthorizationCodeFlow(
                new Google.Apis.Auth.OAuth2.Flows.GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = ConfigurationHelper.GoogleClientId,
                        ClientSecret = ConfigurationHelper.GoogleClientSecret
                    }
                });
            return new UserCredential(flow, "user", tokenResponse);
        }
        
        public async Task<IEnumerable<Person>> GetContacts()
        {
            var list = CacheManager.Get(_cacheKey);
            if (list != null)
            {
                return list as IEnumerable<Person>;
            }

            var credential = await GetUserCredential();

            var peopleService = new PeopleService(new BaseClientService.Initializer
            {
                ApplicationName = ConfigurationHelper.ApplicationName,
                HttpClientInitializer = credential
            });

            var request = peopleService.People.Connections.List("people/me");
            request.RequestMaskIncludeField = "person.addresses,person.email_addresses,person.metadata,person.names,person.organizations,person.phone_numbers";

            var response = request.Execute();
            
            CacheManager.Insert(_cacheKey, response.Connections, new CacheEvictionPolicy(TimeSpan.FromSeconds(5), CacheTimeoutType.Sliding));
            _persons = response.Connections;

            return response.Connections.AsEnumerable();
        }
        
        public async Task<Person> GetContact(string resourceName)
        {
            var list = CacheManager.Get(_cacheKey);
            if (list != null)
            {
                var contact = ((IEnumerable<Person>) list).FirstOrDefault(x => x.ResourceName.Equals(resourceName));
                if (contact != null)
                {
                    return contact;
                }
            }

            var credential = await GetUserCredential();

            var peopleService = new PeopleService(new BaseClientService.Initializer
            {
                ApplicationName = ConfigurationHelper.ApplicationName,
                HttpClientInitializer = credential
            });

            var response = peopleService.People.Get(resourceName).Execute();

            return response;
        }
    }
}
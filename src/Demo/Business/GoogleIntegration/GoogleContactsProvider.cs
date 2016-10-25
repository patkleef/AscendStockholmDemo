using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Models.Other;
using EPiServer;
using EPiServer.Construction;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using Google.Apis.People.v1.Data;

namespace Demo.Business.GoogleIntegration
{
    public class GoogleContactsProvider : ContentProvider
    {
        public const string Key = "google-contacts-provider";

        protected override IList<GetChildrenReferenceResult> LoadChildrenReferencesAndTypes(ContentReference contentLink, string languageID, out bool languageSpecific)
        {
            var identityMappingService = ServiceLocator.Current.GetInstance<IdentityMappingService>();
            var googleContactsService = ServiceLocator.Current.GetInstance<GoogleContactsService>();

            var contacts = googleContactsService.GetContacts();

            languageSpecific = false;

            var result = contacts.Result.Select(p =>
               new GetChildrenReferenceResult()
               {
                   // MappedIdentity.ConstructExternalIdentifier(ProviderKey, p.ResourceName.Split('/')[1]): epi.cms.identity://google-contacts-provider/c5792122681440133761
                   ContentLink = identityMappingService.Get(MappedIdentity.ConstructExternalIdentifier(ProviderKey, p.ResourceName.Split('/')[1]), true).ContentLink,
                   ModelType = typeof(ContactData)
               }).ToList();
            return result;
        }

        protected override IContent LoadContent(ContentReference contentLink, ILanguageSelector languageSelector)
        {
            // contentLink: Id: 319, ProviderName = "google-contacts-provider"

            var identityMappingService = ServiceLocator.Current.GetInstance<IdentityMappingService>();
            var googleContactsService = ServiceLocator.Current.GetInstance<GoogleContactsService>();

            // tblMappedIdentity
            var mappedIdentity = identityMappingService.Get(contentLink); 

            string resourceName = mappedIdentity.ExternalIdentifier.Segments[1]; // c5792122681440133761

            return Convert(googleContactsService.GetContact("people/" + resourceName).Result);
        }
        
        
        protected override void SetCacheSettings(ContentReference contentReference, IEnumerable<GetChildrenReferenceResult> children, CacheSettings cacheSettings)
        {
            cacheSettings.CancelCaching = true;

            base.SetCacheSettings(contentReference, children, cacheSettings);
        }
        
        private ContactData Convert(Person person)
        {
            if (person != null)
            {
                var contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();
                var identityMappingService = ServiceLocator.Current.GetInstance<IdentityMappingService>();

                var contentFactory = ServiceLocator.Current.GetInstance<IContentFactory>();
                ContentType type = contentTypeRepository.Load(typeof(ContactData));

                ContactData contactData = contentFactory.CreateContent(type, new BuildingContext(type)
                {
                    Parent = DataFactory.Instance.Get<ContentFolder>(EntryPoint),
                }) as ContactData;

                Uri externalId = MappedIdentity.ConstructExternalIdentifier(ProviderKey, person.ResourceName.Split('/')[1]);
                MappedIdentity mappedContent = identityMappingService.Get(externalId, true);

                contactData.ContentLink = mappedContent.ContentLink;
                contactData.ContentGuid = mappedContent.ContentGuid;
                contactData.Status = VersionStatus.Published;
                contactData.IsPendingPublish = false;
                contactData.StartPublish = DateTime.Now.Subtract(TimeSpan.FromDays(1));

                var name = (person.Names != null && person.Names.FirstOrDefault() != null ? person.Names.FirstOrDefault().DisplayName : string.Empty);
                var email = (person.EmailAddresses != null && person.EmailAddresses.FirstOrDefault() != null ? person.EmailAddresses.FirstOrDefault().Value : string.Empty);
                var telephonenumber = (person.PhoneNumbers != null && person.PhoneNumbers.FirstOrDefault() != null ? person.PhoneNumbers.FirstOrDefault().Value : string.Empty);
                
                contactData.Name = name;
                contactData.FullName = name;
                contactData.Email = email;
                contactData.Phonenumber = telephonenumber;

                var address = (person.Addresses != null && person.Addresses.FirstOrDefault() != null ? person.Addresses.FirstOrDefault() : null);

                if (address != null)
                {
                    contactData.StreetAddress = address.StreetAddress;
                    contactData.PostalCode = address.PostalCode;
                    contactData.PoBox = address.PoBox;
                    contactData.City = address.City;
                    contactData.Region = address.Region;
                    contactData.Country = address.Country;
                }

                if (person.Organizations != null && person.Organizations.FirstOrDefault() != null)
                {
                    var organization = person.Organizations.FirstOrDefault();

                    contactData.Company = organization.Name;
                    contactData.Function = organization.Title;
                }

                contactData.MakeReadOnly();

                return contactData;
            }
            return null;
        }
    }
}
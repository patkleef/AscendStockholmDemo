using System.Web.Mvc;
using Demo.Helpers;
using Demo.Models.Other;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell.Services.Rest;

namespace Demo.Business.Store
{
    [RestStore("contactstore")]
    public class ContactRestStore : RestControllerBase
    {
        private readonly IContentRepository _contentRepository;

        public ContactRestStore()
        {
            _contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
        }

        [HttpGet]
        public RestResult Get(int? id, string providerName)
        {
            var model = new ContactViewModel();

            if (id.HasValue)
            {
                ContentReference contentReference;
                if (!string.IsNullOrEmpty(providerName))
                {
                    contentReference = new ContentReference(id.Value, providerName);
                }
                else
                {
                    contentReference = new ContentReference(id.Value);
                }
                var contact = _contentRepository.Get<ContactData>(contentReference);

                model.FullName = contact.FullName;
                model.AddressText = contact.GetAddressText();
                model.StreetAddress = contact.StreetAddress;
                model.PostalCode = contact.PostalCode;
                model.PoBox = contact.PoBox;
                model.City = contact.City;
                model.Region = contact.Region;
                model.Country = contact.Country;
                model.Email = contact.Email;
                model.Phonenumber = contact.Phonenumber;
                model.Company = contact.Company;
                model.Function = contact.Function;
            }
            return base.Rest(model);
        }
    }
}
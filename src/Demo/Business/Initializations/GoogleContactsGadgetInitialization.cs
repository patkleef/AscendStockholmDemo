using System;
using System.Collections.Specialized;
using System.Linq;
using Demo.Business.GoogleIntegration;
using EPiServer.Configuration;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Security;
using EPiServer.ServiceLocation;

namespace Demo.Business.Initializations
{
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    internal class GoogleContactsGadgetInitialization : IInitializableModule
    {
        private ContentRootService _contentRootService;
        private IContentSecurityRepository _contentSecurityRepository;

        private const string GoogleContactsRootName = "Google contacts";
        private static readonly Guid GoogleContactsRootGuid = new Guid("{F551E3B6-36A0-49A3-B055-A2CA706C4117}");

        public static ContentReference GoogleContactsRoot;
        
        public void Initialize(InitializationEngine context)
        {
            _contentRootService = ServiceLocator.Current.GetInstance<ContentRootService>();
            _contentSecurityRepository = ServiceLocator.Current.GetInstance<IContentSecurityRepository>();

            InitializeGoogleContactsComponent(context);
        }
        
        public void Uninitialize(InitializationEngine context)
        {

        }

        private void InitializeGoogleContactsComponent(InitializationEngine context)
        {
            GoogleContactsRoot = CreateRootFolder(GoogleContactsRootName, GoogleContactsRootGuid);

            var providerValues = new NameValueCollection();
            providerValues.Add(ContentProviderElement.EntryPointString, GoogleContactsRoot.ToString());
            providerValues.Add(ContentProviderElement.CapabilitiesString, "Search");

            var googleContactsProvider = new GoogleContactsProvider();
            googleContactsProvider.Initialize(GoogleContactsProvider.Key, providerValues);

            var providerManager = context.Locate.Advanced.GetInstance<IContentProviderManager>();
            providerManager.ProviderMap.AddProvider(googleContactsProvider);
        }
        
        private ContentReference CreateRootFolder(string rootName, Guid rootGuid)
        {
            _contentRootService.Register<ContentFolder>(rootName, rootGuid, ContentReference.RootPage);

            var fieldRoot = _contentRootService.Get(rootName);

            var securityDescriptor = _contentSecurityRepository.Get(fieldRoot).CreateWritableClone() as IContentSecurityDescriptor;

            if (securityDescriptor != null)
            {
                securityDescriptor.IsInherited = false;

                var everyoneEntry = securityDescriptor.Entries.FirstOrDefault(e => e.Name.Equals("everyone", StringComparison.InvariantCultureIgnoreCase));

                if (everyoneEntry != null)
                {
                    securityDescriptor.RemoveEntry(everyoneEntry);
                    _contentSecurityRepository.Save(fieldRoot, securityDescriptor, SecuritySaveType.Replace);
                }
            }
            return fieldRoot;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Models.Other;
using EPiServer;
using EPiServer.Cms.Shell.Search;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework.Localization;
using EPiServer.Globalization;
using EPiServer.Search;
using EPiServer.ServiceLocation;
using EPiServer.Shell;
using EPiServer.Shell.Search;
using EPiServer.Web;
using EPiServer.Web.Routing;

namespace Demo.Business.Search
{
    [SearchProvider]
    public class ContactDataSearchProvider : EPiServerSearchProviderBase<ContactData, ContentType>
    {
        public ContactDataSearchProvider(LocalizationService localizationService, SiteDefinitionResolver siteDefinitionResolver, IContentTypeRepository<ContentType> contentTypeRepository, EditUrlResolver editUrlResolver, IContentRepository contentRepository, ILanguageBranchRepository languageBranchRepository, SearchHandler searchHandler, ContentSearchHandler contentSearchHandler, SearchIndexConfig searchIndexConfig, UIDescriptorRegistry uiDescriptorRegistry) : base(localizationService, siteDefinitionResolver, contentTypeRepository, editUrlResolver, contentRepository, languageBranchRepository, searchHandler, contentSearchHandler, searchIndexConfig, uiDescriptorRegistry)
        {
        }

        public ContactDataSearchProvider(LocalizationService localizationService, SiteDefinitionResolver siteDefinitionResolver, IContentTypeRepository<ContentType> contentTypeRepository, EditUrlResolver editUrlResolver, ServiceAccessor<SiteDefinition> currentSiteDefinition, IContentRepository contentRepository, ILanguageBranchRepository languageBranchRepository, SearchHandler searchHandler, ContentSearchHandler contentSearchHandler, SearchIndexConfig searchIndexConfig, UIDescriptorRegistry uiDescriptorRegistry, LanguageResolver languageResolver, UrlResolver urlResolver, TemplateResolver templateResolver) : base(localizationService, siteDefinitionResolver, contentTypeRepository, editUrlResolver, currentSiteDefinition, contentRepository, languageBranchRepository, searchHandler, contentSearchHandler, searchIndexConfig, uiDescriptorRegistry, languageResolver, urlResolver, templateResolver)
        {
        }

        public override string Area { get { return "CMS/contacts";  } }
        public override string Category { get { return base.LocalizationService.GetString(ContentSearchProviderConstants.BlockCategory); } }
        protected override string IconCssClass { get { return "epi-resourceIcon epi-resourceIcon-block";  } }
        
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public override IEnumerable<SearchResult> Search(Query query)
        {
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            
            var list = contentRepository.GetChildren<ContactData>(new ContentReference(query.SearchRoots.FirstOrDefault()));

            var results = list.Where(x => !string.IsNullOrEmpty(x.FullName) && x.FullName.IndexOf(query.SearchQuery, StringComparison.CurrentCultureIgnoreCase) != -1);

            return results.Select(CreateSearchResult);
        }
    }
}
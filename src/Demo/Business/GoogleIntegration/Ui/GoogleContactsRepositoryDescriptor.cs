using System;
using System.Collections.Generic;
using Demo.Business.Initializations;
using Demo.Models.Other;
using EPiServer.Cms.Shell.UI.CompositeViews;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell;

namespace Demo.Business.GoogleIntegration.Ui
{
    [ServiceConfiguration(typeof(IContentRepositoryDescriptor))]
    public class GoogleContactsRepositoryDescriptor : ContentRepositoryDescriptorBase
    {
        public static string RepositoryKey
        {
            get { return "google-contacts"; }
        }

        public override string Key
        {
            get
            {
                return RepositoryKey;
            }
        }

        public override string Name
        {
            get { return "GoogleContacts"; }
        }

        public override IEnumerable<Type> MainNavigationTypes
        {
            get
            {
                return new[]
                {
                    typeof(ContentFolder)
                };
            }
        }

        public override IEnumerable<Type> ContainedTypes
        {
            get
            {
                return new[]
                {
                    typeof(ContentFolder),
                    typeof(ContactData)
                };
            }
        }

        public override IEnumerable<string> MainViews {
            get
            {
                return new string[] { };
            }
        }

        public override IEnumerable<ContentReference> Roots
        {
            get
            {
                return new[] { GoogleContactsGadgetInitialization.GoogleContactsRoot };
            }
        }

        public override string SearchArea
        {
            get
            {
                return "CMS/contacts";
            }
        }
    }
}
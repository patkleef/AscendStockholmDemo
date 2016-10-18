using System;
using System.Collections.Generic;
using Demo.Business.Initializations;
using Demo.Models.Other;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell;

namespace Demo.Business.Ui
{
    [ServiceConfiguration(typeof(IContentRepositoryDescriptor))]
    public class ContactDataRepositoryDescriptor : ContentRepositoryDescriptorBase
    {
        public static string RepositoryKey
        {
            get { return "contacts"; }
        }
        
        public override string Key
        {
            get { return RepositoryKey; }
        }
        
        public override string Name
        {
            get { return "Contacts"; }
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
        
        public override IEnumerable<string> MainViews
        {
            get { return new string[] {}; }
        }
        
        public override IEnumerable<Type> CreatableTypes
        {
            get
            {
                return new System.Type[]
                {
                    typeof(ContactData)
                };
            }
        }
        
        public override IEnumerable<ContentReference> Roots
        {
            get { return new[] { ContactInitialization.ContactsRoot }; }
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
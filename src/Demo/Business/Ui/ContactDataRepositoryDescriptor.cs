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
        /// <summary>
        /// Set the repository key, used in the ChartsGadgetComponent class
        /// </summary>
        public static string RepositoryKey
        {
            get { return "contacts"; }
        }

        /// <summary>
        /// Key of this repository descriptor
        /// </summary>
        public override string Key
        {
            get { return RepositoryKey; }
        }

        /// <summary>
        /// Name of the repository
        /// </summary>
        public override string Name
        {
            get { return "Contacts"; }
        }

        /// <summary>
        /// Navigation type, only a content folder
        /// </summary>
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

        /// <summary>
        /// Both folders and of course ChartData types
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public override IEnumerable<string> MainViews
        {
            get { return new string[] {}; }
        }

        /// <summary>
        /// Which items can be created, ChartData
        /// </summary>
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

        /// <summary>
        /// Root of the gadget
        /// </summary>
        public override IEnumerable<ContentReference> Roots
        {
            get { return new[] { ContactInitialization.ChartsRoot }; }
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
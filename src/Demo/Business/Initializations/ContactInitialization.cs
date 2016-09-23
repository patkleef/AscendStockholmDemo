﻿using System;
using System.Linq;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using InitializationModule = EPiServer.Web.InitializationModule;

namespace Demo.Business.Initializations
{
    [ModuleDependency(typeof(InitializationModule))]
    public class ContactInitialization : IInitializableModule
    {
        public const string ContactsRootName = "Contacts";
        public static Guid ContactsRootGuid = new Guid("414E5F5A-CCEC-4C50-AE80-0CC0E1DC66FE");

        public static ContentReference ChartsRoot;

        /// <summary>
        /// Initialize method
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(InitializationEngine context)
        {
            var contentRootService = ServiceLocator.Current.GetInstance<ContentRootService>();
            var contentSecurityRepository = ServiceLocator.Current.GetInstance<IContentSecurityRepository>();

            contentRootService.Register<ContentFolder>(ContactsRootName, ContactsRootGuid, ContentReference.RootPage);

            ChartsRoot = contentRootService.Get(ContactsRootName);

            var securityDescriptor = contentSecurityRepository.Get(ChartsRoot).CreateWritableClone() as IContentSecurityDescriptor;

            if (securityDescriptor != null)
            {
                securityDescriptor.IsInherited = false;

                var everyoneEntry = securityDescriptor.Entries.FirstOrDefault(e => e.Name.Equals("everyone", StringComparison.InvariantCultureIgnoreCase));

                if (everyoneEntry != null)
                {
                    securityDescriptor.RemoveEntry(everyoneEntry);
                    contentSecurityRepository.Save(ChartsRoot, securityDescriptor, SecuritySaveType.Replace);
                }
            }
        }

        /// <summary>
        /// Uninitialize
        /// </summary>
        /// <param name="context"></param>
        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
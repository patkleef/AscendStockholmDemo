using Demo.Models.Other;
using EPiServer.ServiceLocation;
using EPiServer.Shell;

namespace Demo.Business.Ui
{
   [ServiceConfiguration(typeof(ViewConfiguration))]
    public class ContactViewConfiguration : ViewConfiguration<ContactData>
    {
        /// <summary>
        /// Public constructor
        /// Set path to the Dojo widget and some layout settings
        /// </summary>
        public ContactViewConfiguration()
        {
            SortOrder = 1;
            Key = "contactInformationContent";
            Name = "Contact information";
            Description = "Contactinformatoin";
            ControllerType = "app/editors/contactinformation";
            IconClass = "epi-iconLayout";
            HideFromViewMenu = false;
        }
    }
}
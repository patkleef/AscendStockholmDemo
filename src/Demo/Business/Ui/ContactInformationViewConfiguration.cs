using Demo.Models.Other;
using EPiServer.ServiceLocation;
using EPiServer.Shell;

namespace Demo.Business.Ui
{
   [ServiceConfiguration(typeof(ViewConfiguration))]
    public class ContactInformationViewConfiguration : ViewConfiguration<ContactData>
    {
        public ContactInformationViewConfiguration()
        {
            SortOrder = 1;
            Key = "contactInformation";
            Name = "Contact information";
            Description = "Contactinformation";
            ControllerType = "app/editors/contactinformation";
            IconClass = "epi-iconLayout";
            HideFromViewMenu = false;
        }
    }
}
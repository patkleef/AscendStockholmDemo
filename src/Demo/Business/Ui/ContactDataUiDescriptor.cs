using Demo.Models.Other;
using EPiServer.Shell;

namespace Demo.Business.Ui
{
    [UIDescriptorRegistration]
    public class ContactDataUiDescriptor : UIDescriptor<ContactData>
    {
        public ContactDataUiDescriptor()
        {
            DefaultView = "contactInformationContent";

            AddDisabledView(CmsViewNames.OnPageEditView);
        }
    }
}
using EPiServer.Shell;
using EPiServer.Shell.ViewComposition;

namespace Demo.Business.Ui
{
    [Component]
    public class ContactsGadget : ComponentDefinitionBase
    {
        public ContactsGadget() : base("epi-cms.widget.HierarchicalList")
        {
            Categories = new string[] { "content" };
            Title = "Contacts";
            Description = "Gadget for managing contacts";
            SortOrder = 1000;
            PlugInAreas = new[] { PlugInArea.Assets };
            Settings.Add(new Setting("repositoryKey", ContactDataRepositoryDescriptor.RepositoryKey));
        }
    }
}
using Demo.Business.GoogleIntegration.Ui;
using EPiServer.Shell;
using EPiServer.Shell.ViewComposition;

namespace Demo.Business.GoogleIntegration.Ui
{
    /// <summary>
    /// Gadget that displays all Google Contacts
    /// </summary>
    [Component]
    public class GoogleContactsGadget : ComponentDefinitionBase
    {
        /// <summary>
        /// Public constructor.
        /// Settings for widget
        /// </summary>
        public GoogleContactsGadget() : base("epi-cms/widget/HierarchicalList")
        {
            Categories = new string[] { "content" };
            Title = "[Google] Contacts";
            Description = "Gadget for managing Google contacts";
            SortOrder = 105;
            PlugInAreas = new string[] { PlugInArea.Assets  };
            Settings.Add(new Setting("repositoryKey", GoogleContactsRepositoryDescriptor.RepositoryKey));
        }
    }
}
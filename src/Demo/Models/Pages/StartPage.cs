using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace Demo.Models.Pages
{
    [ContentType(DisplayName = "Start page", GUID = "adad92d6-a105-4a08-81ff-9b0ebfb7a7c4", Description = "")]
    public class StartPage : PageData
    {
        [CultureSpecific]
        [Display(
            Name = "Contacts",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual ContentArea Contacts { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace Demo.Models.Other
{
    [ContentType(DisplayName = "Contact data", GUID = "{C1D8A647-6AF3-4D00-8E09-6A7BC4084F88}", Description = "")]
    public class ContactData : ContentBase
    {
        [Required]
        [Display(
            Name = "Name",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string FullName { get; set; }
        
        [Display(
            Name = "Street address",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        public virtual string StreetAddress { get; set; }

        [Display(
            Name = "Postal code",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 21)]
        public virtual string PostalCode { get; set; }

        [Display(
            Name = "Po Box",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 22)]
        public virtual string PoBox { get; set; }

        [Display(
            Name = "City",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 23)]
        public virtual string City { get; set; }

        [Display(
            Name = "Region",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 24)]
        public virtual string Region { get; set; }

        [Display(
            Name = "Country",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 25)]
        public virtual string Country { get; set; }

        [Required]
        [Display(
            Name = "Email",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 30)]
        public virtual string Email { get; set; }

        [Display(
            Name = "Phonenumber",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 40)]
        public virtual string Phonenumber { get; set; }

        [Display(
           Name = "Company",
           Description = "",
           GroupName = SystemTabNames.Content,
           Order = 50)]
        public virtual string Company { get; set; }

        [Display(
           Name = "Function",
           Description = "",
           GroupName = SystemTabNames.Content,
           Order = 60)]
        public virtual string Function { get; set; }
    }
}
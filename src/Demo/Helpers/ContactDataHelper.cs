using System.Collections.Generic;
using System.Linq;
using Demo.Models.Other;

namespace Demo.Helpers
{
    public static class ContactDataHelper
    {
        public static string GetAddressText(this ContactData contact)
        {
            var list = new List<string> { contact.StreetAddress, contact.PostalCode, contact.PoBox, contact.City, contact.Region, contact.Country }.Where(x => !string.IsNullOrEmpty(x));

            return string.Join(" ", list);
        }
    }
}
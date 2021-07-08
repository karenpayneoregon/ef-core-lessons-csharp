using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWindDataProviderLibrary.Models
{
    public class CustomerItem
    {
        public int CustomerIdentifier { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int ContactId { get; set; }
        public int CountryIdentifier { get; set; }
        public string Country { get; set; }
        public string CompanyPhone { get; set; }
        public int PhoneTypeIdentifier { get; set; }
        public string ContactPhone { get; set; }
        public DateTime ModifiedDate { get; set; }

        public override string ToString() => CompanyName;

    }
}

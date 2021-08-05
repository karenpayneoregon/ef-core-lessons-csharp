using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWindCoreLibrary.Models;

namespace NorthWindCoreLibrary.LanguageExtensions
{
    public static class QueryExtensions
    {

        /// <summary>
        /// Include <see cref="Countries"/> <see cref="Contacts"/> and <see cref="ContactDevices"/> navigations
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<Customers> IncludeContactsCountryDevices(this IQueryable<Customers> query) => query
            .Include(customer => customer.CountryIdentifierNavigation)
            .Include(customer => customer.Contact)
            .ThenInclude(contact => contact.ContactDevices);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWindCoreLibrary.Data;
using NorthWindCoreLibrary.Projections;

namespace NorthWindCoreLibrary.Classes
{
    public class ContactOperations
    {
        public static async Task<List<ContactItem>> GetContactsWithProjection()
        {
            List<ContactItem> contactList = new();
            
            await Task.Run(async () =>
            {
                await using var context = new NorthwindContext();
                contactList =  await context.Contacts.Select(ContactItem.Projection).ToListAsync();
            });

            return contactList;
        }

        public static async Task<List<IGrouping<int?, ContactItem>>> ContactsGroupedByTitleAsync()
        {
            List<ContactItem> contactList = new();
            await Task.Run(async () =>
            {
                contactList = await GetContactsWithProjection();
            });

            /*
             * Alternate via LINQ, just add a order by
             *
             * (from c in contacts group c by c.ContactTypeIdentifier into items select items).ToList();
             *
             */
            return contactList
                .GroupBy(contactItem => contactItem.ContactTypeIdentifier)
                .Select(grouped => grouped)
                .OrderBy(contactItem => contactItem.FirstOrDefault().ContactTitle)
                .ToList();

        }
    }
}

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWindCoreLibrary.Data;
using NorthWindCoreLibrary.Models;

namespace SaveChangesInterceptor.Classes
{
    /// <summary>
    /// Methods to interact with the SQL-Server database NorthWindAzureForInserts
    /// </summary>
    public class NorthWindOperations
    {
        /// <summary>
        /// Get an <see cref="Employees"/> by an existing primary key
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns>Employee for identifier</returns>
        public static async Task<Employees> ReadEmployee(int identifier)
        {
            await using var context = new NorthwindContext();

            return await Task.Run(() =>
                context.Employees.FirstOrDefaultAsync(emp => emp.EmployeeId == identifier));
        }

        public static void Test()
        {
            using var context = new NorthwindContext();
            var emp = context.Employees.FirstOrDefault(emp => emp.EmployeeId == 7);
            emp.FirstName = "Robert1";
            context.Entry(emp).State = EntityState.Modified;
            Debug.WriteLine(message: context.ChangeTracker.DebugView.LongView);

            context.SaveChanges();


        }

        /// <summary>
        /// Read Employee current values from the database
        /// </summary>
        /// <param name="identifier">Employee key to return data for</param>
        /// <returns>Employee for identifier</returns>
        public static Employees OriginalEmployee(int identifier)
        {
            var context = new NorthwindContext();

            /*
             * AsNoTracking indicates that the ChangeTracker is not tracking the entity
             */
            return context.Employees.AsNoTracking().FirstOrDefault(employee => employee.EmployeeId == identifier);
        }

        /// <summary>
        /// Save a single <see cref="Employees"/>
        /// </summary>
        /// <param name="employee"><see cref="Employees"/></param>
        /// <returns>1 for success, other values failure</returns>
        public static bool SaveEmployee(Employees employee)
        {
            /*
             * Connect to database
             */
            using var context = new NorthwindContext();
            context.SavedChanges += ContextOnSavedChanges;
            context.SaveChangesFailed += ContextOnSaveChangesFailed;

            /*
             * Tell Entity Framework we are saving changes to an existing record,
             */
            context.Entry(employee).State = EntityState.Modified;

            /*
             * SaveChanges returns count of changes e.g. one record = 1, two records = 2 etc.
             * While 0 means nothing updated.
             *
             * Contrary to the above the ChangeTracker for EF Core will return 1 even if no
             * properties changed.
             */
            return context.SaveChanges() == 1;
        }

        private static void ContextOnSaveChangesFailed(object? sender, SaveChangesFailedEventArgs saveChangesFailedEvent)
        {
            Debug.WriteLine(saveChangesFailedEvent.Exception.Message);
        }

        private static void ContextOnSavedChanges(object? sender, SavedChangesEventArgs savedChangesEvent)
        {
            
            Debug.WriteLine(((NorthwindContext)sender).ChangeTracker.DebugView.LongView);
        }
    }
}

using NorthWindCoreLibrary.Models;

namespace NorthWindCoreUnitTest.Classes
{
    public class EmpItem
    {
        public int Id { get; set; }
        public int Count { get; set; }  
        public Employees Employee { get; set; }
    }
}
using System;
using System.IO;

namespace WhereIsDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Helper.DotNetIsInstalled() ? "Yes" : "No");
            Console.ReadLine();
        }
    }

    class Helper
    {
        public static bool DotNetIsInstalled() => File.Exists("C:\\Program Files\\dotnet\\dotnet.exe");
    }
    
    
}

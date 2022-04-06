using RegexReplacer.Shared;
using RegexReplacer.Shared.DisplayHelper;
using System.Reflection;

namespace RegexReplacer.FormsApp
{
    internal static class Program
    {
        

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FormRegexReplacer());
        }
    }
}
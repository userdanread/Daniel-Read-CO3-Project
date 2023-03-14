using System;
using System.Windows.Forms;


namespace Daniel_Read_CO3_Project
{
    internal static class Program
    {
        public static object ApplicationConfiguration { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.Run(new formStart());
        }
    }
}
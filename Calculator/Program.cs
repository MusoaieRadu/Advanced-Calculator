using System;
using System.Windows.Forms;

namespace Calculator
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static MainWindow _main;
        public static MainWindow MainWindow { get { return _main; } }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _main = new MainWindow();
            Application.Run(_main);
        }
    }
}

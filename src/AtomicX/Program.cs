﻿using AtomicX.Forms;
using DarkUI.Win32;
using log4net;
using System;
using System.Threading;
using System.Windows.Forms;

namespace AtomicX
{
    static class Program
    {
        public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // Initialize log4net.
            log4net.Config.XmlConfigurator.Configure();
            Log.Info("Configured logging settings");

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.AddMessageFilter(new ControlScrollFilter());
            Application.Run(new MainForm());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MainForm.mainForm.SetStatus(string.Format("An unknown error occurred : {0} - See log file for more details", e.Exception.Message), e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MainForm.mainForm.SetStatus(string.Format("An unknown error occurred : {0} - See log file for more details", ((Exception)e.ExceptionObject).Message), (Exception)e.ExceptionObject);
        }
    }
}

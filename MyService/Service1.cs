using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;
using System.IO;
using System.Net;

namespace MyService
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        private string dataFolder;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // App.config에 DataFolder를 AppSettings 밑에 지정
            dataFolder = ConfigurationManager.AppSettings["DataFolder"];

            timer = new Timer();
//            timer.Interval = 5 * 60 * 1000;
            timer.Interval = 3000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string outputFile = Path.Combine(dataFolder, "csharp-news.html");

            WebClient cli = new WebClient();
            cli.DownloadFile("http://csharp.news", outputFile);
        }

        protected override void OnStop()
        {
        }
    }
}

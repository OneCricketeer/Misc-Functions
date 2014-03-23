 using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace ConsoleApplications
{
    internal class ExternalIPLogFile
	{
 
		 #region LogFile
        public void fileWriter()
        {
            StreamWriter log;
            String year = DateTime.Now.Year.ToString();

            // See if the log file for the year exists
            if (!File.Exists(year + "_access.log"))
            {
                log = new StreamWriter(year + "_access.log");
                log.WriteLine("[" + year + " LOG]");
            }
            else
            {
                log = File.AppendText(year + "_access.log");
            }

            // Write to the file:
            log.WriteLine(DateTime.Now);

            // Close the stream:
            log.Close();
        }

        public void fileReader()
        {
            // Uses years to sort access logs
            String year = DateTime.Now.Year.ToString();
            string time;
            DateTime lastAccess = DateTime.Now;

            if (File.Exists(year + "_access.log"))
            {
                StreamReader file = new StreamReader(year + "_access.log");

                // Reads the last time the IP Address was attempted to be received
                while ((time = file.ReadLine()) != null)
                {
                    if (time.EndsWith("AM") || time.EndsWith("PM"))
                        lastAccess = Convert.ToDateTime(time);
                }
                file.Close();
            }

            TimeSpan span = DateTime.Now - lastAccess;

            // Tests if more than 5 minutes between IP lookup has elapsed because whatismyip.org complains about it
            // I don't think an IP Address will change within 2 weeks
            if (span.TotalDays > 14 || !File.Exists("externalIP"))
            {
                Console.WriteLine("More than 2 weeks has elapsed");
                String extIP =  getExternalIP().ToString();
                Console.WriteLine("The external IP is {0}", extIP);
                // Writes the external IP to a file for easy access
                if (!File.Exists("externalIP"))
                {
                    File.WriteAllText("externalIP", extIP);
                }
            }
            else
            {
                // Reads the external IP from a file if it exists
                if (File.Exists("externalIP"))
                {
                    StreamReader reader = File.OpenText("externalIP");
                    IPAddress ip = new IPAddress(0); // Initialize with a fake value
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        IPAddress.TryParse(line, out ip);
                    }
                    Console.WriteLine("The external IP is {0}", ip);
                    reader.Close();
                }
            }
        }

        // Gets the external IP from whatismyip.org
        private IPAddress getExternalIP()
        {
            string whatIsMyIp = "http://automation.whatismyip.com/n09230945.asp";
            WebClient client = new WebClient();

            // No idea what this is, but it said it needed it
            client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:12.0) Gecko/20100101 Firefox/12.0");
            UTF8Encoding utf8 = new UTF8Encoding();
            string requestHtml = "";

            try
            {
                requestHtml = utf8.GetString(client.DownloadData(whatIsMyIp));
            }
            catch (WebException e)
            {
                Console.WriteLine(e.ToString());
            }

            IPAddress externalIP = IPAddress.Parse(requestHtml);
            return externalIP;
        }
	}
}

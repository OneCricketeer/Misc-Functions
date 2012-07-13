using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleApplications
{
    internal class Functions
    {
        // Fields
        private int points = 0;

        // Returns the sum of the highest 3 integers with the least number of "points" 
        // Points are accumulated through comparisons and addition
        public int sumOfHighestTwoSolution(int a, int b, int c)
        {
            if (a > b)
            {
                this.points++;
                if (b > c)
                {
                    this.points += 2;
                    return b + a;
                }
                else
                {
                    this.points += 2;
                    return c + a;
                }
            }
            else
            {
                this.points++;
                if (a > c)
                {
                    this.points += 2;
                    return a + b;
                }
                else
                {
                    this.points += 2;
                    return c + b;
                }
            }
        }

        // Makes the computer squeal :O
        public void mosquito()
        {
            // Range = 37 to 32767
            int i = 37;
            while (i < 32767)
            {
                Console.Beep(i, 500);
                i++;
            }
        }

        // Returns the numeral value of a roman numeral
        public int RomanToInt(char r)
        {
            switch (r)
            {
                case 'i':
                case 'I':
                    return 1;
                case 'v':
                case 'V':
                    return 5;
                case 'x':
                case 'X':
                    return 10;
                case 'l':
                case 'L':
                    return 50;
                case 'c':
                case 'C':
                    return 100;
                case 'd':
                case 'D':
                    return 500;
                case 'm':
                case 'M':
                    return 1000;
                default:
                    throw new InvalidOperationException("Unknown Roman Numeral");
            }
        }

        // Works correctly, even though invalid roman numerals can be passed
        //- TODO: Fix invalid problem with a regex (?) to check valid roman numerals
        public int RomanValue(string roman)
        {
            int lastnumber = 0, value = 0, num = 0;
            foreach (char c in roman)
            {
                num = RomanToInt(c);
                value += lastnumber < num ? num - 2 * lastnumber : num;
                lastnumber = num;
            }
            return value;
        }
        #region LogFile
        public void fileWriter()
        {
            StreamWriter log;
            String year = DateTime.Now.Year.ToString();

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

            // There probably is a better way to do this, but whatever
            TimeSpan span = new TimeSpan(
                DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).Subtract(
                new TimeSpan(lastAccess.Day, lastAccess.Hour, lastAccess.Minute, lastAccess.Second));

            // Tests if more than 5 minutes between IP lookup has elapsed because whatismyip.org complains about it
            // I don't think an IP Address will change within 2 weeks
            if (span.Days > 14)
            {
                Console.WriteLine("More than 2 weeks has elapsed");
                Console.WriteLine("The external IP is {0}", getExternalIP());
                // Writes the external IP to a file for easy access
                if (!File.Exists("externalIP"))
                {
                    File.WriteAllText("externalIP", getExternalIP().ToString());
                }
            }
            else
            {
                // Reads the external IP from a file if it exists
                if (File.Exists("externalIP"))
                {
                    StreamReader reader = File.OpenText("externalIP");
                    IPAddress ip;
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        IPAddress.TryParse(line, out ip);
                    }
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
        #endregion LogFile
    }
}
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace ConsoleApplications
{
    internal class Functions
    {
        // Fields
        private int points = 0;
        private ArrayList memo = new ArrayList();

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

       

        public void swap(int a, int b)
        {
            int temp;
            temp = a;
            a = b;
            b = temp;
        }
        public String arraylistToString(ArrayList arr)
        {
            if (arr.Count == 0)
                return "{}";
            StringBuilder sb = new StringBuilder("{");
            foreach (var item in arr)
            {
                sb.AppendFormat("{0}, ", item);
            }

            sb.Remove(sb.Length - 2, 2);
            sb.Append("}");
            return sb.ToString();
        }
        
        #region Prime Factorization
        public class Factor
        {
            public int num = 0;
            public int exp;
            public Factor(int num)
            {
                this.num = num;
                this.exp = 1;
            }

            public override bool Equals(object obj)
            {
                Factor f = (Factor)obj;
                return f.num == this.num;
            }
            public override string ToString()
            {
                return exp > 1 ? String.Format("{0}^{1}", num, exp) : "" + num;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
            #region Operators
            public static bool operator ==(Factor f1, Factor f2)
            {
                return f1.num == f2.num;
            }
            public static bool operator !=(Factor f1, Factor f2)
            {
                return !(f1 == f2);
            }
            public static bool operator ==(Factor f, int c)
            {
                return f.num == c;
            }
            public static bool operator !=(Factor f, int c)
            {
                return f.num != c;
            }
            public static bool operator ==(int c, Factor f)
            {
                return f == c;
            }
            public static bool operator !=(int c, Factor f)
            {
                return f != c;
            }
            #endregion

        }
        public ArrayList primeFactors(long n)
        {
            ArrayList factors = new ArrayList();
            long num = n;

            if ((int)num == 0 || (int)num == 1)
                return factors;
            else if ((int)num == 2 || (int)num == 3)
            {
                return factors;
            }
            
            int lastfactor = 1;
            while (n % 2 == 0)
            {
                n /= 2;
                lastfactor = 2;
                factors.Add(lastfactor);
            }
            int factor = 3;
            double maxFactor = Math.Sqrt(n);
            while (n > 1)
            {
                if ((long)factor >= num)
                {
                    factors.Clear();
                    return factors;
                }
                    
                //return num + " is prime";
                if (n % factor == 0)
                {
                    n /= factor;
                    factors.Add(factor);
                    lastfactor = factor;
                    while (n % factor == 0)
                    {
                        n /= factor;
                        factors.Add(factor);
                    }
                    maxFactor = Math.Sqrt(n);
                }
                factor += 2;
            }

            return factors;

            // Console.Write("{0} = ", arraylistToString(factors));


        }
        public String factorsToString(ArrayList factors)
        {
            if (factors.Count == 0)
                return "Prime";
            StringBuilder sb2 = new StringBuilder("{");

            // A loop that stops on an element and slides on that until a new element is seen
            // Assumption: the list is sorted
            int start = 0;
            int end = start + 1;
            Factor curr = new Factor((int)factors[start]);
            Factor next = new Factor((int)factors[end]);

        nextnum:
            while (curr == next && next != 0)
            {
                curr.exp++;
                end++;
                try
                {
                    next = new Factor((int)factors[end]);
                }
                catch
                {
                    next = new Factor(0);
                }
            }

            sb2.AppendFormat("{0}, ", curr);

            try
            {
                start += curr.exp;
                end = start + 1;
                curr = new Factor((int)factors[start]);
                next = new Factor((int)factors[end]);
                goto nextnum;
            }
            catch
            {
                sb2.AppendFormat(next == 0 ? "" : "{0}, ", next);
                // Remove the additional comma and return
                if (sb2.Length >= 2)
                    sb2.Remove(sb2.Length - 2, 2);
                sb2.Append("}");
                return sb2.ToString();
            }
        }
        #endregion

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
        #endregion LogFile
    }
}

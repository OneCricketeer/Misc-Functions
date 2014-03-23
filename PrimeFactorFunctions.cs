 using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace ConsoleApplications
{
    internal class PrimeFactorFunctions
	{
 
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
        public ArrayList getPrimeFactors(long n)
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
	}
}

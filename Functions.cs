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

       
    }
}

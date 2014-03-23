 using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace ConsoleApplications
{
    internal class RomanNumeralConverter
	{
 
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
	}
}

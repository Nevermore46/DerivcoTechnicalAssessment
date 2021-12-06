using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DerivcoTA
{
    class Program
    {

        static void Main(string[] args)
        {

            String Name1;

            String Name2;

            String Match, Result = "", Result2 = "";
            int count = 0;
            bool DupeCheck = false;
            var Duplicate = new List<char>();

            Console.WriteLine("Enter the first name");
            Name1 = Console.ReadLine();

            if (string.IsNullOrEmpty(Name1))
            {
                Console.WriteLine("Name cannot be empty, please try again");
                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                Environment.Exit(0);
            }

            if (Regex.IsMatch(Name1, @"^[a-zA-Z]+$") == false)
            {
                Console.WriteLine("Please enter alphabetic characters only");
                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                Environment.Exit(0);
            }

            Console.WriteLine("Enter the second name");
            Name2 = Console.ReadLine();

            if (string.IsNullOrEmpty(Name2))
            {
                Console.WriteLine("Name cannot be empty, please try again");
                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                Environment.Exit(0);
            }

            if (Regex.IsMatch(Name2, @"^[a-zA-Z]+$") == false)
            {
                Console.WriteLine("Please enter alphabetic characters only");
                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                Environment.Exit(0);
            }


            Match = Name1 + "matches" + Name2;

            for (int i = 0; i < (Match.Length); i++)
            {
                DupeCheck = false;
                count = 0;

                for (int d = 0; d < (Duplicate.Count); d++)
                {
                    if (Match[i] == Duplicate[d])
                    {
                        DupeCheck = true;
                    }
                }

                if (DupeCheck == false)
                {
                    for (int x = 0; x < (Match.Length); x++)
                    {
                        if (Match[i] == Match[x])
                        {
                            count += 1;
                            Duplicate.Add(Match[x]);

                        }
                    }
                    Result += count.ToString();
                }
            }

            do
            {
                int count2 = 0, Len = Result.Length;
                if ((Len) % 2 != 0)
                {
                    count2 = 1;
                }
                for (int z = 0; z < ((Len - count2) / 2) + count2; z++)
                {

                    if ((count2 == 1) && (z == ((Len - count2) / 2)))
                    {
                        Result2 += Result[z];
                    }
                    else
                    {
                        Result2 += (Char.GetNumericValue(Result[z]) + Char.GetNumericValue(Result[Len - z - 1]));
                    }
                }
                Result = Result2;
                Result2 = "";

            }
            while (Result.Length != 2);


            if ((Int32.Parse(Result)) > 80)
            {
                Console.WriteLine(Name1 + " matches " + Name2 + " " + Result + "%, good match");
            }
            else
            {
                Console.WriteLine(Name1 + " matches " + Name2 + " " + Result + "%");
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}

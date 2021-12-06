using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CSV
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ReadCSVFile();
            Console.ReadLine();

        }

        private static void ReadCSVFile()
        {
            var Male = new List<string>();
            var Female = new List<string>();
            var Male2 = new List<string>();
            var Female2 = new List<string>();
            var BadExecutionDates = new List<string>();
            var GoodExecutionDates = new List<string>();
            var MaleNameDupesRemoved = new List<string>();
            var FemaleNameDupesRemoved = new List<string>();
            string fileName;
            string SingleLine; 
            string Name="";
            OpenFileDialog fd = new OpenFileDialog();
            fd.ShowDialog();
            fileName = fd.FileName;

            var lines = File.ReadAllLines(fileName);

            for(int i = 0; i<lines.Length; i++)
            {
                SingleLine = lines[i];
                int index1 = SingleLine.IndexOf(',');
                Name = "";

                for (int j = 0; j < index1; j++)
                {
                    Name += SingleLine[j];
                }

                char c = SingleLine.Last();

                if (c == 'f')
                {                    
                    Female.Add(Name);
                }
                else if (c == 'm')
                {
                    Male.Add(Name);
                }
                else
                {
                    BadExecutionDates.Add(DateTime.Now.ToString());                  
                    File.WriteAllLines("/Finall/CVS_Input_Final/BadExecutionTimes.txt",BadExecutionDates);
                    Console.WriteLine("Please enter a valid csv file that also has the correct format- the Name followed by a comma, then their gender using an f or m -->  _Name_ , f/m");
                    System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                    Console.ReadLine();
                }
            }

            foreach (string name in Male.Distinct())
            {
                Male2.Add(name);
            }

            foreach (string name in Female.Distinct())
            {
                Female2.Add(name);
            }

            MaleNameDupesRemoved.Add( (Male.Count  -Male2.Count).ToString());
            FemaleNameDupesRemoved.Add((Female.Count - Female2.Count).ToString());
            File.WriteAllLines("/Finall/CVS_Input_Final/MaleDupesRemoved.txt", MaleNameDupesRemoved);
            File.WriteAllLines("/Finall/CVS_Input_Final/FemaleDupesRemoved.txt", FemaleNameDupesRemoved);
            var Duplicate = new List<char>();
            string Result = "", Result2 = "";
            var Final = new List<string>();


            for (int o=0;o < Male2.Count;o++)
            {
                
                for(int p=0;p < Female2.Count;p++)
                {
                    Duplicate.Clear();
                    String Match = Male2[o] + "matches" + Female2[p];

                    for (int i = 0; i < (Match.Length); i++)
                    {
                        bool DupeCheck = false;
                        int count = 0;

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


                    Final.Add(Male2[o] + " matches " + Female2[p] + " " + Result);
                    

                }
            }

            string num1 = "";
            string num2 = "";
            string NameHold1 = "";
            string NameHold2 = "";
            int index = 0;
            string holder = "";

            Final.Sort();

                for (int i=0;i<Final.Count;i++)
            {              
                index = i;
                for (int j=0;j<Final.Count;j++)
                {
                    num1 = Final[i].Substring(Final[i].Length - 2);
                    num2 = Final[j].Substring(Final[j].Length - 2);

                    NameHold1 = Final[i];
                    NameHold2 = Final[j];
                    if (Int32.Parse(num2)<Int32.Parse(num1))
                    {
                        holder = Final[index];
                        Final[index] = Final[j];
                        Final[j] = holder;
                        
                    }
                    else if ( (Int32.Parse(num2) == Int32.Parse(num1)) && (NameHold1[0].CompareTo(NameHold2[0])<0 ))
                    {
                        holder = Final[index];
                        Final[index] = Final[j];
                        Final[j] = holder;
                    }

                }

            }





                for (int z = 0; z < Final.Count; z++)
                {
                    if ((Int32.Parse(Result)) > 80)
                    {
                        Final[z] += "%, good match";
                        Console.WriteLine(Final[z]);
                    
                    
                    }
                    else
                    {
                        Final[z] += "%";
                        Console.WriteLine(Final[z]);
                    }
                }
            GoodExecutionDates.Add(DateTime.Now.ToString());
            File.WriteAllLines("/Finall/CVS_Input_Final/GoodExecution.txt", GoodExecutionDates);
            File.WriteAllLines("/Finall/CVS_Input_Final/output.txt", Final);
            Console.WriteLine("This data has also been added to the text file labelled - Output.txt");
            Console.ReadKey();
            
        }
        


    }
}

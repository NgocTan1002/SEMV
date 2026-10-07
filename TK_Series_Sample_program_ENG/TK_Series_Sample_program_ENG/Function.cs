using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Collections;
using System.Threading;

namespace TK_Series_Sample_program_ENG
{
    class Function
    {

        public string function_num;
        public string Func01_sel;
        public string Func02_sel;
        public string Func03_sel;
        public string Func04_sel;
        public string Func05_sel;
        public string Func06_sel;
        public string Func06_sel_sv;
        public string Func16_sel;
        public string Stop_serial;

        SerialCommunication GoSerial = new SerialCommunication();

        //----------------------------------------------
        //Function Select
        //----------------------------------------------
        public void Select()
        {
            function_num = null;
            Func01_sel = null;
            Func02_sel = null;
            Func03_sel = null;
            Func04_sel = null;
            Func05_sel = null;
            Func06_sel = null;
            Func06_sel_sv = null;
            Func16_sel = null;
            Stop_serial = null;
            

            Console.WriteLine("");
            Console.WriteLine("[Available function]");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("<Func01>");
            Console.WriteLine("1. RUN/STOP Check");
            Console.WriteLine("2. Auto tuning status check");
            Console.WriteLine("");
            Console.WriteLine("<Func04>");
            Console.WriteLine("1. Model name check");
            Console.WriteLine("2. PV(Present value) read");
            Console.WriteLine("3. SV(Setting value) read");
            Console.WriteLine("4. Out1 LED Check");
            Console.WriteLine("5. AL1 LED Check");
            Console.WriteLine("");
            Console.WriteLine("<Func05>");
            Console.WriteLine("1. Control out RUN");
            Console.WriteLine("2. Control out STOP");
            Console.WriteLine("3. Auto tuning Start");
            Console.WriteLine("4. Auto tuning Stop");
            Console.WriteLine("");
            Console.WriteLine("<Func06>");
            Console.WriteLine("1. SV(Setting value) Write");
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Enter 'X' or 'x', if you would like to dispose the connection.");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Enter the Function you would like to use.");
            Console.Write("Func : ");
            function_num = Console.ReadLine();

            while (true)
            {
                if (function_num == "1")
                {
                    Func01();
                    break;
                }
                else if (function_num == "2")
                {
                    Func02();
                    break;
                }
                else if (function_num == "3")
                {
                    Func03();
                    break;
                }

                else if (function_num == "4")
                {
                    Func04();
                    break;
                }
                else if (function_num == "5")
                {
                    Func05();
                    break;
                }
                else if (function_num == "6")
                {
                    Func06();
                    break;
                }
                else if (function_num == "16")
                {
                    Func16();
                    break;
                }
                else if(function_num =="X" || function_num =="x")
                {
                    StopSerial();
                    break;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("--------------------");
                    Console.WriteLine("<Choose Again>");
                    Console.Write("Func : ");
                    function_num = Console.ReadLine();
                    continue;
                }

            }

        }

        //----------------------------------------------
        //Function 01
        //----------------------------------------------
        
        public void Func01()
        {
            Console.WriteLine("");
            Console.WriteLine("----------Func01 Start----------");
            Console.WriteLine("Select a register you wouldl like to execute.");
            Console.WriteLine("1. RUN/STOP Check");
            Console.WriteLine("2. Auto tuning status check");
            Console.Write("Enter the number : ");
            Func01_sel = Console.ReadLine();
            Console.WriteLine("");

            while (true)
            {
                if (Func01_sel == "1")
                {
                    break;
                }
                else if (Func01_sel == "2")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("----------Func01 Start----------");
                    Console.WriteLine("Select a register you wouldl like to execute.");
                    Console.WriteLine("1. RUN/STOP Check");
                    Console.WriteLine("2. Auto tuning status check");
                    Console.Write("Enter the number : ");
                    Func01_sel = Console.ReadLine();
                    continue;
                }
            }
        }

        //----------------------------------------------
        //Function 02
        //----------------------------------------------

        public void Func02()
        {
            Console.WriteLine("");
            Console.WriteLine("----------Func02 Start----------");
            Console.WriteLine("Under development.");
            Console.WriteLine("Available Function : Func01 / Func04 / Func05 / Func06");
            Console.WriteLine("");

        }

        //----------------------------------------------
        //Function 03
        //----------------------------------------------

        public void Func03()
        {
            Console.WriteLine("");
            Console.WriteLine("----------Func03 Start----------");
            Console.WriteLine("Under development.");
            Console.WriteLine("Available Function : Func01 / Func04 / Func05 / Func06");
            Console.WriteLine("");
        }

        //----------------------------------------------
        //Function 04
        //----------------------------------------------

        public void Func04()
        {

            Console.WriteLine("");
            Console.WriteLine("----------Func04 Start----------");
            Console.WriteLine("");
            Console.WriteLine("Select a register you wouldl like to execute.");
            Console.WriteLine("1. Model name check");
            Console.WriteLine("2. PV(Present value) read");
            Console.WriteLine("3. SV(Setting value) read");
            Console.WriteLine("4. Out1 LED Check");
            Console.WriteLine("5. AL1 LED Check");
            Console.Write("Enter the number : ");
            Func04_sel = Console.ReadLine();

            while (true)
            {
                if (Func04_sel == "1")
                {
                    break;
                }
                else if (Func04_sel == "2")
                {

                    break;

                }
                else if (Func04_sel == "3")
                {
                    break;
                }

                else if (Func04_sel == "4")
                {
                    break;
                }
                else if (Func04_sel == "5")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("--------------------");
                    Console.WriteLine("<Choose Again>");
                    Console.WriteLine("Select a register you wouldl like to execute.");
                    Console.WriteLine("1. Model name check");
                    Console.WriteLine("2. PV(Present value) read");
                    Console.WriteLine("3. SV(Setting value) read");
                    Console.WriteLine("4. Out1 LED Check");
                    Console.WriteLine("5. AL1 LED Check");
                    Console.Write("Enter the number : ");
                    Func04_sel = Console.ReadLine();
                    continue;
                }

            }

        }

        //----------------------------------------------
        //Function 05
        //----------------------------------------------
        public void Func05()
        {
            Console.WriteLine("");
            Console.WriteLine("----------Func05 Start----------");
            Console.WriteLine("Select a register you wouldl like to execute.");
            Console.WriteLine("1. Control out RUN");
            Console.WriteLine("2. Control out STOP");
            Console.WriteLine("3. Auto tuning Start");
            Console.WriteLine("4. Auto tuning Stop");
            Console.Write("Enter the number : ");
            Func05_sel = Console.ReadLine();

            while (true)
            {
                if (Func05_sel == "1")
                {
                    break;
                }
                else if (Func05_sel == "2")
                {
                    break;
                }
                else if (Func05_sel == "3")
                {
                    break;
                }
                else if (Func05_sel == "4")
                {
                    break;
                }

                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("----------Func05 Start----------");
                    Console.WriteLine("Select a register you wouldl like to execute.");
                    Console.WriteLine("1. Control out RUN");
                    Console.WriteLine("2. Control out STOP");
                    Console.WriteLine("3. Auto tuning Start");
                    Console.WriteLine("4. Auto tuning Stop");
                    Console.Write("Enter the number : ");
                    Func05_sel = Console.ReadLine();
                    continue;
                }

            }

        }

        //----------------------------------------------
        //Function 06
        //----------------------------------------------
        public void Func06()
        {
            Console.WriteLine("");
            Console.WriteLine("----------Func06 Start----------");
            Console.WriteLine("Select a register you wouldl like to execute.");
            Console.WriteLine("1. SV(Setting value) Write");
            Console.Write("Enter the number : ");
            Func06_sel = Console.ReadLine();

            while(true)
            {
                if (Func06_sel == "1")
                {                    
                    Console.WriteLine("----------Select the setting value----------");
                    Console.WriteLine("1. 0");
                    Console.WriteLine("2. 50");
                    Console.WriteLine("3. 100");
                    Console.WriteLine("4. User value");
                    Console.Write("Enter the number : ");
                    Func06_sel_sv = Console.ReadLine();
                    while (true)
                    {
                        if (Func06_sel_sv == "1")
                        {
                            break;
                        }
                        else if(Func06_sel_sv == "2")
                        {
                            break;
                        }
                        else if (Func06_sel_sv == "3")
                        {
                            break;
                        }
                        else if (Func06_sel_sv == "4")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("----------Select the setting value----------");
                            Console.WriteLine("1. 0");
                            Console.WriteLine("2. 50");
                            Console.WriteLine("3. 100");
                            Console.WriteLine("4. User value");
                            Console.Write("Enter the number : ");
                            Func06_sel_sv = Console.ReadLine();
                            continue;
                        }

                    }
                    break;
                    

                }

                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("----------Func06 Start----------");
                    Console.WriteLine("Select a register you wouldl like to execute.");
                    Console.WriteLine("1. SV(Setting value) Write");
                    Console.Write("Enter the number : ");
                    Func06_sel = Console.ReadLine();
                    continue;
                }
            }
                
        }

        //----------------------------------------------
        //Function 16
        //----------------------------------------------
        public void Func16()
        {
            Console.WriteLine("");
            Console.WriteLine("----------Func16 Start----------");
            Console.WriteLine("Under development.");
            Console.WriteLine("Available Function : Func01 / Func04 / Func05 / Func06");
            Console.WriteLine("");
        }

        //----------------------------------------------
        //PORT disposing
        //----------------------------------------------
        public void StopSerial()
        {
            Stop_serial = "1";
            
        }

    }
}

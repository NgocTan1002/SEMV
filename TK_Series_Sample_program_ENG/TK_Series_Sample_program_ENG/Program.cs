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
    class MainProgram
    {
        static void Main(string[] args)
        {
            Serial_setting Setting_part = new Serial_setting();
            SerialCommunication GoSerial = new SerialCommunication();
            Function Functioncs = new Function();
            

            //--------------------Communication setting
            string ComPort = Setting_part.Comportset();
            string Baud = Setting_part.Baudset();
            string Stop = Setting_part.Stopset();
            string Parity = Setting_part.Parityset();
            Console.WriteLine(GoSerial.Comport_Open(ComPort, Baud, "8", Parity, Stop));
            //--------------------Communication setting

            //--------------------Display model name
            byte[] Query_con_m = new byte[8] { 0x01, 0x04, 0x00, 0x68, 0x00, 0x05, 0xB1, 0xD5 };
            GoSerial.SendSerialComm(Query_con_m, Query_con_m.Length);
            GoSerial.RcvSerialComm();
            string Bytecnt_dec_m = Convert.ToInt32(GoSerial.Response[2]).ToString();
            int Bytecnt_num_m = int.Parse(Bytecnt_dec_m);
            byte[] Model_arr_m = new byte[Bytecnt_num_m];
            Array.Copy(GoSerial.Response,3, Model_arr_m, 0, Bytecnt_num_m);
            string Model_name = System.Text.Encoding.ASCII.GetString(Model_arr_m);
            Console.WriteLine("Model name : " + Model_name);
            //--------------------Display model name

            //--------------------Display Present Value
            byte[] Query_con_p = new byte[8] { 0x01, 0x04, 0x03, 0xE8, 0x00, 0x01, 0xB1, 0xBA };
            GoSerial.SendSerialComm(Query_con_p, Query_con_p.Length);
            GoSerial.RcvSerialComm();
            string Bytecnt_dec_p = Convert.ToInt32(GoSerial.Response[2]).ToString();
            int Bytecnt_num_p = int.Parse(Bytecnt_dec_p);
            byte[] PV_arr = new byte[Bytecnt_num_p];
            Array.Copy(GoSerial.Response, 3, PV_arr, 0, Bytecnt_num_p);
            string PV_arr_str = BitConverter.ToString(PV_arr).Replace("-", "");
            string PV_dec = Convert.ToInt32(PV_arr_str, 16).ToString();
            Console.WriteLine("Present Value(PV) : " + PV_dec);
            //--------------------Display Present Value

            //--------------------Display Set Value
            byte[] Query_con_s = new byte[8] { 0x01, 0x04, 0x03, 0xEB, 0x00, 0x01, 0x41, 0xBA };
            GoSerial.SendSerialComm(Query_con_s, Query_con_s.Length);
            GoSerial.RcvSerialComm();
            string Bytecnt_dec_s = Convert.ToInt32(GoSerial.Response[2]).ToString();
            int Bytecnt_num_s = int.Parse(Bytecnt_dec_s);
            byte[] SV_arr = new byte[Bytecnt_num_s];
            Array.Copy(GoSerial.Response, 3, SV_arr, 0, Bytecnt_num_s);
            string SV_arr_str = BitConverter.ToString(SV_arr).Replace("-","");
            string SV_dec = Convert.ToInt32(SV_arr_str, 16).ToString();
            Console.WriteLine("Setting value(SV) : " + SV_dec);
            //--------------------Display Set Value

            Console.WriteLine();
            Console.Write("Enter any key, if you completed communication connection...");
            Console.ReadLine();
            
            string loop = "y";
            while (loop == "y")
            {
                Functioncs.Select();

                if (Functioncs.Func01_sel == "1") // Check RUN/STOP
                {
                    byte[] Query = new byte[8] { 0x01, 0x01, 0x00, 0x00, 0x00, 0x01, 0xFD, 0xCA };
                    string Query_Check = BitConverter.ToString(Query).Replace("-", " ");
                    Console.WriteLine(Query_Check);
                    GoSerial.SendSerialComm(Query, Query.Length);
                    GoSerial.RcvSerialComm();
                    Console.WriteLine(GoSerial.Response_String.Replace("-", " "));
                    if (GoSerial.Response[1] == 0x81)
                    {
                        Console.WriteLine("###############################################");
                        Console.WriteLine("An error has been occured.");
                        Console.WriteLine("Check the connection or setting of the device");
                        Console.WriteLine("###############################################");
                        Console.WriteLine("Would you like to continue?(y/n)");
                    }
                    else if (GoSerial.Response[3] == 0x00)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("<<Execution Result>>");
                        Console.WriteLine("It is RUN MODE.");
                        Console.WriteLine("");
                    }
                    else if (GoSerial.Response[3] == 0x01)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("<<Execution Result>>");
                        Console.WriteLine("It is STOP MODE.");
                        Console.WriteLine("");
                    }

                    else Console.WriteLine("ERROR, Check and try again...");
                    Console.WriteLine("Would you like to continue?(y/n)");
                    loop = Console.ReadLine();
                }

                else if (Functioncs.Func01_sel == "2") //Check Auto tuning
                {
                    byte[] Query = new byte[8] { 0x01, 0x01, 0x00, 0x01, 0x00, 0x01, 0xAC, 0x0A };
                    string Query_Check = BitConverter.ToString(Query).Replace("-", " ");
                    Console.WriteLine(Query_Check);
                    GoSerial.SendSerialComm(Query, Query.Length);
                    GoSerial.RcvSerialComm();
                    Console.WriteLine(GoSerial.Response_String.Replace("-", " "));
                    if (GoSerial.Response[1] == 0x81)
                    {
                        Console.WriteLine("###############################################");
                        Console.WriteLine("An error has been occured.");
                        Console.WriteLine("Check the connection or setting of the device");
                        Console.WriteLine("###############################################");
                        Console.WriteLine("Would you like to continue?(y/n)");
                    }

                    else if (GoSerial.Response[3] == 0x00)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("<<Execution Result>>");
                        Console.WriteLine("Auto tuning is not working");
                        Console.WriteLine("");
                    }
                    else if (GoSerial.Response[3] == 0x01)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("<<Execution Result>>");
                        Console.WriteLine("Auto tunig is working");
                        Console.WriteLine("");
                    }


                    else Console.WriteLine("ERROR, Check and try again...");
                    Console.WriteLine("Would you like to continue?(y/n)");
                    loop = Console.ReadLine();
                }


                else if (Functioncs.Func04_sel == "1") //Check Model name
                {
                    byte[] Query = new byte[8] { 0x01, 0x04, 0x00, 0x68, 0x00, 0x04, 0x70, 0x15 };
                    string Query_Check = BitConverter.ToString(Query).Replace("-", " ");
                    Console.WriteLine(Query_Check);
                    GoSerial.SendSerialComm(Query, Query.Length);
                    GoSerial.RcvSerialComm();
                    Console.WriteLine(GoSerial.Response_String.Replace("-", " "));
                    if (GoSerial.Response[1] == 0x84)
                    {
                        Console.WriteLine("###############################################");
                        Console.WriteLine("An error has been occured.");
                        Console.WriteLine("Check the connection or setting of the device");
                        Console.WriteLine("###############################################");
                        Console.WriteLine("Would you like to continue?(y/n)");
                    }
                    Console.WriteLine("Would you like to continue?(y/n)");
                    loop = Console.ReadLine();
                }
                else if (Functioncs.Func04_sel == "2") //Read Present value
                {
                    byte[] Query = new byte[8] { 0x01, 0x04, 0x03, 0xE8, 0x00, 0x01, 0xB1, 0xBA };
                    string Query_Check = BitConverter.ToString(Query).Replace("-", " ");
                    Console.WriteLine(Query_Check);
                    GoSerial.SendSerialComm(Query, Query.Length);
                    GoSerial.RcvSerialComm();
                    Console.WriteLine(GoSerial.Response_String.Replace("-", " "));
                    if (GoSerial.Response[1] == 0x84)
                    {
                        Console.WriteLine("###############################################");
                        Console.WriteLine("An error has been occured.");
                        Console.WriteLine("Check the connection or setting of the device");
                        Console.WriteLine("###############################################");
                        Console.WriteLine("Would you like to continue?(y/n)");
                    }
                    Console.WriteLine("Would you like to continue?(y/n)");
                    loop = Console.ReadLine();
                }
                else if (Functioncs.Func04_sel == "3") //Check Setting value
                {
                    byte[] Query = new byte[8] { 0x01, 0x04, 0x03, 0xEB, 0x00, 0x01, 0x41, 0xBA };
                    string Query_Check = BitConverter.ToString(Query).Replace("-", " ");
                    Console.WriteLine(Query_Check);
                    GoSerial.SendSerialComm(Query, Query.Length);
                    GoSerial.RcvSerialComm();
                    Console.WriteLine(GoSerial.Response_String.Replace("-", " "));
                    if (GoSerial.Response[1] == 0x84)
                    {
                        Console.WriteLine("###############################################");
                        Console.WriteLine("An error has been occured.");
                        Console.WriteLine("Check the connection or setting of the device");
                        Console.WriteLine("###############################################");
                        Console.WriteLine("Would you like to continue?(y/n)");
                    }
                    Console.WriteLine("Would you like to continue?(y/n)");
                    loop = Console.ReadLine();
                }
                else if (Functioncs.Func04_sel == "4") // Check OUT1 LED
                {
                    byte[] Query = new byte[8] { 0x01, 0x04, 0x03, 0xEE, 0x00, 0x01, 0x51, 0xBB };
                    string Query_Check = BitConverter.ToString(Query).Replace("-", " ");
                    Console.WriteLine(Query_Check);
                    GoSerial.SendSerialComm(Query, Query.Length);
                    GoSerial.RcvSerialComm();
                    Console.WriteLine(GoSerial.Response_String.Replace("-", " "));
                    bool Out1_Check = (GoSerial.Response[4] & (1 << 3)) != 0;
                    if (GoSerial.Response[1] == 0x84)
                    {
                        Console.WriteLine("###############################################");
                        Console.WriteLine("An error has been occured.");
                        Console.WriteLine("Check the connection or setting of the device");
                        Console.WriteLine("###############################################");
                        Console.WriteLine("Would you like to continue?(y/n)");

                    }
                    else if (Out1_Check == true)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("<<Execution Result>>");
                        Console.WriteLine("Out1 is ON");
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("<<Execution Result>>");
                        Console.WriteLine("Out1 is OFF");
                        Console.WriteLine("");
                    }
                    Console.WriteLine("Would you like to continue?(y/n)");
                    loop = Console.ReadLine();

                }
                else if (Functioncs.Func04_sel == "5") // Check Alram1 LED
                {
                    byte[] Query = new byte[8] { 0x01, 0x04, 0x03, 0xEE, 0x00, 0x01, 0x51, 0xBB };
                    string Query_Check = BitConverter.ToString(Query).Replace("-", " ");
                    Console.WriteLine(Query_Check);
                    GoSerial.SendSerialComm(Query, Query.Length);
                    GoSerial.RcvSerialComm();
                    Console.WriteLine(GoSerial.Response_String.Replace("-", " "));
                    bool AL1_Check = (GoSerial.Response[3] & (1 << 1)) != 0;
                    if (GoSerial.Response[1] == 0x84)
                    {
                        Console.WriteLine("###############################################");
                        Console.WriteLine("An error has been occured.");
                        Console.WriteLine("Check the connection or setting of the device");
                        Console.WriteLine("###############################################");
                        Console.WriteLine("Would you like to continue?(y/n)");
                    }
                    else if (AL1_Check == true)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("<<Execution Result>>");
                        Console.WriteLine("AL1 is ON");
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("<<Execution Result>>");
                        Console.WriteLine("AL1 is OFF");
                        Console.WriteLine("");
                    }
                    Console.WriteLine("Would you like to continue?(y/n)");
                    loop = Console.ReadLine();
                }

                else if (Functioncs.Func05_sel == "1") // Control out RUN
                {
                    byte[] Query = new byte[8] { 0x01, 0x05, 0x00, 0x00, 0x00, 0x00, 0xCD, 0xCA };
                    string Query_Check = BitConverter.ToString(Query).Replace("-", " ");
                    Console.WriteLine(Query_Check);
                    GoSerial.SendSerialComm(Query, Query.Length);
                    GoSerial.RcvSerialComm();
                    Console.WriteLine(GoSerial.Response_String.Replace("-", " "));
                    if (GoSerial.Response[1] == 0x85)
                    {
                        Console.WriteLine("###############################################");
                        Console.WriteLine("An error has been occured.");
                        Console.WriteLine("Check the connection or setting of the device");
                        Console.WriteLine("###############################################");
                        Console.WriteLine("Would you like to continue?(y/n)");
                    }
                    else if (GoSerial.Response[4] == 0x00)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("<<Execution Result>>");
                        Console.WriteLine("Controller RUN");
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("<<Execution Result>>");
                        Console.WriteLine("ERROR, Check and try again...");
                        Console.WriteLine("");
                    }

                    Console.WriteLine("Would you like to continue?(y/n)");
                    loop = Console.ReadLine();
                }

                else if (Functioncs.Func05_sel == "2") // Control out STOP
                {
                    byte[] Query = new byte[8] { 0x01, 0x05, 0x00, 0x00, 0xFF, 0x00, 0x8C, 0x3A };
                    string Query_Check = BitConverter.ToString(Query).Replace("-", " ");
                    Console.WriteLine(Query_Check);
                    GoSerial.SendSerialComm(Query, Query.Length);
                    GoSerial.RcvSerialComm();
                    Console.WriteLine(GoSerial.Response_String.Replace("-", " "));

                    if (GoSerial.Response[1] == 0x85)
                    {
                        Console.WriteLine("###############################################");
                        Console.WriteLine("An error has been occured.");
                        Console.WriteLine("Check the connection or setting of the device");
                        Console.WriteLine("###############################################");
                        Console.WriteLine("Would you like to continue?(y/n)");
                    }
                    else if (GoSerial.Response[4] == 0xFF)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("<<Execution Result>>");
                        Console.WriteLine("Controller STOP");
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("<<Execution Result>>");
                        Console.WriteLine("ERROR, Check and try again...");
                        Console.WriteLine("");
                    }
                    Console.WriteLine("Would you like to continue?(y/n)");
                    loop = Console.ReadLine();
                }
                else if (Functioncs.Func05_sel == "3") // Start Autotuning
                {
                    byte[] Query = new byte[8] { 0x01, 0x05, 0x00, 0x01, 0xFF, 0x00, 0xDD, 0xFA };
                    string Query_Check = BitConverter.ToString(Query).Replace("-", " ");
                    Console.WriteLine(Query_Check);
                    GoSerial.SendSerialComm(Query, Query.Length);
                    GoSerial.RcvSerialComm();
                    Console.WriteLine(GoSerial.Response_String.Replace("-", " "));

                    if (GoSerial.Response[1] == 0x85)
                    {
                        Console.WriteLine("###############################################");
                        Console.WriteLine("An error has been occured.");
                        Console.WriteLine("Check the connection or setting of the device");
                        Console.WriteLine("###############################################");
                        Console.WriteLine("Would you like to continue?(y/n)");
                    }
                    else if (GoSerial.Response[4] == 0xFF)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("<<Execution Result>>");
                        Console.WriteLine("Auto tuning starts");
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("<<Execution Result>>");
                        Console.WriteLine("ERROR, Check and try again...");
                        Console.WriteLine("오토튜닝이 정지 상태 일 수 있습니다.");
                        Console.WriteLine("Auto tuning may be stopped");
                        Console.WriteLine("");
                    }
                    Console.WriteLine("Would you like to continue?(y/n)");
                    loop = Console.ReadLine();
                }

                else if (Functioncs.Func05_sel == "4") // Stop Autotuning
                {
                    byte[] Query = new byte[8] { 0x01, 0x05, 0x00, 0x01, 0x00, 0x00, 0x9C, 0x0A };
                    string Query_Check = BitConverter.ToString(Query).Replace("-", " ");
                    Console.WriteLine(Query_Check);
                    GoSerial.SendSerialComm(Query, Query.Length);
                    GoSerial.RcvSerialComm();
                    Console.WriteLine(GoSerial.Response_String.Replace("-", " "));

                    if (GoSerial.Response[1] == 0x85)
                    {
                        Console.WriteLine("###############################################");
                        Console.WriteLine("An error has been occured.");
                        Console.WriteLine("Check the connection or setting of the device");
                        Console.WriteLine("###############################################");
                        Console.WriteLine("Would you like to continue?(y/n)");
                    }
                    else if (GoSerial.Response[4] == 0x00)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("<<Execution Result>>");
                        Console.WriteLine("Auto tuning stops working.");
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("<<Execution Result>>");
                        Console.WriteLine("ERROR, Check and try again...");
                        Console.WriteLine("");
                    }
                    Console.WriteLine("Would you like to continue?(y/n)");
                    loop = Console.ReadLine();
                }


                else if (Functioncs.Func06_sel == "1") // Set the SV Value
                {
                    if (Functioncs.Func06_sel_sv == "1") // Set SV to 0
                    {
                        byte[] Query = new byte[8] { 0x01, 0x06, 0x00, 0x00, 0x00, 0x00, 0x89, 0xCA };
                        string Query_Check = BitConverter.ToString(Query).Replace("-", " ");
                        Console.WriteLine(Query_Check);
                        GoSerial.SendSerialComm(Query, Query.Length);
                        GoSerial.RcvSerialComm();
                        Console.WriteLine(GoSerial.Response_String.Replace("-", " "));

                        if (GoSerial.Response[1] == 0x86)
                        {
                            Console.WriteLine("###############################################");
                            Console.WriteLine("An error has been occured.");
                            Console.WriteLine("Check the connection or setting of the device");
                            Console.WriteLine("###############################################");
                            Console.WriteLine("Would you like to continue?(y/n)");
                            loop = Console.ReadLine();
                        }
                        else if (GoSerial.Response[6] == 0x89 && GoSerial.Response[7] == 0xCA)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("<<SV is set to 0>>");
                            Console.WriteLine("");
                            Console.WriteLine("Would you like to continue?(y/n)");
                            loop = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("ERROR, Check and try again...");
                            Console.WriteLine("");
                        }

                    }

                    else if (Functioncs.Func06_sel_sv == "2") // Set SV to 50
                    {
                        byte[] Query = new byte[8] { 0x01, 0x06, 0x00, 0x00, 0x00, 0x32, 0x08, 0x1F };
                        string Query_Check = BitConverter.ToString(Query).Replace("-", " ");
                        Console.WriteLine(Query_Check);
                        GoSerial.SendSerialComm(Query, Query.Length);
                        GoSerial.RcvSerialComm();
                        Console.WriteLine(GoSerial.Response_String.Replace("-", " "));

                        if (GoSerial.Response[1] == 0x86)
                        {
                            Console.WriteLine("###############################################");
                            Console.WriteLine("An error has been occured.");
                            Console.WriteLine("Check the connection or setting of the device");
                            Console.WriteLine("###############################################");
                            Console.WriteLine("Would you like to continue?(y/n)");
                            loop = Console.ReadLine();
                        }
                        else if (GoSerial.Response[6] == 0x08 && GoSerial.Response[7] == 0x1F)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("<<SV is set to 50>>");
                            Console.WriteLine("");
                            Console.WriteLine("Would you like to continue?(y/n)");
                            loop = Console.ReadLine();
                        }

                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("ERROR, Check and try again...");
                            Console.WriteLine("");
                        }

                    }

                    else if (Functioncs.Func06_sel_sv == "3") // Set SV to 100
                    {
                        byte[] Query = new byte[8] { 0x01, 0x06, 0x00, 0x00, 0x00, 0x64, 0x88, 0x21 };
                        string Query_Check = BitConverter.ToString(Query).Replace("-", " ");
                        Console.WriteLine(Query_Check);
                        GoSerial.SendSerialComm(Query, Query.Length);
                        GoSerial.RcvSerialComm();
                        Console.WriteLine(GoSerial.Response_String.Replace("-", " "));

                        if (GoSerial.Response[1] == 0x86)
                        {
                            Console.WriteLine("###############################################");
                            Console.WriteLine("An error has been occured.");
                            Console.WriteLine("Check the connection or setting of the device");
                            Console.WriteLine("###############################################");
                            Console.WriteLine("Would you like to continue?(y/n)");
                            loop = Console.ReadLine();
                        }
                        else if (GoSerial.Response[6] == 0x88 && GoSerial.Response[7] == 0x21)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("<<SV is set to 1000>>");
                            Console.WriteLine("");
                            Console.WriteLine("Would you like to continue?(y/n)");
                            loop = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("ERROR, Check and try again...");
                            Console.WriteLine("");
                        }
                    }

                    
                    else if (Functioncs.Func06_sel_sv == "4")//Set SV to 'User value'
                    {
                        int SV_Dec;
                        string SV_Dec_str;
                        Console.Write("Enter the SV value(Decimal) : ");
                        SV_Dec_str = Console.ReadLine();
                        SV_Dec = int.Parse(SV_Dec_str);
                        Console.WriteLine("");
                        byte[] SV_byte = BitConverter.GetBytes(SV_Dec);
                        byte[] ForCRC = { 0x01, 0x06, 0x00, 0x00, SV_byte[1], SV_byte[0] };
                        byte[] CRC_Cal = BitConverter.GetBytes(CRC16_GEN.ComputeChecksum(ForCRC));
                        byte[] Query = new byte[8] { 0x01, 0x06, 0x00, 0x00, SV_byte[1], SV_byte[0], CRC_Cal[0], CRC_Cal[1]};
                        string Query_Check = BitConverter.ToString(Query).Replace("-", " ");
                        Console.WriteLine(Query_Check);
                        GoSerial.SendSerialComm(Query, Query.Length);
                        GoSerial.RcvSerialComm();
                        Console.WriteLine(GoSerial.Response_String.Replace("-", " "));

                        if (GoSerial.Response[1] == 0x86)
                        {
                            Console.WriteLine("###############################################");
                            Console.WriteLine("An error has been occured.");
                            Console.WriteLine("Check the connection or setting of the device");
                            Console.WriteLine("###############################################");
                            Console.WriteLine("Would you like to continue?(y/n)");
                            loop = Console.ReadLine();
                        }
                        else if (GoSerial.Response[6] == CRC_Cal[0] && GoSerial.Response[7] == CRC_Cal[1])
                        {
                            Console.WriteLine("");
                            Console.WriteLine("<<SV Value is set to " + SV_Dec_str+">>");
                            Console.WriteLine("");
                            Console.WriteLine("Would you like to continue?(y/n)");
                            loop = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("ERROR, Check and try again...");
                            Console.WriteLine("");
                        }

                    }
                }

                else if (Functioncs.Stop_serial == "1")//Communication dispose and re-connect
                {

                    GoSerial.CloseSerialComm();
                    GoSerial.Dispose();
                    Console.WriteLine("");
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("Serial connection is disposed.");
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("");
                    Console.WriteLine("Would you like to re-connect?(y/n)");
                    string Stop_check = null ;
                    Stop_check = Console.ReadLine();
                    if (Stop_check == "y")
                    {
                        ComPort = Setting_part.Comportset();
                        Baud = Setting_part.Baudset();
                        Stop = Setting_part.Stopset();
                        Parity = Setting_part.Parityset();
                        GoSerial.Comport_Open(ComPort, Baud, "8", Parity, Stop);
                        loop = "y";
                    }
                    else break;

                }
            }
        }
    }
}

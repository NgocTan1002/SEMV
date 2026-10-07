using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace TK_Series_Sample_program_ENG
{
    class Serial_setting
    {
        public string Comportset()
        {
            string[] port_check = SerialPort.GetPortNames();
            Console.WriteLine("<Enter COM Port Number>");
            Console.WriteLine("");
            Console.WriteLine("----------------------");
            Console.WriteLine("Available Ports");
            foreach (string port_val in port_check)
            {
                Console.WriteLine(port_val);
            }
            Console.WriteLine("----------------------");
            Console.Write("COM Port Number : COM");
            string ComPort = "COM" + Console.ReadLine();
            while (true)
                if (port_check.Contains(ComPort))
                {
                    return ComPort;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("----------------------");
                    Console.WriteLine("<Not available COM Port. Try to enter again.>");
                    foreach (string port_val in port_check)
                    {
                        Console.WriteLine(port_val);
                    }
                    Console.WriteLine("----------------------");
                    Console.Write("COM Port Number : COM");
                    ComPort = "COM" + Console.ReadLine();
                    continue;
                }
        }

        public string Baudset()
        {

            Console.WriteLine("");
            Console.WriteLine("--------------------");
            Console.WriteLine("<Choose Buadrate>");
            Console.WriteLine("1. 9600");
            Console.WriteLine("2. 19200");
            Console.WriteLine("3. 38400");
            Console.WriteLine("4. 57600");
            Console.WriteLine("5. 115200");
            Console.WriteLine("--------------------");
            Console.Write("Baudrate : ");
            string S_Baud = Console.ReadLine();

            string Baud;
            while (true)
            {
                if (S_Baud == "1")
                {
                    Baud = "9600";
                    Console.WriteLine("Baudrate is set to " + Baud);
                    return Baud;
                }
                else if (S_Baud == "2")
                {
                    Baud = "19200";
                    Console.WriteLine("Baudrate is set to " + Baud);
                    return Baud;
                }
                else if (S_Baud == "3")
                {
                    Baud = "38400";
                    Console.WriteLine("Baudrate is set to " + Baud);
                    return Baud;
                }

                else if (S_Baud == "4")
                {
                    Baud = "57600";
                    Console.WriteLine("Baudrate is set to " + Baud);
                    return Baud;
                }
                else if (S_Baud == "5")
                {
                    Baud = "115200";
                    Console.WriteLine("Baudrate is set to " + Baud);
                    return Baud;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("--------------------");
                    Console.WriteLine("<Choose Again>");
                    Console.WriteLine("1. 9600");
                    Console.WriteLine("2. 19200");
                    Console.WriteLine("3. 38400");
                    Console.WriteLine("4. 57600");
                    Console.WriteLine("5. 115200");
                    Console.WriteLine("--------------------");
                    Console.Write("Baudrate : ");
                    S_Baud = Console.ReadLine();
                    continue;
                }

            }
        }

        public string Parityset()
        {
            Console.WriteLine("");
            Console.WriteLine("--------------------");
            Console.WriteLine("<Choose Parity Bit>");
            Console.WriteLine("0. None");
            Console.WriteLine("1. Odd");
            Console.WriteLine("2. Even");
            Console.Write("Parity bit : ");
            string S_Parity = Console.ReadLine();
            string Parity;

            while (true)
            {
                if (S_Parity == "0")
                {
                    Parity = "0";
                    Console.WriteLine("Parity Bit is set to None");
                    return Parity;
                }
                else if (S_Parity == "1")
                {
                    Parity = "1";
                    Console.WriteLine("Parity Bit is set to Odd");
                    return Parity;
                }
                else if (S_Parity == "2")
                {
                    Parity = "2";
                    Console.WriteLine("Parity Bit is set to Even");
                    return Parity;
                }

                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("--------------------");
                    Console.WriteLine("<Choose Again>");
                    Console.WriteLine("0. None");
                    Console.WriteLine("1. Odd");
                    Console.WriteLine("2. Even");
                    Console.Write("Parity bit : ");
                    S_Parity = Console.ReadLine();
                    continue;
                }
            }
        }

        public string Stopset()
        {
            Console.WriteLine("");
            Console.WriteLine("--------------------");
            Console.WriteLine("<Choose Stop Bit>");
            Console.WriteLine("0. None");
            Console.WriteLine("1. One");
            Console.WriteLine("2. Two");
            Console.Write("Stop bit : ");
            string S_Stop = Console.ReadLine();
            string Stop;

            while (true)
            {
                if (S_Stop == "0")
                {
                    Stop = "0";
                    Console.WriteLine("Stop Bit is set to None");
                    return Stop;
                }
                else if (S_Stop == "1")
                {
                    Stop = "1";
                    Console.WriteLine("Stop Bit is set to One");
                    return Stop;
                }

                else if (S_Stop == "2")
                {
                    Stop = "2";
                    Console.WriteLine("Stop Bit is set to Two");
                    return Stop;
                }

                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("--------------------");
                    Console.WriteLine("<Choose Again>");
                    Console.WriteLine("0. None");
                    Console.WriteLine("1. One");
                    Console.WriteLine("2. Two");
                    Console.Write("Stop bit : ");
                    S_Stop = Console.ReadLine();
                    continue;
                }
            }
        }


    }
}

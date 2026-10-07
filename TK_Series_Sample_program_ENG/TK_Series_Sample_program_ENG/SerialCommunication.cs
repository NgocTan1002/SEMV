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
    class SerialCommunication
    {
        public ArrayList Serial_Buf_Arr= new ArrayList(); 
        private SerialPort Sp = new SerialPort();

        //--------------------------------------------------------
        //Open Comport        
        //--------------------------------------------------------
        public string Comport_Open(string port, string baud, string databits, string parity, string stop)
        {
            try
            {
                Sp.PortName = port;
                Sp.BaudRate = int.Parse(baud);
                Sp.DataBits = int.Parse(databits);
                Sp.Parity = (Parity)Enum.Parse(typeof(Parity), parity);
                Sp.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stop);

                if (!Sp.IsOpen)
                {
                    Sp.Open();
                }
                if (Sp.IsOpen)
                {
                    Console.WriteLine();
                    Console.WriteLine("--------------------");
                    return "-------------Connected-------------";
                }

                else  
                {
                    return "Connection fail";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------------------------------------------
        //Receiving 
        //--------------------------------------------------------
        public void Reference(int number)
        {
            byte[] buf = new byte[number];
        }

        public string Response_String;
        public byte[] Response;
        public void RcvSerialComm()
        {

            try
            {
                if (Sp.IsOpen)
                {
                    int nbyte = Sp.BytesToRead;
                    byte[] rbuff = new byte[nbyte];
                    if (nbyte > 0)
                    {
                        Sp.Read(rbuff, 0, nbyte);
                    }
                    Response = rbuff;
                    Response_String = BitConverter.ToString(rbuff);

                    for (int i = 0; i < nbyte; i++)
                    {
                        Serial_Buf_Arr.Add(rbuff[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                Serial_Buf_Arr.Clear();
                throw ex;
            }
        }

        //--------------------------------------------------------
        //Checking PORT Status
        //--------------------------------------------------------
        public bool IsOpened()
        {
            return Sp.IsOpen;
        }

        //--------------------------------------------------------
        //Count received buffer
        //--------------------------------------------------------
        public int RcvCnt()
        {

            return Serial_Buf_Arr.Count;

        }

        //--------------------------------------------------------
        //Clear received buffer
        //--------------------------------------------------------
        public void RcvBuffClear()
        {

            Serial_Buf_Arr.Clear();
        }

        //--------------------------------------------------------
        //Sending
        //--------------------------------------------------------
        public void SendSerialComm(byte[] SendComm_Packet, int len)
        {
            try
            {
                if (Sp.IsOpen)
                    Sp.Write(SendComm_Packet, 0, len);
                    //Console.WriteLine("Sending Completed");
                    Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        //--------------------------------------------------------
        //Close the PORT
        //--------------------------------------------------------
        public void CloseSerialComm()

        {
            try
            {
                if (Sp != null)
                    Sp.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        //--------------------------------------------------------
        //Dispose the PORT
        //--------------------------------------------------------
        public void Dispose()
        {
            if (Serial_Buf_Arr.Count > 0)
            {
                Serial_Buf_Arr.Clear();
            }
            if (Sp != null)
                Sp.Dispose();
        }
    }
}

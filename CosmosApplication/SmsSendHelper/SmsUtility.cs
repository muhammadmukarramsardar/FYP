using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CosmosApplication.SmsSendHelper
{
    public class SmsUtility
    {
        public AutoResetEvent receiveNow;
        SerialPort port = new SerialPort();
        public bool SendSms(string phone, string msg)
        {

            try
            {
                bool isSend = false;


                List<COMPortInfo> comPorts = COMPortInfo.GetCOMPortsInfo();
                string comPort = "";
                if (comPorts != null && comPorts.Count() > 0)
                {
                    receiveNow = new AutoResetEvent(false);

                    foreach (var item in comPorts)
                    {
                        if (item.Description.Equals("USB Modem Application Interface"))
                        {
                            comPort = item.Name;
                        }
                    }

                    port.PortName = comPort;                 //COM1
                    port.BaudRate = 9600;                   //9600
                    port.DataBits = 8;                   //8
                    port.StopBits = StopBits.One;                  //1
                    port.Parity = Parity.None;                     //None
                    port.ReadTimeout = 300;             //300
                    port.WriteTimeout = 300;           //300
                    port.Encoding = Encoding.GetEncoding("iso-8859-1");
                    port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                    port.Open();
                    port.DtrEnable = true;
                    port.RtsEnable = true;




                    string recievedData = ExecCommand(port, "AT", 300, "No phone connected");
                    recievedData = ExecCommand(port, "AT+CSCS=\"GSM\"", 300, "Set Charset to GSM");
                    recievedData = ExecCommand(port, "AT+CMGF=1", 300, "Failed to set message format.");
                    String command = "AT+CMGS=\"" + phone + "\"";
                    recievedData = ExecCommand(port, command, 300, "Failed to accept phoneNo");
                    command = msg + char.ConvertFromUtf32(26) + "\r";
                    recievedData = ExecCommand(port, command, 3000, "Failed to send message"); //3 seconds
                    if (recievedData.EndsWith("\r\nOK\r\n"))
                    {
                        isSend = true;
                    }
                    else if (recievedData.Contains("ERROR"))
                    {
                        isSend = false;
                    }
                    return isSend;
                }
                return false;
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                return false;
            }
            finally
            {
                this.ClosePort(port);
            }
        }

        private void ClosePort(SerialPort port)
        {
            try
            {
                port.Close();
                port.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
                port = null;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (e.EventType == SerialData.Chars)
                    receiveNow.Set();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ExecCommand(SerialPort port, string command, int responseTimeout, string errorMessage)
        {
            try
            {

                port.DiscardOutBuffer();
                port.DiscardInBuffer();
                receiveNow.Reset();
                port.Write(command + "\r");

                string input = ReadResponse(port, responseTimeout);
                if ((input.Length == 0) || ((!input.EndsWith("\r\n> ")) && (!input.EndsWith("\r\nOK\r\n"))))
                    throw new ApplicationException("No success message was received.");
                return input;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(errorMessage, ex);
            }
        }

        private string ReadResponse(SerialPort port, int timeout)
        {
            string buffer = string.Empty;
            try
            {
                do
                {
                    if (receiveNow.WaitOne(timeout, false))
                    {
                        string t = port.ReadExisting();
                        buffer += t;
                    }
                    else
                    {
                        if (buffer.Length > 0)
                            throw new ApplicationException("Response received is incomplete.");
                        else
                            throw new ApplicationException("No data received from phone.");
                    }
                }
                while (!buffer.EndsWith("\r\nOK\r\n") && !buffer.EndsWith("\r\n> ") && !buffer.EndsWith("\r\nERROR\r\n"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return buffer;
        }
    }
}

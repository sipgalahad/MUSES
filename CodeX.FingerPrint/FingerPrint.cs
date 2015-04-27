using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zkemkeeper;

namespace CodeX.FingerPrint
{
    public class FingerPrint
    {
        private CZKEMClass _fp;

        public CZKEMClass Fp
        {
          get { return _fp; }
          set { _fp = value; }
        }

        public FingerPrint()
        { 
            CZKEMClass _fp = new CZKEMClass();
        }

        public bool Connect_Net(string IPAdd, int Port)
        {
            bool result = true;
            try
            {
                _fp.Connect_Net(IPAdd, Port);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool Disconnect() 
        {
            bool result = true;
            try
            {
                _fp.Disconnect();
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool GetUserInfo(string dwEnrollNumber, out string StringResult, out string errMessage) 
        { 
            StringBuilder sb = new StringBuilder();
            bool result = true;
            string Name;
            string Password;
            int Privilege = 1;
            bool Enabled = true;

            try
            {
                _fp.SSR_GetUserInfo(_fp.MachineNumber, dwEnrollNumber, out Name, out Password, out Privilege, out Enabled);
                sb.Append(String.Format("{0};{1};{2};{3};{4}|",dwEnrollNumber,Name,Password,Privilege,Enabled));
                
                StringResult = sb.ToString();
                errMessage = "";
            }
            catch(Exception ex)
            {
                StringResult = "";
                errMessage = ex.Message;
                result = false;
            }
            
            return result;
        }

        public bool GetAllUserInfo(out string StringResult, out string errMessage) 
        {
            StringBuilder sb = new StringBuilder();
            bool result = true;
            string dwEnrollNumber;
            string Name;
            string Password;
            int Privilege = 1;
            bool Enabled = true;

            try
            {
                while(_fp.SSR_GetAllUserInfo(_fp.MachineNumber, out dwEnrollNumber, out Name, out Password, out Privilege, out Enabled))
                {
                    sb.Append(String.Format("{0};{1};{2};{3};{4}|",dwEnrollNumber,Name,Password,Privilege,Enabled));
                }
                StringResult = sb.ToString();
                errMessage = "";
            }
            catch(Exception ex)
            {
                StringResult = "";
                errMessage = ex.Message;
                result = false;
            }
            
            return result;
        }

        public bool GetGeneralLogData(out string StringResult, out string errMessage) 
        {
            StringBuilder sb = new StringBuilder();
            bool result = true;
            string dwEnrollNumber;
            int dwVerifyMode;
            int dwInOutMode;
            int dwYear;
            int dwMonth;
            int dwDay;
            int dwHour;
            int dwMinute;
            int dwSecond;
            int dwWorkCode = 1;

            try
            {
                while (_fp.SSR_GetGeneralLogData(_fp.MachineNumber, out dwEnrollNumber, out dwVerifyMode, out dwInOutMode, out dwYear, out dwMonth, out dwDay, out dwHour, out dwMinute, out dwSecond, ref dwWorkCode))
                {
                    sb.Append(String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}|", dwEnrollNumber, dwVerifyMode, dwInOutMode, dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond, dwWorkCode));
                }
                StringResult = sb.ToString();
                errMessage = "";
            }
            catch (Exception ex)
            {
                StringResult = "";
                errMessage = ex.Message;
                result = false;
            }

            return result;
        }

        public bool ClearGeneralLog() 
        {
            return _fp.ClearGLog(_fp.MachineNumber);
        }
    }
}

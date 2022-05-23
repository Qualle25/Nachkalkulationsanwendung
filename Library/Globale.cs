using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Globale
    {
        public class ReturnInfo
        {
            //-------------------------------------------------------------------------
            public bool Ok { get; set; } = true;
            public bool Error { get { return !Ok; } set { Ok = !value; } }
            public string Message { get; set; } = string.Empty;
            public long IntValue { get; set; } = 0;
            public string StrValue { get; set; } = string.Empty;
            public bool BoolValue { get; set; } = false;
            public object? Obj { get; set; } = null;
            public Exception? Except { get; set; } = null;
            public bool ConnectionError { get; set; } = false;
            public static ReturnInfo Empty { get { return new ReturnInfo(); } }
            public static ReturnInfo NotOk { get { return new ReturnInfo(false); } }

            //-------------------------------------------------------------------------
            public ReturnInfo()
            {
            }

            //-------------------------------------------------------------------------
            public ReturnInfo(bool isOk)
            {
                Ok = isOk;
            }

            //-------------------------------------------------------------------------
            public ReturnInfo(string msg)
            {
                Ok = true;
                Message = msg;
            }

            //-------------------------------------------------------------------------
            public ReturnInfo(object obj)
            {
                Ok = true;
                Obj = obj;
            }

            //-------------------------------------------------------------------------
            public ReturnInfo(int ival, object obj)
            {
                Ok = true;
                IntValue = ival;
                Obj = obj;
            }

            //-------------------------------------------------------------------------
            public ReturnInfo(Exception ex)
            {
                Ok = false;
                Except = ex;
                if (ex != null)
                {
                    this.Message = ex.Message;
                    string? s = ex.StackTrace;
                    if (!string.IsNullOrEmpty(s)) StrValue = s;
                }
                else
                    this.Message = "Es wurde eine nicht initialisierte Ausnahme übergeben.";
            }

            //-------------------------------------------------------------------------
            public ReturnInfo(bool isOk, string msg)
            {
                Ok = isOk;
                this.Message = msg;
            }

            //-------------------------------------------------------------------------
            public ReturnInfo(bool isOk, string msg, long intVal)
            {
                Ok = isOk;
                this.Message = msg;
                IntValue = intVal;
            }

            //-------------------------------------------------------------------------
            public ReturnInfo(bool isOk, string msg, string strVal)
            {
                Ok = isOk;
                this.Message = msg;
                StrValue = strVal;
            }

            //-------------------------------------------------------------------------
            public ReturnInfo(bool isOk, string msg, bool boolVal)
            {
                Ok = isOk;
                this.Message = msg;
                BoolValue = boolVal;
            }

            //-------------------------------------------------------------------------
            public ReturnInfo(bool isOk, string msg, object obj)
            {
                Ok = isOk;
                this.Message = msg;
                Obj = obj;
            }

            //-------------------------------------------------------------------------
            public ReturnInfo(bool isOk, string msg, string msgBoxText, long ival, string sval)
            {
                Ok = isOk;
                this.Message = msg;
                Message = msgBoxText;
                IntValue = ival;
                StrValue = sval;
            }
        }
}

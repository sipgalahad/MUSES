using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeX.Data.Model
{
    #region Variable
    public class Variable
    {
        private string _Code;
        private string _Value;

        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }
    #endregion
    #region Words
    public class Words
    {
        public string Code { get; set; }
        public string Text { get; set; }
    }
    #endregion
    #region UserLogin
    public partial class UserLogin
    {
        public Int32 UserID { get; set; }
        public String UserName { get; set; }
        public String UserFullName { get; set; }
        public Boolean IsSysAdmin { get; set; }
        public String SiteID { get; set; }
        public String SiteName { get; set; }
        public String ModuleID { get; set; }
    }
    #endregion
    #region Matrix
    public class ProceedEntity
    {
        private String _ID;
        private ProceedEntityStatus _Status;

        public String ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public ProceedEntityStatus Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public enum ProceedEntityStatus
        {
            Remove = 0,
            Add = 1
        }
    }

    public class CEntity
    {
        private string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    }

    public class CMatrix
    {
        public bool IsChecked { get; set; }
        public object ID { get; set; }
        public string Name { get; set; }
    }
    #endregion
}

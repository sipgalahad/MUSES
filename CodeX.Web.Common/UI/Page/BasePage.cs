using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Xml.Linq;
using CodeX.Data.Model;

namespace CodeX.Web.Common.UI
{
    public abstract class BasePage : Page
    {
        protected List<Words> words;

        protected void LoadWords()
        {
            words = Helper.LoadWords(this);
        }

        public List<Words> GetWords()
        {
            return words;
        }

        public string GetLabel(string code)
        {
            return Helper.GetWordsLabel(words, code);
        }

        public virtual string OnGetCustomLang()
        {
            return "";
        }

        public virtual string OnGetDepartmentID()
        {
            return "";
        }

        public virtual bool OnBeforeDirectPrint(ReportMaster reportMaster, ref string errMessage)
        {
            return true;
        }

        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
            if (!Page.IsCallback)
            {
                LoadWords();
            }
        }
    }
}

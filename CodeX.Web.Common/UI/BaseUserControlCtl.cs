using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeX.Data.Model;

namespace CodeX.Web.Common.UI
{
    public abstract class BaseUserControlCtl : System.Web.UI.UserControl 
    {
        #region Words
        protected List<Words> words;
        protected void LoadWords()
        {
            words = Helper.LoadWords(this);
        }
        public string GetLabel(string code)
        {
            return Helper.GetWordsLabel(words, code);
        }
        #endregion

        public virtual void LoadMasterControl()
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            words = ((BasePage)Page).GetWords();
            if (words == null)
                LoadWords();
        }
    }
}

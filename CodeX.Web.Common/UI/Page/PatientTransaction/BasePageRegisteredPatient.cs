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
    public abstract class BasePageRegisteredPatient : BasePageTrx
    {
        public abstract string GetFilterExpression();
        public abstract void LoadAllWords();
        public abstract void OnGrdRowClick(string transactionNo);
    }
}

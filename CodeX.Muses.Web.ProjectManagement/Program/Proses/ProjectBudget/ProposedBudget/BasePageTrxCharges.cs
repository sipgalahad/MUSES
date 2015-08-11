using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeX.Web.Common.UI;
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public abstract class BasePageTrxCharges : BasePageTrx
    {
        public abstract void SaveTransactionHeader(IDbContext ctx, ref int transactionID, ref string transactionNo);
        public abstract Int32 GetClassID();
        public abstract Int32 GetRegistrationPhysicianID();
        public abstract Int32 GetLocationID();
        public abstract Int32 GetLogisticLocationID();
        public abstract String GetDepartmentID();
        public abstract Int32 GetSiteServiceUnitID();
        public abstract String GetTransactionHdID();
        public abstract String GetTransactionDate();
        public abstract String GetTransactionTime();
        public abstract String GetGCTransactionStatus();
        public abstract String GetGCCustomerType();
        public abstract String GetHdnIsRounded();
        public abstract String GetHdnRoundedValue();
        public abstract void SetTransactionHdID(string val);
        public virtual Boolean IsPatientBillSummaryPage()
        {
            return false;
        }
    }
}

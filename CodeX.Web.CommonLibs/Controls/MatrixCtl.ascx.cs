using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Core.Dal;
using System.Collections;
using CodeX.Common;

namespace CodeX.Web.CommonLibs.Controls
{
    public partial class MatrixCtl : BaseViewPopupCtl
    {
        #region GL Account Payable
        private void InitializeGLAccountPayable(string queryString)
        {
            lblHeader.InnerText = "Jenis";
            lblHeader2.InnerText = "Tipe Item";

            vGLAccountPayable entity = BusinessLayer.GetvGLAccountPayableList(string.Format("ID = {0}", queryString))[0];
            txtHeader.Text = entity.AccountPayableType;
            txtHeader2.Text = entity.ItemType;

            List<BusinessPartners> ListAvailableMember = BusinessLayer.GetBusinessPartnersList(string.Format("BusinessPartnerID NOT IN (SELECT BusinessPartnerID FROM GLAccountPayableDt WHERE ID = {0}) AND GCBusinessPartnerType = '{1}' AND IsDeleted = 0", queryString, Constant.BusinessObjectType.SUPPLIER));
            List<BusinessPartners> ListSelectedMember = BusinessLayer.GetBusinessPartnersList(string.Format("BusinessPartnerID IN (SELECT BusinessPartnerID FROM GLAccountPayableDt WHERE ID = {0}) AND IsDeleted = 0", queryString));

            ListAvailable = (from p in ListAvailableMember
                             select new CMatrix { IsChecked = false, ID = p.BusinessPartnerID.ToString(), Name = p.BusinessPartnerName }).OrderBy(p => p.Name).ToList();

            ListSelected = (from p in ListSelectedMember
                            select new CMatrix { IsChecked = false, ID = p.BusinessPartnerID.ToString(), Name = p.BusinessPartnerName }).OrderBy(p => p.Name).ToList();
        }

        private bool SaveGLAccountPayable(string queryString, ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = false;
            try
            {
                int ID = Convert.ToInt32(queryString);
                GLAccountPayableDtDao entityDao = new GLAccountPayableDtDao(ctx);
                foreach (ProceedEntity row in ListProceedEntity)
                {
                    if (row.Status == ProceedEntity.ProceedEntityStatus.Add)
                    {
                        GLAccountPayableDt entity = new GLAccountPayableDt();
                        entity.ID = ID;
                        entity.BusinessPartnerID = Convert.ToInt32(row.ID);
                        entityDao.Insert(entity);
                    }
                    else
                        entityDao.Delete(ID, Convert.ToInt32(row.ID));
                }
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region GL Warehouse Product Line Account Dt
        private void InitializeGLWarehouseProductLineAccountDt(string queryString)
        {
            lblHeader.InnerText = "Tipe Item";
            lblHeader2.InnerText = "Product Line";

            vGLWarehouseProductLineAccount entity = BusinessLayer.GetvGLWarehouseProductLineAccountList(string.Format("ID = {0}", queryString))[0];
            txtHeader.Text = entity.ItemType;
            txtHeader2.Text = entity.ProductLineName;

            List<Location> ListAvailableMember = BusinessLayer.GetLocationList(string.Format("LocationID NOT IN (SELECT LocationID FROM GLWarehouseProductLineAccountDt WHERE ID = {0}) AND IsHeader = 0 AND IsDeleted = 0", queryString));
            List<Location> ListSelectedMember = BusinessLayer.GetLocationList(string.Format("LocationID IN (SELECT LocationID FROM GLWarehouseProductLineAccountDt WHERE ID = {0})", queryString));

            ListAvailable = (from p in ListAvailableMember
                             select new CMatrix { IsChecked = false, ID = p.LocationID.ToString(), Name = p.LocationName }).OrderBy(p => p.Name).ToList();

            ListSelected = (from p in ListSelectedMember
                            select new CMatrix { IsChecked = false, ID = p.LocationID.ToString(), Name = p.LocationName }).OrderBy(p => p.Name).ToList();
        }

        private bool SaveGLWarehouseProductLineAccountDt(string queryString, ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = false;
            try
            {
                int ID = Convert.ToInt32(queryString);
                GLWarehouseProductLineAccountDtDao entityDao = new GLWarehouseProductLineAccountDtDao(ctx);
                foreach (ProceedEntity row in ListProceedEntity)
                {
                    if (row.Status == ProceedEntity.ProceedEntityStatus.Add)
                    {
                        GLWarehouseProductLineAccountDt entity = new GLWarehouseProductLineAccountDt();
                        entity.ID = ID;
                        entity.LocationID = Convert.ToInt32(row.ID);
                        entityDao.Insert(entity);
                    }
                    else
                        entityDao.Delete(ID, Convert.ToInt32(row.ID));
                }
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Module Menu
        private void InitializeModuleMenu(string queryString)
        {
            lblHeader.InnerText = "Module";

            Module module = BusinessLayer.GetModule(queryString);
            txtHeader.Text = string.Format("{0} - {1}", module.ModuleID, module.ModuleName);

            List<MenuMaster> ListAvailableMenu = BusinessLayer.GetMenuMasterList(string.Format("ModuleID != '{0}' OR ModuleID IS NULL ORDER BY MenuCaption ASC", queryString));
            List<MenuMaster> ListSelectedMenu = BusinessLayer.GetMenuMasterList(string.Format("ModuleID = '{0}' ORDER BY MenuCaption ASC", queryString));

            ListAvailable = (from p in ListAvailableMenu
                             select new CMatrix { IsChecked = false, ID = p.MenuID.ToString(), Name = p.MenuCaption }).ToList();

            ListSelected = (from p in ListSelectedMenu
                            select new CMatrix { IsChecked = false, ID = p.MenuID.ToString(), Name = p.MenuCaption }).ToList();
        }

        private bool SaveModuleMenu(string queryString, ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = false;
            try
            {
                string ModuleID = queryString;
                MenuMasterDao entityDao = new MenuMasterDao(ctx);
                foreach (ProceedEntity row in ListProceedEntity)
                {
                    if (row.Status == ProceedEntity.ProceedEntityStatus.Add)
                    {
                        Int32 MenuID = Convert.ToInt32(row.ID);
                        MenuMaster entity = entityDao.Get(MenuID);
                        entity.ModuleID = ModuleID;
                        entityDao.Update(entity);
                    }
                    else
                    {
                        Int32 MenuID = Convert.ToInt32(row.ID);
                        MenuMaster entity = entityDao.Get(MenuID);
                        entity.ModuleID = null;
                        entityDao.Update(entity);
                    }
                }
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Treasury Book COA
        private void InitializeTreasuryBookCOA(string queryString)
        {
            lblHeader.InnerText = "Buku";

            TreasuryBook entity = BusinessLayer.GetTreasuryBook(Convert.ToInt32(queryString));
            txtHeader.Text = entity.BookName;

            List<vChartOfAccount> ListAvailableMember = BusinessLayer.GetvChartOfAccountList(string.Format("GLAccountID NOT IN (SELECT GLAccount FROM TreasuryBookCOA WHERE BookID = {0}) AND IsHeader = 0 AND IsDeleted = 0", queryString));
            List<vChartOfAccount> ListSelectedMember = BusinessLayer.GetvChartOfAccountList(string.Format("GLAccountID IN (SELECT GLAccount FROM TreasuryBookCOA WHERE BookID = {0}) AND IsDeleted = 0", queryString));

            ListAvailable = (from p in ListAvailableMember
                             select new CMatrix { IsChecked = false, ID = p.GLAccountID.ToString(), Name = p.GLAccountName }).OrderBy(p => p.Name).ToList();

            ListSelected = (from p in ListSelectedMember
                            select new CMatrix { IsChecked = false, ID = p.GLAccountID.ToString(), Name = p.GLAccountName }).OrderBy(p => p.Name).ToList();
        }

        private bool SaveTreasuryBookCOA(string queryString, ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = false;
            try
            {
                int ID = Convert.ToInt32(queryString);
                TreasuryBookCOADao entityDao = new TreasuryBookCOADao(ctx);
                foreach (ProceedEntity row in ListProceedEntity)
                {
                    if (row.Status == ProceedEntity.ProceedEntityStatus.Add)
                    {
                        TreasuryBookCOA entity = new TreasuryBookCOA();
                        entity.BookID = ID;
                        entity.GLAccount = Convert.ToInt32(row.ID);
                        entityDao.Insert(entity);
                    }
                    else
                        entityDao.Delete(ID, Convert.ToInt32(row.ID));
                }
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        
        private void InitializeListMatrix(string type, string queryString)
        {
            switch (type)
            {
                case "GLAccountPayable": InitializeGLAccountPayable(queryString); break;
                case "GLWarehouseProductLineAccountDt": InitializeGLWarehouseProductLineAccountDt(queryString); break;
                case "ModuleMenu": InitializeModuleMenu(queryString); break;
                case "TreasuryBookCOA": InitializeTreasuryBookCOA(queryString); break;
            }
        }

        private bool SaveMatrix(string type, string queryString, ref string errMessage)
        {
            switch (type)
            {
                case "GLAccountPayable": return SaveGLAccountPayable(queryString, ref errMessage);
                case "GLWarehouseProductLineAccountDt": return SaveGLWarehouseProductLineAccountDt(queryString, ref errMessage);
                case "ModuleMenu": return SaveModuleMenu(queryString, ref errMessage);
                case "TreasuryBookCOA": return SaveTreasuryBookCOA(queryString, ref errMessage);
            }                
            return false;
        }



        protected int PageCountAvailable = 1;
        protected int PageCountSelected = 1;

        public override void InitializeDataControl(string param)
        {
            ListProceedEntity.Clear();
            hdnParam.Value = param;

            string type = param.Split('|')[0];
            string[] temp = param.Split('|').Skip(1).ToArray();
            string queryString = String.Join("|", temp);

            InitializeListMatrix(type, queryString);

            BindGridAvailable(1, true, ref PageCountAvailable);
            BindGridSelected(1, true, ref PageCountSelected);
        }

        #region Available
        private void BindGridAvailable(int pageIndex, bool isCountPageCount, ref int pageCount, List<string> listCheckedAvailable = null)
        {
            List<CMatrix> lstEntity = ListAvailable.Where(p => p.Name.Contains(hdnAvailableSearchText.Value)).ToList();
            if (isCountPageCount)
            {
                pageCount = Helper.GetPageCount(lstEntity.Count, Constant.GridViewPageSize.GRID_MATRIX);
            }
            List<CMatrix> lst = lstEntity.Skip((pageIndex - 1) * 10).Take(10).ToList();
            foreach (CMatrix mtx in lst)
            {
                if (listCheckedAvailable != null && listCheckedAvailable.Contains(mtx.ID.ToString()))
                {
                    mtx.IsChecked = true;
                    listCheckedAvailable.Remove(mtx.ID.ToString());
                }
                else
                    mtx.IsChecked = false;
            }

            grdAvailable.DataSource = lst;
            grdAvailable.DataBind();
        }

        protected void cbpMatrixAvailable_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            List<string> listCheckedAvailable = hdnCheckedAvailable.Value.Split(';').ToList();
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    string[] newCheckedAvailable = param[2].Split(';');
                    foreach (string a in newCheckedAvailable)
                    {
                        if (a != "")
                            listCheckedAvailable.Add(a);
                    }

                    BindGridAvailable(Convert.ToInt32(param[1]), false, ref pageCount, listCheckedAvailable);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridAvailable(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpCheckedAvailable"] = string.Join(";", listCheckedAvailable.ToArray());
        }
        #endregion

        #region Selected
        private void BindGridSelected(int pageIndex, bool isCountPageCount, ref int pageCount, List<string> listCheckedSelected = null)
        {
            List<CMatrix> lstEntity = ListSelected.Where(p => p.Name.Contains(hdnSelectedSearchText.Value)).ToList();
            if (isCountPageCount)
            {
                pageCount = Helper.GetPageCount(lstEntity.Count, Constant.GridViewPageSize.GRID_MATRIX);
            }
            List<CMatrix> lst = lstEntity.Skip((pageIndex - 1) * 10).Take(10).ToList();
            foreach (CMatrix mtx in lst)
            {
                if (listCheckedSelected != null && listCheckedSelected.Contains(mtx.ID.ToString()))
                {
                    mtx.IsChecked = true;
                    listCheckedSelected.Remove(mtx.ID.ToString());
                }
                else
                    mtx.IsChecked = false;
            }

            grdSelected.DataSource = lst;
            grdSelected.DataBind();
        }

        protected void cbpMatrixSelected_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            List<string> listCheckedSelected = hdnCheckedSelected.Value.Split(';').ToList();
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    string[] newCheckedSelected = param[2].Split(';');
                    foreach (string a in newCheckedSelected)
                    {
                        if (a != "")
                            listCheckedSelected.Add(a);
                    }

                    BindGridSelected(Convert.ToInt32(param[1]), false, ref pageCount, listCheckedSelected);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridSelected(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpCheckedSelected"] = string.Join(";", listCheckedSelected.ToArray());

        }
        #endregion


        protected void cbpMatrixProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] param = e.Parameter.Split('|');
            string result = param[0] + "|";
            if (param[0] == "rightAll")
            {
                List<CMatrix> lst = ListAvailable.Where(p => p.Name.Contains(hdnAvailableSearchText.Value)).ToList();
                foreach (CMatrix row in lst)
                {
                    ListSelected.Add(row); 

                    ProceedEntity obj = ListProceedEntity.FirstOrDefault(p => p.ID == row.ID.ToString());
                    if (obj != null)
                        ListProceedEntity.Remove(obj);
                    else
                    {
                        ProceedEntity proceedEntity = new ProceedEntity();
                        proceedEntity.ID = row.ID.ToString();
                        proceedEntity.Status = ProceedEntity.ProceedEntityStatus.Add;
                        ListProceedEntity.Add(proceedEntity);
                    }
                }
                ListSelected = ListSelected.OrderBy(p => p.Name).ToList();
                ListAvailable.RemoveAll(x => x.Name.Contains(hdnAvailableSearchText.Value));
            }
            else if (param[0] == "right")
            {
                List<string> listCheckedAvailable = hdnCheckedAvailable.Value.Split(';').ToList();
                string[] newCheckedAvailable = param[1].Split(';');
                foreach (string a in newCheckedAvailable)
                {
                    if (a != "")
                        listCheckedAvailable.Add(a);
                }

                foreach (string value in listCheckedAvailable)
                {
                    if (value != "")
                    {
                        ProceedEntity obj = ListProceedEntity.FirstOrDefault(p => p.ID == value.ToString());
                        if (obj != null)
                            ListProceedEntity.Remove(obj);
                        else
                        {
                            ProceedEntity proceedEntity = new ProceedEntity();
                            proceedEntity.ID = value.ToString();
                            proceedEntity.Status = ProceedEntity.ProceedEntityStatus.Add;
                            ListProceedEntity.Add(proceedEntity);
                        }

                        CMatrix removeObj = ListAvailable.FirstOrDefault(p => p.ID.ToString() == value);
                        if (removeObj != null)
                        {
                            ListSelected.Add(removeObj);
                            ListAvailable.Remove(removeObj);
                        }
                    }
                }

                ListSelected = ListSelected.OrderBy(p => p.Name).ToList();
            }
            else if (param[0] == "left")
            {
                List<string> listCheckedSelected = hdnCheckedSelected.Value.Split(';').ToList();
                string[] newCheckedSelected = param[1].Split(';');
                foreach (string a in newCheckedSelected)
                {
                    if (a != "")
                        listCheckedSelected.Add(a);
                }

                foreach (string value in listCheckedSelected)
                {
                    if (value != "")
                    {
                        ProceedEntity obj = ListProceedEntity.FirstOrDefault(p => p.ID == value.ToString());
                        if (obj != null)
                            ListProceedEntity.Remove(obj);
                        else
                        {
                            ProceedEntity proceedEntity = new ProceedEntity();
                            proceedEntity.ID = value.ToString();
                            proceedEntity.Status = ProceedEntity.ProceedEntityStatus.Remove;
                            ListProceedEntity.Add(proceedEntity);
                        }

                        CMatrix removeObj = ListSelected.FirstOrDefault(p => p.ID.ToString() == value);
                        if (removeObj != null)
                        {
                            ListAvailable.Add(removeObj);
                            ListSelected.Remove(removeObj);
                        }
                    }
                }

                ListAvailable = ListAvailable.OrderBy(p => p.Name).ToList();
            }
            else if (param[0] == "leftAll")
            {
                List<CMatrix> lst = ListSelected.Where(p => p.Name.Contains(hdnSelectedSearchText.Value)).ToList();
                foreach (CMatrix row in lst)
                {
                    ListAvailable.Add(row);

                    ProceedEntity obj = ListProceedEntity.FirstOrDefault(p => p.ID == row.ID.ToString());
                    if (obj != null)
                        ListProceedEntity.Remove(obj);
                    else
                    {
                        ProceedEntity proceedEntity = new ProceedEntity();
                        proceedEntity.ID = row.ID.ToString();
                        proceedEntity.Status = ProceedEntity.ProceedEntityStatus.Remove;
                        ListProceedEntity.Add(proceedEntity);
                    }
                }
                ListAvailable = ListAvailable.OrderBy(p => p.Name).ToList();
                ListSelected.RemoveAll(x => x.Name.Contains(hdnSelectedSearchText.Value));
            }
            else if (param[0] == "save")
            {
                string errMessage = "";
                string paramTemp = hdnParam.Value;

                string type = paramTemp.Split('|')[0];
                string[] temp = paramTemp.Split('|').Skip(1).ToArray();
                string queryString = String.Join("|", temp);

                if (SaveMatrix(type, queryString, ref errMessage))
                    result += "success";
                else
                    result += "fail|" + errMessage;
            }
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private const string SESSION_NAME_SELECTED_ENTITY = "SelectedEntity";
        private const string SESSION_NAME_AVAILABLE_ENTITY = "AvailableEntity";
        private const string SESSION_PROCEED_ENTITY = "ProceedEntity";

        #region Matrix       
        public static List<CMatrix> ListSelected
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_NAME_SELECTED_ENTITY] == null) HttpContext.Current.Session[SESSION_NAME_SELECTED_ENTITY] = new List<CMatrix>();
                return (List<CMatrix>)HttpContext.Current.Session[SESSION_NAME_SELECTED_ENTITY];
            }
            set
            {
                HttpContext.Current.Session[SESSION_NAME_SELECTED_ENTITY] = value;
            }
        }
        public static List<CMatrix> ListAvailable
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_NAME_AVAILABLE_ENTITY] == null) HttpContext.Current.Session[SESSION_NAME_AVAILABLE_ENTITY] = new List<CMatrix>();
                return (List<CMatrix>)HttpContext.Current.Session[SESSION_NAME_AVAILABLE_ENTITY];
            }
            set
            {
                HttpContext.Current.Session[SESSION_NAME_AVAILABLE_ENTITY] = value;
            }
        }

        private static List<ProceedEntity> ListProceedEntity
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_PROCEED_ENTITY] == null) HttpContext.Current.Session[SESSION_PROCEED_ENTITY] = new List<ProceedEntity>();
                return (List<ProceedEntity>)HttpContext.Current.Session[SESSION_PROCEED_ENTITY];
            }
            set
            {
                HttpContext.Current.Session[SESSION_PROCEED_ENTITY] = value;
            }
        }

        public static List<CMatrix> SelectListAvailableEntity()
        {
            return ListAvailable;
        }
        public static List<CMatrix> SelectListSelectedEntity()
        {
            return ListSelected;
        }
        #endregion
    }
}
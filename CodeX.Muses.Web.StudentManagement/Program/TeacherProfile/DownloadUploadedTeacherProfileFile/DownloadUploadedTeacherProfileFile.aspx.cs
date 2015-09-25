using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Core.Dal;
using System.Data;
using CodeX.Common;
using System.Text.RegularExpressions;
using System.IO;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class DownloadUploadedTeacherProfileFile : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        List<TeacherProfile> lstTp = new List<TeacherProfile>();

        public class TeacherProfile
        {
            Int32 _NIK;
            public Int32 NIK
            {
                get { return _NIK; }
                set { _NIK = value; }
            }

            String _Name;
            public String Name
            {
                get { return _Name; }
                set { _Name = value; }
            }

            String _DataFromFile;
            public String DataFromFile
            {
                get { return _DataFromFile; }
                set { _DataFromFile = value; }
            }

            #region Profil Talent
            String _Talent;
            String _IQ;
            String _Drive;
            String _Komunikasi;
            String _Loyalitas;
            String _Teliti;
            String _Konsistensi;

            public String Talent
            {
                get { return _Talent; }
                set { _Talent = value; }
            }

            public String IQ
            {
                get { return _IQ; }
                set { _IQ = value; }
            }

            public String Drive
            {
                get { return _Drive; }
                set { _Drive = value; }
            }

            public String Komunikasi
            {
                get { return _Komunikasi; }
                set { _Komunikasi = value; }
            }

            public String Loyalitas
            {
                get { return _Loyalitas; }
                set { _Loyalitas = value; }
            }

            public String Teliti
            {
                get { return _Teliti; }
                set { _Teliti = value; }
            }

            public String Konsistensi
            {
                get { return _Konsistensi; }
                set { _Konsistensi = value; }
            }
            #endregion

            #region Kompetensi Pedagogik & Profesional
            #region Pedagogik
            String _Col1;
            String _Col1Score;

            public String Col1Score
            {
                get { return _Col1Score; }
                set { _Col1Score = value; }
            }
            String _Col2;
            String _Col2Score;

            public String Col2Score
            {
                get { return _Col2Score; }
                set { _Col2Score = value; }
            }
            String _Col3;
            String _Col3Score;

            public String Col3Score
            {
                get { return _Col3Score; }
                set { _Col3Score = value; }
            }
            String _Col4;
            String _Col4Score;

            public String Col4Score
            {
                get { return _Col4Score; }
                set { _Col4Score = value; }
            }
            String _Col5;
            String _Col5Score;

            public String Col5Score
            {
                get { return _Col5Score; }
                set { _Col5Score = value; }
            }
            String _Col6;
            String _Col6Score;

            public String Col6Score
            {
                get { return _Col6Score; }
                set { _Col6Score = value; }
            }
            String _Col7;
            String _Col7Score;

            public String Col7Score
            {
                get { return _Col7Score; }
                set { _Col7Score = value; }
            }
            String _Col8;
            String _Col8Score;

            public String Col8Score
            {
                get { return _Col8Score; }
                set { _Col8Score = value; }
            }
            String _Col9;
            String _Col9Score;

            public String Col9Score
            {
                get { return _Col9Score; }
                set { _Col9Score = value; }
            }
            String _PedagogikScore;

            public String PedagogikScore
            {
                get { return _PedagogikScore; }
                set { _PedagogikScore = value; }
            }

            String _PedagogikScoreInPercentage;

            public String PedagogikScoreInPercentage
            {
                get { return _PedagogikScoreInPercentage; }
                set { _PedagogikScoreInPercentage = value; }
            }

            String _PedagogikResult;

            public String PedagogikResult
            {
                get { return _PedagogikResult; }
                set { _PedagogikResult = value; }
            }

            public String Col1
            {
                get { return _Col1; }
                set { _Col1 = value; }
            }

            public String Col2
            {
                get { return _Col2; }
                set { _Col2 = value; }
            }

            public String Col3
            {
                get { return _Col3; }
                set { _Col3 = value; }
            }

            public String Col4
            {
                get { return _Col4; }
                set { _Col4 = value; }
            }

            public String Col5
            {
                get { return _Col5; }
                set { _Col5 = value; }
            }

            public String Col6
            {
                get { return _Col6; }
                set { _Col6 = value; }
            }

            public String Col7
            {
                get { return _Col7; }
                set { _Col7 = value; }
            }

            public String Col8
            {
                get { return _Col8; }
                set { _Col8 = value; }
            }

            public String Col9
            {
                get { return _Col9; }
                set { _Col9 = value; }
            }
            #endregion
            #region Aspek Profesional
            String _Subject;

            public String Subject
            {
                get { return _Subject; }
                set { _Subject = value; }
            }

            String _Score;

            public String Score
            {
                get { return _Score; }
                set { _Score = value; }
            }

            String _ScoreInPercentage;

            public String ScoreInPercentage
            {
                get { return _ScoreInPercentage; }
                set { _ScoreInPercentage = value; }
            }

            String _Mutu;

            public String Mutu
            {
                get { return _Mutu; }
                set { _Mutu = value; }
            }
            #endregion
            #endregion

            #region Profil Menurut Siswa
            #region Aspek Kepribadian
            String _Discipline;

            public String Discipline
            {
                get { return _Discipline; }
                set { _Discipline = value; }
            }
            String _DisciplineScore;

            public String DisciplineScore
            {
                get { return _DisciplineScore; }
                set { _DisciplineScore = value; }
            }
            String _Atmosphere;

            public String Atmosphere
            {
                get { return _Atmosphere; }
                set { _Atmosphere = value; }
            }
            String _AtmosphereScore;

            public String AtmosphereScore
            {
                get { return _AtmosphereScore; }
                set { _AtmosphereScore = value; }
            }
            String _Encourage;

            public String Encourage
            {
                get { return _Encourage; }
                set { _Encourage = value; }
            }
            String _EncourageScore;

            public String EncourageScore
            {
                get { return _EncourageScore; }
                set { _EncourageScore = value; }
            }
            String _RoleModel;

            public String RoleModel
            {
                get { return _RoleModel; }
                set { _RoleModel = value; }
            }
            String _RoleModelScore;

            public String RoleModelScore
            {
                get { return _RoleModelScore; }
                set { _RoleModelScore = value; }
            }
            String _Inspirator;

            public String Inspirator
            {
                get { return _Inspirator; }
                set { _Inspirator = value; }
            }
            String _InspiratorScore;

            public String InspiratorScore
            {
                get { return _InspiratorScore; }
                set { _InspiratorScore = value; }
            }
            String _Sympathy;

            public String Sympathy
            {
                get { return _Sympathy; }
                set { _Sympathy = value; }
            }
            String _SympathyScore;

            public String SympathyScore
            {
                get { return _SympathyScore; }
                set { _SympathyScore = value; }
            }
            String _PersonalityAverage;

            public String PersonalityAverage
            {
                get { return _PersonalityAverage; }
                set { _PersonalityAverage = value; }
            }
            String _PersonalityResult;

            public String PersonalityResult
            {
                get { return _PersonalityResult; }
                set { _PersonalityResult = value; }
            }
            #endregion
            #region Aspek Pedagogik
            String _DeliveryOfMaterial;

            public String DeliveryOfMaterial
            {
                get { return _DeliveryOfMaterial; }
                set { _DeliveryOfMaterial = value; }
            }
            String _DeliveryOfMaterialScore;

            public String DeliveryOfMaterialScore
            {
                get { return _DeliveryOfMaterialScore; }
                set { _DeliveryOfMaterialScore = value; }
            }
            String _Kindess;

            public String Kindess
            {
                get { return _Kindess; }
                set { _Kindess = value; }
            }
            String _KindessScore;

            public String KindessScore
            {
                get { return _KindessScore; }
                set { _KindessScore = value; }
            }
            String _TempatCurhat;

            public String TempatCurhat
            {
                get { return _TempatCurhat; }
                set { _TempatCurhat = value; }
            }
            String _TempatCurhatScore;

            public String TempatCurhatScore
            {
                get { return _TempatCurhatScore; }
                set { _TempatCurhatScore = value; }
            }
            String _SiswaBertanya;

            public String SiswaBertanya
            {
                get { return _SiswaBertanya; }
                set { _SiswaBertanya = value; }
            }
            String _SiswaBertanyaScore;

            public String SiswaBertanyaScore
            {
                get { return _SiswaBertanyaScore; }
                set { _SiswaBertanyaScore = value; }
            }
            String _AnswerQuestion;

            public String AnswerQuestion
            {
                get { return _AnswerQuestion; }
                set { _AnswerQuestion = value; }
            }
            String _AnswerQuestionScore;

            public String AnswerQuestionScore
            {
                get { return _AnswerQuestionScore; }
                set { _AnswerQuestionScore = value; }
            }

            String _PedagogikSiswaAverage;

            public String PedagogikSiswaAverage
            {
                get { return _PedagogikSiswaAverage; }
                set { _PedagogikSiswaAverage = value; }
            }
            String _PedagogikSiswaResult;

            public String PedagogikSiswaResult
            {
                get { return _PedagogikSiswaResult; }
                set { _PedagogikSiswaResult = value; }
            }
            #endregion
            #region Aspek Profesional
            String _ProCol1;

            public String ProCol1
            {
                get { return _ProCol1; }
                set { _ProCol1 = value; }
            }
            String _ProCol1Score;

            public String ProCol1Score
            {
                get { return _ProCol1Score; }
                set { _ProCol1Score = value; }
            }
            String _ProCol2;

            public String ProCol2
            {
                get { return _ProCol2; }
                set { _ProCol2 = value; }
            }
            String _ProCol2Score;

            public String ProCol2Score
            {
                get { return _ProCol2Score; }
                set { _ProCol2Score = value; }
            }
            String _ProCol3;

            public String ProCol3
            {
                get { return _ProCol3; }
                set { _ProCol3 = value; }
            }
            String _ProCol3Score;

            public String ProCol3Score
            {
                get { return _ProCol3Score; }
                set { _ProCol3Score = value; }
            }
            String _ProCol4;

            public String ProCol4
            {
                get { return _ProCol4; }
                set { _ProCol4 = value; }
            }
            String _ProCol4Score;

            public String ProCol4Score
            {
                get { return _ProCol4Score; }
                set { _ProCol4Score = value; }
            }
            String _ProCol5;

            public String ProCol5
            {
                get { return _ProCol5; }
                set { _ProCol5 = value; }
            }
            String _ProCol5Score;

            public String ProCol5Score
            {
                get { return _ProCol5Score; }
                set { _ProCol5Score = value; }
            }
            String _ProCol6;

            public String ProCol6
            {
                get { return _ProCol6; }
                set { _ProCol6 = value; }
            }
            String _ProCol6Score;

            public String ProCol6Score
            {
                get { return _ProCol6Score; }
                set { _ProCol6Score = value; }
            }
            String _ProCol7;

            public String ProCol7
            {
                get { return _ProCol7; }
                set { _ProCol7 = value; }
            }
            String _ProCol7Score;

            public String ProCol7Score
            {
                get { return _ProCol7Score; }
                set { _ProCol7Score = value; }
            }
            String _ProCol8;

            public String ProCol8
            {
                get { return _ProCol8; }
                set { _ProCol8 = value; }
            }
            String _ProCol8Score;

            public String ProCol8Score
            {
                get { return _ProCol8Score; }
                set { _ProCol8Score = value; }
            }
            String _ProCol9;

            public String ProCol9
            {
                get { return _ProCol9; }
                set { _ProCol9 = value; }
            }
            String _ProCol9Score;

            public String ProCol9Score
            {
                get { return _ProCol9Score; }
                set { _ProCol9Score = value; }
            }
            String _ProCol10;

            public String ProCol10
            {
                get { return _ProCol10; }
                set { _ProCol10 = value; }
            }
            String _ProCol10Score;

            public String ProCol10Score
            {
                get { return _ProCol10Score; }
                set { _ProCol10Score = value; }
            }

            String _ProAverage;

            public String ProAverage
            {
                get { return _ProAverage; }
                set { _ProAverage = value; }
            }
            String _ProResult;

            public String ProResult
            {
                get { return _ProResult; }
                set { _ProResult = value; }
            }
            #endregion
            #region Kompetensi Sosial
            String _SosCol1;

            public String SosCol1
            {
                get { return _SosCol1; }
                set { _SosCol1 = value; }
            }
            String _SosCol1Score;

            public String SosCol1Score
            {
                get { return _SosCol1Score; }
                set { _SosCol1Score = value; }
            }

            String _SosCol2;

            public String SosCol2
            {
                get { return _SosCol2; }
                set { _SosCol2 = value; }
            }
            String _SosCol2Score;

            public String SosCol2Score
            {
                get { return _SosCol2Score; }
                set { _SosCol2Score = value; }
            }
            String _SosCol3;

            public String SosCol3
            {
                get { return _SosCol3; }
                set { _SosCol3 = value; }
            }
            String _SosCol3Score;

            public String SosCol3Score
            {
                get { return _SosCol3Score; }
                set { _SosCol3Score = value; }
            }
            String _SosCol4;

            public String SosCol4
            {
                get { return _SosCol4; }
                set { _SosCol4 = value; }
            }
            String _SosCol4Score;

            public String SosCol4Score
            {
                get { return _SosCol4Score; }
                set { _SosCol4Score = value; }
            }
            String _SosCol5;

            public String SosCol5
            {
                get { return _SosCol5; }
                set { _SosCol5 = value; }
            }
            String _SosCol5Score;

            public String SosCol5Score
            {
                get { return _SosCol5Score; }
                set { _SosCol5Score = value; }
            }

            String _SosColAverage;

            public String SosColAverage
            {
                get { return _SosColAverage; }
                set { _SosColAverage = value; }
            }
            String _SosColResult;

            public String SosColResult
            {
                get { return _SosColResult; }
                set { _SosColResult = value; }
            }
            #endregion
            #endregion

            #region OpenQuestion
            String _Question1;

            public String Question1
            {
                get { return _Question1; }
                set { _Question1 = value; }
            }
            String _Question2;

            public String Question2
            {
                get { return _Question2; }
                set { _Question2 = value; }
            }
            String _Question3;

            public String Question3
            {
                get { return _Question3; }
                set { _Question3 = value; }
            }
            #endregion
        }

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.TEACHER_PROFILE;
        }

        protected override void InitializeDataControl()
        {
            hdnTransactionCode.Value = Constant.TransactionCode.TEACHER_PROFILE;

            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();

            List<StandardCode> lstVar = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SCHOOL_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboGrade, lstVar, "StandardCodeName", "StandardCodeID");
            cboGrade.SelectedIndex = 0;

            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.VOID));
            Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
            if (selectedSchoolPeriod == null)
                cboSchoolPeriod.SelectedIndex = 0;
            else
                cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();

            BindGridView(1, true, ref PageCount, ref RowCount);


            //Helper.SetControlEntrySetting(txtQuantity, new ControlEntrySetting(true, true, true), "mpTrx");
            //Helper.SetControlEntrySetting(txtItemCode, new ControlEntrySetting(true, true, true), "mpTrx");
            //Helper.SetControlEntrySetting(cboItemUnit, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnTransactionID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtTransactionNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(cboGrade, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));

            SetControlEntrySetting(txtTransactionDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnPageCount.Value = "0";
            hdnIsEditable.Value = "1";
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        protected string GetFilterExpression()
        {
            string filterExpression = String.Format("TransactionCode = '{0}'", hdnTransactionCode.Value);
            if (hdnRecordFilterExpression.Value != "")
                filterExpression += string.Format(" AND {0}", hdnRecordFilterExpression.Value);
            return filterExpression;
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvTransTeacherProfileHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vTransTeacherProfileHd entity = BusinessLayer.GetvTransTeacherProfileHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvTransTeacherProfileHdRowIndex(filterExpression, keyValue, "TransactionID DESC");
            vTransTeacherProfileHd entity = BusinessLayer.GetvTransTeacherProfileHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vTransTeacherProfileHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                hdnIsEditable.Value = "0";
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
            }
            else
                hdnIsEditable.Value = "1";

            //if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN && entity.GCTransactionStatus != Constant.TransactionStatus.VOID)
            //    hdnPrintStatus.Value = "true";
            //else
            //    hdnPrintStatus.Value = "false";

            hdnTransactionID.Value = entity.TransactionID.ToString();
            txtTransactionNo.Text = entity.TransactionNo;
            txtTransactionDate.Text = entity.TransactionDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            cboGrade.Value = entity.GCSchoolType.ToString();
            txtNotes.Text = entity.Remarks;

            BindGridView(1, true, ref PageCount, ref RowCount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnTransactionID.Value != "")
                filterExpression = string.Format("TransactionID = {0}", hdnTransactionID.Value);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvTransTeacherProfileDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vTransTeacherProfileDt> lstEntity = BusinessLayer.GetvTransTeacherProfileDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "TeacherName ASC");
            hdnPageCount.Value = pageCount.ToString();
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save & Edit Header
        private void ControlToEntityHd(TransTeacherProfileHd entityHd) 
        {
            entityHd.TransactionDate = Helper.GetDatePickerValue(txtTransactionDate.Text);
            entityHd.GCSchoolType = cboGrade.Value.ToString();
            entityHd.Remarks = txtNotes.Text;
        }

        public void SaveTransTeacherProfileHd(IDbContext ctx, ref int TransactionID)
        {
            TransTeacherProfileHdDao ttpDao = new TransTeacherProfileHdDao(ctx);
            if (hdnTransactionID.Value == "0")
            {
                TransTeacherProfileHd ttphd = new TransTeacherProfileHd();
                ttphd.TransactionNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.TEACHER_PROFILE, DateTime.Now, ctx);
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                ttphd.TransactionCode = Constant.TransactionCode.TEACHER_PROFILE;
                ttphd.TransactionDate = Helper.GetDatePickerValue(txtTransactionDate.Text);
                ttphd.GCSchoolType = cboGrade.Value.ToString();
                ttphd.Remarks = txtNotes.Text;
                ttphd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                ttphd.CreatedBy = AppSession.UserLogin.UserID;
                ttpDao.Insert(ttphd);
                TransactionID = BusinessLayer.GetTransTeacherProfileHdMaxID(ctx);
            }
            else
            {
                TransactionID = Convert.ToInt32(hdnTransactionID.Value);
            }
        }
        
        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            //IDbContext ctx = DbFactory.Configure(true);
            try
            {
                //int OrderID = 0;
                //SaveItemRequestHd(ctx, ref OrderID);
                //retval = OrderID.ToString();
                //ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                //ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally
            {
                //ctx.Close();
            }
            return result;
        }

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            try
            {
                //ItemRequestHd entity = BusinessLayer.GetItemRequestHd(Convert.ToInt32(hdnTransactionID.Value));
                //ControlToEntityHd(entity);
                //entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                //BusinessLayer.UpdateItemRequestHd(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        #region Approved Proposed Void Entity
        protected override bool OnApproveRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransTeacherProfileHdDao ttphdDao = new TransTeacherProfileHdDao(ctx);
            TransTeacherProfileDtDao ttpdtDao = new TransTeacherProfileDtDao(ctx);
            try
            {
                TransTeacherProfileHd entityHd = ttphdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                ControlToEntityHd(entityHd);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                ttphdDao.Update(entityHd);

                string filterExpression = String.Format("TransactionID = {0} AND GCTeacherDetailStatus != '{0}'", hdnTransactionID.Value, Constant.TransactionStatus.VOID);
                List<TransTeacherProfileDt> lstTransTeacherProfileDt = BusinessLayer.GetTransTeacherProfileDtList(filterExpression, ctx);
                foreach (TransTeacherProfileDt entityDt in lstTransTeacherProfileDt)
                {
                    entityDt.GCTeacherDetailStatus = Constant.TransactionStatus.APPROVED;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    ttpdtDao.Update(entityDt);
                }

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnProposeRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransTeacherProfileHdDao ttphdDao = new TransTeacherProfileHdDao(ctx);
            TransTeacherProfileDtDao ttpdtDao = new TransTeacherProfileDtDao(ctx);
            try
            {
                TransTeacherProfileHd entityHd = ttphdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                ControlToEntityHd(entityHd);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.PROCESSED;
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                ttphdDao.Update(entityHd);

                string filterExpression = String.Format("TransactionID = {0} AND GCTeacherDetailStatus != '{0}'", hdnTransactionID.Value, Constant.TransactionStatus.VOID);
                List<TransTeacherProfileDt> lstTransTeacherProfileDt = BusinessLayer.GetTransTeacherProfileDtList(filterExpression, ctx);
                foreach (TransTeacherProfileDt entityDt in lstTransTeacherProfileDt)
                {
                    entityDt.GCTeacherDetailStatus = Constant.TransactionStatus.PROCESSED;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    ttpdtDao.Update(entityDt);
                }

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnVoidRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransTeacherProfileHdDao ttphdDao = new TransTeacherProfileHdDao(ctx);
            TransTeacherProfileDtDao ttpdtDao = new TransTeacherProfileDtDao(ctx);
            try
            {
                TransTeacherProfileHd entityHd = ttphdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                ControlToEntityHd(entityHd);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                ttphdDao.Update(entityHd);

                string filterExpression = String.Format("TransactionID = {0} AND GCTeacherDetailStatus != '{0}'", hdnTransactionID.Value, Constant.TransactionStatus.VOID);
                List<TransTeacherProfileDt> lstTransTeacherProfileDt = BusinessLayer.GetTransTeacherProfileDtList(filterExpression, ctx);
                foreach (TransTeacherProfileDt entityDt in lstTransTeacherProfileDt)
                {
                    entityDt.GCTeacherDetailStatus = Constant.TransactionStatus.VOID;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    ttpdtDao.Update(entityDt);
                }

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion

        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            int TransactionID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    TransactionID = Convert.ToInt32(hdnTransactionID.Value);
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage, ref TransactionID))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                TransactionID = Convert.ToInt32(hdnTransactionID.Value);
                if (OnDeleteEntityDt(ref errMessage, TransactionID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else if (param[0] == "upload") 
            {
                if (OnUploadAddRecordEntityDt(ref errMessage, ref TransactionID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpTransactionID"] = TransactionID.ToString();
        }

        private void ControlToEntity(ItemRequestDt entityDt)
        {
            //entityDt.ItemID = Convert.ToInt32(hdnItemID.Value);
            //entityDt.Quantity = Convert.ToDecimal(txtQuantity.Text);
            //entityDt.GCItemUnit = cboItemUnit.Value.ToString();
            //entityDt.GCBaseUnit = hdnGCBaseUnit.Value;
            //entityDt.ConversionFactor = Convert.ToDecimal(hdnItemUnitValue.Value);
            //entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
        }

        private bool OnUploadAddRecordEntityDt(ref string errMessage, ref int TransactionID) 
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                String data = GetDataFromFile();
                UploadFile(data);
                OnUploadAddRecord(ctx, ref TransactionID);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally 
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int OrderID)
        {
            bool result = true;
            //IDbContext ctx = DbFactory.Configure(true);
            //ItemRequestDtDao entityDtDao = new ItemRequestDtDao(ctx);
            try
            {
                //SaveItemRequestHd(ctx, ref OrderID);
                //ItemRequestDt entityDt = new ItemRequestDt();
                //ControlToEntity(entityDt);
                //entityDt.ItemRequestID = OrderID;
                //entityDt.CreatedBy = AppSession.UserLogin.UserID;
                //entityDtDao.Insert(entityDt);
                //ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                //ctx.RollBackTransaction();
            }
            finally
            {
                //ctx.Close();
            }
            return result;
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            //ItemRequestDtDao entityDtDao = new ItemRequestDtDao(ctx);
            try
            {
                //ItemRequestDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                //ControlToEntity(entityDt);
                //entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                //entityDtDao.Update(entityDt);
                //ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                //ctx.RollBackTransaction();
            }
            finally
            {
                //ctx.Close();
            }
            return result;
        }

        private bool OnDeleteEntityDt(ref string errMessage, int ID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            //ItemRequestDtDao entityDtDao = new ItemRequestDtDao(ctx);
            try
            {
                //ItemRequestDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                //entityDt.IsDeleted = true;
                //entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                //entityDtDao.Update(entityDt);
                //ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                //result = false;
            }
            finally
            {
                //ctx.Close();
            }
            return result;
        }
        #endregion

        #region CallBack Trigger
        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
        #endregion

        #region Upload Data
        public String GetDataFromFile()
        {
            string imageData = hdnUploadedFile1.Value;
            if (imageData != "")
            {
                string[] parts = Regex.Split(imageData, ",").Skip(1).ToArray();
                imageData = String.Join(",", parts);
            }

            byte[] data = Convert.FromBase64String(imageData);
            var stream = new StreamReader(new MemoryStream(data));
            string text = stream.ReadToEnd();
            return text;
        }
        public void UploadFile(String data)
        {
            try
            {
                data = data.Replace("\r", "");
                List<String> lstData = data.Split('\n').ToList();
                lstData.Remove("");
                foreach (String temp in lstData.Skip(4))
                {
                    String[] obj = temp.Split(',');
                    TeacherProfile tp = new TeacherProfile();
                    tp.NIK = Convert.ToInt32(obj[0]);
                    tp.Name = obj[1];

                    if (cboGrade.Value.ToString() == Constant.SchoolTypeName.TK)
                    {
                        #region TK
                        #region Kompetensi Pedagogik
                        tp.Col1 = obj[2];
                        tp.Col1Score = obj[3];
                        tp.Col2 = obj[4];
                        tp.Col2Score = obj[5];
                        tp.Col3 = obj[6];
                        tp.Col3Score = obj[7];
                        tp.PedagogikScore = obj[8];
                        tp.PedagogikScoreInPercentage = obj[9];
                        tp.PedagogikResult = obj[10];
                        tp.DataFromFile = String.Join(",", obj.Skip(2).Take(9).Select(x => x.Replace("%", "")));
                        #endregion

                        #region Profil Talent
                        tp.Talent = obj[12];
                        tp.IQ = obj[13];
                        tp.Drive = obj[15];
                        tp.Komunikasi = obj[16];
                        tp.Loyalitas = obj[17];
                        tp.Teliti = obj[18];
                        tp.Konsistensi = obj[19].Replace("%", "");
                        #endregion

                        #region Presensi
                        #endregion
                        #endregion
                    }
                    else if (cboGrade.Value.ToString() == Constant.SchoolTypeName.SD)
                    {
                        #region SD
                        #region Kompetensi Pedagogik & Profesional
                        #region Pedagogik
                        tp.Col1 = obj[2];
                        tp.Col1Score = obj[3];
                        tp.Col2 = obj[4];
                        tp.Col2Score = obj[5];
                        tp.Col3 = obj[6];
                        tp.Col3Score = obj[7];
                        tp.Col4 = obj[8];
                        tp.Col4Score = obj[9];
                        tp.Col5 = obj[10];
                        tp.Col5Score = obj[11];
                        tp.Col6 = obj[12];
                        tp.Col6Score = obj[13];
                        tp.Col7 = obj[14];
                        tp.Col7Score = obj[15];
                        tp.PedagogikScore = obj[16];
                        tp.PedagogikScoreInPercentage = obj[17];
                        tp.PedagogikResult = obj[18];
                        tp.DataFromFile = String.Join(",", obj.Skip(2).Take(14).Select(x => x.Replace("%", "")));
                        #endregion

                        #region Kompetensi Profesional
                        tp.Subject = obj[20];
                        tp.Score = obj[21];
                        tp.ScoreInPercentage = obj[22];
                        tp.Mutu = obj[23];
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(21).Take(2).Select(x => x.Replace("%", "")));
                        #endregion
                        #endregion

                        #region Profil Talent
                        tp.Talent = obj[25];
                        tp.IQ = obj[26];
                        tp.Drive = obj[28];
                        tp.Komunikasi = obj[29];
                        tp.Loyalitas = obj[30];
                        tp.Teliti = obj[31];
                        tp.Konsistensi = obj[32].Replace("%", "");
                        #endregion

                        #region Profil Menurut Siswa
                        #region Aspek Kepribadian
                        tp.Discipline = obj[35];
                        tp.DisciplineScore = obj[36];
                        tp.Atmosphere = obj[37];
                        tp.AtmosphereScore = obj[38];
                        tp.Encourage = obj[39];
                        tp.EncourageScore = obj[40];
                        tp.RoleModel = obj[41];
                        tp.RoleModelScore = obj[42];
                        tp.Inspirator = obj[43];
                        tp.InspiratorScore = obj[44];
                        tp.Sympathy = obj[45];
                        tp.SympathyScore = obj[46];
                        tp.PersonalityAverage = obj[47];
                        tp.PersonalityResult = obj[48];
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(35).Take(12).Select(x => x.Replace("%", "")));
                        #endregion
                        #region Aspek Pedagogik
                        tp.DeliveryOfMaterial = obj[50];
                        tp.DeliveryOfMaterialScore = obj[51];
                        tp.Kindess = obj[52];
                        tp.KindessScore = obj[53];
                        tp.TempatCurhat = obj[54];
                        tp.TempatCurhatScore = obj[55];
                        tp.SiswaBertanya = obj[56];
                        tp.SiswaBertanyaScore = obj[57];
                        tp.AnswerQuestion = obj[58];
                        tp.AnswerQuestionScore = obj[58];
                        tp.PedagogikSiswaAverage = obj[60];
                        tp.PedagogikSiswaResult = obj[61];
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(50).Take(10).Select(x => x.Replace("%", "")));
                        #endregion
                        #region Kompetensi Profesional
                        tp.ProCol1 = obj[63];
                        tp.ProCol1Score = obj[64];
                        tp.ProCol2 = obj[65];
                        tp.ProCol2Score = obj[66];
                        tp.ProCol3 = obj[67];
                        tp.ProCol3Score = obj[68];
                        tp.ProCol4 = obj[69];
                        tp.ProCol4Score = obj[70];
                        tp.ProCol5 = obj[71];
                        tp.ProCol5Score = obj[72];
                        tp.ProCol6 = obj[73];
                        tp.ProCol6Score = obj[74];
                        tp.ProCol7 = obj[75];
                        tp.ProCol7Score = obj[76];
                        tp.ProCol8 = obj[77];
                        tp.ProCol8Score = obj[78];
                        tp.ProCol9 = obj[79];
                        tp.ProCol9Score = obj[80];
                        tp.ProCol10 = obj[81];
                        tp.ProCol10Score = obj[82];
                        tp.ProAverage = obj[83];
                        tp.ProResult = obj[84];
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(63).Take(20).Select(x => x.Replace("%", "")));
                        #endregion
                        #region Kompetensi Sosial
                        tp.SosCol1 = obj[86];
                        tp.SosCol1Score = obj[87];
                        tp.SosCol2 = obj[88];
                        tp.SosCol2Score = obj[89];
                        tp.SosCol3 = obj[90];
                        tp.SosCol3Score = obj[91];
                        tp.SosCol4 = obj[92];
                        tp.SosCol4Score = obj[93];
                        tp.SosCol5 = obj[94];
                        tp.SosCol5Score = obj[95];

                        tp.SosColAverage = obj[96];
                        tp.SosColResult = obj[97];
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(86).Take(10).Select(x => x.Replace("%", "")));
                        #endregion
                        #endregion

                        #region OpenQuestion
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(100).Take(2).Select(x => x.Replace("%", "")));
                        #endregion
                        #endregion
                    }
                    else if (cboGrade.Value.ToString() == Constant.SchoolTypeName.SMP)
                    {
                        #region Kompetensi Pedagogik & Profesional
                        #region Pedagogik
                        tp.Col1 = obj[2];
                        tp.Col1Score = obj[3];
                        tp.Col2 = obj[4];
                        tp.Col2Score = obj[5];
                        tp.Col3 = obj[6];
                        tp.Col3Score = obj[7];
                        tp.Col4 = obj[8];
                        tp.Col4Score = obj[9];
                        tp.Col5 = obj[10];
                        tp.Col5Score = obj[11];
                        tp.Col6 = obj[12];
                        tp.Col6Score = obj[13];
                        tp.Col7 = obj[14];
                        tp.Col7Score = obj[15];
                        tp.Col8 = obj[16];
                        tp.Col8Score = obj[17];
                        tp.Col9 = obj[18];
                        tp.Col9Score = obj[19];

                        tp.PedagogikScore = obj[20];
                        tp.PedagogikScoreInPercentage = obj[21];
                        tp.PedagogikResult = obj[22];
                        tp.DataFromFile = String.Join(",", obj.Skip(2).Take(18).Select(x => x.Replace("%", "")));
                        #endregion

                        #region Kompetensi Profesional
                        tp.Subject = obj[24];
                        tp.Score = obj[25];
                        tp.ScoreInPercentage = obj[26];
                        tp.Mutu = obj[27];
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(25).Take(2).Select(x => x.Replace("%", "")));
                        #endregion
                        #endregion

                        #region Profil Talent
                        tp.Talent = obj[29];
                        tp.IQ = obj[30];
                        tp.Drive = obj[32];
                        tp.Komunikasi = obj[33];
                        tp.Loyalitas = obj[34];
                        tp.Teliti = obj[35];
                        tp.Konsistensi = obj[36].Replace("%", "");
                        #endregion

                        #region Profil Menurut Siswa
                        #region Aspek Kepribadian
                        tp.Discipline = obj[39];
                        tp.DisciplineScore = obj[40];
                        tp.Atmosphere = obj[41];
                        tp.AtmosphereScore = obj[42];
                        tp.Encourage = obj[43];
                        tp.EncourageScore = obj[44];
                        tp.RoleModel = obj[45];
                        tp.RoleModelScore = obj[46];
                        tp.Inspirator = obj[47];
                        tp.InspiratorScore = obj[48];
                        tp.Sympathy = obj[49];
                        tp.SympathyScore = obj[50];
                        tp.PersonalityAverage = obj[51];
                        tp.PersonalityResult = obj[52];
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(39).Take(12).Select(x => x.Replace("%", "")));
                        #endregion

                        #region Aspek Pedagogik
                        tp.DeliveryOfMaterial = obj[54];
                        tp.DeliveryOfMaterialScore = obj[55];
                        tp.Kindess = obj[56];
                        tp.KindessScore = obj[57];
                        tp.TempatCurhat = obj[58];
                        tp.TempatCurhatScore = obj[59];
                        tp.SiswaBertanya = obj[60];
                        tp.SiswaBertanyaScore = obj[61];
                        tp.AnswerQuestion = obj[62];
                        tp.AnswerQuestionScore = obj[63];
                        tp.PedagogikSiswaAverage = obj[64];
                        tp.PedagogikSiswaResult = obj[65];
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(54).Take(10).Select(x => x.Replace("%", "")));
                        #endregion

                        #region Kompetensi Profesional
                        tp.ProCol1 = obj[67];
                        tp.ProCol1Score = obj[68];
                        tp.ProCol2 = obj[69];
                        tp.ProCol2Score = obj[70];
                        tp.ProCol3 = obj[71];
                        tp.ProCol3Score = obj[72];
                        tp.ProCol4 = obj[73];
                        tp.ProCol4Score = obj[74];
                        tp.ProCol5 = obj[75];
                        tp.ProCol5Score = obj[76];
                        tp.ProCol6 = obj[77];
                        tp.ProCol6Score = obj[78];
                        tp.ProCol7 = obj[79];
                        tp.ProCol7Score = obj[80];
                        tp.ProCol8 = obj[81];
                        tp.ProCol8Score = obj[82];
                        tp.ProCol9 = obj[83];
                        tp.ProCol9Score = obj[84];
                        tp.ProCol10 = obj[85];
                        tp.ProCol10Score = obj[86];
                        tp.ProAverage = obj[87];
                        tp.ProResult = obj[88];
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(67).Take(20).Select(x => x.Replace("%", "")));
                        #endregion

                        #region Kompetensi Sosial
                        tp.SosCol1 = obj[90];
                        tp.SosCol1Score = obj[91];
                        tp.SosCol2 = obj[92];
                        tp.SosCol2Score = obj[93];
                        tp.SosCol3 = obj[94];
                        tp.SosCol3Score = obj[95];
                        tp.SosCol4 = obj[96];
                        tp.SosCol4Score = obj[97];
                        tp.SosCol5 = obj[98];
                        tp.SosCol5Score = obj[99];

                        tp.SosColAverage = obj[100];
                        tp.SosColResult = obj[101];
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(90).Take(10).Select(x => x.Replace("%", "")));
                        #endregion
                        #endregion

                        #region OpenQuestion
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(103).Take(2).Select(x => x.Replace("%", "")));
                        #endregion
                    }

                    else if (cboGrade.Value.ToString() == Constant.SchoolTypeName.SMA)
                    {
                        #region Kompetensi Pedagogik & Profesional
                        #region Pedagogik
                        tp.Col1 = obj[2];
                        tp.Col1Score = obj[3];
                        tp.Col2 = obj[4];
                        tp.Col2Score = obj[5];
                        tp.Col3 = obj[6];
                        tp.Col3Score = obj[7];
                        tp.Col4 = obj[8];
                        tp.Col4Score = obj[9];
                        tp.Col5 = obj[10];
                        tp.Col5Score = obj[11];
                        tp.Col6 = obj[12];
                        tp.Col6Score = obj[13];
                        tp.Col7 = obj[14];
                        tp.Col7Score = obj[15];

                        tp.Col8 = obj[16];
                        tp.Col8Score = obj[17];

                        tp.Col9 = obj[18];
                        tp.Col9Score = obj[19];

                        tp.PedagogikScore = obj[20];
                        tp.PedagogikScoreInPercentage = obj[21];
                        tp.PedagogikResult = obj[22];
                        tp.DataFromFile = String.Join(",", obj.Skip(2).Take(18).Select(x => x.Replace("%", "")));
                        #endregion

                        #region Kompetensi Profesional
                        tp.Subject = obj[24];
                        tp.Score = obj[25];
                        tp.ScoreInPercentage = obj[26];
                        tp.Mutu = obj[27];
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(25).Take(2).Select(x => x.Replace("%", "")));
                        #endregion
                        #endregion

                        #region Profil Talent
                        tp.Talent = obj[29];
                        tp.IQ = obj[30];
                        tp.Drive = obj[32];
                        tp.Komunikasi = obj[33];
                        tp.Loyalitas = obj[34];
                        tp.Teliti = obj[35];
                        tp.Konsistensi = obj[36].Replace("%", "");
                        #endregion

                        #region Profil Menurut Siswa
                        #region Aspek Kepribadian
                        tp.Discipline = obj[39];
                        tp.DisciplineScore = obj[40];
                        tp.Atmosphere = obj[41];
                        tp.AtmosphereScore = obj[42];
                        tp.Encourage = obj[43];
                        tp.EncourageScore = obj[44];
                        tp.RoleModel = obj[45];
                        tp.RoleModelScore = obj[46];
                        tp.Inspirator = obj[47];
                        tp.InspiratorScore = obj[48];
                        tp.Sympathy = obj[49];
                        tp.SympathyScore = obj[50];
                        tp.PersonalityAverage = obj[51];
                        tp.PersonalityResult = obj[52];
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(39).Take(12).Select(x => x.Replace("%", "")));
                        #endregion

                        #region Aspek Pedagogik
                        tp.DeliveryOfMaterial = obj[54];
                        tp.DeliveryOfMaterialScore = obj[55];
                        tp.Kindess = obj[56];
                        tp.KindessScore = obj[57];
                        tp.TempatCurhat = obj[58];
                        tp.TempatCurhatScore = obj[59];
                        tp.SiswaBertanya = obj[60];
                        tp.SiswaBertanyaScore = obj[61];
                        tp.AnswerQuestion = obj[62];
                        tp.AnswerQuestionScore = obj[63];
                        tp.PedagogikSiswaAverage = obj[64];
                        tp.PedagogikSiswaResult = obj[65];
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(54).Take(10).Select(x => x.Replace("%", "")));
                        #endregion

                        #region Kompetensi Profesional
                        tp.ProCol1 = obj[67];
                        tp.ProCol1Score = obj[68];
                        tp.ProCol2 = obj[69];
                        tp.ProCol2Score = obj[70];
                        tp.ProCol3 = obj[71];
                        tp.ProCol3Score = obj[72];
                        tp.ProCol4 = obj[73];
                        tp.ProCol4Score = obj[74];
                        tp.ProCol5 = obj[75];
                        tp.ProCol5Score = obj[76];
                        tp.ProCol6 = obj[77];
                        tp.ProCol6Score = obj[78];
                        tp.ProCol7 = obj[79];
                        tp.ProCol7Score = obj[80];
                        tp.ProCol8 = obj[81];
                        tp.ProCol8Score = obj[82];
                        tp.ProCol9 = obj[83];
                        tp.ProCol9Score = obj[84];
                        tp.ProCol10 = obj[85];
                        tp.ProCol10Score = obj[86];
                        tp.ProAverage = obj[87];
                        tp.ProResult = obj[88];
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(67).Take(20).Select(x => x.Replace("%", "")));
                        #endregion

                        #region Kompetensi Sosial
                        tp.SosCol1 = obj[90];
                        tp.SosCol1Score = obj[91];
                        tp.SosCol2 = obj[92];
                        tp.SosCol2Score = obj[93];
                        tp.SosCol3 = obj[94];
                        tp.SosCol3Score = obj[95];
                        tp.SosCol4 = obj[96];
                        tp.SosCol4Score = obj[97];
                        tp.SosCol5 = obj[98];
                        tp.SosCol5Score = obj[99];

                        tp.SosColAverage = obj[100];
                        tp.SosColResult = obj[101];
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(90).Take(10).Select(x => x.Replace("%", "")));
                        #endregion
                        #endregion

                        #region OpenQuestion
                        tp.DataFromFile += "," + String.Join(",", obj.Skip(103).Take(2).Select(x => x.Replace("%", "")));
                        #endregion
                    }
                    lstTp.Add(tp);
                }
            }
            catch (Exception ex)
            {
                String errMessage = ex.Message;
            }
            finally
            {

            }
        }
        public void OnUploadAddRecord(IDbContext ctx, ref int TransactionID)
        {
            String lstNIK = String.Join("','", lstTp.Select(x => x.NIK));
            List<vTeacher> tch = BusinessLayer.GetvTeacherList(String.Format("TeacherCode IN ('{0}')", lstNIK));
            List<PersonalityType> lstPersonType = BusinessLayer.GetPersonalityTypeList("IsDeleted = 0");

            TransTeacherProfileDtDao ttpdtDao = new TransTeacherProfileDtDao(ctx);
            TransTeacherProfileDtItemDao ttpItemDao = new TransTeacherProfileDtItemDao(ctx);
            
            SaveTransTeacherProfileHd(ctx, ref TransactionID);
            foreach (TeacherProfile tp in lstTp)
            {
                TransTeacherProfileDt ttpdt = new TransTeacherProfileDt();
                ttpdt.TransactionID = TransactionID;
                ttpdt.TeacherID = tch.FirstOrDefault(x => x.TeacherCode == tp.NIK.ToString()).TeacherID;
                ttpdt.PersonalityTypeID = lstPersonType.FirstOrDefault(x => x.PersonalityTypeName.Contains(tp.Talent)).PersonalityTypeID;
                ttpdt.IQScore = Convert.ToInt32(tp.IQ);
                ttpdt.DScore = Convert.ToInt32(tp.Drive);
                ttpdt.KScore = Convert.ToInt32(tp.Komunikasi);
                ttpdt.LScore = Convert.ToInt32(tp.Loyalitas);
                ttpdt.TScore = Convert.ToInt32(tp.Teliti);
                ttpdt.KonsScoreInPercentage = Convert.ToInt32(Convert.ToDecimal(tp.Konsistensi.Replace("%", "")));
                ttpdt.Remarks = "";
                ttpdt.GCTeacherDetailStatus = Constant.TransactionStatus.OPEN;
                ttpdt.CreatedBy = AppSession.UserLogin.UserID;
                ttpdtDao.Insert(ttpdt);

                Int32 DtID = BusinessLayer.GetTransTeacherProfileDtMaxID(ctx);
                List<TeacherProfileItem> LstTeacherProfileItem = BusinessLayer.GetTeacherProfileItemList(String.Format("TeacherProfileGroupID IN (SELECT TeacherProfileGroupID FROM SchoolTypeTeacherProfileGroup WHERE GCSchoolType = '{0}') AND IsDeleted = 0 ORDER BY TeacherProfileGroupID ASC,DisplayOrder ASC", cboGrade.Value), ctx);
                String[] temp = tp.DataFromFile.Split(',');
                int i = 0;
                List<Int32> lstGroupID = LstTeacherProfileItem.GroupBy(x => x.TeacherProfileGroupID).Select(x => x.Key).ToList();

                foreach (TeacherProfileItem tpi in LstTeacherProfileItem)
                {
                    TransTeacherProfileDtItem ttpItem = new TransTeacherProfileDtItem();
                    ttpItem.TransTeacherProfileDtID = DtID;
                    ttpItem.TeacherProfileItemID = tpi.TeacherProfileItemID;
                    if (tpi.TeacherProfileGroupID == 12)
                    {
                        ttpItem.Remarks = temp[i];
                        ttpItemDao.Insert(ttpItem);
                        i += 1;
                    }
                    else
                    {
                        if (temp[i] != "")
                            ttpItem.Score = Convert.ToDecimal(temp[i]);
                        else
                            ttpItem.Score = 0;

                        if (tpi.IsDynamicQualityPercentage)
                        {
                            if (temp[i + 1] != "")
                            {
                                ttpItem.ScoreInPercentage = Convert.ToDecimal(temp[i + 1]);
                                ttpItem.QualityPercentage = ttpItem.Score / ttpItem.ScoreInPercentage * 100;
                            }
                            else
                            {
                                ttpItem.ScoreInPercentage = 0;
                                ttpItem.QualityPercentage = 0;
                            }
                        }
                        else
                        {
                            ttpItem.ScoreInPercentage = ttpItem.Score / tpi.QualityPercentage * 100;
                            ttpItem.QualityPercentage = ttpItem.Score / tpi.QualityPercentage * 100;
                        }
                        ttpItemDao.Insert(ttpItem);
                        i += 2;
                    }
                }
            }
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Common;
using DevExpress.Web.ASPxCallbackPanel;
using System.IO;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ChangeStudentPhotoEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.ST_CHANGE_STUDENT_PHOTO;
        }

        protected override void InitializeDataControl()
        {
            vStudent entity = BusinessLayer.GetvStudentList(string.Format("StudentID = {0}", AppSession.StudentID))[0];
            hdnGender.Value = entity.GCGender;
            imgPreview.Src = entity.StudentImageUrl;
            hdnStudentCode.Value = entity.StudentCode;
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            BinaryWriter bw = null;
            try
            {
                string path = string.Format("{0}Student\\#StudentCode\\", AppConfigManager.CDXPhysicalDirectory);

                path = path.Replace("#StudentCode", hdnStudentCode.Value);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileName = string.Format("{0}.jpg", hdnStudentCode.Value);

                FileStream fs = new FileStream(string.Format("{0}{1}", path, fileName), FileMode.Create);
                bw = new BinaryWriter(fs);

                byte[] data = Convert.FromBase64String(hdnImageData.Value);
                bw.Write(data);
            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                if (bw != null)
                    bw.Close();
            }
            return result;
        }
    }
}
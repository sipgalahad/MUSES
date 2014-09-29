using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class RoomEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.ROOM;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                Room entity = BusinessLayer.GetRoom(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtRoomCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtRoomCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRoomName, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(Room entity)
        {
            txtRoomCode.Text = entity.RoomCode;
            txtRoomName.Text = entity.RoomName;
        }

        private void ControlToEntity(Room entity)
        {
            entity.RoomCode = txtRoomCode.Text;
            entity.RoomName = txtRoomName.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("RoomCode = '{0}'", txtRoomCode.Text);
            List<Room> lst = BusinessLayer.GetRoomList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Ruangan Dengan Kode " + txtRoomCode.Text + " Sudah Ada!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("RoomCode = '{0}' AND RoomID != {1}", txtRoomCode.Text, hdnID.Value);
            List<Room> lst = BusinessLayer.GetRoomList(FilterExpression);

            if (lst.Count > 0)
                errMessage = "Ruangan Dengan Kode " + txtRoomCode.Text + " Sudah Ada!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            RoomDao entityDao = new RoomDao(ctx);
            bool result = false;
            try
            {
                Room entity = new Room();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetRoomMaxID(ctx).ToString();
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
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

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                Room entity = BusinessLayer.GetRoom(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateRoom(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
    }
}
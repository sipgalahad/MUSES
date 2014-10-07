using System;
using CodeX.Data.Core.Dal;

/***************************************************************************
 * $Archive: $
 * $Workfile: $
 * $Author: $
 * $Date: $
 * $Modtime: $  
 * $Revision: $
 ***************************************************************************/
namespace CodeX.Data.Model
{
    #region GetItemQtyOnOrder
    [Serializable]
    [Table(Name = "GetItemQtyOnOrder")]
    public class GetItemQtyOnOrder
    {
        private Int32 _QtyOnOrder;

        [Column(Name = "QtyOnOrder", DataType = "Int32")]
        public Int32 QtyOnOrder
        {
            get { return _QtyOnOrder; }
            set { _QtyOnOrder = value; }
        }
    }
    #endregion
}
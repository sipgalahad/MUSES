using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeX.Data.Model
{
    #region ClassSubjectModel
    public partial class ClassSubjectModel
    {
        public Int32 ClassSubjectID { get; set; }
        public Int32 ClassScheduleID { get; set; }
        public Int32 ClassMeetingID { get; set; }
    }
    #endregion
}

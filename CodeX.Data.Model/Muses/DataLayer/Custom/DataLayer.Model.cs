using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeX.Data.Model
{
    #region ClassSubjectModel
    public partial class ClassSubjectModel
    {
        public Int32 PeriodSectionID { get; set; }
        public Int32 ClassSubjectID { get; set; }
        public Int32 CurriculumID { get; set; }
        public Int32 ClassScheduleID { get; set; }
        public Int32 ClassMeetingID { get; set; }
    }
    #endregion
    #region ClassStudentModel
    public partial class ClassStudentModel
    {
        public Int32 SchoolClassID { get; set; }
        public Int32 StudentID { get; set; }
        public Int32 PeriodSectionID { get; set; }
    }
    #endregion
    #region SubjectModel
    public partial class SubjectModel
    {
        public Int32 SubjectID { get; set; }
        public String GCSchoolType { get; set; }
    }
    #endregion
}

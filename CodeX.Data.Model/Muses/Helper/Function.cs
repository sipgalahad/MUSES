using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Globalization;
using System.Text.RegularExpressions;
using CodeX.Common;

namespace CodeX.Data.Model
{
    public partial class Function
    {
        public static String GenerateStudentPictureFileName(string pictureFileName, string studentCode)
        {
            Random random = new Random();
            int randomNum = random.Next(1000000, 100000000);
            string imageUrl = string.Format("{0}{1}{2}", AppConfigManager.CDXVirtualDirectory, AppConfigManager.CDXStudentImagePath, pictureFileName).Replace("#StudentCode", studentCode);
            return string.Format("{0}?{1}", imageUrl, randomNum);
        }
    }
}

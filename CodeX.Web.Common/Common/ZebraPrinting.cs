using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeX.Data.Model;
using System.Web;
using System.IO;
using CodeX.Web.Common;
using System.Text.RegularExpressions;

namespace CodeX.Common
{
    public static class ZebraPrinting
    {
        public enum PrintingGroup
        {
            Cover,
            DrugLabel,
            Registration,
            MRLabel,
            PatientLabel,
            Tracer,
            Wristband
        }

        public enum LabelGroup
        {
            DrugLabel,
            MRLabel
        }

        public enum LabelSize
        {
            Label_4x2,
            Label_5x3,
            Label_7x3,
            Label_8x3
        }

        private static string line1, line2, line3, line4, line5, line6 = string.Empty;
        private static string allergyInfo = string.Empty;

        #region Zebra Label Print

        private static string GetLabelCommand(LabelGroup labelGroup = LabelGroup.DrugLabel, LabelSize labelSize = LabelSize.Label_8x3)
        {
            #region Get Label Format
            string filePath = HttpContext.Current.Server.MapPath("~/Libs/App_Data");
            string fileName = string.Format(@"{0}\label\{1}\{2}.zpl", filePath, labelGroup.ToString(), labelSize.ToString());
            IEnumerable<string> lstCommand = File.ReadAllLines(fileName);
            StringBuilder commandText = new StringBuilder();
            foreach (string command in lstCommand)
            {
                commandText.AppendLine(command);
            }
            string result = commandText.ToString();
            #endregion
            return result;
        }
        private static List<string> GetLabelMetadata(LabelGroup labelGroup)
        {
            List<string> metadata = new List<string>();
            string filePath = HttpContext.Current.Server.MapPath("~/Libs/App_Data");
            string fileName = string.Format(@"{0}\label\{1}\label.ddl", filePath, labelGroup.ToString());
            IEnumerable<string> lstField = File.ReadAllLines(fileName);
            metadata = lstField.ToList();
            return metadata;
        }

        #endregion
    }
}

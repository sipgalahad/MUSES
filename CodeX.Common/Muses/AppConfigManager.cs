using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CodeX.Common
{
    public static class AppConfigManager
    {
        static private string _CDXNameFormat;
        static private string _CDXTableLogFolder;
        static private string _CDXVirtualDirectory;
        static private string _CDXPhysicalDirectory;
        static private string _CDXAppVirtualDirectory;
        static private string _CDXLibsPhysicalDirectory;
        static private string _CDXSiteName;
        static public string CDXTableLogFolder { get { return _CDXTableLogFolder; } }
        static public string CDXNameFormat { get { return _CDXNameFormat; } }
        static public string CDXVirtualDirectory { get { return _CDXVirtualDirectory; } }
        static public string CDXPhysicalDirectory { get { return _CDXPhysicalDirectory; } }
        static public string CDXAppVirtualDirectory { get { return _CDXAppVirtualDirectory; } }
        static public string CDXLibsPhysicalDirectory { get { return _CDXLibsPhysicalDirectory; } }
        static public string CDXSiteName { get { return _CDXSiteName; } }

        static AppConfigManager()
        {
            // Cache all these values in static properties.
            _CDXTableLogFolder = ConfigurationManager.AppSettings["CDXTableLogFolder"];
            _CDXNameFormat = ConfigurationManager.AppSettings["CDXNameFormat"];
            _CDXVirtualDirectory = ConfigurationManager.AppSettings["CDXVirtualDirectory"];
            _CDXPhysicalDirectory = ConfigurationManager.AppSettings["CDXPhysicalDirectory"];
            _CDXAppVirtualDirectory = ConfigurationManager.AppSettings["CDXAppVirtualDirectory"];
            _CDXLibsPhysicalDirectory = ConfigurationManager.AppSettings["CDXLibsPhysicalDirectory"];
            _CDXSiteName = ConfigurationManager.AppSettings["CDXSiteName"];
        }
    }
}

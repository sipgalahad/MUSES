using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace CodeX.Common
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct EventStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public String ServiceCode;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public String EventName;

        public DateTime EventDate;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string EID;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string Message;

        public bool status;
    }

    #region Native API Signatures and Types

    /// <summary>
    /// The COPYDATASTRUCT structure contains data to be passed to another 
    /// application by the WM_COPYDATA message. 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;       // Specifies data to be passed
        public int cbData;          // Specifies the data size in bytes
        public IntPtr lpData;       // Pointer to data to be passed
    }

    /// <summary>
    /// The class exposes Windows APIs to be used in this code sample.
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    public class NativeMethod
    {
        /// <summary>
        /// Sends the specified message to a window or windows. The SendMessage 
        /// function calls the window procedure for the specified window and does 
        /// not return until the window procedure has processed the message. 
        /// </summary>
        /// <param name="hWnd">
        /// Handle to the window whose window procedure will receive the message.
        /// </param>
        /// <param name="Msg">Specifies the message to be sent.</param>
        /// <param name="wParam">
        /// Specifies additional message-specific information.
        /// </param>
        /// <param name="lParam">
        /// Specifies additional message-specific information.
        /// </param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);


        /// <summary>
        /// The FindWindow function retrieves a handle to the top-level window 
        /// whose class name and window name match the specified strings. This 
        /// function does not search child windows. This function does not 
        /// perform a case-sensitive search.
        /// </summary>
        /// <param name="lpClassName">Class name</param>
        /// <param name="lpWindowName">Window caption</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    }

    #endregion

    public static class EventViewerHelper
    {
        public static bool SendMessageToEventViewer(String ServiceCode, String EventName, String EventID, String Messge, Boolean Status = true)
        {
            EventStruct entity = new EventStruct();
            entity.ServiceCode = ServiceCode;
            entity.EventName = EventName;
            entity.EventDate = DateTime.Now;
            entity.EID = EventID;
            entity.Message = Messge;
            entity.status = Status;
            return SendMessageToEventViewer(entity);
        }

        public static bool SendMessageToEventViewer(EventStruct entity)
        {
            // Find the target window handle.
            IntPtr hTargetWnd = NativeMethod.FindWindow(null, "CodeX - Event Viewer");
            if (hTargetWnd == IntPtr.Zero)
            {
                return false;
            }

            // Marshal the managed struct to a native block of memory.
            int myStructSize = Marshal.SizeOf(entity);
            IntPtr pMyStruct = Marshal.AllocHGlobal(myStructSize);
            try
            {
                Marshal.StructureToPtr(entity, pMyStruct, true);

                COPYDATASTRUCT cds = new COPYDATASTRUCT();
                cds.cbData = myStructSize;
                cds.lpData = pMyStruct;

                // Send the COPYDATASTRUCT struct through the WM_COPYDATA message to 
                // the receiving window. (The application must use SendMessage, 
                // instead of PostMessage to send WM_COPYDATA because the receiving 
                // application must accept while it is guaranteed to be valid.)
                NativeMethod.SendMessage(hTargetWnd, Constant.EventViewer.WM_COPYDATA, IntPtr.Zero, ref cds);

                int result = Marshal.GetLastWin32Error();
                if (result != 0)
                {
                    return false;
                    //MessageBox.Show(String.Format("SendMessage(WM_COPYDATA) failed w/err 0x{0:X}", result));
                }
            }
            finally
            {
                Marshal.FreeHGlobal(pMyStruct);
            }
            return true;
        }
    }
}

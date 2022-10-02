using UnityEngine;

namespace Tools
{
    public class ClipboardUtils
    {
        /// <summary>
        /// Puts the string into the Clipboard.
        /// </summary>
        public static void CopyToClipboard(string str)
        {
            GUIUtility.systemCopyBuffer = str;
        }

        public static string PasteFromClipboard()
        {
            return GUIUtility.systemCopyBuffer;
        }
    }
}
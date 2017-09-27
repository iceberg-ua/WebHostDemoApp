using mshtml;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WebHostDemo
{
    class WebHostControl : WebBrowser
    {
        public WebHostControl()
        {
            Dock = DockStyle.Fill;
            //ScriptErrorsSuppressed = true;
            DocumentCompleted += WHDocumentCompleted;
        }

        public WebHostControl(string html, string style = null, string script = null)
            : this()
        {
            DocumentText = html;
        }

        public void ScripErrorHanfling(string message, string url, string linenumber)
        {

        }

        private void WHDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Document.Body.MouseLeave += Document_MouseLeave;
            //Document.MouseLeave += Document_MouseLeave;
        }

        private void Document_MouseLeave(object sender, HtmlElementEventArgs e)
        {
            Hide();
        }

        public void SetEditorText(string text)
        {
            Document.InvokeScript("setValue", new object[] { text });
        }

        public string GetEditorText()
        {
            var value = Document.InvokeScript("getValue");

            return (value is string) ? (string)value : string.Empty;
        }

        private bool FindString(HtmlElement elem, string str)
        {
            bool strFound = false;
            IHTMLTxtRange rng = null;

            try
            {
                if (rng != null)
                {
                    rng.collapse(false);
                    strFound = rng.findText(str, 1000000000, 0);
                    if (strFound)
                    {
                        rng.select();
                        rng.scrollIntoView(true);
                    }
                }
                if (rng == null)
                {
                    IHTMLDocument2 doc =
                           elem.Document.DomDocument as IHTMLDocument2;

                    IHTMLBodyElement body = doc.body as IHTMLBodyElement;

                    rng = body.createTextRange();
                    rng.moveToElementText(elem.DomElement as IHTMLElement);
                    strFound = rng.findText(str, 1000000000, 0);
                    if (strFound)
                    {
                        rng.select();
                        rng.scrollIntoView(true);
                    }

                }
            }
            catch
            {

            }
            return strFound;
        }

        public Bitmap GetWebControlImage()
        {
            //int width = Width * 4, height = Height * 4;
            //int width = Width, height = Height;
            if (Document.Body == null)
                return null;

            int width = Document.Body.ScrollRectangle.Width, height = Document.Body.ScrollRectangle.Height;
            Bitmap bmp = new Bitmap(width, height);

            try
            {
                var viewObject = Document.DomDocument as IViewObject;

                using (Graphics grphx = Graphics.FromImage(bmp))
                {
                    IntPtr hdc = grphx.GetHdc();

                    var targetRect = new tagRECT();
                    targetRect.left = 0;
                    targetRect.top = 0;
                    targetRect.right = width;
                    targetRect.bottom = height;

                    var sourceRect = new tagRECT();
                    sourceRect.left = 0;
                    sourceRect.top = 0;
                    sourceRect.right = width;
                    sourceRect.bottom = height;

                    try
                    {
                        int hr = viewObject.Draw(1 /*DVASPECT_CONTENT*/,
                          (int)-1,
                            IntPtr.Zero,
                            IntPtr.Zero,
                            IntPtr.Zero,
                            hdc,
                            ref targetRect,
                            ref sourceRect,
                            IntPtr.Zero,
                            (uint)0);
                    }
                    finally
                    {
                        grphx.ReleaseHdc();
                    }
                }
                //Random rnd = new Random();
                //bmp.Save($"{rnd.Next(int.MaxValue)}.jpg");
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show("Content of the control is not loaded.");
            }

            return bmp;
        }

        // Replacement for mshtml imported interface, Tlbimp.exe generates wrong signatures
        [ComImport, InterfaceType((short)1), Guid("3050F669-98B5-11CF-BB82-00AA00BDCE0B")]
        private interface IHTMLElementRender
        {
            void DrawToDC(IntPtr hdc);
            void SetDocumentPrinter(string bstrPrinterName, IntPtr hdc);
        }

        [ComVisible(true), ComImport()]
        [GuidAttribute("0000010d-0000-0000-C000-000000000046")]
        [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IViewObject
        {
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Draw(
                ////tagDVASPECT                
                [MarshalAs(UnmanagedType.U4)] UInt32 dwDrawAspect,
                int lindex,
                IntPtr pvAspect,
                [In] IntPtr ptd,
                //// [MarshalAs(UnmanagedType.Struct)] ref DVTARGETDEVICE ptd,
                IntPtr hdcTargetDev, IntPtr hdcDraw,
                [MarshalAs(UnmanagedType.Struct)] ref tagRECT lprcBounds,
                [MarshalAs(UnmanagedType.Struct)] ref tagRECT lprcWBounds,
                IntPtr pfnContinue,
                [MarshalAs(UnmanagedType.U4)] UInt32 dwContinue);
        }
    }
}

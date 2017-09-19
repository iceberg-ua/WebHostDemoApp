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
            ScriptErrorsSuppressed = true;
            DocumentCompleted += WHDocumentCompleted;
        }

        public WebHostControl(string html, string style = null, string script = null)
            : this()
        {

            DocumentText = html;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);


        }

        private void WHDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

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

        public void GetWebControlImage()
        {
            try
            {
                var viewObject = Document.DomDocument as IViewObject;

                int width = Width * 4, height = Height * 4;

                using (Bitmap bmp = new Bitmap(width, height))
                {
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

                    Random rnd = new Random();
                    bmp.Save($"{rnd.Next(int.MaxValue)}.jpg");
                }
            }
            catch(NullReferenceException e)
            {
                MessageBox.Show("Content of the control is not loaded.");
            }
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

using mshtml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebHostDemo
{
    class WebHostControl : WebBrowser
    {
        static string _script = "";
        static string _basePage = "";
        static string _d3Script = "_d3Script";
        static string _stylePath = "_styleFile";

        static WebHostControl()
        {
            string filePath = @"file:///" + AppDomain.CurrentDomain.BaseDirectory.Replace('\\', '/');

            //_basePage = File.ReadAllText("WebHostControl/BaseHTML.html");
            //_basePage = File.ReadAllText("WebHostControl/Gears.html");
            //_script = File.ReadAllText("WebHostControl/d3.v3.min.js");

            //show Gantt diagrams chart
            //_basePage = File.ReadAllText("WebHostControl/GanttChart.html");
            //_basePage = _basePage.Replace(_stylePath, filePath + @"WebHostControl/style.css");
            //_basePage = _basePage.Replace(_d3Script, filePath + @"WebHostControl/d3.v3.min.js");

            //show Markdown editor
            _basePage = File.ReadAllText("WebHostControl/MarkdownEditor.html");
            _basePage = _basePage.Replace(_stylePath, filePath + @"WebHostControl/simplemde.min.css");
            _basePage = _basePage.Replace(_d3Script, filePath + @"WebHostControl/simplemde.min.js");
        }

        public WebHostControl()
        {
            Dock = DockStyle.Fill;
            ScriptErrorsSuppressed = true;
            //DocumentCompleted += WHDocumentCompleted;
            DocumentText = _basePage;
        }

        public WebHostControl(string html)
        {

        }

        private void WHDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        { }

        //protected override void OnMouseDown(MouseEventArgs e)
        public void GetWebControlImage()
        {
            //base.OnMouseDown(e);
            //if (ctrl is WebBrowser)
            {
                WebBrowser wb = this;
                //mshtml.IHTMLDocument2 doc = (mshtml.IHTMLDocument2)wb.Document.DomDocument;
                //mshtml.IHTMLElement body = (mshtml.IHTMLElement)doc.body;
                //IHTMLElementRender render = (IHTMLElementRender)body;

                var viewObject = wb.Document.DomDocument as IViewObject;

                int width = 2130, height = 1000;

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



                        //        render.DrawToDC(hdc);
                        //        grphx.ReleaseHdc();
                    }

                    Random rnd = new Random();

                    bmp.Save($"{rnd.Next(int.MaxValue)}.jpg");
                }
            }

        }

        public void SetEditorText(string text)
        {
            //var divs = Document.GetElementsByTagName("div");

            //foreach (HtmlElement div in divs)
            //{
            //    if (div.GetAttribute("className") == "CodeMirror-code")
            //    {
            //        div.InnerText = text;
            //    }
            //}

            //object data = Clipboard.GetData("Text");
            //Clipboard.SetData("Text", text);

            //IHTMLDocument2 doc = (mshtml.IHTMLDocument2)Document.DomDocument;
            //doc.execCommand("Paste", false, null);

            //Clipboard.SetData("Text", data);

            Document.InvokeScript("setValue", new object[] { text });
        }

        public string GetEditorText()
        {
            var value = Document.InvokeScript("getValue");

            return (value is string) ? (string)value : string.Empty;

            //var divs = Document.GetElementsByTagName("div");

            //foreach (HtmlElement div in divs)
            //{
            //    if(div.GetAttribute("className") == "CodeMirror-code")
            //    {
            //        return div.InnerText;
            //    }
            //}

            //var element = Document.GetElementsByTagName("CodeMirror-code");
            //mshtml.IHTMLDocument2 doc = (mshtml.IHTMLDocument2)Document.DomDocument;

            //var selection = doc.selection.createRange().text;

            //if (selection != null)
            //{
            //    IHTMLTxtRange range = selection.createRange() as IHTMLTxtRange;
            //}

            //doc.execCommand("SelectAll", false, null);

            //var allSelection = doc.selection;

            //if (allSelection != null)
            //{
            //    IHTMLTxtRange range = allSelection.createRange() as IHTMLTxtRange;
            //}

            //if (element != null)
            //    return element.InnerHtml;
            //else
            return string.Empty;
        }

        //private void WHDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{
        //    if (sender is WebBrowser)
        //    {
        //        WebBrowser ctrl = sender as WebBrowser;
        //        mshtml.IHTMLDocument2 doc = (mshtml.IHTMLDocument2)ctrl.Document.DomDocument;
        //        mshtml.IHTMLElement body = (mshtml.IHTMLElement)doc.body;
        //        IHTMLElementRender render = (IHTMLElementRender)body;

        //        using (Bitmap bmp = new Bitmap(ctrl.ClientSize.Width, ctrl.ClientSize.Height))
        //        {
        //            using (Graphics grphx = Graphics.FromImage(bmp))
        //            {
        //                IntPtr hdc = grphx.GetHdc();
        //                render.DrawToDC(hdc);
        //                grphx.ReleaseHdc();
        //            }

        //            Random rnd = new Random();

        //            bmp.Save($"{rnd.Next(int.MaxValue)}.jpg");
        //        }
        //    }
        //}

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

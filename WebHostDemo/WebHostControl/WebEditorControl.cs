using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHostDemo
{
    class WebEditorControl : WebHostControl
    {
        public static WebEditorControl CreateWebEditorControl()
        {
            string _basePage = File.ReadAllText(@"WebHostControl/MediumEditor/MediumEditor.html");
            _basePage = _basePage.Replace("_sourcesPath", _appPath + @"WebHostControl/MediumEditor");

            return CreateWebEditorControl(_basePage);
        }

        public static WebEditorControl CreateWebEditorControl(string baseHtml)
        {
            return new WebEditorControl(baseHtml);
        }

        public WebEditorControl(string baseHtml)
            : base(baseHtml)
        {
            ScrollBarsEnabled = true;
        }

        public override void SetEditorText(string text)
        {
            Document.InvokeScript("setValue", new object[] { text });
        }

        public override string GetEditorText()
        {
            return base.GetEditorText();
        }
    }
}

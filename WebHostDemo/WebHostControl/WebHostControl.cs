using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebHostDemo
{
    class WebHostControl : WebBrowser
    {
        static string _script = "";
        static string _basePage = "";
        static string _3dScript = "&3dScript";

        static WebHostControl()
        {
            _script = File.ReadAllText("WebHostControl/d3.js");
            _basePage = File.ReadAllText("WebHostControl/BaseHTML.html");

            _basePage = _basePage.Replace(_3dScript, _script);
        }

        public WebHostControl()
        {
            Dock = DockStyle.Fill;
            DocumentCompleted += WHDocumentCompleted;
            DocumentText = _basePage;
        }

        private void WHDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }
    }
}

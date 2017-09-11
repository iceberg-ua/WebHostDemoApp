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
        static string _d3Script = "_d3Script";
        static string _stylePath = "_styleFile";

        static WebHostControl()
        {
            string filePath = @"file:///" + AppDomain.CurrentDomain.BaseDirectory.Replace('\\', '/');

            //_basePage = File.ReadAllText("WebHostControl/BaseHTML.html");
            //_basePage = File.ReadAllText("WebHostControl/Gears.html");
            //_script = File.ReadAllText("WebHostControl/d3.v3.min.js");

            //_basePage = File.ReadAllText("WebHostControl/GanttChart.html");
            //_basePage = _basePage.Replace(_stylePath, filePath + @"WebHostControl/style.css");
            //_basePage = _basePage.Replace(_d3Script, filePath + @"WebHostControl/d3.v3.min.js");

            _basePage = File.ReadAllText("WebHostControl/MarkdownEditor.html");
            _basePage = _basePage.Replace(_stylePath, filePath + @"WebHostControl/simplemde.min.css");
            _basePage = _basePage.Replace(_d3Script, filePath + @"WebHostControl/simplemde.min.js");
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

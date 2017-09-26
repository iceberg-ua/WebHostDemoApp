using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebHostDemo
{
    public partial class WebHostDemoForm : Form
    {
        public WebHostDemoForm()
        {
            LoadWebControl();
            InitializeComponent();
        }

        static string _script = "";
        static string _basePage = "";
        static string _d3Script = "_d3Script";
        static string _stylePath = "_styleFile";
        WebHostControl _webHostCtrl;

        private void LoadWebControl()
        {
            string filePath = @"file:///" + AppDomain.CurrentDomain.BaseDirectory.Replace('\\', '/');

            //_basePage = File.ReadAllText("WebHostControl/BaseHTML.html");
            //_basePage = File.ReadAllText("WebHostControl/Gears.html");
            //_script = File.ReadAllText("WebHostControl/d3.v3.min.js");

            ////show Gantt diagrams chart
            //_basePage = File.ReadAllText("WebHostControl/GanttChart/GanttChart.html");
            //_basePage = _basePage.Replace(_stylePath, filePath + @"WebHostControl/GanttChart/style.css");
            //_basePage = _basePage.Replace(_d3Script, filePath + @"WebHostControl/d3.v3.min.js");

            //show Markdown editor
            //_basePage = File.ReadAllText("WebHostControl/SimpleMDE/MarkdownEditor.html");
            //_basePage = _basePage.Replace(_stylePath, filePath + @"WebHostControl/SimpleMDE/simplemde.min.css");
            //_basePage = _basePage.Replace(_d3Script, filePath + @"WebHostControl/SimpleMDE/simplemde.min.js");

            //show Quill editor
            //_basePage = File.ReadAllText("WebHostControl/QuillEditor/QuillEditor.html");
            //_basePage = _basePage.Replace(_stylePath, filePath + @"WebHostControl/QuillEditor/quill.snow.css");
            //_basePage = _basePage.Replace("_d3Script", filePath + @"WebHostControl/QuillEditor/quill.js");
            //_basePage = _basePage.Replace(_d3Script, filePath + @"WebHostControl/QuillEditor/quill.min.js");

            //show Pen editor
            //_basePage = File.ReadAllText("WebHostControl/PenEditor/PenEditor.html");
            //_basePage = _basePage.Replace(_stylePath, filePath + @"WebHostControl/PenEditor/pen.css");
            //_basePage = _basePage.Replace(_d3Script, filePath + @"WebHostControl/PenEditor/pen.js");

            //MediumEditor
            _basePage = File.ReadAllText("WebHostControl/MediumEditor/MediumEditor.html");
            _basePage = _basePage.Replace("_sourcesPath", filePath + @"WebHostControl/MediumEditor");

            _webHostCtrl = new WebHostControl(_basePage);
            //_webHostCtrl = new WebHostControl();
            //_webHostCtrl.Navigate("https://quilljs.com/playground/#quill-playground");

            Controls.Add(_webHostCtrl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            _webHostCtrl.GetWebControlImage();
            watch.Stop();

            button1.Text = $"Image ({watch.ElapsedMilliseconds})";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _webHostCtrl.Visible = !_webHostCtrl.Visible;
            if (_webHostCtrl.Visible)
                button2.Text = "Hide";
            else
                button2.Text = "Show";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _webHostCtrl.SetEditorText(textBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = _webHostCtrl.GetEditorText();
        }
    }
}

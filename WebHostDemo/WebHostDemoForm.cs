using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebHostDemo
{
    public partial class WebHostDemoForm : Form
    {
        WebHostControl _webHostCtrl = new WebHostControl();

        public WebHostDemoForm()
        {
            InitializeComponent();

            Controls.Add(_webHostCtrl);
            _webHostCtrl.Visible = true;
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

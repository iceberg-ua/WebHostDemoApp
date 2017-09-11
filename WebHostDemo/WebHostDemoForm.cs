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
        public WebHostDemoForm()
        {
            InitializeComponent();

            WebHostControl webHostCtrl = new WebHostControl();
            Controls.Add(webHostCtrl);
            webHostCtrl.Visible = true;
        }
    }
}

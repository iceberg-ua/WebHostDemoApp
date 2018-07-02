using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHostDemo
{
    public class WebChartControl : WebHostControl
    {
        public static WebChartControl CreateWebChartControl()
        {
            string baseHtml = File.ReadAllText(@"Helpers/UIElements/WebHostControl/WebChartControl/GanttChart.html");
            baseHtml = baseHtml.Replace("_appPath", _appPath + @"Helpers/UIElements/WebHostControl/WebChartControl");

            return new WebChartControl(baseHtml);
        }

        public WebChartControl(string baseHtml)
            : base(baseHtml)
        {
            ScrollBarsEnabled = false;
        }
    }
}

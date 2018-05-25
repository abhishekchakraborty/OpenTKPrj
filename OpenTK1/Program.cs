using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenTK1
{
    // Following http://dreamstatecoding.blogspot.in/2017/01/opengl-4-with-opentk-in-c-part-1.html

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new MainWindow().Run(60);
        }
    }
}

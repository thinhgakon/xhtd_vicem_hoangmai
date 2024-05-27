using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMXHTD
{
    public partial class frmControl : Form
    {
        IntPtr h = IntPtr.Zero;

        public frmControl()
        {
            InitializeComponent();
        }

        //4.1  call connect function
        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "Connect")]
        public static extern IntPtr Connect(string Parameters);
        [DllImport("plcommpro.dll", EntryPoint = "PullLastError")]
        public static extern int PullLastError();
    }
}

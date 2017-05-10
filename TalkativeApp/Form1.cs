using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TalkativeApp
{
    public partial class Form1 : Form
    {
        public Form1 ()
        {
            InitializeComponent();
        }

        private void output_TextBox_KeyPress ( object sender, KeyPressEventArgs e )
        {
            e.Handled = true;
        }
    }
}

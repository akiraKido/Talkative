using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TalkativeCompiler;

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

        private void compile_Button_Click ( object sender, EventArgs e )
        {
            IEnumerable<Token> result = Talkative.GetTokens( input_TextBox.Text );
            output_TextBox.Text = string.Empty;
            foreach ( Token token in result )
            {
                output_TextBox.Text += $"{token.Type}:{token.Value}" + Environment.NewLine;
            }
        }
    }
}

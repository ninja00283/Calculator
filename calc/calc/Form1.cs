using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace calc
{
    public partial class Form1 : Form
    {
        String UserInput = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void NumberButton_Click(object sender, EventArgs e)
        {
            var NumberButton = (Button)sender;
            UserInput += NumberButton.Text;
            MainScreen.Text = UserInput;
        }

        private void MainScreen_TextChanged(object sender, EventArgs e)
        {
        }

        private void UtilityButtonBackSpace_Click(object sender, EventArgs e)
        {
            if (UserInput.Length > 0)
            {
                UserInput = UserInput.Remove(UserInput.Length - 1);
                MainScreen.Text = UserInput;
            }
        }
    }
}

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
        bool UserInputObsolete = false;
        String SecondaryInput = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void NumberButton_Click(object sender, EventArgs e)
        {
            if (UserInputObsolete) {
                UserInput = "";
                UserInputObsolete = false;
            }
            var NumberButton = (Button)sender;
            UserInput += NumberButton.Text;
            
            MainScreen.Text = UserInput;
        }

        private void MainScreen_TextChanged(object sender, EventArgs e)
        {
            char[] Operators = { '*', '/', '+', '-' };
            while (UserInput.Contains("+") || UserInput.Contains("-") || UserInput.Contains("*") || UserInput.Contains("/"))
            {
                var OperatorPosition = UserInput.IndexOfAny(Operators);
                UserInput.Replace(UserInput[OperatorPosition].ToString(), UserInput[OperatorPosition].ToString() + "S");
                string[] split = UserInput.Split('S');
                SecondaryInput += split[0];
                if (split.GetLength(0) > 1)
                {
                    UserInput = split[1];
                }
                else
                {
                    UserInputObsolete = true;
                    break;
                }
            }
            SecondaryScreen.Text = SecondaryInput;
            MainScreen.Text = UserInput;
        }

        private void UtilityButtonBackSpace_Click(object sender, EventArgs e)
        {
            if (UserInput.Length > 0)
            {
                UserInput = UserInput.Remove(UserInput.Length - 1);
                MainScreen.Text = UserInput;
            }
        }
        
        private void SecondaryScreen_TextChanged(object sender, EventArgs e)
        {
            char[] Operators = { '*', '/', '+', '-' };
            var Equation = new string[SecondaryInput.Count(x => x == '+') + SecondaryInput.Count(x => x == '-') + SecondaryInput.Count(x => x == '*') + SecondaryInput.Count(x => x == '/')];
            for (int i = 0; i < SecondaryInput.Count(x => x == '+') + SecondaryInput.Count(x => x == '-') + SecondaryInput.Count(x => x == '*') + SecondaryInput.Count(x => x == '/'); i++)
            { 
                if (SecondaryInput.Contains("+") || SecondaryInput.Contains("-") || SecondaryInput.Contains("*") || SecondaryInput.Contains("/"))
                {
                    var PrimaryOperatorPosition = -1;
                    if (i > 0) {
                        PrimaryOperatorPosition = SecondaryInput.IndexOfAny(Operators);
                    }
                    var SecondaryOperatorPosition = 1;
                    if ( PrimaryOperatorPosition != SecondaryInput.LastIndexOfAny(Operators)) {
                        SecondaryOperatorPosition = SecondaryInput.IndexOfAny(Operators, PrimaryOperatorPosition + 1);
                    } else {
                        SecondaryOperatorPosition = SecondaryInput.Length;
                    }
                    Equation[i] = SecondaryInput.Substring(PrimaryOperatorPosition + 1, SecondaryOperatorPosition - PrimaryOperatorPosition);
                }
            }
            for (int i = 0; i < Equation.Length; i++)
            {
                Console.Write(Equation[i]);
            }
            Console.Write("\n");
        }
    }
}

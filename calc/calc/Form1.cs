using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                UserInputObsolete = false;
            }
            var NumberButton = (Button)sender;
            UserInput += NumberButton.Text;
            
            MainScreen.Text = UserInput;
        }

        private void MainScreen_TextChanged(object sender, EventArgs e)
        {
            /*
            char[] Operators = { '*', '/', '+', '-' };
            var SecondaryInput = "";
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
            */
            char[] Operators = { '*', '/', '+', '-', '=' };
            if (UserInput.LastIndexOfAny(Operators) == 1)
            {
                SecondaryInput += UserInput;
                UserInputObsolete = true;
                UserInput = "";
            }
            SecondaryScreen.Text = SecondaryInput;
        }

        private void UtilityButtonBackSpace_Click(object sender, EventArgs e)
        {
            if (UserInput.Length > 0)
            {
                UserInput = UserInput.Remove(UserInput.Length - 1);
                MainScreen.Text = UserInput;
            }
        }
        //()C#-3
        private void SecondaryScreen_TextChanged(object sender, EventArgs e)
        {
            char[] Operators = { '*', '/', '+', '-' };
            char[] OperatorsPlus = { '*', '/', '+', '-', '(', ')', '=' };
            List<string> Equation = new List<string>();

            var SecondaryScreenText = SecondaryScreen.Text;
            Console.WriteLine(SecondaryScreenText);
            var SplitCount = 0;
            SplitCount += SecondaryScreenText.Length - SecondaryScreenText.Replace("*", "").Length;
            SplitCount += SecondaryScreenText.Length - SecondaryScreenText.Replace("+", "").Length;
            SplitCount += SecondaryScreenText.Length - SecondaryScreenText.Replace("/", "").Length;
            SplitCount += SecondaryScreenText.Length - SecondaryScreenText.Replace("-", "").Length;
            SplitCount += SecondaryScreenText.Length - SecondaryScreenText.Replace("(", "").Length;
            SplitCount += SecondaryScreenText.Length - SecondaryScreenText.Replace(")", "").Length;
            var LastWasNumber = 0;
            for (int i = 0; i < SecondaryScreenText.Length; i++)
            {
                Console.WriteLine("ssl = " + SecondaryScreenText.Length);
                if (Regex.IsMatch(SecondaryScreenText[i].ToString(), @"^[0-9]$"))
                {
                    LastWasNumber = 1;
                    SplitCount += LastWasNumber;
                    Console.WriteLine("pop" + SplitCount);
                }
                else {
                    LastWasNumber = 0;
                }
            }
            Console.WriteLine(SplitCount);
            for (int i = 0; i < SplitCount; i++)
            {
                if (Regex.IsMatch(SecondaryScreenText, @"^[0-9]+$"))
                {
                    Equation.Add(SecondaryScreenText.Substring(0, SecondaryScreenText.IndexOfAny(OperatorsPlus)));
                    SecondaryScreenText = SecondaryScreenText.Remove(0, SecondaryScreenText.Substring(0, SecondaryScreenText.IndexOfAny(OperatorsPlus)).Length);
                }
                else {
                    Equation.Add(SecondaryScreenText.Substring(0,1));
                    SecondaryScreenText = SecondaryScreenText.Remove(0, 1);
                }
            }
            for (int i = 0; i < Equation.Count; i++)
            {
                Console.WriteLine(Equation[i]);
            }
            if (Equation.Count > 2)
            {
                while (Equation.Count > 1)
                {
                    for (int i = 0; i < Equation.Count; i++)
                    {
                        var StartIndex = -1;
                        if (Equation.Exists(x => x == "("))
                        {
                            while (true)
                            {
                                if (StartIndex == Equation.FindIndex(x => x == "("))
                                {
                                    break;
                                }
                                StartIndex = Equation.FindIndex(x => x == "(");
                            }
                        }
                        else
                        {
                            StartIndex = -1;
                        }
                        var Var0 = Equation[StartIndex + 1];
                        var Var1 = Equation[StartIndex + 2];
                        var Var2 = Equation[StartIndex + 3];
                        var output = 0;
                        if (Var1 == "+")
                        {
                            output = Int32.Parse(Var0) + Int32.Parse(Var2);
                        }
                        else if (Var1 == "-")
                        {
                            output = Int32.Parse(Var0) - Int32.Parse(Var2);
                        }
                        if (Equation.Exists(x => x == "("))
                        {
                            Equation.RemoveAt(StartIndex + 4);
                            Equation.RemoveAt(StartIndex + 3);
                            Equation.RemoveAt(StartIndex + 2);
                            Equation.RemoveAt(StartIndex + 1);
                            Equation.RemoveAt(StartIndex);
                        }
                        else
                        {
                            Equation.RemoveAt(StartIndex + 3);
                            Equation.RemoveAt(StartIndex + 2);
                            Equation.RemoveAt(StartIndex + 1);
                        }
                        Equation.Insert(0, output.ToString());
                    }
                }
            }
            if (Equation.Count > 0) {
                MainScreen.Text = Equation[0];
                Console.WriteLine("Equation= "+ Equation[0]);
            }
        }
    }
}


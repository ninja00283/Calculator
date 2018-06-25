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
            char[] Operators = { '*', '/', '+', '-', '=' };
            if (UserInput.LastIndexOfAny(Operators) == UserInput.Length-1)
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
            if (SecondaryScreen.Text[SecondaryScreen.Text.Length-1] == '=') {
                SecondaryScreen.Text.Remove(SecondaryScreen.Text.Length - 1,1);
            }

            char[] Operators = { '*', '/', '+', '-' };
            char[] OperatorsPlus = { '*', '/', '+', '-', '(', ')', '=' };
            Regex regex = new Regex(@"^[0-9]$");
            List<string> Equation = new List<string>();
            var SecondaryScreenText = SecondaryScreen.Text;
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
                if (regex.IsMatch(SecondaryScreenText[i].ToString()))
                {
                    SplitCount += Math.Abs(LastWasNumber - 1);
                    LastWasNumber = 1;
                }
                else {
                    LastWasNumber = 0;
                }
                
            }
            for (int i = 0; i < SplitCount; i++)
            {
                if (regex.IsMatch(SecondaryScreenText[0].ToString()))
                {
                    Equation.Add(SecondaryScreenText.Substring(0, SecondaryScreenText.IndexOfAny(OperatorsPlus)));
                    SecondaryScreenText = SecondaryScreenText.Remove(0, SecondaryScreenText.Substring(0, SecondaryScreenText.IndexOfAny(OperatorsPlus)).Length);
                }
                else {
                    Equation.Add(SecondaryScreenText.Substring(0,1));
                    SecondaryScreenText = SecondaryScreenText.Remove(0, 1);
                }
            }
            
            if (Equation.Count > 2)
            {
                while (Equation.Count > 1 && String.Join("",Equation).LastIndexOfAny(Operators) != String.Join("", Equation).ToString().Length-1)
                {
                    for (int i = 0; i < Equation.Count; i++)
                    {
                        var StartIndex = -1;
                        if (Equation.Exists(x => x == "("))
                        {
                            while (true)
                            {
                                if (StartIndex == Equation.IndexOf("("))
                                {
                                    break;
                                }
                                StartIndex = Equation.IndexOf("(");
                            }
                        }

                        if (Equation.Exists(x => x == "*") || Equation.Exists(x => x == "/"))
                        {
                            if (Equation.IndexOf("*", StartIndex + 1) < Equation.IndexOf(")", StartIndex + 1) || Equation.IndexOf("/", StartIndex + 1) < Equation.IndexOf(")", StartIndex + 1))
                            {
                                var EndIndex = 0;
                                if (Equation.Exists(x => x == ")"))
                                {
                                    EndIndex = Equation.IndexOf("*", StartIndex);
                                }
                                else
                                {
                                    EndIndex = Equation.Count();
                                }

                                var StartIndex1 = 0;
                                var StartIndex2 = 0;

                                while (true && Equation.Exists(x => x == "*"))
                                {
                                    if (StartIndex1 == Equation.IndexOf("*", Math.Max(StartIndex, 0)))
                                    {
                                        break;
                                    }
                                    StartIndex1 = Equation.IndexOf("*", Math.Max(StartIndex, 0));
                                }

                                while (true && Equation.Exists(x => x == "/"))
                                {
                                    if (StartIndex2 == Equation.IndexOf("/", Math.Max(StartIndex, 0)))
                                    {
                                        break;
                                    }
                                    StartIndex2 = Equation.IndexOf("/", Math.Max(StartIndex, 0));
                                }

                                StartIndex = Math.Max(Math.Max(StartIndex1 - 1, StartIndex2 - 1), 0) - 1;
                            }
                        }
                        Console.WriteLine(StartIndex);
                        var Var0 = Equation[StartIndex + 1];
                        var Var1 = Equation[StartIndex + 2];
                        var Var2 = Equation[StartIndex + 3];
                        var output = 0f;
                        if (Var1 == "+")
                        {
                            output = float.Parse(Var0) + float.Parse(Var2);
                            Console.WriteLine(Var0 + "+" + Var2 + "=" + output);
                        }
                        else if (Var1 == "*")
                        {
                            Console.WriteLine(Var0 + "*" + Var2 + "=" + output);
                            output = float.Parse(Var0) * float.Parse(Var2);
                            Console.WriteLine(Var0 + "*" + Var2 + "=" + output);
                        }
                        else if (Var1 == "/")
                        {
                            Console.WriteLine(Var0 + "/" + Var2 + "=" + output);
                            output = float.Parse(Var0) / float.Parse(Var2);
                            Console.WriteLine(Var0 + "/" + Var2 + "=" + output);
                        }
                        else if (Var1 == "-")
                        {
                            output = float.Parse(Var0) - float.Parse(Var2);
                            Console.WriteLine(Var0 + "-" + Var2 + "=" + output);
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
                            Console.WriteLine(Equation[StartIndex + 3] + "," + Equation[StartIndex + 2] + "," + Equation[StartIndex + 1] + ":removed");
                            Equation.RemoveAt(StartIndex + 3);
                            Equation.RemoveAt(StartIndex + 2);
                            Equation.RemoveAt(StartIndex + 1);
                        }
                        Equation.Insert(Math.Max(StartIndex+1,0), output.ToString());
                        
                    }
                }
            }
            if (Equation.Count > 0) {
                MainScreen.Text = Equation[0];
            }
        }
    }
}


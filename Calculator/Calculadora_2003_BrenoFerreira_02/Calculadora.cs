using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora_2003_BrenoFerreira_02
{
    public partial class Calculadora : Form
    {
      
        public Calculadora()
        {
            InitializeComponent();
        }//Initialize Application

        private string Operation;
        private string LastText;
        private double Value1 = 0;
        private double Value2 = 0;
        private bool EndOperation = false;
        private bool GoOperation1 = true;
        private bool IniSecond = false;

        private void click_button(object sender, EventArgs e)
        {
            bool HaveV = (Resultado.Text.IndexOfAny(new char[] { ',' })) >= 0;
            Button VText = null;
            VText = (Button)sender;
            if (Resultado.Text == "0" || IniSecond || EndOperation)
            {
                EndOperation = false;
                IniSecond = false;
                Resultado.Clear();
                Resultado.Text += VText.Text;
                if (VText.Text == "," && HaveV)
                {
                    LastText = Resultado.Text;
                    LastText = LastText.Remove(LastText.Length - 1);
                    Resultado.Text = LastText;
                }
            }
            else if (!IniSecond)
            {
                Resultado.Text += VText.Text;
                if (VText.Text == "," && HaveV)
                {
                    LastText = Resultado.Text;
                    LastText = LastText.Remove(LastText.Length - 1);
                    Resultado.Text = LastText;
                }
            }
            if (EndOperation)
            {
                EndOperation = false;
                Resultado.Clear();
            }

            if (string.IsNullOrWhiteSpace(Resultado.Text) && VText.Text == ",")
            {
                Resultado.Text = "0";
            }
            if (Resultado.Text == "0" && VText.Text == ",")
            {
                Resultado.Text = "0,";
            }
        }//Numbers
       
        private void click_operacoes(object sender, EventArgs e)
        {
            try
            {
                if (Operation != null)
                {
                    ClassOperating.Identificacao(this.Value1, this.Value2, this.GoOperation1, this.IniSecond, this.EndOperation, this.Operation, this.Resultado.Text);
                    Resultado.Text = ClassOperating.Result;
                    this.Value1 = double.Parse(Resultado.Text);
                    this.Value2 = 0;
                    Button OText = (Button)sender;
                    Operation = OText.Text;
                    IniSecond = true;
                }
                else
                {
                    if (EndOperation)
                    {
                        EndOperation = false;
                        Operation = null;
                    }
                    Button OText = (Button)sender;
                    Operation = OText.Text;

                    if (Value1 == 0 && Resultado.Text != "0" || Resultado.Text != "0," || Resultado.Text != ",")
                    {
                        this.Value1 = double.Parse(Resultado.Text);
                        this.Value2 = 0;
                        this.IniSecond = true;
                        this.GoOperation1 = false;
                    }
                    else
                    {
                        GoOperation1 = true;
                    }
                }
            }
            catch
            {
                Resultado.Text = "0";
                MessageBox.Show("Operação Impossível, tente novamente!");
            }
            
        }//Operations

        private void Resultado_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool HaveV = (Resultado.Text.IndexOfAny(new char[] { ',' })) >= 0;

            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
            }
            else
            {
                if (HaveV == false)
                {
                    if (e.KeyChar.ToString().IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',' }) == -1)
                    {
                        e.Handled = true;
                    }
                }
                else if (e.KeyChar.ToString().IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }) == -1)
                {
                    e.Handled = true;
                }
            }
        }//Lock Text Box

        private void Seno_Click(object sender, EventArgs e)
        {
            try
            {
                ClassOperating.Seno(Value1, Value2, GoOperation1, IniSecond, Operation, Resultado.Text);
                Resultado.Text = ClassOperating.Result;
            }
            catch
            {
                Resultado.Text = "0";
                MessageBox.Show("Operação Impossível, tente novamente!");
            }
            
        }//Sin

        private void Cosseno_Click(object sender, EventArgs e)
        {
            try
            {
                ClassOperating.Cosseno(Value1, Value2, GoOperation1, IniSecond, Operation, Resultado.Text);
                Resultado.Text = ClassOperating.Result;
            }
            catch
            {
                Resultado.Text = "0";
                MessageBox.Show("Operação Impossível, tente novamente!");
            }
        }//Cos

        private void Tangente_Click(object sender, EventArgs e)
        {
            try
            {
                ClassOperating.Tangente(Value1, Value2, GoOperation1, IniSecond, Operation, Resultado.Text);
                Resultado.Text = ClassOperating.Result;
            }
            catch
            {
                Resultado.Text = "0";
                MessageBox.Show("Operação Impossível, tente novamente!");
            }
        }//Tan

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                ClassOperating.Raiz(Value1, Value2, GoOperation1, IniSecond, Operation, Resultado.Text);
                Resultado.Text = ClassOperating.Result;
            }
            
            catch
            {
                Resultado.Text = "0";
                MessageBox.Show("Operação Impossível, tente novamente!");
            }
        }//Raiz

        private void button26_Click(object sender, EventArgs e)
        {
            try
            {
                ClassOperating.Identificacao(this.Value1, this.Value2, this.GoOperation1, this.IniSecond, this.EndOperation, this.Operation, this.Resultado.Text);
                Resultado.Text = ClassOperating.Result;
                this.Value1 = 0;
                this.Value2 = 0;
                this.Operation = null;
            }
            catch
            {
                Resultado.Text = "0";
                MessageBox.Show("Operação Impossível,tente novamente!");
            }
            
        }//Identify Operation

        private void button21_Click(object sender, EventArgs e)
        {
            ClassOperating.LimparTudo(Value1, Value2, GoOperation1, IniSecond, EndOperation, Operation);
            Resultado.Text = ClassOperating.Result;
            Value1 = 0;
            Value2 = 0;
        }//Clear All

        private void button20_Click(object sender, EventArgs e)
        {
            Resultado.Text = "0";
        }//Clear Partial

        private void button19_Click(object sender, EventArgs e)
        {
            ClassOperating.Delete(Resultado.TextLength, Resultado.Text, LastText);
            Resultado.Text = ClassOperating.Result;
        }//Delete

        private void Recursiva_Fat_Click(object sender, EventArgs e)
        {
            try
            {
                if (Resultado.Text == null)
                {
                    MessageBox.Show("Digite um valor");
                }
                else
                {
                    IniSecond = true;
                    Resultado.Text = (ClassOperating.Rec_Fat(double.Parse(Resultado.Text))).ToString();
                }
            }            
            catch
            {
                Resultado.Text = "0";
                MessageBox.Show("Operação Impossível, tente novamente!");
            }
        }//Recursive Fatorial

        private void Iterativa_Fat_Click(object sender, EventArgs e)
        {
            try
            {
                if (Resultado.Text == null)
                {
                    MessageBox.Show("Digite um valor");
                }
                else
                {
                    IniSecond = true;
                    Resultado.Text = (ClassOperating.Ite_Fat(Resultado.Text)).ToString(); ;
                }
            }
            catch
            {
                Resultado.Text = "0";
                MessageBox.Show("Operação Impossível, tente novamente!");
            }
        }//Iterative Fatorial

        private void Recursiva_Fib_Click(object sender, EventArgs e)
        {
            try
            {
                if (Resultado.Text == null)
                {
                    MessageBox.Show("Digite um valor");
                }
                else
                {
                    IniSecond = true;
                    if (double.Parse(Resultado.Text) <= 30)
                    {
                        Resultado.Text = (ClassOperating.Rec_Fib(double.Parse(Resultado.Text))).ToString(); ;
                    }
                    else
                    {
                        Resultado.Text = (ClassOperating.FiboNumeroAlto(double.Parse(Resultado.Text))).ToString(); ;
                    }
                }
            }
            catch
            {
                Resultado.Text = "0";
                MessageBox.Show("Operação Impossível, tente novamente!");
            }
        }//Recursiva Fibonacci

        private void Iterativa_Fib_Click(object sender, EventArgs e)
        {
            try
            {
                if (Resultado.Text == null)
                {
                    MessageBox.Show("Digite um valor");
                }
                else
                {
                    IniSecond = true;
                    Resultado.Text = (ClassOperating.Ite_Fib(Resultado.Text)).ToString();
                }
            }
            catch
            {
                Resultado.Text = "0";
                MessageBox.Show("Operação Impossível, tente novamente!");
            }
        }//Iterative Fibonacci
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_2003_BrenoFerreira_02
{
    class ClassOperating
    {
        public static string Result;
        public static string Identificacao(double V1, double V2, bool GO, bool Ini,bool EndOP, string OP, string ResultadoLa)
        {
            V2 = double.Parse(ResultadoLa);
            ResultadoLa = "";
            if (OP == "+")
            {
                Result = (V1 + V2).ToString();
                V1 = 0;
                V2 = 0;
                GO = true;
                Ini = true;
                OP = null;
                EndOP = true;
            }
            else if (OP == "-")
            {
                Result = (V1 - V2).ToString();
                V1 = 0;
                V2 = 0;
                GO = true;
                Ini = true;
                OP = null;
                EndOP = true;
            }
            else if (OP == "*")
            {
                Result = (V1 * V2).ToString();
                V1 = 0;
                V2 = 0;
                GO = true;
                Ini = true;
                OP = null;
                EndOP = true;
            }
            else if (OP == "/")
            {
                Result = (V1 / V2).ToString();
                V1 = 0;
                V2 = 0;
                GO = true;
                Ini = true;
                OP = null;
                EndOP = true;
            }
            else if (OP == "R")
            {
                Result = (V1 % V2).ToString(); 
                V1 = 0;
                V2 = 0;
                GO = true;
                Ini = true;
                OP = null;
                EndOP = true;
            }
            else if (OP == "^")
            {
                Result = (Math.Pow(V1, V2)).ToString();
                V1 = 0;
                V2 = 0;
                GO = true;
                Ini = true;
                OP = null;
                EndOP = true;
            }
            else if (OP == "%")
            {
                Result = (V1 / 100 * V2).ToString();
                V1 = 0;
                V2 = 0;
                GO = true;
                Ini = true;
                OP = null;
                EndOP = true;
            }
            return Result;
        }//IdentificarOperação
        public static string Seno(double V1, double V2, bool GO, bool Ini, string OP, string ResultadoLa)
        {
            V1 = 0;
            V2 = 0;
            V1 = double.Parse(ResultadoLa);
            Result = (Math.Sin((Math.PI / 180) * V1)).ToString();
            GO = true;
            Ini = false;
            OP = null;
            return Result;
        }//Seno
        public static string Cosseno( double V1,double V2,bool GO, bool Ini, string OP,string ResultadoLa)
        {
            V1 = 0;
            V2 = 0;
            V1 = double.Parse(ResultadoLa);
            Result = (Math.Cos((Math.PI / 180) * V1)).ToString();
            GO = true;
            Ini= false;
            OP = null;
            return Result;
        }//Cosseno
        public static string Tangente(double V1, double V2, bool GO, bool Ini, string OP, string ResultadoLa)
        {
            V1 = 0;
            V2 = 0;
            V1 = double.Parse(ResultadoLa);
            Result = (Math.Tan((Math.PI / 180) * V1)).ToString();
            GO = true;
            Ini = false;
            OP = null;
            return Result;
        }//Tangente
        public static string Raiz(double V1, double V2, bool GO, bool Ini, string OP, string ResultadoLa)
        {
            V1 = 0;
            V2 = 0;
            V1 = double.Parse(ResultadoLa);
            Result = (Math.Sqrt(V1)).ToString();
            GO = true;
            Ini = false;
            OP = null;
            return Result;
        }//Tangente
        public static string LimparTudo(double V1, double V2, bool GO, bool Ini, bool EndOP, string OP)
        {
            
            EndOP = false;
            GO = true;
            Ini = false;
            OP = null;
            Result = "0";
            V1 = 0;
            V2 = 0;
            return Result;
        }//C
        public static string Delete(int QuantL, string ResultadoLa, string Last)
        {
            if (QuantL == 1)
            {
                Result = "0";
            }
            else
            {
                Last = ResultadoLa;
                Last = Last.Remove(Last.Length - 1);
                Result = Last;
            }
            return Result;
        }//Delete
        public static double Rec_Fat(double Number)
        {
            if (Number < 1)
            {
                return Number = 1;
            }
            return Rec_Fat(Number - 1) * Number;
        }//Recurssive Fatorial
        public static double Ite_Fat(string Text)
        {
            double Total = 1;
            for (double i = double.Parse(Text); i > 0; i--)
            {
                Total *= i;
            }
            return Total;
        }//Iteractive Fatorial
        public static double FiboNumeroAlto(double Text, double Number1 = 1, double Number2 = 0)
        {
            if (Text == 0)
            {
                return Number2;
            }
            if (Text == 1)
            {
                return Number1;
            }
            return FiboNumeroAlto(Text - 1, Number1 + Number2, Number1);
        }//High Number Fibonacci
        public static double Rec_Fib(double Number)
        {
            if (Number == 0 || Number == 1)
            {
                return Number;
            }
            else
            {
                return Rec_Fib(Number - 1) + Rec_Fib(Number - 2);
            } 
        }//Recursive Fibonacci
        public static double Ite_Fib(string Text)
        {
            double Number1 = 0;
            double Number2 = 1;
            for (int i = 0; i < double.Parse(Text); i++)
            {
                double SaveN = Number1;
                Number1 = Number2;
                Number2 += SaveN;
            }
            return Number1;
        }//Iteractive Fibonacci
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Numerics;
using System.Collections;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Ferma
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public int Log2n(BigInteger n)
        {
            if (n < 1)
            {
                return -1;
            }
            BigInteger x = 0;
            BigInteger num = 1;
            while (num <= n)
            {
                x++;
                num *= 2;
            }
            return (int)x;
        }
        //генератор
        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            
            Stopwatch clock = new Stopwatch();
            if (Mode.SelectedIndex == 0)
            {
                clock.Start();
                if (BitSize.Text == "")
                {
                    MessageBox.Show("Введите число бит");
                    return;
                }
                int bits = int.Parse(BitSize.Text);
                BigInteger num = 0;

                do
                {

                    num = Fermas.getRandom(bits, Fermas.minimum(bits), Fermas.maximum(bits));


                } while (!Fermas.FermaAlg(num, bits));
                clock.Stop();
                ResultNumber.Text = num.ToString();

                Error.Text = Math.Pow(2, -(Log2n(num))).ToString();
                Time1.Text = clock.ElapsedMilliseconds.ToString() + " миллисекунд";
            }

        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch clock = new Stopwatch();
            
            if (Mode.SelectedIndex==0)
            {
                if (NumberForChecking.Text == "")
                {
                    MessageBox.Show("Введите число");
                    return;
                }
                clock.Start();
                BigInteger num = BigInteger.Parse(NumberForChecking.Text);
                int log = Log2n(num);
               
                if (Fermas.FermaAlg(num, log))
                {
                    clock.Stop();
                    MessageBox.Show("Число вероятно простое");
                    Error.Text = Math.Pow(2, -log).ToString();
                }
                else
                {
                    clock.Stop();
                    MessageBox.Show("Число составное");
                    Error.Text = "0";
                }
                Time1.Text = clock.ElapsedMilliseconds.ToString() + " миллисекунд";
                

            }



        }

        //ввод в бит
        private void BitSize_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            /*if (Regex.IsMatch(e.Text, "@[^0-9]"))
            {
                e.Handled = true;
            }*/
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }
        //вставка в бит
        private void BitSize_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            string TextFotChecking = (string)e.DataObject.GetData(typeof(String));
            if (Regex.IsMatch(TextFotChecking, @"[^0-9]"))
            {
                e.CancelCommand();
            }
        }

        //ввод в число
        private void NumberForChecking_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        //вставка в число
        private void NumberForChecking_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            string TextFotChecking = (string)e.DataObject.GetData(typeof(String));
            if (Regex.IsMatch(TextFotChecking, @"[^0-9]"))
            {
                e.CancelCommand();
            }
        }

        private void NumberForChecking_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Space)
            {
                e.Handled = true;

            }
        }

        private void BitSize_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Space)
            {
                e.Handled = true;

            }
        }

        private void Mode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        private void SecCheckBut_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch clock = new Stopwatch();
            if (NumberForChecking.Text == "")
            {
                MessageBox.Show("Введите число");
                return;
            }

            clock.Start();
            BigInteger num = BigInteger.Parse(NumberForChecking.Text);
            int log = Log2n(num);
            if (M_R.MRTest(num, log))
            {
                clock.Stop();
                MessageBox.Show("Число вероятно простое");
                SecError.Text = Math.Pow(4, -log).ToString();
            }
            else
            {
                clock.Stop();
                MessageBox.Show("Число составное");
                SecError.Text = "0";
            }
            Time2.Text = clock.ElapsedMilliseconds.ToString() + " миллисекунд";
            
        }

        private void SecGenBut_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch clock = new Stopwatch();
            if (BitSize.Text == "")
            {
                MessageBox.Show("Введите число бит");
                return;
            }
            clock.Start();
            int bits = int.Parse(BitSize.Text);
            BigInteger num = 0;
            do
            {

                num = Fermas.getRandom(bits, Fermas.minimum(bits), Fermas.maximum(bits));


            } while (!M_R.MRTest(num, Log2n(num)));
            clock.Stop();
            SecNumber.Text = num.ToString();
            SecError.Text = Math.Pow(4, -Log2n(num)).ToString();
            Time2.Text = clock.ElapsedMilliseconds.ToString() + " миллисекунд";
           
        }

        private void SSGenerateButton_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch clock = new Stopwatch();
            if (BitSize.Text == "")
            {
                MessageBox.Show("Введите число бит");
                return;
            }
            clock.Start();
            int bits = int.Parse(BitSize.Text);
            BigInteger num = 0;
            int log = 0;
            do
            {

                num = Fermas.getRandom(bits, Fermas.minimum(bits), Fermas.maximum(bits));
                log = Log2n(num);

            } while (!SSh.SShAlg(num, log));
            clock.Stop();
            SSGenerateNumber.Text = num.ToString();
            SSError.Text = Math.Pow(2, log).ToString();
            SSTime.Text = clock.ElapsedMilliseconds.ToString() + " миллисекунд";
        }

        private void SsCheckButton_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch clock = new Stopwatch();
            if (NumberForChecking.Text == "")
            {
                MessageBox.Show("Введите число");
                return;
            }
            clock.Start();
            BigInteger num = BigInteger.Parse(NumberForChecking.Text);

            int log = Log2n(num);
            if (SSh.SShAlg(num, log))
            {
                clock.Stop();
                MessageBox.Show("Число вероятно простое");
                SSError.Text = Math.Pow(2, -log).ToString();
            }
            else
            {
                clock.Stop();
                MessageBox.Show("Число составное");
                SSError.Text = "0";
            }
            SSTime.Text = clock.ElapsedMilliseconds.ToString() + " миллисекунд";
        }
    }
}


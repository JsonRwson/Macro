using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int LD = 0x02;
        private const int LU = 0x04;

        public MainWindow()
        {
            InitializeComponent();
        }

        bool on = false;
        int delay = 50;
        bool cancel = false;
        private void auto_button_click(object sender, RoutedEventArgs e)
        {
            cancel = false;

            if (on)
            {
                cancel = true;
                autoButton.Content = "Start";
                on = false;
            }

            if (on == false)
            {
                if (cancel == false)
                {
                    Thread a = new Thread(AutoClick)
                    {
                        IsBackground = true
                    };
                    autoButton.Content = "Stop";
                    on = true;
                    a.Start();
                }
            }
        }

        public void AutoClick()
        {
            Thread.Sleep(delay);
            while (cancel == false)
            {
                mouse_event(LD, 0, 0, 0, 0);
                mouse_event(LU, 0, 0, 0, 0);
                Thread.Sleep(delay);

            }
        }

        bool on1 = false;
        int delay1 = 50;
        bool cancel1 = false;
        string messageText = "";
        public void spam_button_click(object sender, RoutedEventArgs e)
        {
            messageText = messageContent.Text;
            cancel1 = false;         

            if (on1)
            {
                cancel1 = true;
                on1 = false;
                spamButton.Content = "Start";
            }

            if (on1 == false)
            {
                if (cancel1 == false)
                {
                    Thread b = new Thread(AutoSpammer)
                    {
                        IsBackground = true
                    };
                    spamButton.Content = "Stop";
                    on1 = true;
                    b.Start();

                }
            }

        }

        private void AutoSpammer()
        {
            Thread.Sleep(delay1);
            if (pressEnter)
            {
                while (cancel1 == false)
                {
                    SendKeys.SendWait(messageText);
                    SendKeys.SendWait("{ENTER}");
                    Thread.Sleep(delay1);
                }
            }

            if (pressEnter == false)
            {
                while (cancel1 == false)
                {
                    SendKeys.SendWait(messageText);
                    Thread.Sleep(delay1);
                }
            }
        }

        int i = 0;
        private void testClicks(object sender, RoutedEventArgs e)
        {
            i++;
            clickCounter.Content = i;
        }

        private void resetCounter(object sender, RoutedEventArgs e)
        {
            clickCounter.Content = 0;
            i = 0;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            delay = (int)Delay.Value + 50;
            delayCounter.Content = $"{(int)Delay.Value + 50} ms";

        }

        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            delay1 = (int)Delay1.Value + 50;
            delayCounter1.Content = $"{(int)Delay1.Value + 50} ms";

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Topmost = false;
        }

        bool pressEnter = false;
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            pressEnter = true;
        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            pressEnter = false;
        }
    }
}
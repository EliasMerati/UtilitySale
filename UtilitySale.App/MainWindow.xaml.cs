using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using UtilitySale.App._ٌWindows;
using UtilitySale.Utility;
using UtilitySale.Data;
using System.Transactions;
using System.Reflection;

namespace UtilitySale.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string V = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHl2AD0gPVknKsaW0un+3PuM6TTcPMUAWEURKXNso0e5OFPaZYasFtsxNoDemsFOXbvf7SIcnyAkFX/4u37NTfx7g+0IqLXw6QIPolr1PvCSZz8Z5wjBNakeCVozGGOiuCOQDy60XNqfbgrOjxgQ5y/u54K4g7R/xuWmpdx5OMAbUbcy3WbhPCbJJYTI5Hg8C/gsbHSnC2EeOCuyA9ImrNyjsUHkLEh9y4WoRw7lRIc1x+dli8jSJxt9C+NYVUIqK7MEeCmmVyFEGN8mNnqZp4vTe98kxAr4dWSmhcQahHGuFBhKQLlVOdlJ/OT+WPX1zS2UmnkTrxun+FWpCC5bLDlwhlslxtyaN9pV3sRLO6KXM88ZkefRrH21DdR+4j79HA7VLTAsebI79t9nMgmXJ5hB1JKcJMUAgWpxT7C7JUGcWCPIG10NuCd9XQ7H4ykQ4Ve6J2LuNo9SbvP6jPwdfQJB6fJBnKg4mtNuLMlQ4pnXDc+wJmqgw25NfHpFmrZYACZOtLEJoPtMWxxwDzZEYYfT";

        public MainWindow()
        {
            
            Stimulsoft.Base.StiLicense.Key = V;
            InitializeComponent();
            Timer();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LblVersion.Content = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            lbldate.Text = DateTime.Now.Date();
            Lblhour.Content = DateTime.Now.ToString("HH:mm:ss");
            using (TransactionScope ts = new TransactionScope())
            {
                using (UtilitySale_DBEntities db = new UtilitySale_DBEntities())
                {
                    var v = db.Tax.Any();
                    var f = db.People.Any();
                    if (v == true && f == true)
                    {
                        return;
                    }
                    else if (v == false && f == false)
                    {
                        try
                        {
                            db.InsertTax(0);
                            db.InsertPeople("متفرقه", "", "", 0, 0, "مشتری");
                            db.SaveChanges();
                            MessageBox.Show("مقادیر پیش فرض  با موفقیت ثبت شدند");
                            ts.Complete();
                        }
                        catch (Exception n)
                        {
                            MessageBox.Show(n.Message);
                        }
                    }

                }


            }

        }

        private void Timer()
        {
            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Tick += new EventHandler(timer_Tick);
            tmr.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            tmr.Start();
        }
        private void timer_Tick(object sender, EventArgs e)//// دستورات ساعت
        {
            Lblhour.Content = DateTime.Now.ToString("HH:mm:ss");
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void People_Click(object sender, RoutedEventArgs e)
        {
            new WinPeople().ShowDialog();
        }

        private void Product_click(object sender, RoutedEventArgs e)
        {
            new WinProduct().ShowDialog();
        }

        private void Showinvoice_click(object sender, RoutedEventArgs e)
        {
            new WinShowInvoice().ShowDialog();
        }

        private void company_click(object sender, RoutedEventArgs e)
        {
            new WinCompany().ShowDialog();
        }

        private void NewInvoice_click(object sender, RoutedEventArgs e)
        {
            WinInvoice invoice = new WinInvoice();
            invoice.type = false;
            invoice.ShowDialog();
        }

        private void Tax_click(object sender, RoutedEventArgs e)
        {
            new WinTax().ShowDialog();
        }

        private void account_click(object sender, RoutedEventArgs e)
        {
            new WinAccount().ShowDialog();
        }

        private void Backup_click(object sender, RoutedEventArgs e)
        {
            BackupRestor b = new BackupRestor();
            b.BackUpMyDB();

        }

        private void Restor_click(object sender, RoutedEventArgs e)
        {
            BackupRestor r = new BackupRestor();
            r.ReStorMyDB();
        }

        private void us_click(object sender, RoutedEventArgs e)
        {
            new WinUs().ShowDialog();
        }

        private void Upgrade_click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("برای بروزرسانی، نرم افزار بسته خواهد شد و پنجره بروزرسانی نرم افزار نمایش داده خواهد شد.آیا ادامه می دهید؟", "توجه", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "//Update.exe");
                Environment.Exit(0);
            }
        }
    }
}

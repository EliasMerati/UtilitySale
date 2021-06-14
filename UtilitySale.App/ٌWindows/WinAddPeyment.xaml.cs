using System;
using System.Windows;
using System.Windows.Input;
using UtilitySale.Utility;
using System.Windows.Threading;
using UtilitySale.Data;
using System.Transactions;

namespace UtilitySale.App._ٌWindows
{
    /// <summary>
    /// Interaction logic for WinAddPeyment.xaml
    /// </summary>
    public partial class WinAddPeyment : Window
    {
        public WinAddPeyment()
        {
            InitializeComponent();
            Timer();
        }
        
        public int PeopleID { get; set; }
        private void timer_Tick(object sender, EventArgs e)//// دستورات ساعت
        {
            LblDate.Content = DateTime.Now.Date() + " _ " + DateTime.Now.ToString("HH:mm:ss");
        }
        private void Timer()
        {
            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Tick += new EventHandler(timer_Tick);
            tmr.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            tmr.Start();
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LblDate.Content = DateTime.Now.Date() + " _ " + DateTime.Now.ToString("HH:mm:ss");
            Cmbtype.SelectedIndex = 0;
            Cmbtype2.SelectedIndex = 0;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    using (UtilitySale_DBEntities db = new UtilitySale_DBEntities())
                    {
                        var g = db.People.Find(PeopleID);
                        if (Cmbtype.Text == "خرید" && Cmbtype2.Text == "نقدی")
                        {
                            if (g.PeopleCreditor != 0)
                            {
                                db.UpdateCreditor(PeopleID, int.Parse(txtPayment.Text.Trim()));
                                db.SaveChanges();
                            }
                        }
                        else if (Cmbtype.Text == "خرید" && Cmbtype2.Text == "نسیه")
                        {
                            if (g.PeopleCreditor != 0)
                            {
                                db.UpdateCreditor(PeopleID, int.Parse(txtPayment.Text.Trim()));
                                db.SaveChanges();
                            }
                            else
                            {
                                db.UpdateDeptor1(PeopleID, int.Parse(txtPayment.Text.Trim()));
                                db.SaveChanges();
                            }
                        }
                        else if (Cmbtype.Text == "بدهی از قبل" && Cmbtype2.Text == "نقدی")
                        {
                            db.UpdateDeptor2(PeopleID, int.Parse(txtPayment.Text.Trim()));
                            db.SaveChanges();
                        }
                        else if (Cmbtype.Text == "بدهی از قبل" && Cmbtype2.Text == "نسیه")
                        {
                            MessageBox.Show("امکان پذیر نیست ، لطفا موارد دیگر را انتخاب کنید");
                            reset();
                        }
                        else if (Cmbtype.Text == "خرید" && Cmbtype2.Text == "تسویه حساب")
                        {
                            if (g.PeopleCreditor > 0)
                            {
                                db.UpdateCreditor(PeopleID, int.Parse(txtPayment.Text.Trim()));
                                db.SaveChanges();
                            }
                            MessageBox.Show("امکان پذیر نیست ، لطفا موارد دیگر را انتخاب کنید");
                            reset();
                        }
                        else if (Cmbtype.Text == "بدهی از قبل" && Cmbtype2.Text == "تسویه حساب")
                        {
                            db.UpdateDeptor2(PeopleID, int.Parse(txtPayment.Text.Trim()));
                            db.SaveChanges();
                        }
                        db.InsertPay(PeopleID, int.Parse(txtPayment.Text.Trim()), DateTime.Now.Date() + " _ " + DateTime.Now.ToString("HH:mm:ss")
                            , Cmbtype.Text, Cmbtype2.Text);
                        db.SaveChanges();
                        reset();
                        ts.Complete();
                    }
                }
                catch (Exception n)
                {
                    MessageBox.Show(n.Message);
                }


            }
        }

        void reset()
        {
            txtPayment.Text = string.Empty;
            Cmbtype.SelectedIndex = 0;
            Cmbtype2.SelectedIndex = 0;
        }
    }
}

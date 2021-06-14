using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using UtilitySale.Data;
using System.Text.RegularExpressions;


namespace UtilitySale.App._ٌWindows
{
    /// <summary>
    /// Interaction logic for WinAccount.xaml
    /// </summary>
    public partial class WinAccount : Window
    {
        public WinAccount()
        {
            InitializeComponent();
        }
        UtilitySale_DBEntities db = new UtilitySale_DBEntities();
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var n = db.Account.Any();
            try
            {
                if (n)
                {
                    db.UpdateAccount(txtAccountName.Text.Trim(),txtAccountNumber.Text.Trim(),txtAccountShaba.Text.Trim(),txtAccountCart.Text.Trim(),
                        txtBank.Text.Trim(),txtShobe.Text.Trim());
                    MessageBox.Show("ویرایش با موفقیت انجام شد");
                }
                else
                {
                    db.InsertAccount(txtAccountName.Text.Trim(), txtAccountNumber.Text.Trim(), txtAccountShaba.Text.Trim(), txtAccountCart.Text.Trim(),
                        txtBank.Text.Trim(), txtShobe.Text.Trim());
                    MessageBox.Show("ثبت با موفقیت انجام شد");
                }
                db.SaveChanges();
            }
            catch (Exception k)
            {
                MessageBox.Show(k.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var n = db.Account.Any();
            var m = db.Account.ToList();
            if (n)
            {
                txtAccountName.Text = m[0].AccountName;
                txtAccountNumber.Text = m[0].AccountNumber;
                txtAccountShaba.Text = m[0].AccountShaba;
                txtBank.Text = m[0].AccountBank;
                txtShobe.Text = m[0].AccountShobeh;
                txtAccountCart.Text = m[0].AccountCart;
            }
        }

        private void txtCompanyName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[0-9]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void txtAccountNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[آ-ی a-z A-Z]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void txtAccountShaba_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[آ-ی]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void txtAccountCart_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[آ-ی a-z A-Z]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void txtBank_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[0-9]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void txtShobe_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[0-9]");
            e.Handled = Regex.IsMatch(e.Text);
        }
    }
}

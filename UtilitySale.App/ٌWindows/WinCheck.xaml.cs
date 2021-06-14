using System;
using System.Transactions;
using System.Windows;
using System.Windows.Input;
using UtilitySale.Data;
using System.Text.RegularExpressions;

namespace UtilitySale.App._ٌWindows
{
    /// <summary>
    /// Interaction logic for WinCheck.xaml
    /// </summary>
    public partial class WinCheck : Window
    {
        public WinCheck()
        {
            InitializeComponent();
        }
        UtilitySale_DBEntities db = new UtilitySale_DBEntities();
        public string name { get; set; }
        public int pid { get; set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblpeople.Content = name;
            cmbtype.SelectedIndex = 1;
        }
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
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    db.InsertCheck(pid, txtChecknumber.Text.Trim(), txtCheckdate.Text.Trim(),
    txtBank.Text.Trim(), int.Parse(txtCheckprice.Text.Trim()), cmbtype.Text, txtdesc.Text.Trim());
                    db.UpdateDeptor1(pid, int.Parse(txtCheckprice.Text.Trim()));
                    MessageBox.Show("ثبت با موفقیت انجام شد");
                    db.SaveChanges();
                    Close();
                    ts.Complete();
                }
                catch (Exception b)
                {
                    MessageBox.Show(b.Message);
                }
            }
        }

        private void txtChecknumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[a-z A-Z آ-ی]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void txtCheckdate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[a-z A-Z آ-ی]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void txtBank_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[0-9]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void txtCheckprice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[a-z A-Z آ-ی]");
            e.Handled = Regex.IsMatch(e.Text);
        }
    }
}

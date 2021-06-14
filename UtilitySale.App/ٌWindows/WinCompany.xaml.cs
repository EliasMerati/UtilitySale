using System;
using System.Linq;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Input;
using UtilitySale.Data;

namespace UtilitySale.App._ٌWindows
{
    /// <summary>
    /// Interaction logic for WinCompany.xaml
    /// </summary>
    public partial class WinCompany : Window
    {
        public WinCompany()
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

        private void txtCompanyName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[0-9]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void txtTel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[آ-ی a-z A-Z]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void txtMobile_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[آ-ی a-z A-Z]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var g = db.Company.Any();
            var f = db.Company.ToList();
            if (g)
            {
                txtCompanyName.Text = f[0].CompanyName;
                txtDesc.Text = f[0].CompanyDesc;
                txtMobile.Text = f[0].CompanyMobile;
                txtTel.Text = f[0].CompanyTel;
            }
        }
        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var f = db.Company.Any();
            try
            {
                if (f)
                {
                    db.UpdateCompany(txtCompanyName.Text.Trim(), txtTel.Text.Trim(), txtMobile.Text.Trim(), txtDesc.Text.Trim());
                    MessageBox.Show("آپدیت با موفقیت انجام گردید");
                    Close();
                }
                else
                {
                    db.InsertCompany(txtCompanyName.Text.Trim(), txtTel.Text.Trim(), txtMobile.Text.Trim(), txtDesc.Text.Trim());
                    MessageBox.Show("ثبت با موفقیت انجام گردید");
                    Close();
                }
                db.SaveChanges();
            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message);
            }
        }
    }
}

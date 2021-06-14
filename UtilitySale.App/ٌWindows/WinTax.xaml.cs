using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using UtilitySale.Data;
using System.Text.RegularExpressions;

namespace UtilitySale.App._ٌWindows
{
    /// <summary>
    /// Interaction logic for WinTax.xaml
    /// </summary>
    public partial class WinTax : Window
    {
        public WinTax()
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
            try
            {
                var res = db.Tax.Any();
                if (res)
                {
                    if (txtTax.Text == null)
                    {
                        db.UpdateTax(0);
                    }
                    else
                    {
                        db.UpdateTax(int.Parse(txtTax.Text.Trim()));
                    }
                    
                }
                else
                {
                    if (txtTax.Text == null)
                    {
                        db.InsertTax(0);
                    }
                    else
                    {
                        db.InsertTax(int.Parse(txtTax.Text.Trim()));
                    }
                    
                }
                db.SaveChanges();
                Close();
            }
            catch (Exception v)
            {
               MessageBox.Show(v.Message);
            }


        }

        private void txtTax_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[آ-ی a-z A-Z]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var res = db.Tax.Any();
            var result = db.Tax.ToList();
            if (res)
            {
                txtTax.Text = result[0].TaxAmount.ToString();
            }
        }
    }
}

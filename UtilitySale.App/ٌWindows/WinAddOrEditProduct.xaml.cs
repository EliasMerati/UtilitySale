using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using UtilitySale.Data;
using System.Text.RegularExpressions;
using System.Transactions;

namespace UtilitySale.App._ٌWindows
{
    /// <summary>
    /// Interaction logic for WinAddOrEditProduct.xaml
    /// </summary>
    public partial class WinAddOrEditProduct : Window
    {
        public WinAddOrEditProduct()
        {
            InitializeComponent();
        }
        UtilitySale_DBEntities db = new UtilitySale_DBEntities();
        public int ProductID { get; set; }
        public string ProductName { get; set; }
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
            var product = db.Product.Find(ProductID);
            if (ProductID != 0)
            {
                txtDescription.Text = product.ProductDesc;
                txtProductName.Text = product.ProductName;
                txtProductPrice.Text = string.Format("{0:N0}", product.ProductLastPrice);
                txtSuply.Text = product.ProductLastCount.ToString();
                txtUnit1.Text = product.ProductUnit;
            }
        }

        void reset()
        {
            txtSuply.Text = string.Empty;
            txtUnit1.Text = string.Empty;
            txtProductPrice.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtProductName.Text = string.Empty;

        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    var pname = db.Product.Where(d => d.ProductName == txtProductName.Text.Trim()).Any();
                    if (ProductID == 0)
                    {
                        if (pname)
                        {
                            MessageBox.Show("این محصول از قبل ثبت شده است ، لطفا نام دیگری انتخاب کنید");
                        }
                        else
                        {
                            db.InsertProduct(txtProductName.Text.Trim(), int.Parse(txtSuply.Text.Trim()), int.Parse(string.Format("{0:N0}", txtProductPrice.Text.Trim()))
                            , txtUnit1.Text.Trim(), txtDescription.Text.Trim());
                            MessageBox.Show("ثبت کالا با موفقیت انجام شد");
                            reset();
                        }

                    }
                    else
                    {
                        db.UpdateProduct(ProductID, txtProductName.Text.Trim(), int.Parse(txtSuply.Text.Trim()), int.Parse(string.Format("{0:N0}", txtProductPrice.Text.Trim()))
                            , txtUnit1.Text.Trim(), txtDescription.Text.Trim());
                        MessageBox.Show("ویرایش کالا با موفقیت انجام شد");
                        Close();
                    }
                    db.SaveChanges();
                    ts.Complete();
                }
                catch (Exception n)
                {
                    MessageBox.Show(n.Message);
                }
            }

        }


        private void txtName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[0-9]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void txtProductPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[آ-ی a-z A-Z]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void txtSuply_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[آ-ی a-z A-Z]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void txtUnit1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[0-9]");
            e.Handled = Regex.IsMatch(e.Text);
        }
    }
}

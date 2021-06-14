using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UtilitySale.Data;
using System.Transactions;

namespace UtilitySale.App._ٌWindows
{
    /// <summary>
    /// Interaction logic for WinProductPrice.xaml
    /// </summary>
    public partial class WinProductPrice : Window
    {
        public WinProductPrice()
        {
            InitializeComponent();
        }
        UtilitySale_DBEntities db = new UtilitySale_DBEntities();
        public int productID { get; set; }
        public string productname { get; set; }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        void bindGrid()
        {
            dgvPrice.ItemsSource = db.ProductPrice.Where(w => w.ProductID == productID).ToList();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object item = dgvPrice.SelectedItem;
            int priceid = int.Parse((dgvPrice.SelectedCells[0].Column.GetCellContent(item) as TextBlock).ToString());
            try
            {
                if (item != null)
                {
                    if (MessageBox.Show("آیا از حذف مطمئن هستید؟", "توجه", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        db.DeletePrice(priceid);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception b)
            {
                MessageBox.Show(b.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    db.insertPrice(productID, int.Parse(txtBuy.Text.Trim()), int.Parse(txtSell.Text.Trim()), txtDesc.Text.Trim());
                    db.UpdatePrice(productID, int.Parse(txtSell.Text.Trim()));
                    db.SaveChanges();
                    reset();
                    MessageBox.Show("ثبت با موفقیت انجام شد");
                    Close();
                    ts.Complete();
                }
                catch (Exception b)
                {
                    MessageBox.Show(b.Message);
                }
            }

        }

        void reset()
        {
            txtBuy.Text = string.Empty;
            txtDesc.Text = string.Empty;
            txtSell.Text = string.Empty;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblProduct.Text = productname;
            bindGrid();
        }
    }
}

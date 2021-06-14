using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UtilitySale.Data;

namespace UtilitySale.App._ٌWindows
{
    /// <summary>
    /// Interaction logic for WinProduct.xaml
    /// </summary>
    public partial class WinProduct : Window
    {
        public WinProduct()
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

        private void Grid()
        {
            dgvProduct.ItemsSource = db.showProduct();
        }
        private void NewProduct_Click(object sender, RoutedEventArgs e)
        {
            new WinAddOrEditProduct().ShowDialog();
            Grid();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            object item = dgvProduct.SelectedItem;
            if (item != null)
            {
                WinAddOrEditProduct editProduct = new WinAddOrEditProduct();
                editProduct.ProductID = int.Parse((dgvProduct.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                editProduct.ProductName = (dgvProduct.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                editProduct.ShowDialog();
                Grid();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object item = dgvProduct.SelectedItem;
            int PID = int.Parse((dgvProduct.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            string PName = (dgvProduct.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            if (item != null)
            {
                if (MessageBox.Show($"آیا از حذف {PName} مطمئن هستید؟", "توجه", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    db.DeleteProduct(PID);
                    db.SaveChanges();
                    Grid();
                }
            }
        }

        private void Suply_click(object sender, RoutedEventArgs e)
        {
            object item = dgvProduct.SelectedItem;
            if (item != null)
            {
                WinAddSuply suply = new WinAddSuply();
                suply.productID = int.Parse((dgvProduct.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                suply.productname = (dgvProduct.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                suply.ShowDialog();
                Grid();
            }

        }

        private void Price_Click(object sender, RoutedEventArgs e)
        {
            object item = dgvProduct.SelectedItem;
            if (item != null)
            {
                WinProductPrice price = new WinProductPrice();
                price.productID = int.Parse((dgvProduct.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                price.productname = (dgvProduct.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                price.ShowDialog();
                Grid();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Grid();
        }

        private void txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgvProduct.ItemsSource = db.Product.Where(p => p.ProductName.Contains(txtsearch.Text.Trim()) ||
           p.ProductUnit.Contains(txtsearch.Text.Trim()) || p.ProductDesc.Contains(txtsearch.Text.Trim())).ToList();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Grid();
        }
    }
}

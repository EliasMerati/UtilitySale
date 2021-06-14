using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Transactions;
using UtilitySale.Data;

namespace UtilitySale.App._ٌWindows
{
    /// <summary>
    /// Interaction logic for WinAddSuply.xaml
    /// </summary>
    public partial class WinAddSuply : Window
    {
        public WinAddSuply()
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object item = dgvsuply.SelectedItem;
            try
            {
                if (item != null)
                {
                    int suplyid = int.Parse((dgvsuply.SelectedCells[0].Column.GetCellContent(item) as TextBlock).ToString());
                    if (MessageBox.Show("", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        db.DeleteSuply(suplyid);
                        db.SaveChanges();
                    }
                }

            }
            catch (Exception b)
            {
                MessageBox.Show(b.Message);
            }
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
                    db.InsertSuply(productID,int.Parse(txtSuply.Text.Trim()) , cmbstatus.Text, txtDesc.Text.Trim());
                    if (cmbstatus.Text == "ورود به انبار")
                    {
                        db.Updatesuply1(productID, int.Parse(txtSuply.Text.Trim()));
                    }
                    else if (cmbstatus.Text == "برگشتی")
                    {
                        db.Updatesuply2(productID, int.Parse(txtSuply.Text.Trim()));
                    }
                    db.SaveChanges();
                    MessageBox.Show("ثبت با موفقیت انجام شد");
                    reset();
                    grid();
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
            txtDesc.Text = string.Empty;
            txtSuply.Text = string.Empty;
            cmbstatus.SelectedIndex = 0;
        }
        void grid()
        {
            dgvsuply.ItemsSource = db.ProductSuply.Where(s => s.ProductID == productID).ToList();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblProduct.Text = productname;
            grid();
        }
    }
}

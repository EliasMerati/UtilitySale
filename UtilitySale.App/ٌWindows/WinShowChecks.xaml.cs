using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UtilitySale.Data;
using System.Transactions;
using UtilitySale.Utility;

namespace UtilitySale.App._ٌWindows
{
    /// <summary>
    /// Interaction logic for WinShowChecks.xaml
    /// </summary>
    public partial class WinShowChecks : Window
    {
        public WinShowChecks()
        {
            InitializeComponent();
        }
        UtilitySale_DBEntities db = new UtilitySale_DBEntities();
        public int ID { get; set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bind();
        }

        void bind()
        {
            dgvCheck.ItemsSource = db.Check.Where(l => l.peopleID == ID).OrderBy(k => k.CheckDate).ToList();
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void pas_click(object sender, RoutedEventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                object item = dgvCheck.SelectedItem;
                if (item != null)
                {
                    try
                    {
                        int id = int.Parse((dgvCheck.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                        int price = int.Parse((dgvCheck.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text.Replace(",", ""));
                        string date = (dgvCheck.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                        db.CheckPassed(id);
                        db.InsertPayment(ID, price, DateTime.Now.Date(),
                        $"نقد شدن چک به تاریخ{date}", "چک پاس شده");
                        db.MinesDeptor(ID, price);
                        db.SaveChanges();
                        bind();
                        MessageBox.Show("عملیات پاس شدن چک با موفقیت انجام شد");
                        Close();
                        ts.Complete();
                    }
                    catch (Exception b)
                    {
                        MessageBox.Show(b.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            bind();
        }

        private void txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
                dgvCheck.ItemsSource = db.Check.Where(m => m.CheckType.Contains(txtsearch.Text.Trim()) ||
             m.CheckDate.Contains(txtsearch.Text.Trim()) || m.CheckDesc.Contains(txtsearch.Text.Trim()) || m.CheckBank.Contains(txtsearch.Text.Trim())
             || m.Checknumber.Contains(txtsearch.Text.Trim())).Where(k => k.peopleID == ID).ToList();
        }
    }
}

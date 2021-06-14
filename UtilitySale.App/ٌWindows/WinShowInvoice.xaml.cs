using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UtilitySale.Data;
using Stimulsoft.Report;

namespace UtilitySale.App._ٌWindows
{
    /// <summary>
    /// Interaction logic for WinShowInvoice.xaml
    /// </summary>
    public partial class WinShowInvoice : Window
    {
        public WinShowInvoice()
        {
            InitializeComponent();
        }
        UtilitySale_DBEntities db = new UtilitySale_DBEntities();
        public string PName { get; set; }
        public int pid { get; set; }
        

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblname.Content = PName;
            bind();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void NewInvoice_Click(object sender, RoutedEventArgs e)
        {
            WinInvoice invoice = new WinInvoice();
            invoice.type = false;
            invoice.ShowDialog();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            object item = dgvInvoice.SelectedItem;
            if (item != null)
            {
                WinInvoice invoice = new WinInvoice();
                invoice.type = true;
                invoice.InvoiceID2 = int.Parse((dgvInvoice.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                invoice.Desc = (dgvInvoice.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                invoice.cname = (dgvInvoice.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                invoice.Date = (dgvInvoice.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text;
                invoice.pricetax = Convert.ToInt64((dgvInvoice.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text.Replace(",",""));
                invoice.price2 = Convert.ToInt64((dgvInvoice.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text.Replace(",",""));
                invoice.ShowDialog();
            }
            bind();
        }

        private void txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgvInvoice.ItemsSource = db.vw_Invoice2.Where(m => m.InvoiceDate.Contains(txtsearch.Text.Trim()) ||
            m.InvoiceDesc.Contains(txtsearch.Text.Trim())).Where(n=> n.peopleID == pid).ToList();
        }
        void bind ()
        {
            dgvInvoice.ItemsSource = db.vw_Invoice2.Where(m=>m.peopleID == pid).OrderByDescending(m=>m.InvoiceDate).ToList();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            bind();
        }

        private void PRINT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                var Report = StiReport.CreateNewReport();

                var item = dgvInvoice.SelectedItem;
                if (item != null)
                {
                    int ID = int.Parse((dgvInvoice.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    Report["@id"] = ID;
                    Report["@id3"] = pid;
                }
                Report.Load(path + @"/Report.mrt");
                Report.RenderWithWpf();
                Report.ShowWithWpf();
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }
    }
}

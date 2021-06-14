using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UtilitySale.Utility;
using UtilitySale.Data;
using System.Transactions;
using System.Text.RegularExpressions;

namespace UtilitySale.App._ٌWindows
{
    /// <summary>
    /// Interaction logic for WinInvoice.xaml
    /// </summary>
    public partial class WinInvoice : Window
    {
        public WinInvoice()
        {
            InitializeComponent();
        }
        public int PID { get; set; }
        public int pid { get; set; }
        public int InvoiceID { get; set; }
        public int InvoiceID2 { get; set; }
        public long price { get; set; }
        public long price2 { get; set; }
        public int suply { get; set; }
        public long pricetax { get; set; }
        public string Date { get; set; }
        public string cname { get; set; }
        public string Desc { get; set; }
        UtilitySale_DBEntities db = new UtilitySale_DBEntities();
        public bool type { get; set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (type == true)
            {
                btnstartinvoice.Visibility = Visibility.Hidden;
                btncloseinvoice.Content = "ویرایش فاکتور";
                lbltotal.Content = pricetax.ToString();
                dgvProduct.ItemsSource = db.showProduct();
                dgvItems.ItemsSource = db.vw_Items.Where(k => k.InvoiceID == InvoiceID2).ToList();
                lblcustomername.Content = cname;
                lblDate.Content = Date;
                lblTotalTax.Content = price2.ToString();
                lblInvoiceID.Content = InvoiceID2;
            }
            else
            {
                lblDate.Content = DateTime.Now.Date();
                dgvPeople.ItemsSource = db.SelectPeople();
                dgvProduct.ItemsSource = db.showProduct();
                dgvProduct.IsEnabled = false;
                txtsearch2.IsEnabled = false;
                btnRefresh2.IsEnabled = false;
                txtcount.IsEnabled = false;
                btnInsert.IsEnabled = false;
                btncloseinvoice.IsEnabled = false;

            }

        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void btnstartinvoice_Click(object sender, RoutedEventArgs e)
        {
            object item = dgvPeople.SelectedItem;

            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    if (item != null)
                    {
                        ////////فاکتور جدید////////
                        pid = int.Parse((dgvPeople.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                        db.CreateInvoice(pid, DateTime.Now.Date(), 0, null);
                        db.SaveChanges();
                        //////// نمایش شماره فاکتور ///////////
                        var res = db.GetLastInvoiceID().ToList();
                        InvoiceID = res[0].InvoiceID;
                        lblInvoiceID.Content = InvoiceID;

                    }
                    else if (item == null)
                    {
                        MessageBox.Show("لطفا یک مشتری انتخاب کنید");
                        return;
                    }
                    ts.Complete();
                }
                catch (Exception n)
                {
                    MessageBox.Show(n.Message);
                }
                finally
                {
                    btnstartinvoice.IsEnabled = false;
                    //////// فعال کردن دیتا گرید کالاها /////
                    dgvProduct.IsEnabled = true;
                    txtsearch2.IsEnabled = true;
                    btnRefresh2.IsEnabled = true;
                    /////// غیر فعال کردن دیتا گرید مشتریان ///
                    dgvPeople.IsEnabled = false;
                    txtsearch.IsEnabled = false;
                    btnRefresh.IsEnabled = false;
                    txtcount.IsEnabled = true;
                    btnInsert.IsEnabled = true;
                    //////////////////////////////////////////////
                    btncloseinvoice.IsEnabled = true;
                }
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    object item = dgvItems.SelectedItem;
                    string name = (dgvItems.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                    int id = int.Parse((dgvItems.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    int suply = int.Parse((dgvItems.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text);
                    long price = int.Parse((dgvItems.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text.Replace(",", ""));

                    if (MessageBox.Show($"آیااز حذف {name} مطمئن هستید؟ ", "توجه", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        db.RemoveFromInvoice(id, int.Parse(lblInvoiceID.Content.ToString()));
                        db.Updatesuply1(id, suply);
                        ////////////////////////////////////////////////////////////////////// بروزرسانی قیمت
                        lbltotal.Content = Convert.ToInt64(lbltotal.Content) - price;
                        var tax = db.Tax.ToList();
                        int tax1 = int.Parse(tax[0].TaxAmount.ToString());
                        lblTotalTax.Content = (tax1 * Convert.ToInt64(lbltotal.Content) / 100) + Convert.ToInt64(lbltotal.Content);

                        db.SaveChanges();
                        bind2();
                    }

                    ts.Complete();
                }
                catch (Exception b)
                {
                    MessageBox.Show(b.Message);
                }
            }
        }

        private void txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgvPeople.ItemsSource = db.People.Where(w => w.PeopleName.Contains(txtsearch.Text.Trim())).ToList();
        }

        private void dgvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = dgvPeople.SelectedItem;
            lblcustomername.Content = (dgvPeople.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (dgvPeople.SelectedItem == null)
            {
                dgvPeople.ItemsSource = db.SelectPeople();
            }
        }

        private void dgvProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = dgvProduct.SelectedItem;

            if (item != null)
            {
                PID = int.Parse((dgvProduct.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                lblProductName.Content = (dgvProduct.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                lblProductUnit.Content = (dgvProduct.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                suply = int.Parse((dgvProduct.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text);
                lblProductSuply.Content = suply;
                price = int.Parse((dgvProduct.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text.Replace(",", ""));
                lblProductPrice.Content = price;
            }

        }

        private void btnRefresh2_Click(object sender, RoutedEventArgs e)
        {
            if (dgvProduct.SelectedItem == null)
            {
                dgvProduct.ItemsSource = db.showProduct();
            }
        }

        private void txtsearch2_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgvProduct.ItemsSource = db.Product.Where(p => p.ProductName.Contains(txtsearch2.Text.Trim())).ToList();
        }

        private void txtcount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[آ-ی a-z A-Z]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                object item = dgvProduct.SelectedItem;
                int count = int.Parse(txtcount.Text.Trim());
                int Pcount = int.Parse((dgvProduct.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text);
                if (count > Pcount)
                {
                    MessageBox.Show("تعداد سفارش داده شده از موجودی بیشتر است");
                    return;
                }
                try
                {
                    //////////////////////////// بررسی تکراری نبودن کالا /////////////////////////
                    var f = db.InvoiceItem.Where(d => d.InvoiceID == InvoiceID).ToList();
                    for (int i = 0; i < f.Count; i++)
                    {
                        if (f[i].ProductID == PID)
                        {
                            MessageBox.Show("این کالا از قبل در این فاکتور موجود میباشد ، برای ویرایش مقادیر آن ابتدا آن را حذف نمایید");
                            return;
                        }
                    }
                    //////////////////////////// ورود اطلاعات فاکتور /////////////////////////
                    if (type)
                    {
                        db.InsertItems(PID, InvoiceID2, int.Parse(txtcount.Text.Trim()), price, price * int.Parse(txtcount.Text.Trim()));
                        bind2();
                    }
                    else
                    {
                        db.InsertItems(PID, InvoiceID, int.Parse(txtcount.Text.Trim()), price, price * int.Parse(txtcount.Text.Trim()));
                        bind();
                    }
                    db.SaveChanges();
                    //////////////////////////// بروز رسانی گرید /////////////////////////

                    //////////////////////////// بروز رسانی موجودی کالا /////////////////////////
                    db.Updatesuply2(PID, int.Parse(txtcount.Text.Trim()));
                    db.SaveChanges();
                    dgvProduct.ItemsSource = db.showProduct();
                    //////////////////////////// بروز رسانی قیمت کل /////////////////////////
                    lbltotal.Content = (price * Convert.ToInt32(txtcount.Text.Trim())) + Convert.ToInt64(lbltotal.Content);
                    var tax = db.Tax.ToList();
                    int tax1 =int.Parse(tax[0].TaxAmount.ToString());
                    lblTotalTax.Content = (tax1 * Convert.ToInt64(lbltotal.Content) / 100) + Convert.ToInt64(lbltotal.Content);
                    ///////////////////////////////////////////////////////////////////////////
                    ts.Complete();
                }
                catch (Exception m)
                {
                    MessageBox.Show(m.Message);
                }
                finally
                {
                    txtcount.Text = string.Empty;
                    lblProductName.Content = string.Empty;
                    lblProductPrice.Content = string.Empty;
                    lblProductSuply.Content = string.Empty;
                    lblProductUnit.Content = string.Empty;

                }
            }

        }

        void bind()
        {
            dgvItems.ItemsSource = db.vw_Items.Where(k => k.InvoiceID == InvoiceID).ToList();
            dgvProduct.ItemsSource = db.showProduct();
        }

        void bind2()
        {
            dgvItems.ItemsSource = db.vw_Items.Where(k => k.InvoiceID == InvoiceID2).ToList();
            dgvProduct.ItemsSource = db.showProduct();
        }

        private void btncloseinvoice_Click(object sender, RoutedEventArgs e)
        {
            object item = dgvPeople.SelectedItem;
            try
            {

                if (type)
                {
                    if (MessageBox.Show("آیا از بروزرسانی فاکتور مطمئن هستید؟", "توجه", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        db.CloseInvoice(InvoiceID2, int.Parse(lbltotal.Content.ToString()), int.Parse(lblTotalTax.Content.ToString()), txtDesc.Text.Trim());
                        MessageBox.Show("فاکتور با موفقیت بروزرسانی شد");
                        Close();
                    }
                }
                else
                {
                    string name = (dgvPeople.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                    if (MessageBox.Show($"آیا از بستن فاکتور آقای {Name} مطمئن هستید؟", "توجه", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        db.CloseInvoice(InvoiceID, int.Parse(lbltotal.Content.ToString()), int.Parse(lblTotalTax.Content.ToString()), txtDesc.Text.Trim());
                        MessageBox.Show("فاکتور با موفقیت بسته شد");
                        Close();
                    }
                }
                db.SaveChanges();

            }
            catch (Exception n)
            {
                MessageBox.Show(n.Message);
            }
        }

        private void btnShowInvoice_Click(object sender, RoutedEventArgs e)
        {
          
        }

        //void clear()
        //{
        //    txtsearch.Text = string.Empty;
        //    lblcustomername.Content = string.Empty;
        //    btnstartinvoice.IsEnabled = true;
        //    lblInvoiceID.Content = 0;
        //    btnRefresh.IsEnabled = true;
        //    txtsearch.IsEnabled = true;
        //    txtsearch2.IsEnabled = false;
        //    btnRefresh2.IsEnabled = false;
        //    lblProductName.Content = string.Empty;
        //    lblProductUnit.Content = string.Empty;
        //    lblProductSuply.Content = string.Empty;
        //    lblProductPrice.Content = string.Empty;
        //    txtcount.IsEnabled = false;
        //    btnInsert.IsEnabled = false;
        //    dgvItems.ItemsSource = null;
        //    lbltotal.Content = 0;
        //    lblTotalTax.Content = 0;
        //    txtDesc.Text = string.Empty;
        //    btncloseinvoice.IsEnabled = false;
        //    btnShowInvoice.IsEnabled = false;
        //}
    }
}

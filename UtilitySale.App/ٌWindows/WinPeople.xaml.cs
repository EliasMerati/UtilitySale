using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UtilitySale.Data;

namespace UtilitySale.App._ٌWindows
{
    /// <summary>
    /// Interaction logic for WinPeople.xaml
    /// </summary>
    public partial class WinPeople : Window
    {
        public WinPeople()
        {
            InitializeComponent();

        }
        UtilitySale_DBEntities db = new UtilitySale_DBEntities();
        private void BindGrid()
        {
            dgvcustomers.ItemsSource = db.showPeople();
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void NewPeople_Click(object sender, RoutedEventArgs e)
        {
            new WinAddOrEditPeople().ShowDialog();
            BindGrid();
        }


        private void Payment_click(object sender, RoutedEventArgs e)
        {
            object item = dgvcustomers.SelectedItem;

            if (item != null)
            {
                WinAddPeyment winAdd = new WinAddPeyment();
                winAdd.PeopleID = int.Parse((dgvcustomers.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                winAdd.ShowDialog();
            }

        }

        private void ShowPayment_Click(object sender, RoutedEventArgs e)
        {
            object item = dgvcustomers.SelectedItem;

            if (item != null)
            {
                WinShowPayment winShow = new WinShowPayment();
                winShow.PeopleName = (dgvcustomers.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                winShow.PID = int.Parse((dgvcustomers.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                winShow.ShowDialog();
                BindGrid();
            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BindGrid();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object item = dgvcustomers.SelectedItem;
            try
            {
                if (item != null)
                {
                    var id = int.Parse((dgvcustomers.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    var name = (dgvcustomers.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                    if (MessageBox.Show($"آیا از حذف {name} مطمئن هستید؟", "توجه", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        db.DeletePeople(id);
                        db.SaveChanges();
                        BindGrid();
                    }
                }
            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message);
            }

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            object item = dgvcustomers.SelectedItem;
            if (item != null)
            {
                WinAddOrEditPeople editPeople = new WinAddOrEditPeople();
                editPeople.PID = int.Parse((dgvcustomers.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                editPeople.ShowDialog();
                BindGrid();
            }
        }

        private void txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgvcustomers.ItemsSource = db.People.Where(p => p.PeopleAddress.Contains(txtsearch.Text.Trim()) ||
                p.PeopleName.Contains(txtsearch.Text.Trim()) || p.PeopleTel.Contains(txtsearch.Text.Trim()) ||
                p.PeopleType.Contains(txtsearch.Text.Trim())).ToList();
        }

        private void ShowInvoice_Click(object sender, RoutedEventArgs e)
        {
            object item = dgvcustomers.SelectedItem;
            if (item != null)
            {
                WinShowInvoice invoice = new WinShowInvoice();
                invoice.PName = (dgvcustomers.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                invoice.pid = int.Parse((dgvcustomers.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                invoice.ShowDialog();
                BindGrid();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            BindGrid();
        }

        private void Check_click(object sender, RoutedEventArgs e)
        {
            object item = dgvcustomers.SelectedItem;
            if (item != null)
            {
                WinCheck check = new WinCheck();
                check.pid = int.Parse((dgvcustomers.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                check.name = (dgvcustomers.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                check.ShowDialog();
                BindGrid();
            }
        }

        private void ShowCheck_click(object sender, RoutedEventArgs e)
        {
            object item = dgvcustomers.SelectedItem;
            if (item != null)
            {
                WinShowChecks winShow = new WinShowChecks();
                winShow.ID = int.Parse((dgvcustomers.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                winShow.ShowDialog();
                BindGrid();
            }
        }
    }
}

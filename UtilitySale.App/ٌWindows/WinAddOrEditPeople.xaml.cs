using System;
using System.Windows;
using System.Windows.Input;
using UtilitySale.Data;
using System.Text.RegularExpressions;

namespace UtilitySale.App._ٌWindows
{
    /// <summary>
    /// Interaction logic for WinAddOrEditPeople.xaml
    /// </summary>
    public partial class WinAddOrEditPeople : Window
    {
        public WinAddOrEditPeople()
        {
            InitializeComponent();
        }
        UtilitySale_DBEntities db;
        public int PID { get; set; }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            db = new UtilitySale_DBEntities();
            try
            {
                if (PID == 0)
                {
                    db.InsertPeople(txtName.Text.Trim(), txtTel.Text.Trim(), txtAddress.Text.Trim(), int.Parse(string.Format("{0:N0}",txtDeptor.Text.Trim())),
                       int.Parse(string.Format("{0:N0}", txtCreditor.Text.Trim())), cmbType.Text);
                    MessageBox.Show(" عملیات ثبت با موفقیت انجام شد");
                    db.SaveChanges();
                    db.Dispose();
                }
                else
                {

                    db.UpdatePeople(PID, txtName.Text.Trim(), txtTel.Text.Trim(), txtAddress.Text.Trim(), int.Parse(string.Format("{0:N0}", txtDeptor.Text.Trim())),
                       int.Parse(string.Format("{0:N0}", txtCreditor.Text.Trim())), cmbType.Text);
                    MessageBox.Show(" عملیات ویرایش با موفقیت انجام شد");
                    db.SaveChanges();
                    db.Dispose();
                }
            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message);
            }

            db.Dispose();
            Close();
        }

        private void txtTel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[آ-ی a-z A-Z]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void txtName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[0-9]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void txtDeptor_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[آ-ی a-z A-Z]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void txtCreditor_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex Regex = new Regex("[آ-ی a-z A-Z]");
            e.Handled = Regex.IsMatch(e.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new UtilitySale_DBEntities();
            try
            {
                var p = db.People.Find(PID);
                if (PID != 0)
                {
                    txtAddress.Text = p.PeopleAddress;
                    txtCreditor.Text = string.Format("{0:N0}", p.PeopleCreditor);
                    txtDeptor.Text = string.Format("{0:N0}", p.PeopleDeptor);
                    txtName.Text = p.PeopleName;
                    txtTel.Text = p.PeopleTel;
                    cmbType.Text = p.PeopleType;
                }
                else
                {
                    cmbType.SelectedIndex = 0;
                }
            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message);
            }
        }
    }
}

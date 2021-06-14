using System.Windows;
using System.Windows.Input;
using UtilitySale.Data;

namespace UtilitySale.App._ٌWindows
{
    /// <summary>
    /// Interaction logic for WinShowPayment.xaml
    /// </summary>
    public partial class WinShowPayment : Window
    {
        public WinShowPayment()
        {
            InitializeComponent();
            
        }
        public int PID { get; set; }
        public string PeopleName { get; set; }
        UtilitySale_DBEntities db = new UtilitySale_DBEntities();
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
         private void Bind()
        {
            Dgvpeyment.ItemsSource = db.ShowPayment(PID);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Bind();
            lblname.Text = PeopleName;
            lblTotal_Creditor.Content =string.Format("{0:N0}", db.People.Find(PID).PeopleCreditor);
            lblTotal_Deptor.Content =string.Format("{0:N0}", db.People.Find(PID).PeopleDeptor);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Bind();
        }
    }
}

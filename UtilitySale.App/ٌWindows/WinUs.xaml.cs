using System.Windows;
using System.Windows.Input;

namespace UtilitySale.App._ٌWindows
{
    /// <summary>
    /// Interaction logic for WinUs.xaml
    /// </summary>
    public partial class WinUs : Window
    {
        public WinUs()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}

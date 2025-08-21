using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NicksProject.View.Layout
{
    /// <summary>
    /// Logique d'interaction pour sidebar.xaml
    /// </summary>
    public partial class stockSidebar : UserControl
    {
        public stockSidebar()
        {
            InitializeComponent();
        }

        private T? FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null && parent is not T)
                parent = VisualTreeHelper.GetParent(parent);

            return parent as T;
        }

        private void btn_dashboard_Click(object sender, RoutedEventArgs e)
        {
            var mainLayout = FindParent<MainLayout>(this);
            if (mainLayout != null)
            {
                mainLayout.NavigateTo(new testDashboard());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainLayout = FindParent<MainLayout>(this);
            if (mainLayout != null)
            {
                mainLayout.NavigateTo(new ListeEmploye());
            }
        }
    }
}

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

using Nick_sProject.View;
using Nick_sProject.View.Layout;

namespace Nick_sProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Employe employeWindow = new Employe();
            employeWindow.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ListeEmploye listeEmploye = new ListeEmploye();
            listeEmploye.Show();
            this.Close();
        }

        private void ShowSidebar_Click(object sender, RoutedEventArgs e)
        {
            var layout = new MainLayout();
            MainContent.Content = layout;
            //MainContentPanel.Visibility = Visibility.Visible;
        }
    }
}

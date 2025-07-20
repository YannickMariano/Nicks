using Nick_sProject.Controller;
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
using System.Windows.Shapes;

namespace Nick_sProject.View
{
    /// <summary>
    /// Logique d'interaction pour AjoutEmploye.xaml
    /// </summary>
    public partial class AjoutEmploye : Window
    {
        private EmployeController _employeController;
        public AjoutEmploye()
        {
            InitializeComponent();
            _employeController = new EmployeController();
        }

        private void AoutEmploye_Click(object sender, RoutedEventArgs e)
        {
            var employe = new Model.Employe(
                txtNomEmploye.Text,
                txtPrenomEmploye.Text,
                txtAdresseEmploye.Text,
                txtTelephoneEmploye.Text
            );
            _employeController.AjouterEmploye(employe);

            ListeEmploye listeEmploye = new ListeEmploye();
            listeEmploye.Show();
            this.Close();
        }

        private void AnnulerAjoutEmploye_Click(object sender, RoutedEventArgs e)
        {
            ListeEmploye listeEmploye = new ListeEmploye();
            listeEmploye.Show();
            this.Close();
        }
    }
}

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
        private ListeEmploye fenetrePrincipale;
        public AjoutEmploye(ListeEmploye parent)
        {
            InitializeComponent();
            _employeController = new EmployeController();
            this.fenetrePrincipale = parent;       
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

            MessageBox.Show("Employé ajouté avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            fenetrePrincipale.RafraichirEmploye();
            this.Close();
        }

        private void AnnulerAjoutEmploye_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Voulez-vous vraiment annuler la modification ?", "Annuler", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.None);
        }
    }
}

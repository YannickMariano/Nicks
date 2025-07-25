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
using Nick_sProject.Controller;
using Nick_sProject.Model;

namespace Nick_sProject.View
{
    /// <summary>
    /// Logique d'interaction pour ModifierEmploye.xaml
    /// </summary>
    public partial class ModifierEmploye : Window
    {
        private EmployeController _employeController;
        private Nick_sProject.Model.Employe employeAModifier;
        private ListeEmploye fenetrePrincipale;

        public ModifierEmploye(Nick_sProject.Model.Employe employe, EmployeController employeController, ListeEmploye parent)
        {
            InitializeComponent();
            employeAModifier = employe;
            _employeController = employeController;
            _employeController = new EmployeController();  
            SetEmployeData(employeAModifier);
            this.fenetrePrincipale = parent;
        }

        private void button_modifier_employe_Click(object sender, RoutedEventArgs e)
        {
            employeAModifier.Name = txtNomEmploye.Text;
            employeAModifier.Prenom = txtPrenomEmploye.Text;
            employeAModifier.Adresse = txtAdresseEmploye.Text;
            employeAModifier.NumTel = txtTelEmploye.Text;
            if (comboboxStatutEmploye.SelectedItem is ComboBoxItem selectedStatut)
            {
                employeAModifier.Statut = selectedStatut.Content.ToString();
            }
            employeAModifier.Identifiant = string.IsNullOrWhiteSpace(txtIdentifiant.Text) ? null : txtIdentifiant.Text.Trim();


            employeAModifier.MotDePasse = string.IsNullOrWhiteSpace(txtMdp.Text)
                ? employeAModifier.MotDePasse
                : txtMdp.Text.Trim();


            if (!string.IsNullOrEmpty(employeAModifier.Identifiant))
            {
                bool existe = EmployeController.ExisteIdentifiantPourUnAutreEmploye(employeAModifier.Identifiant, employeAModifier.Id);
                if (existe)
                {
                    MessageBox.Show("Cet identifiant existe déjà pour un autre employé", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }

            _employeController.ModifierEmploye(employeAModifier);

            MessageBox.Show("Employé modifié avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

            fenetrePrincipale.RafraichirEmploye();
            this.Close();
        }
        public void SetEmployeData(Nick_sProject.Model.Employe employe)
        {
            if (employe == null)
            {
                MessageBox.Show("Aucune donnée d'employé à afficher.");
                return;
            }
          
            txtNomEmploye.Text = employe.Name ?? "";
            txtPrenomEmploye.Text = employe.Prenom ?? "";

            txtAdresseEmploye.Text = employe.Adresse ?? "";
            txtTelEmploye.Text = employe.NumTel ?? "";

            txtIdentifiant.Text = employe.Identifiant ?? "";
            txtMdp.Text = employe.MotDePasse ?? "";

            // Sélection du statut
            if (!string.IsNullOrEmpty((string?)employe.Statut))
            {
                foreach (ComboBoxItem item in comboboxStatutEmploye.Items)
                {
                    if (item.Content.ToString() == employe.Statut)
                    {
                        comboboxStatutEmploye.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void button_annuler_modification_employe_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Voulez-vous vraiment annuler la modification ?", "Annuler", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.None);
        }

        private void Role_Change(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (ComboBoxItem)comboboxStatutEmploye.SelectedItem;
            string statutSelectionne = selectedItem.Content.ToString();

            if (statutSelectionne == "Caissier")
            {
                labelEntrerNomEtMdp.Visibility = Visibility.Visible;
                labelMdp.Visibility = Visibility.Visible;
                labelIdentifiant.Visibility = Visibility.Visible;
                txtMdp.Visibility = Visibility.Visible;
                txtIdentifiant.Visibility = Visibility.Visible;               
            }
            else
            {
                labelEntrerNomEtMdp.Visibility = Visibility.Hidden;
                labelMdp.Visibility = Visibility.Hidden;
                labelIdentifiant.Visibility = Visibility.Hidden;
                txtMdp.Visibility = Visibility.Hidden;
                txtIdentifiant.Visibility = Visibility.Hidden;
                txtMdp.Text = string.Empty;
                txtIdentifiant.Text = string.Empty;
            }

        }
    }
}

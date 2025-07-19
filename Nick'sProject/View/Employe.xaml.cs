using Nick_sProject.Controller;
using Nick_sProject.Model;
using Nick_sProject.Utils;  
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Nick_sProject.View
{
    /// <summary>
    /// Logique d'interaction pour Employe.xaml
    /// </summary>
    public partial class Employe : Window
    {
        private EmployeController _employeController;

        public Employe()
        {
            InitializeComponent();
            _employeController = new EmployeController();
        }
            
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Model.Employe> employes = _employeController.GetAllEmployes();
            MonTableau.ItemsSource = employes;
        }
        
        private void AjouterEmploye_Click(object sender, RoutedEventArgs e)
        {
            var employe = new Model.Employe(
                0,
                nom.Text,
                prenom.Text,
                adresse.Text,
                num_tel.Text,
                mdp.Text,
                statut.Text,
                identifiant.Text
            );

            _employeController.AjouterEmploye(employe);
            MonTableau.ItemsSource = _employeController.GetAllEmployes();
        }

        private void MonTableau_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var employe = (Model.Employe)MonTableau.SelectedItem;
            if (employe != null)
            {
                id_emp.Text = employe.Id.ToString();
                nom.Text = employe.Name;
                prenom.Text = employe.Prenom;
                adresse.Text = employe.Adresse;
                num_tel.Text = employe.NumTel;
                mdp.Text = employe.MotDePasse;
                statut.Text = employe.Statut;
                identifiant.Text = employe.Identifiant;
            }

            MonTableau.ItemsSource = _employeController.GetAllEmployes();
        }

        private void SupprimerEmp_Click(object sender, RoutedEventArgs e)
        {
            var employe = (Model.Employe)MonTableau.SelectedItem;
            if (employe != null)
            {
                _employeController.SupprimerEmploye(employe.Id);

                MonTableau.ItemsSource = _employeController.GetAllEmployes();

                id_emp.Text = "Auto";
                nom.Text = "";
                prenom.Text = "";
                adresse.Text = "";
                num_tel.Text = "";
                mdp.Text = "";
                statut.Text = "";
                identifiant.Text = "";
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un employé à supprimer.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ModifierEmp_Click(object sender, RoutedEventArgs e)
        {
            var employe = (Model.Employe)MonTableau.SelectedItem;
            if (employe != null)
            {
                employe.Name = nom.Text;
                employe.Prenom = prenom.Text;
                employe.Adresse = adresse.Text;
                employe.NumTel = num_tel.Text;
                employe.MotDePasse = mdp.Text;
                employe.Statut = statut.Text;
                employe.Identifiant = identifiant.Text;

                _employeController.ModifierEmploye(employe);
                MonTableau.ItemsSource = _employeController.GetAllEmployes();

                MessageBox.Show("Employé modifié avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un employé à modifier.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Statut_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedItem = (ComboBoxItem)statut.SelectedItem;
            string statutSelectionne = selectedItem.Content.ToString();

            if (statutSelectionne == "Caissier")
            {
                LabelMdp.Visibility = Visibility.Visible;
                mdp.Visibility = Visibility.Visible;

                LabelIdentifiant.Visibility = Visibility.Visible;
                identifiant.Visibility = Visibility.Visible;
            }
            else
            {
                LabelMdp.Visibility = Visibility.Hidden;
                mdp.Visibility = Visibility.Hidden;
                mdp.Text = string.Empty;

                LabelIdentifiant.Visibility = Visibility.Hidden;
                identifiant.Visibility = Visibility.Hidden;
            }
        }
    }
}

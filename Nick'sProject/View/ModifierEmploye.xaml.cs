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
using Nick_sProject.Model;

namespace Nick_sProject.View
{
    /// <summary>
    /// Logique d'interaction pour ModifierEmploye.xaml
    /// </summary>
    public partial class ModifierEmploye : Window
    {
        private Nick_sProject.Model.Employe employeAModifier;
        public ModifierEmploye(Nick_sProject.Model.Employe employe)
        {
            InitializeComponent();
            employeAModifier = employe;
            SetEmployeData(employeAModifier);
            //RemplirChamps(employe);
        }

        //private void RemplirChamps(Nick_sProject.Model.Employe employe)
        //{
        //    txtNomEmploye.Text = employe.Name;
        //    txtPrenomEmploye.Text = employe.Prenom;
        //    txtAdresseEmploye.Text = employe.Adresse;
        //    txtTelEmploye.Text = employe.NumTel;

        //    comboboxStatutEmploye.SelectedItem = comboboxStatutEmploye.Items
        //        .OfType<ComboBoxItem>()
        //        .FirstOrDefault(item => item.Content.ToString() == employe.Statut);
        //}



        private void button_modifier_employe_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public void SetEmployeData(Nick_sProject.Model.Employe employe)
        {
            if (employe == null)
            {
                MessageBox.Show("Aucune donnée d'employé à afficher.");
                return;
            }

            // Séparez le nom complet en nom et prénom (si nécessaire)
            var noms = employe.Name?.Split(' ') ?? new string[0];
            txtNomEmploye.Text = noms.Length > 0 ? noms[0] : "";
            txtPrenomEmploye.Text = noms.Length > 1 ? string.Join(" ", noms.Skip(1)) : "";

            txtAdresseEmploye.Text = employe.Adresse ?? "";
            txtTelEmploye.Text = employe.NumTel ?? "";

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
    }
}

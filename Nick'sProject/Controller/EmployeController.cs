using Nick_sProject.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows;
using Nick_sProject.Utils;
using System.Windows.Controls;

namespace Nick_sProject.Controller
{
    internal class EmployeController
    {
        public EmployeController() { }

        public void AjouterEmploye(Employe employe)
        {
            try
            {
                var conn = DbConfig.GetConnection();

                var query = "INSERT INTO \"Employe\" (\"Nom\", \"Prenom\", \"Adresse\", \"NumTel\", \"MotDePasse\", \"Statut\", \"Identifiant\") VALUES (@name, @prenom, @adresse, @numtel, @motdepasse, @statut, @identifiant)";
                var cmd = new NpgsqlCommand(query ,conn);

                cmd.Parameters.AddWithValue("name", employe.Name);
                cmd.Parameters.AddWithValue("prenom", employe.Prenom);
                cmd.Parameters.AddWithValue("adresse", employe.Adresse);
                cmd.Parameters.AddWithValue("numtel", employe.NumTel);
                cmd.Parameters.AddWithValue("motdepasse", employe.MotDePasse);
                cmd.Parameters.AddWithValue("statut", employe.Statut);
                cmd.Parameters.AddWithValue("identifiant", employe.Identifiant);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    MessageBox.Show("Ajout réussi !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Aucun employé ajouté.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ModifierEmploye(Employe employe)
        {
            try
            {
                using var conn = DbConfig.GetConnection();

                var cmd = new NpgsqlCommand(
                    "UPDATE \"Employe\" SET \"Nom\"=@name, \"Prenom\"=@prenom, \"Adresse\"=@adresse, \"NumTel\"=@numtel, \"MotDePasse\"=@motdepasse, \"Statut\"=@statut WHERE \"IdEmp\"=@id", conn);
                cmd.Parameters.AddWithValue("id", employe.Id);
                cmd.Parameters.AddWithValue("name", employe.Name);
                cmd.Parameters.AddWithValue("prenom", employe.Prenom);
                cmd.Parameters.AddWithValue("adresse", employe.Adresse);
                cmd.Parameters.AddWithValue("numtel", employe.NumTel);
                cmd.Parameters.AddWithValue("motdepasse", employe.MotDePasse);
                cmd.Parameters.AddWithValue("statut", employe.Statut);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    MessageBox.Show("Modification réussie !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Aucun employé modifié.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la modification : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SupprimerEmploye(int id)
        {
            try
            {
                using var conn = DbConfig.GetConnection();
            
                var cmd = new NpgsqlCommand("DELETE FROM \"Employe\" WHERE \"IdEmp\"=@id", conn);
                cmd.Parameters.AddWithValue("id", id);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    MessageBox.Show("Suppression réussie !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Aucun employé supprimé.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<Employe> GetAllEmployes()
        {
            var employes = new List<Employe>();
            try
            {
                using var conn = DbConfig.GetConnection();
                
                var cmd = new NpgsqlCommand("SELECT \"IdEmp\", \"Nom\", \"Prenom\", \"Adresse\", \"NumTel\", \"MotDePasse\", \"Statut\", \"Identifiant\" FROM \"Employe\"", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    employes.Add(new Employe(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetString(6),
                        reader.GetString(7)
                    ));
                    Console.WriteLine("LISTAGE EMPLOYE");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la récupération : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return employes;
        }
    }
}

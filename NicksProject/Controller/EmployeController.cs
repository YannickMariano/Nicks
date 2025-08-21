using NicksProject.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows;
using NicksProject.Utils;
using System.Windows.Controls;

namespace NicksProject.Controller
{
    public class EmployeController
    {
        public EmployeController() { }

        public void AjouterEmploye(Employe employe)
        {
            try
            {
                using var conn = DbConfig.GetConnection();

                employe.Statut = "Employe";

                var query = "INSERT INTO \"Employe\" (\"Nom\", \"Prenom\", \"Adresse\", \"NumTel\", \"Statut\") " +
                            "VALUES (@nom, @prenom, @adresse, @numtel, @statut)";

                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nom", employe.Name);
                cmd.Parameters.AddWithValue("@prenom", employe.Prenom);
                cmd.Parameters.AddWithValue("@adresse", employe.Adresse);
                cmd.Parameters.AddWithValue("@numtel", employe.NumTel);
                cmd.Parameters.AddWithValue("@statut", employe.Statut);
                             


                int rowsAffected = cmd.ExecuteNonQuery();
                MessageBox.Show(
                    rowsAffected > 0 ? "Ajout réussi !" : "Aucun employé ajouté.",
                    "Résultat", MessageBoxButton.OK, rowsAffected > 0 ? MessageBoxImage.Information : MessageBoxImage.Warning
                );
                if (string.IsNullOrEmpty(employe.Name))
                {
                    throw new Exception("Le nom de l'employé est requis.");
                }
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
                    "UPDATE \"Employe\" SET \"Nom\"=@name, \"Prenom\"=@prenom, \"Adresse\"=@adresse, \"NumTel\"=@numtel, \"Statut\"=@statut, \"Identifiant\"=@identifiant, \"MotDePasse\"=@motdepasse WHERE \"IdEmp\"=@id", conn);

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(employe.MotDePasse);

                cmd.Parameters.AddWithValue("name", employe.Name);
                cmd.Parameters.AddWithValue("prenom", employe.Prenom);
                cmd.Parameters.AddWithValue("adresse", employe.Adresse);
                cmd.Parameters.AddWithValue("numtel", employe.NumTel);
                cmd.Parameters.AddWithValue("statut", employe.Statut);
                cmd.Parameters.Add("identifiant", NpgsqlTypes.NpgsqlDbType.Varchar);
                cmd.Parameters["identifiant"].Value = employe.Identifiant ?? (object)DBNull.Value; 
                cmd.Parameters.Add("motdepasse", NpgsqlTypes.NpgsqlDbType.Varchar);
                cmd.Parameters["motdepasse"].Value = hashedPassword ?? (object)DBNull.Value;
                cmd.Parameters.AddWithValue("id", employe.Id);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    MessageBox.Show("Modification réussie !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Aucun employé modifié.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veuillez Enregistrer un identifaint different", "Astuce", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        public static bool ExisteIdentifiantPourUnAutreEmploye(string identifiant, int idEmploye)
        {
            using var conn = DbConfig.GetConnection();
            var cmd = new NpgsqlCommand(
                "SELECT COUNT(*) FROM \"Employe\" WHERE \"Identifiant\" = @identifiant AND \"IdEmp\" != @id", conn);
            cmd.Parameters.AddWithValue("identifiant", identifiant);
            cmd.Parameters.AddWithValue("id", idEmploye);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
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
                
                var cmd = new NpgsqlCommand("SELECT \"IdEmp\", \"Nom\", \"Prenom\", \"NumTel\", \"Statut\", \"Adresse\", \"DateConnection\" FROM \"Employe\"", conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    employes.Add(new Employe
                    { 
                        Id = reader.GetInt32(0),    
                        Name = $"{reader.GetString(1)} {reader.GetString(2)}",
                        NumTel = reader.GetString(3),
                        Statut = reader.GetString(4),
                        Adresse = reader.GetString(5),
                        DerniereConnexion = reader.IsDBNull(6) ? "-" : reader.GetDateTime(6).ToString("dd/MM/yyyy HH:mm:ss")
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la récupération : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return employes;
        }

        public Employe GetEmployeById(int id)
        {
            Employe employe = null;

            using var conn = DbConfig.GetConnection();   
            string query = "SELECT \"IdEmp\", \"Nom\", \"Prenom\", \"Adresse\", \"NumTel\", \"Statut\", \"Identifiant\", \"MotDePasse\" FROM \"Employe\" WHERE \"IdEmp\" = @id";

            using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employe = new Employe
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Prenom = reader.GetString(2),
                            Adresse = reader.GetString(3),
                            NumTel = reader.GetString(4),
                            Statut = reader.GetString(5),
                            Identifiant = reader.IsDBNull(6) ? null : reader.GetString(6),
                            MotDePasse = reader.IsDBNull(7) ? null : reader.GetString(7),
                        };
                    }
                }
            }
            

            return employe;
        }

    }
}

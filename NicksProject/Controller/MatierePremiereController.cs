using System;
using NicksProject.Model;
using NicksProject.Utils;
using Npgsql;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicksProject.Controller
{
    internal class MatierePremiereController
    {
        public MatierePremiereController() { }

        public void AjouterMatierePremiere(MatierePremiere matierePremiere)
        {
            try
            {
                using var conn = DbConfig.GetConnection();
                var query = "INSERT INTO \"MatierePremiere\" (\"Nom\", \"Quantite\", \"PrixUnitaire\") " +
                            "VALUES (@nom, @quantite, @prixUnitaire)";
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nom", matierePremiere.Designation);
                cmd.Parameters.AddWithValue("@prixUnitaire", matierePremiere.SeuilMinimum);
                int rowsAffected = cmd.ExecuteNonQuery();
                MessageBox.Show(
                    rowsAffected > 0 ? "Ajout réussi !" : "Aucune matière première ajoutée.",
                    "Résultat", MessageBoxButton.OK, rowsAffected > 0 ? MessageBoxImage.Information : MessageBoxImage.Warning
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

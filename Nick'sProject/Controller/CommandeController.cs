using System;
using System.Collections.Generic;
using Npgsql;
using Nick_sProject.Model;
using Nick_sProject.Service; // TicketService
using Nick_sProject.Utils;   // DbConfig (si tu as ce helper)

namespace Nick_sProject.Controller
{
    public class CommandeController
    {
        private readonly TicketService _ticketService;

        public CommandeController()
        {
            _ticketService = new TicketService();
        }

        /// <summary>
        /// Marque la commande comme payée, persiste en base et imprime le ticket.
        /// </summary>
        public void PayerCommande(int idCommande)
        {
            // 1) Charger toutes les infos nécessaires à l'impression
            var commande = ChargerCommandeComplete(idCommande);
            if (commande == null)
                throw new Exception("Commande introuvable.");

            // 2) Mettre à jour le statut en base
            using var conn = DbConfig.GetConnection();      // adapte à ta classe de config
            using var tx = conn.BeginTransaction();
            using var cmd = new NpgsqlCommand("UPDATE \"Commande\" SET \"Status\" = @status WHERE \"IdCommande\" = @id", conn, tx);
            cmd.Parameters.AddWithValue("@status", "Payé");
            cmd.Parameters.AddWithValue("@id", idCommande);
            cmd.ExecuteNonQuery();
            tx.Commit();

            commande.Status = "Payé";

            // 3) Impression directe
            _ticketService.PrintTicket(commande);
        }

        /// <summary>
        /// Hydrate la commande avec Employe + lignes (menus) pour le ticket.
        /// </summary>
        private Commande? ChargerCommandeComplete(int idCommande)
        {
            using var conn = DbConfig.GetConnection();

            // Charger la commande + employé
            Employe? employe = null;
            Commande? commande = null;

            using (var cmd = new NpgsqlCommand(
                "SELECT c.\"IdCommande\", c.\"IdTable\", c.\"RefCommande\", c.\"Status\", e.\"IdEmp\", e.\"Nom\", e.\"Prenom\" " +
                "FROM \"Commande\" c " +
                "LEFT JOIN \"Employe\" e ON e.\"IdEmp\" = c.\"IdEmploye\" " +
                "WHERE c.\"IdCommande\" = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", idCommande);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    employe = new Employe
                    {
                        Id = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                        Name = reader.IsDBNull(5) ? "" : reader.GetString(5),
                        Prenom = reader.IsDBNull(6) ? "" : reader.GetString(6)
                    };

                    commande = new Commande
                    {
                        IdCommande = reader.GetInt32(0),
                        IdTable = reader.GetInt32(1),
                        RefCommande = reader.GetString(2),
                        Status = reader.GetString(3),
                        Employe = employe,
                        CommandeMenu = new List<CommandeMenu>()
                    };
                }
            }

            if (commande == null) return null;

            // Charger les lignes (menus)
            using (var cmd = new NpgsqlCommand(
                "SELECT cm.\"IdMenu\", cm.\"Quantite\", m.\"NomMenu\", m.\"Prix\" " +
                "FROM \"CommandeMenu\" cm " +
                "JOIN \"Menu\" m ON m.\"IdMenu\" = cm.\"IdMenu\" " +
                "WHERE cm.\"IdCommande\" = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", idCommande);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var menu = new Menu
                    {
                        IdMenu = reader.GetInt32(0),
                        NomMenu = reader.GetString(2),
                        Prix = (int)reader.GetDecimal(3)
                    };

                    commande.CommandeMenu.Add(new CommandeMenu
                    {
                        IdMenu = reader.GetInt32(0),
                        Quantite = reader.GetInt32(1),
                        Menu = menu
                    });
                }
            }

            return commande;
        }
    }
}

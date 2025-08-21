using NicksProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace NicksProject.Service
{
    public class TicketService
    {
        public FlowDocument GenerateTicket(Commande commande)
        {
            FlowDocument doc = new FlowDocument();
            doc.PageWidth = 200; // ≈ 5.5 cm (200 px en 96dpi)
            doc.FontFamily = new FontFamily("Consolas");
            doc.FontSize = 10;

            // En-tête
            Paragraph header = new Paragraph(new Run("***** FACTURE *****"));
            header.TextAlignment = TextAlignment.Center;
            doc.Blocks.Add(header);

            doc.Blocks.Add(new Paragraph(new Run($"Ref: {commande.RefCommande}")));
            doc.Blocks.Add(new Paragraph(new Run($"Table: {commande.IdTable}")));
            doc.Blocks.Add(new Paragraph(new Run($"Employé: {commande.Employe?.Name}")));

            doc.Blocks.Add(new Paragraph(new Run("--------------------------------")));

            decimal total = 0;
            foreach (var cm in commande.CommandeMenu)
            {
                decimal prixLigne = cm.Menu.Prix * cm.Quantite;
                total += prixLigne;

                string line = $"{cm.Menu.NomMenu} x{cm.Quantite}  {prixLigne:C}";
                doc.Blocks.Add(new Paragraph(new Run(line)));
            }

            doc.Blocks.Add(new Paragraph(new Run("--------------------------------")));
            doc.Blocks.Add(new Paragraph(new Run($"TOTAL: {total:C}")));
            doc.Blocks.Add(new Paragraph(new Run($"Date: {DateTime.Now}")));

            Paragraph footer = new Paragraph(new Run("Merci et à bientôt!"));
            footer.TextAlignment = TextAlignment.Center;
            doc.Blocks.Add(footer);

            return doc;
        }

        public void PrintTicket(Commande commande)
        {
            FlowDocument doc = GenerateTicket(commande);
            PrintDialog pd = new PrintDialog();

            // Impression directe sans boîte de dialogue
            doc.PageHeight = Double.NaN;
            doc.PageWidth = 200;
            doc.ColumnWidth = 200;
            IDocumentPaginatorSource idpSource = doc;
            pd.PrintDocument(idpSource.DocumentPaginator, "Ticket Facture");
        }
    }
}

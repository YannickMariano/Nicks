using Nick_sProject.Model;
using Nick_sProject.View;
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
    public partial class dashboard : Window
    {
        private List<Utilisateur> tousLesUtilisateurs;
        private int pageActuelle = 1;
        private int lignesParPage = 6;

        public dashboard()
        {
            InitializeComponent();
            ChargerUtilisateurs();
            AfficherPage(pageActuelle);
        }
        // Simulation d'affichage d'utilisateur pour voir le rendu du tableau
        private void ChargerUtilisateurs()
        {
            tousLesUtilisateurs = new List<Utilisateur>
            {
                new Utilisateur { NomComplet = "John Doe", Numero = "034 11 111 11", Role = "Serveur", DerniereConnexion = "-", Adresse = "Tanambao RDI" },
                new Utilisateur { NomComplet = "ANDRIANANTENAINA Ninah Eulalie", Numero = "034 22 222 22", Role = "Caissier", DerniereConnexion = "11-07-2025 17:54", Adresse = "Tanambao RDI" },
                new Utilisateur { NomComplet = "RASOLOFOMANANA Diamondra Ny Aina", Numero = "034 33 333 33", Role = "Serveur", DerniereConnexion = "-", Adresse = "Tanambao RDI" },
                new Utilisateur { NomComplet = "RAKOTOHARILALA Kaloina Alicia", Numero = "034 44 444 44", Role = "Caissier", DerniereConnexion = "11-07-2025 17:54", Adresse = "Tanambao RDI" },
                new Utilisateur { NomComplet = "TOTOZAFY Safidy Mendrika", Numero = "034 55 555 55", Role = "Serveur", DerniereConnexion = "-", Adresse = "Tanambao RDI" },
                new Utilisateur { NomComplet = "RABENJAMINA Miarintsoa Cathicia", Numero = "034 66 666 66", Role = "Serveur", DerniereConnexion = "-", Adresse = "Tanambao RDI" },
                new Utilisateur { NomComplet = "Tom Le Mimi", Numero = "034 77 777 77", Role = "Caissier", DerniereConnexion = "-", Adresse = "Tanambao RDI" },
                new Utilisateur { NomComplet = "Lalao Fitiavana", Numero = "034 88 888 88", Role = "Serveur", DerniereConnexion = "-", Adresse = "Tanambao RDI" },
                new Utilisateur { NomComplet = "Andry Ravelo", Numero = "034 99 999 99", Role = "Serveur", DerniereConnexion = "-", Adresse = "Tanambao RDI" },
                new Utilisateur { NomComplet = "Miangaly R", Numero = "034 00 000 00", Role = "Caissier", DerniereConnexion = "-", Adresse = "Tanambao RDI" }
            };
        }

        private void AfficherPage(int page)
        {
            pageActuelle = page;
            int totalPages = (int)Math.Ceiling((double)tousLesUtilisateurs.Count / lignesParPage);

            var utilisateursAffiches = tousLesUtilisateurs
                .Skip((page - 1) * lignesParPage)
                .Take(lignesParPage)
                .ToList();

            UsersDataGrid.ItemsSource = utilisateursAffiches;
            ConstruirePagination(totalPages);
        }

        private void ConstruirePagination(int totalPages)
        {
            PaginationPanel.Children.Clear();

            for (int i = 1; i <= totalPages; i++)
            {
                if (i <= 4 || i == totalPages)
                {
                    var btnPage = new Button
                    {
                        Content = i.ToString(),
                        Margin = new Thickness(5, 0, 5, 0),
                        Padding = new Thickness(10, 5, 10, 5),
                        FontFamily = new FontFamily("Times New Roman"),
                        FontSize = 16,
                        FontWeight = FontWeights.Bold,
                        Cursor = Cursors.Hand,
                        BorderThickness = new Thickness(0),
                        Background = Brushes.Transparent,
                        Foreground = Brushes.Black,
                        MinWidth = 43,
                        MinHeight = 44
                    };

                    // Style arrondi si sélectionné
                    if (i == pageActuelle)
                    {
                        btnPage.Background = (Brush)new BrushConverter().ConvertFrom("#EDEDED");
                        btnPage.Foreground = Brushes.Black;
                        btnPage.Template = new ControlTemplate(typeof(Button))
                        {
                            VisualTree = GetRoundedButtonFactory()
                        };
                    }

                    btnPage.Click += (s, e) =>
                    {
                        int numPage = int.Parse((s as Button).Content.ToString());
                        AfficherPage(numPage);
                    };

                    PaginationPanel.Children.Add(btnPage);
                }
                else if (i == 5 && totalPages > 5)
                {
                    TextBlock ellipses = new TextBlock
                    {
                        Text = "...",
                        Margin = new Thickness(5, 0, 5, 0),
                        FontFamily = new FontFamily("Times New Roman"),
                        FontSize = 16,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    PaginationPanel.Children.Add(ellipses);
                }
            }
        }

        // Bouton arrondi pour page sélectionnée
        private FrameworkElementFactory GetRoundedButtonFactory()
        {
            var border = new FrameworkElementFactory(typeof(Border));
            border.SetValue(Border.BackgroundProperty, (Brush)new BrushConverter().ConvertFrom("#EDEDED"));
            border.SetValue(Border.CornerRadiusProperty, new CornerRadius(8));
            border.SetValue(Border.SnapsToDevicePixelsProperty, true);

            var contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenter.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            contentPresenter.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
            contentPresenter.SetValue(ContentPresenter.MarginProperty, new Thickness(5, 2, 5, 2));
            contentPresenter.SetValue(ContentPresenter.RecognizesAccessKeyProperty, true);

            border.AppendChild(contentPresenter);
            return border;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Réinitialiser les styles de tous les boutons si nécessaire (optionnel selon logique)
            // TODO : tu peux gérer ici les autres boutons si besoin

            if (sender is Button clickedButton)
            {
                // Récupérer le Border parent du bouton
                if (clickedButton.Parent is Border parentBorder)
                {
                    parentBorder.Background = new SolidColorBrush(Color.FromRgb(239, 130, 13)); // orange
                }

                // Modifier le contenu du bouton : icône + texte
                if (clickedButton.Content is StackPanel contentPanel)
                {
                    foreach (var child in contentPanel.Children)
                    {
                        if (child is Image img)
                        {
                            // Remplacer l’image par la version blanche
                            img.Source = new BitmapImage(new Uri("/Ressource/employe_white.png", UriKind.Relative));
                        }
                        else if (child is TextBlock txt)
                        {
                            // Changer la couleur du texte en blanc
                            txt.Foreground = Brushes.White;
                        }
                    }
                }
            }
        }

    }

}







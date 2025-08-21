using NicksProject.Controller;
using NicksProject.Model;
using NicksProject.Controller;
using NicksProject.View;
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

namespace NicksProject.View
{
    /// <summary>
    /// Logique d'interaction pour ListeEmploye.xaml
    /// </summary>
    public partial class ListeEmploye : UserControl
    {
        private EmployeController _employeController = new EmployeController();
        private List<Model.Employe> AllEmploye;

        private int pageActuelle = 1;
        private int lignesParPage = 6;
        //private Employe employe;

        public ListeEmploye()
        {
            InitializeComponent();
            RafraichirEmploye();
        }

        public void RafraichirEmploye()
        {
            ChargeEmployes();
            AfficherPage(pageActuelle);
        }

        private void ChargeEmployes()
        {
            AllEmploye = _employeController.GetAllEmployes();
            TxtTotalEmployes.Text = AllEmploye.Count.ToString();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Model.Employe> employes = _employeController.GetAllEmployes();
            EmplpoyeDataGrid.ItemsSource = employes;
        }

        private void AfficherPage(int page)
        {
            pageActuelle = page;
            int totalPages = (int)Math.Ceiling((double)AllEmploye.Count / lignesParPage);

            var utilisateursAffiches = AllEmploye
                .Skip((page - 1) * lignesParPage)
                .Take(lignesParPage)
                .ToList();

            EmplpoyeDataGrid.ItemsSource = utilisateursAffiches;
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
            AjoutEmploye ajoutEmploye = new AjoutEmploye(this);
            ajoutEmploye.ShowDialog();
        }

        private void Employe_Button_Click(object sender, RoutedEventArgs e)
        {
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

        private void Button_Supprimer_Click(object sender, RoutedEventArgs e)
        {
            if (EmplpoyeDataGrid.SelectedItem is Model.Employe selectedEmploye)
            {
                _employeController.SupprimerEmploye(selectedEmploye.Id);
                MessageBox.Show("Voulez-vous vraiment supprimer cet employé ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.None);
                RafraichirEmploye();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un employé à supprimer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Modifier_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var employe = button?.Tag as NicksProject.Model.Employe;
            if (employe != null)
            {

                var employeComplet = _employeController.GetEmployeById(employe.Id);
                var modifWindow = new ModifierEmploye(employeComplet, _employeController, this);
                modifWindow.ShowDialog();


                ChargeEmployes();
            }
            else
            {
                MessageBox.Show("Employé non trouvé.");
            }
        }



    }
}
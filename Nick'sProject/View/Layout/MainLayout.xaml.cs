using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nick_sProject.View.Layout
{
    public partial class MainLayout : UserControl
    {
        public MainLayout()
        {
            InitializeComponent();
            Loaded += MainLayout_Loaded;
            SizeChanged += MainLayout_SizeChanged;
        }

        private void MainLayout_Loaded(object sender, RoutedEventArgs e)
        {
            AdjustGlobalScale();
        }

        private void MainLayout_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AdjustGlobalScale();
        }

        private void AdjustGlobalScale()
        {
            double baseWidth = 1000; // largeur de référence pour le scale 1.0
            double currentWidth = ActualWidth;

            // On ne dépasse pas 1.0 (évite de trop zoomer)
            double scale = Math.Min(1.0, currentWidth / baseWidth);

            // Appliquer le scale global (sidebar + contenu + polices)
           
        }

        public void NavigateTo(UserControl page)
        {
            MainContent.Content = page;
        }
    }
}

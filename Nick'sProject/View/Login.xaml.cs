using System.Windows;
using Nick_sProject.View;
using Nick_sProject.Controller;

namespace Nick_sProject.View
{
    public partial class LoginWindow : Window
    {
        private readonly AuthentificationService authService = new AuthentificationService();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Connexion_Click(object sender, RoutedEventArgs e)
        {
            string identifiant = txtIdentifiant.Text;
            string motDePasse = txtMotDePasse.Password;

            var user = authService.Login(identifiant, motDePasse);

            if (user != null)
            {
                MessageBox.Show($"Bienvenue {user.Prenom} {user.Name}", "Succès");
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Identifiant ou mot de passe incorrect", "Erreur");
            }
        }
    }
}

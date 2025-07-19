using Nick_sProject.Model;
using Nick_sProject.Utils;
using Npgsql;
using BCrypt.Net;

namespace Nick_sProject.Controller
{
    public class AuthentificationService
    {
        public Employe? Login(string identifiant, string motDePasse)
        {
            using (var conn = DbConfig.GetConnection())
            {

                string query = "SELECT * FROM \"Employe\" WHERE \"Identifiant\" = @identifiant";

                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@identifiant", identifiant);

                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    string motDePasseHashe = reader.GetString(reader.GetOrdinal("MotDePasse"));


                    bool estValide = BCrypt.Net.BCrypt.Verify(motDePasse, motDePasseHashe);

                    if (estValide)
                    {
                        return new Employe
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("IdEmp")),
                            Name = reader.GetString(reader.GetOrdinal("Nom")),
                            Prenom = reader.GetString(reader.GetOrdinal("Prenom")),
                            Identifiant = reader.GetString(reader.GetOrdinal("Identifiant")),
                            MotDePasse = motDePasseHashe,
                            Statut = reader.GetString(reader.GetOrdinal("Statut"))
                        };
                    }
                }
            }

            return null;
        }
    }
}

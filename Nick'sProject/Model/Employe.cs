using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nick_sProject.Model
{
    public class Employe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string NumTel { get; set; }
        public string MotDePasse { get; set; }
        public string DerniereConnexion { get; set; }
        public string Statut { get; set; }
        public string Identifiant { get; set; }
        public DateTime? DateConnection { get; set; }

        public String NomComplet => $"{Name} {Prenom}";

        public Employe() { }
        public Employe(int id, string name, string prenom, string adresse, string numTel, string mdp, string statut, string identifiant)
        {
            Id = id;
            Name = name;
            Prenom = prenom;
            Adresse = adresse;
            NumTel = numTel;
            MotDePasse = mdp;
            Statut = statut;         
            Identifiant = identifiant;
        }

        public Employe(int id, string name, string prenom, string adresse, string numTel)
        {
            Id = id;
            Name = name;
            Prenom = prenom;
            Adresse = adresse;
            NumTel = numTel;
        }

        public Employe(string name, string prenom, string adresse, string numTel)
        {
            Name = name;
            Prenom = prenom;
            Adresse = adresse;
            NumTel = numTel;
        }

        public Employe(int id, string name, string numTel, string adresse, string statut, DateTime? dateConnection)
        {
            Id = id;
            Name = name;
            NumTel = numTel;
            Adresse = adresse;
            Statut = statut;         
            DateConnection = dateConnection;
        }
    }
}

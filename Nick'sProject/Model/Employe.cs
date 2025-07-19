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
        public string Statut { get; set; }
        public string Identifiant { get; set; }
        
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
    }
}

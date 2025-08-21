using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicksProject.Model
{
    public class Utilisateur
    {
        public string NomComplet { get; set; }
        public string Numero { get; set; }
        public string Role { get; set; }
        public string DerniereConnexion { get; set; }
        public string Adresse { get; set; }
    }
}


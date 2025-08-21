using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicksProject.Model
{
    public class Commande
    {
        public int IdCommande { get; set; }
        public int IdTable { get; set; }
        public int IdMenu { get; set; }
        public int IdEmploye { get; set; }
        public string Status { get; set; }
        public string RefCommande { get; set; }
        public List<CommandeMenu> CommandeMenu { get; set; } = new();
        public Employe Employe { get; set; }
    }
}

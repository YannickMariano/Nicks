using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicksProject.Model
{
    public class CommandeMenu
    {
        public int IdCommandeMenu { get; set; }
        public int IdMenu { get; set; }
        public int Quantite { get; set; }
        public Menu Menu { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaulerDeControlRM
{
    public class ConjuntValors
    {
        public string Nom { get; set; }
        public bool Cerca { get; set; }
        public List<string> Valors { get; set; }
        public List<string> ValorsRestants { get; set; }
        public bool EsPotRepetir { get; set; }

        public ConjuntValors(string nom, List<string> valors, bool esPotRepetir, bool cerca)
        {
            Nom = nom;
            Valors = valors;
            ValorsRestants = new List<string>(valors);
            EsPotRepetir = esPotRepetir;
            Cerca = cerca;
        }

        public void EliminarValor(string valor)
        {
            if (EsPotRepetir)
                ValorsRestants.Add(valor);
        }
    }
}


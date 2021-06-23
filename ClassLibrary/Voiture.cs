using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Voiture : Vehicule
    {
        private int _puissance;

        public Voiture () : base(){ }
        public Voiture(int numero, string marque = "Fiat", string modele = "Focus RS", int puissance = 100) : base(numero, marque, modele)
        {
            Puissance = puissance;
        }

        public int Puissance
        {
            get { return _puissance; }
            set { _puissance = value; }
        }

        public override string ToString() => $"Marque : {base.Marque} Modèle : {base.Modele} Numéro : {base.Numero} Puissance : {Puissance}";
        public override string ToStringOnlyProp() => $"{Marque}{Modele}{Numero}{Puissance}";

    }
}

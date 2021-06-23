using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Camion : Vehicule
    {
        private int _poids;

        public Camion() : base(){ }
        public Camion(int numero, string marque = "Mercedes", string modele = "Actros", int poids = 1000) : base(numero, marque, modele)
        {
            Poids = poids;
        }

        public int Poids
        {
            get { return _poids; }
            set { _poids = value; }
        }

        public override string ToString() => $"Marque : {base.Marque} Modèle : {base.Modele} Numéro : {base.Numero} Poids : {Poids}";
        public override string ToStringOnlyProp() => $"{Marque}{Modele}{Numero}{Poids}";
    }
}

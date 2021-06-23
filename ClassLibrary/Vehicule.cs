using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClassLibrary
{
    [Serializable]
    [XmlInclude(typeof(Camion))]
    [XmlInclude(typeof(Voiture))]
    public abstract class Vehicule
    {
        private string _marque;
        private string _modele;
        private int _numero;

        public Vehicule() { }
        public Vehicule(int numero, string marque = "Fiat", string modele = "Focus RS")
        {
            Numero = numero;
            Marque = marque;
            Modele = modele;
        }

        public int Numero
        {
            get { return _numero; }
            set
            {
                if (CheckNumero(value))
                    _numero = value;
            }
        }

        public string Modele
        {
            get { return _modele; }
            set { _modele = value; }
        }

        public string Marque
        {
            get { return _marque; }
            set
            {
                if (CheckMarque(value))
                    _marque = value;
            }
        }

        public override string ToString() => $"Marque : {Marque} Modèle : {Modele} Numéro : {Numero}";
        public virtual string ToStringOnlyProp() => $"{Marque}{Modele}{Numero}";

        public static bool CheckNumero(int numero)
        {
            if (numero >= 1000 && numero <= 999999)
                return true;
            return false;
        }

        public static bool CheckMarque(string marque)
        {
            Regex regMarque = new Regex("^[^0-9]*$");
            if (regMarque.IsMatch(marque))
                return true;
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class Crud
    {
        public const string PATH_FILE = "Vehicule.xml";
        private static List<Vehicule> _listvehicle;

        public static List<Vehicule> ReadAll()
        {
            return _listvehicle;
        }

        public static List<Vehicule> SetAll(List<Vehicule> listvehicle)
        {
            _listvehicle = listvehicle;
            ManageFile<Vehicule>.SetFile(PATH_FILE, _listvehicle);
            return _listvehicle;
        }

        public static void Add(Vehicule vehicle)
        {
            _listvehicle.Add(vehicle);
            ManageFile<Vehicule>.SetFile(PATH_FILE, _listvehicle);
        }

        public static Vehicule ReadOneByNumber(int numero)
        {
            Vehicule vehicle = _listvehicle.FirstOrDefault(l => l.Numero == numero);
            return vehicle;
        }

        public static Vehicule ReadOneByIndex(int index)
        {
            Vehicule vehicle = _listvehicle[index];
            return vehicle;
        }

        public static int FindIndex(Vehicule vehicle)
        {
            int indexVehicle = _listvehicle.FindIndex(v => v == vehicle);
            return indexVehicle;
        }

        public static void Replace(Vehicule vehicle, int index)
        {
            _listvehicle[index] = vehicle;
            ManageFile<Vehicule>.SetFile(PATH_FILE, _listvehicle);
        }

        public static void Remove(Vehicule vehicle)
        {
            _listvehicle.Remove(vehicle);
            ManageFile<Vehicule>.SetFile(PATH_FILE, _listvehicle);
        }

        public static List<Vehicule> OrderByNumero()
        {
            return _listvehicle.OrderBy(l => l.Numero).ToList();
        }
        public static List<Vehicule> OrderByMarque()
        {
            return _listvehicle.OrderBy(l => l.Marque).ToList();
        }
        public static List<Vehicule> OrderByModele()
        {
            return _listvehicle.OrderBy(l => l.Modele).ToList();
        }
        public static List<Vehicule> OrderByPuissance()
        {
            return _listvehicle.Where(v => v is Voiture).OrderBy(v => ((Voiture)v).Puissance).ToList();
        }
        public static List<Vehicule> OrderByPoids()
        {
            return _listvehicle.Where(v => v is Camion).OrderBy(v => ((Camion)v).Poids).ToList();
        }

        public static List<Vehicule> Search(string pattern)
        {
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return _listvehicle.Where(l => l is Voiture && regex.IsMatch(((Voiture)l).ToStringOnlyProp())).Concat(_listvehicle.Where(l => l is Camion && regex.IsMatch(((Camion)l).ToStringOnlyProp()))).ToList();
        }

        public static void Loader()
        {
            _listvehicle = ManageFile<Vehicule>.GetFile(PATH_FILE);
            if (_listvehicle.Count <= 0)// listvehicle is empty so I add data 
            {
                _listvehicle.Add(new Voiture(1001));
                _listvehicle.Add(new Voiture(1002, "Seat", "Ibiza", 150));
                _listvehicle.Add(new Voiture(1003, "Volkswagen", "Polo", 150));
                _listvehicle.Add(new Camion(1004, "Mercedes", "Actros", 400));
            }
        }
    }
}

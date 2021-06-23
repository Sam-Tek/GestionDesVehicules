using ClassLibrary;
using System;
using System.Text.RegularExpressions;


namespace GestionDesVehicules
{
    class Program
    {
        public const string PATH_FILE = "Vehicule.xml";

        static void Main(string[] args)
        {
            int choice;
            bool validSaisir;
            Crud.Loader();
            do
            {
                Console.WriteLine("Quelle action ?");
                Console.WriteLine("1 - Créer un véhicule");
                Console.WriteLine("2 - Voir un véhicule");
                Console.WriteLine("3 - Mettre à jour un véhicule");
                Console.WriteLine("4 - Supprimer un véhicule");
                Console.WriteLine("5 - Trier les véhicules");
                Console.WriteLine("6 - Filtrer les véhicules");
                Console.WriteLine("7 - Sauvegarder les véhicules");

                validSaisir = ReadLine(out choice);

                if (validSaisir == false)
                {
                    Console.WriteLine("Erreur de saisie");
                    continue;
                }

                switch (choice)
                {
                    case 1: Create(); break;
                    case 2: Read(); break;
                    case 3: Update(); break;
                    case 4: Remove(); break;
                    case 5: Order(); break;
                    case 6: Search(); break;
                    case 7: Save(); break;
                    case -1: Console.WriteLine("bye bye"); break;
                    default:
                        Console.WriteLine("Erreur !");
                        break;
                }
            } while (choice != -1);
        }

        static bool ReadLine(out int result, Func<int, bool> validformat = null, string text = "Saisir un nombre")
        {
            Console.WriteLine(text);
            string saisir = Console.ReadLine();
            return int.TryParse(saisir, out result) && (validformat == null || validformat(result));
        }
        static bool ReadLine(out string result, Func<string, bool> validformat = null, string text = "Saisir un texte")
        {
            Console.WriteLine(text);
            result = Console.ReadLine();
            return result != "" && (validformat == null || validformat(result));
        }

        static void Create()
        {
            string marque, modele, typeVehicule = "";
            int numero, puissance, poids;
            bool start = true;
            Vehicule ObjCreate;

            do
            {
                Regex regMarque = new Regex("^[^0-9]*$");
                start = ReadLine(out marque, s => Vehicule.CheckMarque(s), text: "Saisir la marque");
            } while (start == false);

            do
            {
                start = ReadLine(out modele, text: "Saisir le modèle");
            } while (start == false);

            do
            {
                start = ReadLine(out numero, v => Vehicule.CheckNumero(v), "Saisir un identifiant numérique max 1000 min 999999");
            } while (start == false);

            do
            {
                start = ReadLine(out typeVehicule, v => v == "v" || v == "c", "Saisir v pour une voiture et c pour un camion");
            } while (start == false);

            if (typeVehicule == "v")
            {
                do
                {
                    start = ReadLine(out puissance, v => v > 0, "Saisir la puissance de la voiture");
                } while (start == false);

                ObjCreate = new Voiture(numero, marque, modele, puissance);
            }
            else
            {
                do
                {
                    start = ReadLine(out poids, c => c > 0, "Saisir le poids du camion");
                } while (start == false);

                ObjCreate = new Camion(numero, marque, modele, poids);
            }

            Crud.Add(ObjCreate);
        }

        static void Read()
        {
            bool start = true;
            int numero;
            Console.WriteLine(string.Join("\n", Crud.ReadAll()));
            do
            {
                start = ReadLine(out numero, v => Vehicule.CheckNumero(v), "Saisir un identifiant numérique max 1000 min 999999");
                Vehicule vehicle = Crud.ReadOneByNumber(numero);
                if (vehicle == null)
                {
                    Console.WriteLine("L'identifiant n'existe pas");
                    start = false;
                }
                else
                {
                    Console.WriteLine(vehicle);
                }
            } while (start == false);
        }

        static void Update()
        {
            bool start = true;
            int numero;
            string marque, modele;
            int numeroUpdate, puissance, poids;

            Console.WriteLine(string.Join("\n", Crud.ReadAll()));
            do
            {
                start = ReadLine(out numero, v => Vehicule.CheckNumero(v), "Saisir un identifiant numérique max 1000 min 999999");
                Vehicule vehicle = Crud.ReadOneByNumber(numero);
                if (vehicle == null)
                {
                    Console.WriteLine("L'identifiant n'existe pas");
                    start = false;
                }
                else
                {
                    Console.WriteLine($"Vous voulez modifier cette ligne : \n --- {vehicle} ---");
                    do
                    {
                        start = ReadLine(out marque, s => Vehicule.CheckMarque(s), text: "Saisir la marque");
                    } while (start == false);

                    do
                    {
                        start = ReadLine(out modele, text: "Saisir le modèle");
                    } while (start == false);

                    do
                    {
                        start = ReadLine(out numeroUpdate, v => Vehicule.CheckNumero(v), "Saisir un identifiant numérique max 1000 min 999999");
                    } while (start == false);

                    if (vehicle is Voiture)
                    {
                        do
                        {
                            start = ReadLine(out puissance, v => v > 0, "Saisir la puissance de la voiture");
                        } while (start == false);

                        Voiture ObjUpdate = (Voiture)vehicle;
                        ObjUpdate.Marque = marque;
                        ObjUpdate.Modele = modele;
                        ObjUpdate.Numero = numeroUpdate;
                        ObjUpdate.Puissance = puissance;
                        Crud.Replace(vehicle, Crud.FindIndex(vehicle));

                    }
                    else
                    {
                        do
                        {
                            start = ReadLine(out poids, c => c > 0, "Saisir le poids du camion");
                        } while (start == false);

                        Camion ObjUpdate = (Camion)vehicle;
                        ObjUpdate.Marque = marque;
                        ObjUpdate.Modele = modele;
                        ObjUpdate.Numero = numeroUpdate;
                        ObjUpdate.Poids = poids;
                        Crud.Replace(vehicle, Crud.FindIndex(vehicle));
                    }
                }
            } while (start == false);
        }

        static void Remove()
        {
            bool start = true;
            int numero;
            string validRemove = "";
            Console.WriteLine(string.Join("\n", Crud.ReadAll()));
            do
            {
                start = ReadLine(out numero, v => Vehicule.CheckNumero(v), "Saisir un identifiant numérique max 1000 min 999999");
                Vehicule vehicle = Crud.ReadOneByNumber(numero);
                if (vehicle == null)
                {
                    Console.WriteLine("L'identifiant n'existe pas");
                    start = false;
                }
                else
                {
                    Console.WriteLine($"voulez-vous vraiment supprimer => \n--- {vehicle} ---");
                    start = ReadLine(out validRemove, v => v == "y" || v == "n", "Saisir y pour supprimer et n pour annuler");

                    switch (validRemove)
                    {
                        case "y":
                            Crud.Remove(vehicle);
                            break;
                        case "n":
                            start = true;
                            break;
                        default:
                            Console.WriteLine("Erreur");
                            break;
                    }
                }
            } while (start == false);
        }

        static void Order()
        {
            bool start = true;
            string typeOrder;

            do
            {
                Regex regMarque = new Regex("^(n|m|o|pu|po)?$");
                start = ReadLine(out typeOrder, t => regMarque.IsMatch(t), "Quel tri ? n: numéro, m: marque, o: modèle, pu => puissance (voiture seulement), po => poids (camion seulement)");
                if (start == true)
                {
                    switch (typeOrder)
                    {
                        case "n": Console.WriteLine(string.Join("\n", Crud.OrderByNumero())); break;
                        case "m": Console.WriteLine(string.Join("\n", Crud.OrderByMarque())); break;
                        case "o": Console.WriteLine(string.Join("\n", Crud.OrderByModele())); break;
                        case "pu":
                            Console.WriteLine(string.Join("\n", Crud.OrderByPuissance()));
                            break;
                        case "po":
                            Console.WriteLine(string.Join("\n", Crud.OrderByPoids()));
                            break;
                    }
                }
            } while (start == false);
        }

        static void Search()
        {
            bool start = false;
            string search;

            do
            {
                start = ReadLine(out search, text: "Saisir votre recherche");
                
                Console.WriteLine(string.Join("\n", Crud.Search(search)));
            } while (start == false);
        }

        static void Save()
        {
            ManageFile<Vehicule>.SetFile(PATH_FILE, Crud.ReadAll());
            Console.WriteLine("ok");
        }

    }
}

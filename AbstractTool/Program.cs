using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace AbstractTool
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instanciem la ruta de Escriptori.
            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //Nom de la carpeta.
            string carpeta = "ProvaAbstractTool";
            string pathAplicacio = Path.Combine(@ruta, carpeta);

            //Si la carpeta no existeix, crea una.
            if (!Directory.Exists(pathAplicacio))
            {
                Directory.CreateDirectory(pathAplicacio);
            }
            string opcio;
            Console.WriteLine("\nEscriu la opció: \n1: Opció fitxer. \n2: Surt de la aplicació.");
            opcio = Console.ReadLine().ToLower();
            switch (opcio)
            {
                case "1":
                    Console.WriteLine("\nEscriu el nom del fitxer:");
                    string[] llistaFitxers = Directory.GetFiles(pathAplicacio);
                    //Per cada fitxer trobat en la ruta, guarda'l en l'array de strings.
                    Console.WriteLine("\nLlista de fitxers: (exemple: UF1_PT2_001_ArticleExemple)");
                    foreach (string file in llistaFitxers)
                        Console.WriteLine("Fitxer -> " + Path.GetFileName(file));

                    string nomFitxer = Console.ReadLine();
                    string pathFitxer = Path.Combine(pathAplicacio, nomFitxer);
                    string fiDeFitxer = ""; //string que usem per a fer el contingut del fitxer.
                    //Si el fitxer de la ruta existeix...
                    if (File.Exists(pathFitxer))
                    {
                        //Escriu en consola el nom del fitxer i afegim el fitxer en memòria.
                        Console.WriteLine("\n\nNom del fitxer: " + Path.GetFileName(pathFitxer));
                        fiDeFitxer += "Nom del fitxer: " + Path.GetFileName(pathFitxer) + "\n\r";

                        Console.WriteLine("Extensió del fitxer: " + Path.GetExtension(pathFitxer));
                        fiDeFitxer += "Extensió del fitxer: " + Path.GetExtension(pathFitxer) + "\n\r";

                        Console.WriteLine("Data: " + File.GetLastWriteTime(pathFitxer));
                        fiDeFitxer += "Data: " + File.GetLastWriteTime(pathFitxer) + "\n\r";

                        string strFitxer = Origin.llegirFitxerString(pathFitxer); //llegir el fitxer i posarlo a un string
                        Console.WriteLine("Número de paraules del fitxer: " + Origin.contarParaules(strFitxer));
                        fiDeFitxer += "Número de paraules del fitxer: " + Origin.contarParaules(strFitxer) + "\n\r";

                        strFitxer = Origin.parseCadena(strFitxer); //parse de caràcters de puntuació + paraules prohibides
                        strFitxer = Origin.paraulesConcurrents(strFitxer); //determinar les 5 pareules mes usades
                        Console.WriteLine("Temàtica:\n" + strFitxer);
                        fiDeFitxer += "Temàtica:\n" + strFitxer;
                        Origin.fitxerInfo(pathAplicacio, nomFitxer, fiDeFitxer); //escriure en el fitxer info
                        Console.WriteLine("\nPrem qualsevol tecla per sortir");
                        Console.ReadKey();
                    }
                    else
                    {
                        //En cas de no trobar cap fitxer.
                        Console.WriteLine("ERR: Fitxer no trobat...");
                    }
                    break;
                // En cas de posar un 2 sortim.
                case "2":
                    Console.WriteLine("\nPrem qualsevol tecla per sortir");
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("ERR: Opció mal seleccionada...");
                    
                    break;
            }
            Console.ReadKey();
        }
    }
}

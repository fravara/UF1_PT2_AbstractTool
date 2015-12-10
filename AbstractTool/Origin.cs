using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace AbstractTool
{
    public static class Origin
    {
        public static Dictionary<string, int> llistaParaules = new Dictionary<string, int>();

        //LLegeix el fitxer de la ruta, fins que no acaba de llegir el fitxer no el tanca.
        public static string llegirFitxerString(string ruta)
        {
            StreamReader streamReader = new StreamReader(ruta);
            string str = streamReader.ReadToEnd();
            streamReader.Close();
            return str;
        }
        //Escriu la informació fel fitxer.
        public static void fitxerInfo(string path, string nomfitxer, string str)
        {
            //El stream writer escriu en el fitxer a partir del nom del fitxer original.
            StreamWriter sw = new StreamWriter(Path.Combine(@path, nomfitxer.Replace(".txt", "_info.txt")));
            sw.WriteLine(str);
            //Important tencar el fitxer.
            sw.Close();
        }

        //Comptar les paraules del fitxer, sense comptar els espais i els punts. Tots aquest queden substituits per espais.
        public static int contarParaules(string str)
        {
            str = Regex.Replace(str, @".\n", ". ");
            string[] paraules = str.Split(' ');
            return paraules.Count();
        }
        //Desa en una array de string els caracters especials capturats en el reader.
        public static string parseCadena(string str)
        {
            string[] simbols = { ".", ":", ",", ";", "?", "¿", "!", "¡" };

            //Llegeix les paraules prohibides
            string paraulesPFitxer = llegirFitxerString(@"C:\Users\franc\Dropbox\DAM_2\m06\UF1\Practica\AbstractTool\forbiddenWords.txt");
            string[] forbiddenWords = paraulesPFitxer.Split(',');

            //Per cada simbol trobat, canvia'l per un espai. 
            foreach (string p in simbols)
                str = str.Replace(p, " ");

            //Per cada paraula prohibida, canvia'l per un espai.
            foreach (string p in forbiddenWords)
                str = str.Replace(p, " ");

            str = Regex.Replace(str, @"\s+", " "); //expressió regular que cambia multi espai a un unic espai
            return str;
        }

        //Compta les paraules mes repetides.
        public static string paraulesConcurrents(string str)
        {
            string resultat = "";
            int cmtParaules = 0; //Comptador.
            int ultimaparaulaCount = 0; //La ultima paraula amb més concurrències.

            string[] paraules = str.Split(' ');//Paraules separades per espais.

            //Per cada paraula capturada, si existeix ja a la llista, sumem el comptador.
            foreach (string p in paraules)
            {
                if (!llistaParaules.ContainsKey(p))
                    llistaParaules.Add(p, 1); //afegim nou registre
                else
                    //accedim el value amb la [key]
                    llistaParaules[p]++;
            }

            //Contindrà la informació de la cadena a la hora de pasar les diferents claus i valors.
            //Desa el registre al Dictionary, les paraules amb les posicions.
            foreach (KeyValuePair<string, int> kvp in llistaParaules.OrderByDescending(x => x.Value))
            {
                if (cmtParaules == 5 && ultimaparaulaCount == kvp.Value)
                {
                    resultat += "\tParaula: " + kvp.Key + " \n\tocurrencies: " + kvp.Value + "\n\n";
                }
                //Si el comptador de paraules concurrents es menor a 5...
                if (cmtParaules < 5)
                {
                    resultat += "\tParaula: " + kvp.Key + " \n\tocurrencies: " + kvp.Value + "\n\n";
                    cmtParaules++;
                    ultimaparaulaCount = kvp.Value;
                }
            }

            return resultat;
        }

    }
}

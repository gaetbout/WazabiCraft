using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Xml.Linq;
using Wazabi.Model;

namespace Wazabi
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] // 1 seule instance est renvoyé
    public class GestionDe : IGestionDe
    {
        private readonly WazabiEntities context = new WazabiEntities();

        private readonly int nbDesJoueur;
        private readonly int nbDesTotal;
        private IList<De> probabiliteDes = new List<De>();

        public GestionDe(IEnumerable<XElement> xml)
        {
            nbDesJoueur = int.Parse(xml.First().Attribute("nbParJoueur").Value);
            nbDesTotal = int.Parse(xml.First().Attribute("nbTotalDes").Value);

            foreach (var face in xml.Descendants("face"))
            {
                for (int i = 0; i < (int.Parse(face.Attribute("nbFaces").Value)); i++)
                {
                    De tmp = new De();
                    tmp.Id = probabiliteDes.Count();
                    tmp.Valeur = face.Attribute("identif").Value;
                    tmp.ImageRef = face.Attribute("src").Value;

                    probabiliteDes.Add(tmp);
                }
            }
        }

        public void GenererDes(ICollection<JoueurPartie> joueurs)
        {
            foreach (JoueurPartie joueur in joueurs)
            {
                IList<De> liste = probabiliteDes.OrderBy(c => Guid.NewGuid()).ToList();
                Random rand = new Random();

                for (int i = 0; i < nbDesJoueur; i++)
                {
                    De nouvelleValeur = liste.ElementAt(rand.Next(probabiliteDes.Count));

                    De de = new De();
                    de.Valeur = nouvelleValeur.Valeur;
                    de.ImageRef = nouvelleValeur.ImageRef;

                    joueur.Des.Add(de);
                    context.SaveChanges();
                }
            }
        }

        public void MelangerDe(JoueurPartie joueur)
        {
            IList<De> liste = probabiliteDes.OrderBy(c => Guid.NewGuid()).ToList();

            Random rand = new Random();

            foreach (De de in joueur.Des)
            {
                double random = rand.NextDouble();
                int count = probabiliteDes.Count;
                int random2 = (int) Math.Floor(random*count);
                De nouvelleValeur = liste.ElementAt(random2);

                de.Valeur = nouvelleValeur.Valeur;
                de.ImageRef = nouvelleValeur.ImageRef;
            }
        }
    }
}
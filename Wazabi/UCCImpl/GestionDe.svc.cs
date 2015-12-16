using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Wazabi.Model;

namespace Wazabi
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] // 1 seule instance est renvoyé
    public class GestionDe : IGestionDe
    {
        public readonly int NB_DES_JOUEUR;
        public readonly int NB_DES_TOTAL;

        private readonly WazabiEntities context = new WazabiEntities();

        private IList<De> faces;

        public GestionDe(int nbDeJoueur, int nbTotal, IList<De> faces)
        {
            this.NB_DES_JOUEUR = nbDeJoueur;
            this.NB_DES_TOTAL = nbTotal;
            this.faces = new List<De>(faces);
        }

        public IList<De> GenererDes()
        {
            List<De> desDB = faces.OrderBy(c => new Random().Next()).ToList();

            IList<De> des = new List<De>();
            for (int i = 0; i < NB_DES_JOUEUR; i++)
            {
                des.Add(desDB.ElementAt(new Random().Next(NB_DES_TOTAL)));
            }

            return des;
        }

        public IList<De> MelangerDe(int nbrDes)
        {
            List<De> desDB = faces.OrderBy(c => new Random().Next()).ToList();

            IList<De> des = new List<De>();
            for (int i = 0; i < nbrDes; i++)
            {
                des.Add(desDB.ElementAt(new Random().Next(nbrDes)));
            }

            return des;
        }
    }
}
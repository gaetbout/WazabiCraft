using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Wazabi.Model;

namespace Wazabi
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] // 1 seule instance est renvoyé
    public class GestionDe : IGestionDe
    {

        public readonly int NB_DES_JOUEUR;
        public readonly int NB_DES_TOTAL;

        private IList<De> faces;

        public GestionDe(int nbDeJoueur, int nbTotal, IList<De> faces)
        {
            this.NB_DES_JOUEUR = nbDeJoueur;
            this.NB_DES_TOTAL = nbTotal;
            this.faces = new List<De>(faces);
        }
    }
}

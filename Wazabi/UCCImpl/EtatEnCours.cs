using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wazabi.Model;

namespace Wazabi.UCCImpl
{
    public class EtatEnCours : EtatImpl
    {

        public EtatEnCours(WazabiEntities cont, Partie partie)
            : base(cont, partie)
        {
        }

        public override bool LancerPartie()
        {
            return false;
        }

        public override bool RejoindrePartie(JoueurClient joueur)
        {
            return false;
        }
    }
}
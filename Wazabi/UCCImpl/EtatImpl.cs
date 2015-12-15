using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wazabi.Model;

namespace Wazabi.UCCImpl
{
    public abstract class EtatImpl
    {
        internal protected readonly WazabiEntities context;
        internal protected readonly Partie partie;

        public EtatImpl(WazabiEntities cont, Partie partie)
        {
            this.context = cont;
            this.partie = partie;
        }

        public abstract bool LancerPartie();
        public abstract bool RejoindrePartie(JoueurClient joueur);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Wazabi.Model;

namespace Wazabi.Client
{
    public class JoueurPartieClient
    {
        public JoueurPartieClient()
        {
            
        }

        public JoueurPartieClient(JoueurPartie joueurPartie)
        {
            Id = joueurPartie.Id;
            Pseudo = joueurPartie.Joueur.Pseudo;
            MesCartes = new List<CarteClient>();
            foreach (Carte carte in joueurPartie.Cartes)
            {
                MesCartes.Add(new CarteClient(carte));
            }
            MesDes = new List<DeClient>();
            foreach (De de in joueurPartie.Des)
            {
                MesDes.Add(new DeClient(de));
            }
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Pseudo { get; set; }

        [DataMember]
        public List<CarteClient> MesCartes { get; set; }

        [DataMember]
        public List<DeClient> MesDes { get; set; }
    }
}
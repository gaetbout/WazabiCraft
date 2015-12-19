using System.Collections.Generic;
using System.ServiceModel;
using Wazabi.Client;

namespace Wazabi.UCC
{
    [ServiceContract]
    public interface IGestionPartie
    {
        [OperationContract]
        bool CreerPartie(JoueurClient joueur, string nom);

        [OperationContract]
        bool LancerPartie();

        [OperationContract]
        void Init();

        [OperationContract]
        bool RejoindrePartie(JoueurClient joueur);

        [OperationContract]
        PartieClient PartieCourante();

        [OperationContract]
        void QuitterPartie(JoueurClient joueur);

        [OperationContract]
        ICollection<PartieClient> GetParties();

        [OperationContract]
        void TourSuivant();

        [OperationContract]
        ICollection<JoueurPartieClient> GetJoueurPartie(int idPartie);

        [OperationContract]
        void ClearBD();

        [OperationContract]
        bool ActionDes(Dictionary<string, string> dictionary);

        [OperationContract]
        bool ActionCartes(string codeEffet, string idJoueur, string value);
    }
}
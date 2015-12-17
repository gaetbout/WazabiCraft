using System.Collections.Generic;
using System.ServiceModel;
using Wazabi.Client;

namespace Wazabi.UCC
{
    [ServiceContract]
    public interface IGestionJoueur
    {
        [OperationContract]
        bool Inscription(JoueurClient joueur);

        [OperationContract]
        JoueurClient Connexion(JoueurClient joueur);

        [OperationContract]
        ICollection<JoueurClient> GetJoueurs();
    }
}
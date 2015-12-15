using System.ServiceModel;

namespace Wazabi.UCC
{
    [ServiceContract]
    public interface IGestionPartie
    {
        [OperationContract]
        void CreerPartie(JoueurClient joueur, string nom);

        [OperationContract]
        bool LancerPartie();

        [OperationContract]
        void init();
    }
}
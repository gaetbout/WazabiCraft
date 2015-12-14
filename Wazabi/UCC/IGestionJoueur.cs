using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Wazabi.UCC
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IGestionJoueur" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IGestionJoueur
    {
        [OperationContract]
        void Inscription(JoueurClient joueur);

        [OperationContract]
        JoueurClient Connexion(JoueurClient joueur);
    }
}

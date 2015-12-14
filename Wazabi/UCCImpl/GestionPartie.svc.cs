using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Wazabi
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] // 1 seule instance est renvoyé
    public class GestionPartie : IGestionPartie
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
    }
}

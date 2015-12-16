using System.Collections.Generic;
using System.ServiceModel;
using Wazabi.Model;

namespace Wazabi.UCC
{
    [ServiceContract]
    public interface IGestionCarte
    {
        [OperationContract]
        List<Carte> GenererPioche();
    }
}
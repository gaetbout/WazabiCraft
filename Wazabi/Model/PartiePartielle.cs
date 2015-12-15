using System.ComponentModel.DataAnnotations;

namespace Wazabi.Model
{
    public partial class Partie
    {
        public enum State
        {
            CREATION = 0,
            EN_COURS = 1,
            FINIE = 2
        }

        [EnumDataType(typeof (State))]
        public State EtatType
        {
            get { return (State) this.Etat; }
            set { this.Etat = (int) value; }
        }
    }
}
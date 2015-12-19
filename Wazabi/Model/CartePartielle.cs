using Wazabi.Logique;

namespace Wazabi.Model
{
    public partial class Carte
    {
        private StrategyCarte strategy;

        public Carte()
        {
        }

        public Carte(StrategyCarte strategy)
        {
            this.strategy = strategy;
        }

        public bool executeStrategy(Partie p, JoueurPartie jp, int nbe)
        {
            return strategy.faireOperation(p, jp, nbe);
        }
    }
}
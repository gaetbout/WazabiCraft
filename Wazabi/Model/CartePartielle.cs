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

        public bool executeStrategy()
        {
            //TODO 
            return false;
            //return strategy.faireOperation(null,null,0);
        }
    }
}
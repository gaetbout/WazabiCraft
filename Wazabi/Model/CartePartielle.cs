using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wazabi.Logique;

namespace Wazabi.Model
{
    public partial class Carte
    {
        private StrategyCarte strategy;

        public Carte(StrategyCarte strategy)
        {
            this.strategy = strategy;
        }

        public bool executeStrategy(){
            //TODO 
            return strategy.faireOperation(null,null,0);
        }

    }
}
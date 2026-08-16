using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Pool
{
    public class ZZZPotionPool : PotionPoolModel
    {
        public override string EnergyColorName =>"colorless";

        protected override IEnumerable<PotionModel> GenerateAllPotions()
        {
            return [];
        }
    }
}

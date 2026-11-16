using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Powers
{
    /// <summary>
    /// 折枝剑歌 1 对debuff层数大于3（2）的敌人额外造成35%伤害。
    /// </summary>
    public class BranchBladeSongP : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DynamicVar("damageUp", 35)];

        public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource, CardPlay? cardPlay)
        {
            if (target != null && target.Powers.Sum(a =>(
                (a.Type == PowerType.Debuff && a.StackType == PowerStackType.Counter)
                ? a.Amount : 0)) > Amount && dealer==Owner)
            {
                return 1 + (DynamicVars["damageUp"].BaseValue / 100m);
            }
            return 1;
        }
    }
}

using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Model;
using ZZZLib.Powers;

namespace ZZZLib.Cards.Equips.Other
{
    /// <summary>
    /// 霜染寒星 1 未升级（升级过）的攻击牌额外造成3（4）伤害
    /// </summary>
    public class FrostStainedStar : ZZZEquipCardModel
    {
        public FrostStainedStar() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DynamicVar("damageUp", 3)];

        protected override void OnUpgrade()
        {
            DynamicVars["damageUp"].UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if(!IsUpgraded)
            await PowerCmd.Apply<FrostStainedStarA>(choiceContext,
                Owner.Creature, DynamicVars["damageUp"].BaseValue,
                Owner.Creature, this);
            else 
                await PowerCmd.Apply<FrostStainedStarB>(choiceContext,
                Owner.Creature, DynamicVars["damageUp"].BaseValue,
                Owner.Creature, this);
        }
    }
}

using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Model;
using ZZZLib.Powers;

namespace ZZZLib.Cards.Partner
{
    /// <summary>
    /// 白祇重工 1 【伙伴】获得9（12）格挡，受到攻击时，给予攻击者1层【脆弱】
    /// </summary>
    public class BelobogHeavyIndustries : ZZZPartnerCardModel
    {
        public BelobogHeavyIndustries() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(9, ValueProp.Move), new PowerVar<FrailPower>(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<FrailPower>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(3);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
            await PowerCmd.Apply<BelobogHeavyIndustriesP>(choiceContext, Owner.Creature,
                1, Owner.Creature, this);
        }
    }
}

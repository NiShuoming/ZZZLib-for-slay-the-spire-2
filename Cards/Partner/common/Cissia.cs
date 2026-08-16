using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Model;

namespace ZZZLib.Cards.Partner
{
    /// <summary>
    /// 希希芙 2 【伙伴】给予8（11）【中毒】
    /// </summary>
    public class Cissia : ZZZPartnerCardModel
    {
        public Cissia() : base(2, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new PowerVar<PoisonPower>(8)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromPower<PoisonPower>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Power<PoisonPower>().UpgradeValueBy(3);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await PowerCmd.Apply<PoisonPower>(choiceContext, cardPlay.Target,
                DynamicVars.Power<PoisonPower>().BaseValue, Owner.Creature, this);
        }
    }
}

using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
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
    /// 柏妮思 1 【伙伴】给予2（3）次2灼烧
    /// </summary>
    public class Burnice : ZZZPartnerCardModel
    {
        public Burnice() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new PowerVar<BurningAnomalyPower>(2), new RepeatVar(2)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromPower<BurningAnomalyPower>(),
            HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly)];

        protected override void OnUpgrade()
        {
            DynamicVars.Repeat.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            for (int i = 0; i < DynamicVars.Repeat.IntValue; i++)
            {
                await PowerCmd.Apply<BurningAnomalyPower>(
                    choiceContext, cardPlay.Target, DynamicVars.Power<BurningAnomalyPower>().BaseValue,
                    Owner.Creature, this);
            }
        }
    }
}

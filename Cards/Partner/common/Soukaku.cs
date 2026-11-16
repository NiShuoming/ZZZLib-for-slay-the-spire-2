using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Model;
using ZZZLib.Other;
using ZZZLib.Powers;

namespace ZZZLib.Cards.Partner
{
    /// <summary>
    /// 苍角 （1）技 【伙伴】获得4（6）临时【力量】，抽1，给予2【脆弱】
    /// </summary>
    public class Soukaku : ZZZPartnerCardModel
    {
        public Soukaku():base(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<StrengthPower>(4), new CardsVar(1), new PowerVar<FrailPower>(2)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<StrengthPower>(), HoverTipFactory.FromPower<FrailPower>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Power<StrengthPower>().UpgradeValueBy(2);
            DynamicVars.Cards.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            ZZZVoiceManager.PlayVoice("苍角", volumeAdd: 6);
            ZZZVoiceManager.PlayVoice("苍角", volumeAdd: 4, language: true);

            await PowerCmd.Apply<SoukakuP>(choiceContext, Owner.Creature,
                DynamicVars.Power<StrengthPower>().BaseValue, Owner.Creature, this);
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
            await PowerCmd.Apply<FrailPower>(choiceContext, cardPlay.Target,
                DynamicVars.Power<FrailPower>().BaseValue, Owner.Creature, this);

            
        }
    }
}

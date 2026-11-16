using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cmds;
using ZZZLib.Model;
using ZZZLib.Other;

namespace ZZZLib.Cards.Partner
{
    /// <summary>
    /// 佩洛伊斯 1 【伙伴】【架势】抽2，1：获得8活力  2：造成11伤害
    /// </summary>
    public class Pyrois : ZZZPartnerCardModel
    {
        public Pyrois() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
            [ZZZKeyWord.Partner, ZZZKeyWord.Stance];

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new CardsVar(2), new PowerVar<VigorPower>(8),
            new DamageVar(11, MegaCrit.Sts2.Core.ValueProps.ValueProp.Move)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromPower<VigorPower>()];

        public override TargetType TargetType
        {
            get { if (IsUpgraded) return TargetType.Self;
                else return TargetType.AnyEnemy;
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
            if (IsUpgraded)
            {
                ZZZVoiceManager.PlayVoice("佩洛伊斯技",volumeAdd:-2);

                await PowerCmd.Apply<VigorPower>(choiceContext, Owner.Creature,
                    DynamicVars.Power<VigorPower>().BaseValue, Owner.Creature, this);
            }
            else
            {
                ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
                ZZZVoiceManager.PlayVoice("佩洛伊斯语音", language: true);
                await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
                    .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                    .WithHitFx("vfx/vfx_attack_slash")
                    .Execute(choiceContext);
            }
        }
    }
}

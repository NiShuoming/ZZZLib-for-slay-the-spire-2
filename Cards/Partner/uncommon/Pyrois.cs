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

namespace ZZZLib.Cards.Partner
{
    /// <summary>
    /// 佩洛伊斯 1 【伙伴】【架势】回复2第二能量，1：获得4（6）活力  2：造成7（10）伤害
    /// </summary>
    public class Pyrois : ZZZPartnerCardModel
    {
        public Pyrois() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
            [ZZZKeyWord.Partner, ZZZKeyWord.Stance];

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new DynamicVar("SecondEnergy",2), new PowerVar<VigorPower>(4),
            new DamageVar(8, MegaCrit.Sts2.Core.ValueProps.ValueProp.Move)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromKeyword(ZZZKeyWord.SecondEnergy),
            HoverTipFactory.FromPower<VigorPower>()];

        bool stance = true;

        protected override bool ShouldGlowGoldInternal => stance;

        protected override bool ShouldGlowRedInternal => !stance;

        protected override void OnUpgrade()
        {
            DynamicVars.Power<VigorPower>().UpgradeValueBy(2);
            DynamicVars.Damage.UpgradeValueBy(3);
        }

        public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            stance = !stance;
            return Task.CompletedTask;
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await ZZZCmd.AddSecondEnergy(Owner, 2, Owner.Creature, this);
            if (stance)
            {
                await PowerCmd.Apply<VigorPower>(choiceContext, Owner.Creature,
                    DynamicVars.Power<VigorPower>().BaseValue, Owner.Creature, this);
            }
            else
            {
                ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
                await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
                    .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                    .WithHitFx("vfx/vfx_attack_slash")
                    .Execute(choiceContext);

            }
        }
    }
}

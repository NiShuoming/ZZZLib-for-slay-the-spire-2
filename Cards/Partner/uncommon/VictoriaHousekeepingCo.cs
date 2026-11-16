using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Model;

namespace ZZZLib.Cards.Partner
{
    /// <summary>
    /// 维多利亚家政 1 【伙伴】造成8伤害，每有1张【任务】牌，额外造成3（5）伤害
    /// </summary>
    public class VictoriaHousekeepingCo : ZZZPartnerCardModel
    {
        public VictoriaHousekeepingCo() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new CalculationBaseVar(8), new ExtraDamageVar(3),
            new CalculatedDamageVar(ValueProp.Move).WithMultiplier(
                (card, _)=>{ if(card.Owner!=null && card.Owner.PlayerCombatState!=null)
                return card.Owner.PlayerCombatState.AllCards.
                Count(a=>a.Type.Equals(CardType.Quest));
                return 0;})];

        protected override void OnUpgrade()
        {
            DynamicVars.ExtraDamage.UpgradeValueBy(2);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(base.DynamicVars.CalculatedDamage)
                .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
        }
    }
}

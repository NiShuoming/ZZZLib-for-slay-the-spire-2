using MegaCrit.Sts2.Core.Combat;
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
    /// 仪玄 对所有敌人造成伤害7，3-4次，每有1伙伴，伤害+1
    /// </summary>
    public class Yixuan : ZZZPartnerCardModel
    {
        public Yixuan() : base(3, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
        {
        }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new CalculationBaseVar(7m), new RepeatVar(3),
            new ExtraDamageVar(1m), new CalculatedDamageVar(ValueProp.Move).
            WithMultiplier((card, _) => card.Owner.PlayerCombatState.AllCards.
            Count(c => c.Keywords.Contains(ZZZKeyWord.Partner) && c != card))];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await DamageCmd.Attack(base.DynamicVars.CalculatedDamage)
                .WithHitCount(DynamicVars.Repeat.IntValue)
                .FromCard(this,cardPlay).TargetingAllOpponents(CombatState)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);

        }

        protected override void OnUpgrade()
        {
            DynamicVars.Repeat.UpgradeValueBy(1m);
        }
    }
}

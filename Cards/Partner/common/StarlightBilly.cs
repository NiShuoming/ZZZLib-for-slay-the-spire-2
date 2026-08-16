using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
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
    /// 星徽比利 1 【伙伴】消耗1生命，造成8（11）伤害，如果意图为攻击，获得6（8）格挡
    /// </summary>
    public class StarlightBilly : ZZZPartnerCardModel
    {
        public StarlightBilly() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
        { }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new HpLossVar(1), new DamageVar(8, ValueProp.Move),
            new BlockVar(6, ValueProp.Move)];

        protected override bool ShouldGlowGoldInternal
        {
            get
            {
                if (base.CombatState == null)
                {
                    return false;
                }
                return base.CombatState.HittableEnemies.Any((Creature e) => e.Monster?.IntendsToAttack ?? false);
            }
        }
        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(3);
            DynamicVars.Block.UpgradeValueBy(2);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await CreatureCmd.Damage(choiceContext, Owner.Creature,
                DynamicVars.HpLoss.BaseValue, ValueProp.Move, Owner.Creature, this,cardPlay);
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);

            if (cardPlay.Target.Monster.IntendsToAttack)
            {
                await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block,
                    cardPlay);
            }
        }
    }
}

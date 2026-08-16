using BaseLib.Extensions;
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
    /// 露西 1 【伙伴】造成7（10）群伤，两个回合获得3力量
    /// </summary>
    public class Lucy : ZZZPartnerCardModel
    {
        public Lucy() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies) { }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new PowerVar<CheerOn>(2), new DamageVar(7, ValueProp.Move)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromPower<StrengthPower>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(3);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .FromCard(this,cardPlay).TargetingAllOpponents(CombatState)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);

            await PowerCmd.Apply<CheerOn>(choiceContext, Owner.Creature,
                DynamicVars.Power<CheerOn>().BaseValue, Owner.Creature, this);
        }
    }
}

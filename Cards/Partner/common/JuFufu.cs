using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.GameInfo.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Model;

namespace ZZZLib.Cards.Partner
{
    /// <summary>
    /// 橘福福 1 【伙伴】造成7->9点伤害，施加1=2层脆弱.对方有格挡，额外4-6伤 完成
    /// </summary>
    public class JuFufu:ZZZPartnerCardModel
    {
        public JuFufu() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new CalculationBaseVar(7m),new ExtraDamageVar(4m),
            new CalculatedDamageVar(ValueProp.Move).
            WithMultiplier((card, target) =>
            {
                if(target==null)return 0;
                else if(target.Block>0)return 1;
                return 0;
            } ),
            new PowerVar<FrailPower>(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromPower<FrailPower>(),
            HoverTipFactory.Static(StaticHoverTip.Block)];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(base.DynamicVars.CalculatedDamage)
                .FromCard(this,cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);

            await PowerCmd.Apply<FrailPower>(choiceContext, cardPlay.Target, DynamicVars.Power<FrailPower>().IntValue,
                Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.CalculationBase.UpgradeValueBy(2m);
            DynamicVars.ExtraDamage.UpgradeValueBy(2m);
            DynamicVars.Power<FrailPower>().UpgradeValueBy(1);
        }
    }
}

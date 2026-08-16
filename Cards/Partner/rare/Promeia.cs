using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
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
    /// 普罗米娅 2 【伙伴】造成12（18）【属性异常】伤害，触发【异放】
    /// </summary>
    public class Promeia : ZZZPartnerCardModel
    {
        public Promeia() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new DamageVar(12, ValueProp.Move | ZZZValueProp.Anomaly)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Abloom),
            HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(6);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .WithValueProp(ValueProp.Move | ZZZValueProp.Anomaly)
                .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);

            await ZZZCmd.Abloom(choiceContext, cardPlay.Target, Owner.Creature);
        }
    }
}

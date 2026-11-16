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

namespace ZZZLib.Cards.Partner
{
    /// <summary>
    /// 狡兔屋 （1）攻 【伙伴】造成4（6）伤害2次，给予1【易伤】
    /// </summary>
    public class CunningHares: ZZZPartnerCardModel
    {
        public CunningHares() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(4,ValueProp.Move), new RepeatVar(2),new PowerVar<VulnerablePower>(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<VulnerablePower>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
                .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash").WithHitCount(DynamicVars.Repeat.IntValue)
                .Execute(choiceContext);
            await PowerCmd.Apply<VulnerablePower>(choiceContext, cardPlay.Target,
                DynamicVars.Power<VulnerablePower>().BaseValue, Owner.Creature, this);
        }
    }
}

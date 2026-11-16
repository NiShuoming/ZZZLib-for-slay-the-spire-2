using BaseLib.Cards.Variables;
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
    /// 刑侦特勤组 1 【伙伴】造成8=10伤害，将目标格挡转化为额外伤害。将目标【人工】转化为攻击次数。
    /// </summary>
    public class Cisrt: ZZZPartnerCardModel
    {
        public Cisrt() : base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }        

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CalculationBaseVar(8), new ExtraDamageVar(1),
            new CalculatedDamageVar(ValueProp.Move).WithMultiplier(
                (_, target)=>{
                    if(target==null)return 0;
                    return target.Block;
                })];

        protected override IEnumerable<IHoverTip> ExtraHoverTips
            { get { if (IsUpgraded) return [HoverTipFactory.Static(StaticHoverTip.Block),
                        HoverTipFactory.FromPower<ArtifactPower>()];
                return [HoverTipFactory.Static(StaticHoverTip.Block)];
            } }

        protected override void OnUpgrade()
        {
            DynamicVars.CalculationBase.UpgradeValueBy(5);
            AddKeyword(CardKeyword.Retain);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");

            int repeat = 1;
            repeat += cardPlay.Target.GetPowerAmount<ArtifactPower>();
            await PowerCmd.Remove<ArtifactPower>(cardPlay.Target);

            int block = cardPlay.Target.Block;

            await CreatureCmd.LoseBlock(choiceContext, cardPlay.Target, block, Owner.Creature);

            await DamageCmd.Attack(base.DynamicVars.CalculationBase.BaseValue + block)
                .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash").WithHitCount(repeat)
                .Execute(choiceContext);
        }

    }
}

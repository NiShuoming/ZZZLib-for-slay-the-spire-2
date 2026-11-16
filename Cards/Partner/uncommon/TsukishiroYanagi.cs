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
using ZZZLib.Cmds;
using ZZZLib.Model;
using ZZZLib.Other;

namespace ZZZLib.Cards.Partner
{
    /// <summary>
    /// 月城柳 2 【伙伴】【架势】造成5（9）异常伤害，触发一次【紊乱】。奇：给予2易伤。偶：给予2虚弱。
    /// </summary>
    public class TsukishiroYanagi:ZZZPartnerCardModel
    {
        public TsukishiroYanagi() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [ZZZKeyWord.Partner, ZZZKeyWord.Stance];

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(5, ValueProp.Move | ZZZValueProp.Anomaly),
            new DynamicVar("Disorder", 1), new PowerVar<VulnerablePower>(2),
            new PowerVar<WeakPower>(2)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get
            {
                if(IsUpgraded) return [
                    HoverTipFactory.FromPower<WeakPower>(),
                    HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly),
                    HoverTipFactory.FromKeyword(ZZZKeyWord.Disorder)];
                return [HoverTipFactory.FromPower<VulnerablePower>(),
                    HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly),
                    HoverTipFactory.FromKeyword(ZZZKeyWord.Disorder)];
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(4);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            ZZZVoiceManager.PlayVoice("月城柳", volumeAdd: 0);
            ZZZVoiceManager.PlayVoice("月城柳", volumeAdd: 2, language: true);

            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
                .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .WithValueProp(ValueProp.Move|ZZZValueProp.Anomaly)
                .Execute(choiceContext);

            await ZZZCmd.Disorder(choiceContext, cardPlay.Target, Owner.Creature);

            if (IsUpgraded)
            {
                await PowerCmd.Apply<WeakPower>(choiceContext, cardPlay.Target,
                    DynamicVars.Power<WeakPower>().BaseValue, Owner.Creature, this);
            }
            else
            {
                await PowerCmd.Apply<VulnerablePower>(choiceContext, cardPlay.Target,
                    DynamicVars.Power<VulnerablePower>().BaseValue, Owner.Creature, this);

            }
        }
    }
}

using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZZZLib.Model;

namespace ZZZLib.Cards.Partner
{
    /// <summary>
    /// 浅羽悠真 2 【伙伴】【架势】未升级：造成16伤害。升级：造成10（+2）异常伤害2次。 每次到休息处时升级
    /// </summary>
    public class AsabaHarumasa:ZZZPartnerCardModel
    {
        public AsabaHarumasa() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [ZZZKeyWord.Partner, ZZZKeyWord.Stance, ZZZKeyWord.OverLimit];

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(16, ValueProp.Move), 
            new DamageVar("damage2", 8, ValueProp.Move|ZZZValueProp.Anomaly),
            new RepeatVar(2)];

        protected override void OnUpgrade()
        {
            DynamicVars["damage2"].UpgradeValueBy(2);
        }

        public override int MaxUpgradeLevel => 99;

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            
            if(IsUpgraded)
            await DamageCmd.Attack(base.DynamicVars["damage2"].BaseValue)
                .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash").WithHitCount(DynamicVars.Repeat.IntValue)
                .WithValueProp(ValueProp.Move|ZZZValueProp.Anomaly).Execute(choiceContext);
            else await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
                .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
        }

        public override Task AfterRoomEntered(AbstractRoom room)
        {
            if (room is RestSiteRoom) CardCmd.Upgrade(this);
            return base.AfterRoomEntered(room);
        }
    }
}

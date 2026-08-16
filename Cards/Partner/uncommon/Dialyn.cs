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
    /// 琉音 2 【伙伴】对所有敌人造成10=12点伤害，2=3脆弱，获得终结技最后的效果
    /// </summary>
    public class Dialyn : ZZZPartnerCardModel
    {
        public Dialyn() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies) { }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new DamageVar(10m,ValueProp.Move), new PowerVar<FrailPower>(2m)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [ZZZKeyWord.Partner, ZZZKeyWord.Ultimate];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromPower<FrailPower>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
                .FromCard(this,cardPlay).TargetingAllOpponents(CombatState)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);

            await PowerCmd.Apply<FrailPower>(choiceContext, CombatState.Enemies,
                DynamicVars.Power<FrailPower>().BaseValue,
                Owner.Creature, this);

            if(Owner.Character is IZZZCharacter)
            {
                await ((IZZZCharacter)(Owner.Character)).PlayUltimate(choiceContext, cardPlay);
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2m);
            DynamicVars.Power<FrailPower>().UpgradeValueBy(1m);
        }
    }
}

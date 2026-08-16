using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
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
    /// 般岳 2 伙伴 派生 1：造成8=10伤，8=10格挡。2:9=12伤2次 3:15=19群伤 2易伤 完成
    /// </summary>
    public class Banyue : ZZZPartnerCardModel
    {
        public Banyue() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        public override bool GainsBlock => true;

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new DamageVar(8m,ValueProp.Move), new BlockVar(8m, ValueProp.Move),
            new DynamicVar("Damage2", 9m), new DynamicVar("Damage3", 15m)];

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
            [ZZZKeyWord.Partner, ZZZKeyWord.Derive];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromPower<VulnerablePower>()];

        public int DrawCard
        {
            get
            {
                return CombatManager.Instance.History.CardPlaysFinished.Count(
                    (e) => e.HappenedThisTurn(CombatState)
                    && e.CardPlay.Card.Owner == Owner);
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            switch (DrawCard)
            {
                case 0:
                    ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
                    await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                        .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                        .WithHitFx("vfx/vfx_attack_slash")
                        .Execute(choiceContext);
                    await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
                    break;
                case 1:
                    ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
                    await DamageCmd.Attack(DynamicVars["Damage2"].BaseValue)
                        .WithHitCount(2)
                        .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                        .WithHitFx("vfx/vfx_attack_slash")
                        .Execute(choiceContext);
                    break;
                default:
                    await DamageCmd.Attack(DynamicVars["Damage3"].BaseValue)
                     .FromCard(this,cardPlay).TargetingAllOpponents(CombatState)
                     .WithHitFx("vfx/vfx_attack_slash")
                     .Execute(choiceContext);
                    await PowerCmd.Apply<VulnerablePower>(choiceContext, CombatState.Enemies, 2, Owner.Creature, this);
                    break;
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2m);
            DynamicVars.Block.UpgradeValueBy(2m);
            DynamicVars["Damage2"].UpgradeValueBy(3m);
            DynamicVars["Damage3"].UpgradeValueBy(4m);
        }
    }

}

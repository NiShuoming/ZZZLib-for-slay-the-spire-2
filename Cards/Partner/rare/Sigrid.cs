using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
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
    /// 希格莉德 2 【伙伴】造成7（9）伤害，每次抽到手牌中，多造成1次伤害。
    /// </summary>
    public class Sigrid : ZZZPartnerCardModel
    {
        public Sigrid() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new DamageVar(8, ValueProp.Move), new RepeatVar(1)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2);
        }

        private int _extraRepeat;

        private int ExtraRepeat
        {
            get { return _extraRepeat; }
            set
            {
                AssertMutable();
                _extraRepeat = value;
            }
        }

        public override Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel? clonedBy)
        {
            if (card == this && card.Pile != null && card.Pile.Type == PileType.Hand && oldPileType != PileType.Hand)
            {
                DynamicVars.Repeat.BaseValue += 1;
                ExtraRepeat += 1;
            }
            return Task.CompletedTask;

        }

        protected override void AfterDowngraded()
        {
            base.AfterDowngraded();
            DynamicVars.Repeat.BaseValue += ExtraRepeat;
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
                .FromCard(this, cardPlay).Targeting(cardPlay.Target).WithHitCount(DynamicVars.Repeat.IntValue)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);

        }
    }
}

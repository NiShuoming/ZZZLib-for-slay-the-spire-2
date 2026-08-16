using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Model;

namespace ZZZLib.Cards.Partner
{
    /// <summary>
    /// 云岿山 3=2【消耗】将一张【仪玄】、一张【潘引壶】、一张【橘福福】、一张【叶释渊】加入手牌
    /// </summary>
    public class YunkuiSummit : ZZZPartnerCardModel
    {
        public YunkuiSummit() : base(3, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
            [CardKeyword.Exhaust];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromCard<Yixuan>(), HoverTipFactory.FromCard<PanYinhu>(),
            HoverTipFactory.FromCard<JuFufu>(), HoverTipFactory.FromCard<YeShiyuan>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            List<CardModel> cards = new List<CardModel>();
            cards.Add(CombatState.CreateCard<Yixuan>(Owner));
            cards.Add(CombatState.CreateCard<PanYinhu>(Owner));
            cards.Add(CombatState.CreateCard<JuFufu>(Owner));
            cards.Add(CombatState.CreateCard<YeShiyuan>(Owner));
            await CardPileCmd.Add(cards, PileType.Hand);
        }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }
    }
}

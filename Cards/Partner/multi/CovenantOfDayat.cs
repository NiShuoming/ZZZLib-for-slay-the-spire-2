using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Model;

namespace ZZZLib.Cards.Partner
{
    /// <summary>
    /// 技（1）达识结社 【消耗】=》【保留】【伙伴】消耗掉一名队友的诅咒牌和状态牌
    /// </summary>
    public class CovenantOfDayat: ZZZPartnerCardModel
    {
        public CovenantOfDayat():base(1, CardType.Skill, CardRarity.Rare, TargetType.AnyPlayer) { }

        public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust, ZZZKeyWord.Partner];

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Retain);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target!=null && cardPlay.Target.IsPlayer)
            {
                var cards = cardPlay.Target.Player.PlayerCombatState.AllCards.ToList();
                foreach (CardModel card in cards)
                {
                    if (card.Type == CardType.Status || card.Type == CardType.Curse)
                        await CardCmd.Exhaust(choiceContext, card);
                }
            }
        }
    }
}

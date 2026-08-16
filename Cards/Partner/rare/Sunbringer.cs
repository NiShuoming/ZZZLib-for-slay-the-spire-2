using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
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
    /// 挽昼 【伙伴】【消耗】复制1张牌的关键字=》和附魔，给予另一张牌。
    /// </summary>
    public class Sunbringer : ZZZPartnerCardModel
    {
        public Sunbringer() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
            [ZZZKeyWord.Partner, CardKeyword.Exhaust];

        protected LocString SelectionScreenPrompt2
        {
            get
            {
                LocString locString = new LocString("cards", base.Id.Entry + ".selectionScreenPrompt2");
                if (!locString.Exists())
                {
                    throw new InvalidOperationException($"No selection screen prompt for {base.Id}.");
                }

                DynamicVars.AddTo(locString);
                return locString;
            }
        }
        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner.PlayerCombatState.Hand.Cards.Count < 2) return;
            List<CardModel> cardModels = (await CardSelectCmd.FromHand(
                prefs: new CardSelectorPrefs(base.SelectionScreenPrompt, 2),
                context: choiceContext, player: base.Owner,
                filter: null,
                source: this)).ToList();

            foreach (var item in cardModels[0].CanonicalKeywords)
            {
                cardModels[1].AddKeyword(item);
            }
            if (IsUpgraded && cardModels[0].Enchantment != null && cardModels[1].Enchantment == null)
            {
                cardModels[1].EnchantInternal(cardModels[0].Enchantment.ToMutable(), cardModels[0].Enchantment.Amount); ;
            }
        }
    }
}

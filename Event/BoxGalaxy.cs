using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.DevConsole.ConsoleCommands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;
using MegaCrit.Sts2.Core.Runs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Other;
using ZZZLib.Pool;

namespace ZZZLib.Event
{
    /// <summary>
    /// 盒：1.购买音擎:-100金币，从三张武器中选择一张 2.回收：删1张初始牌以外的牌，+50金币
    /// </summary>
    public class BoxGalaxy : CustomEventModel
    {
        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new GoldVar(100), new CardsVar(3), new DynamicVar("Recycle",50)];
        public override bool IsAllowed(IRunState runState)
        {
            if (!ZZZConfig.OpenEvent) return false;

            //如果至少有100金币，且有1张初始牌以外的牌
            if (runState.Players.All(p=>(p.Gold>=100 
            && p.Deck.Cards.Any(c=>c.Rarity!=CardRarity.Basic))))
                return true;
            return false;
        }

        protected override IReadOnlyList<EventOption> GenerateInitialOptions()
        {
            return [new EventOption(this, BuyEngine, InitialOptionKey("BUY_ENGINE")),
                    new EventOption(this, Recycle, InitialOptionKey("RECYCLE"))];

        }

        private async Task BuyEngine()
        {
            await PlayerCmd.LoseGold(DynamicVars.Gold.BaseValue, Owner);
            CardCreationOptions options = CardCreationOptions.ForNonCombatWithUniformOdds(
                [ModelDb.CardPool<ZZZEquipPool>()],
                card => (card.Tags.Contains(ZZZTag.W_Engine)))
                .WithFlags(CardCreationFlags.NoRarityModification | CardCreationFlags.NoCardPoolModifications);
            List<CardCreationResult> cards = CardFactory.CreateForReward(Owner, 3, options).ToList();
            foreach (CardModel item in await CardSelectCmd.FromSimpleGridForRewards(
                prefs: new CardSelectorPrefs(L10NLookup("ZZZLIB-BOX_GALAXY.pages.BUY_ENGINE.selectionScreenPrompt"), 1)
                , context: new BlockingPlayerChoiceContext(), cards: cards, player: Owner))
            {
                CardCmd.PreviewCardPileAdd(await CardPileCmd.Add(item, PileType.Deck));
            }
            SetEventFinished(L10NLookup("ZZZLIB-BOX_GALAXY.pages.BUY_ENGINE.description"));
        }

        private async Task Recycle()
        {
            await PlayerCmd.GainGold(DynamicVars["Recycle"].BaseValue, Owner);
            List<CardModel> cards = (await CardSelectCmd.FromDeckForRemoval(
                prefs: new CardSelectorPrefs(CardSelectorPrefs.RemoveSelectionPrompt, 1)
                , player: base.Owner, filter: (CardModel c) 
                => (c.Rarity!=CardRarity.Basic))).ToList();
            await CardPileCmd.RemoveFromDeck(cards);
            SetEventFinished(L10NLookup("ZZZLIB-BOX_GALAXY.pages.RECYCLE.description"));

        }
    }
}

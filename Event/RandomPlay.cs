using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
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
    /// RandomPlay 1:租碟片，碟片三选一 2.偶遇事件：伙伴牌三选一
    /// </summary>
    public class RandomPlay : CustomEventModel
    {
        protected override IReadOnlyList<EventOption> GenerateInitialOptions()
        {
            return [new EventOption(this, BuyDriveDisc, InitialOptionKey("BUY_DRIVE_DISC")),
                    new EventOption(this, PartnerEncounter, InitialOptionKey("PARTNER_ENCOUNTER"))];
        }
        public override bool IsAllowed(IRunState runState)
        {
            if (!ZZZConfig.OpenEvent) return false;
            return true;
        }

        private async Task BuyDriveDisc()
        {
            CardCreationOptions options = CardCreationOptions.ForNonCombatWithUniformOdds(
                [ModelDb.CardPool<ZZZEquipPool>()], a=>a.Tags.Contains(ZZZTag.DriveDiscs))
                .WithFlags(CardCreationFlags.NoRarityModification | CardCreationFlags.NoCardPoolModifications);
            List<CardCreationResult> cards = CardFactory.CreateForReward(Owner, 3, options).ToList();
            foreach (CardModel item in await CardSelectCmd.FromSimpleGridForRewards(
                prefs: new CardSelectorPrefs(L10NLookup("ZZZLIB-RANDOM_PLAY.pages.BUY_DRIVE_DISC.selectionScreenPrompt"), 1)
                , context: new BlockingPlayerChoiceContext(), cards: cards, player: Owner))
            {
                CardCmd.PreviewCardPileAdd(await CardPileCmd.Add(item, PileType.Deck));
            }
            SetEventFinished(L10NLookup("ZZZLIB-RANDOM_PLAY.pages.BUY_DRIVE_DISC.description"));
        }

        private async Task PartnerEncounter()
        {
            CardCreationOptions options = CardCreationOptions.ForNonCombatWithUniformOdds(
                [ModelDb.CardPool<ZZZPartnerPool>()])
                .WithFlags(CardCreationFlags.NoRarityModification | CardCreationFlags.NoCardPoolModifications);
            List<CardCreationResult> cards = CardFactory.CreateForReward(Owner, 3, options).ToList();
            foreach (CardModel item in await CardSelectCmd.FromSimpleGridForRewards(
                prefs: new CardSelectorPrefs(L10NLookup("ZZZLIB-RANDOM_PLAY.pages.PARTNER_ENCOUNTER.selectionScreenPrompt"), 1)
                , context: new BlockingPlayerChoiceContext(), cards: cards, player: Owner))
            {
                CardCmd.PreviewCardPileAdd(await CardPileCmd.Add(item, PileType.Deck));
            }
            SetEventFinished(L10NLookup("ZZZLIB-RANDOM_PLAY.pages.PARTNER_ENCOUNTER.description"));
        }
    }
}

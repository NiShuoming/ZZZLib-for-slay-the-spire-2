using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZZZLib.Cards.Curse;
using ZZZLib.Other;

namespace ZZZLib.Event
{
    /// <summary>
    /// 零号空洞 1.寻找捷径：受到8伤害，删1张牌  2.进入深处：获得“侵蚀”，2件遗物
    /// </summary>
    public class HollowZero : CustomEventModel
    {
        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(8, ValueProp.Unpowered), new DynamicVar("Relic", 2)];
        public override bool IsAllowed(IRunState runState)
        {
            if (!ZZZConfig.OpenEvent) return false;
            return true;
        }

        protected override List<EventOption> GenerateInitialOptions()
        {
            return [new EventOption(this, Shortcut,InitialOptionKey("SHORTCUT")).ThatDoesDamage(DynamicVars.Damage.BaseValue),
                    new EventOption(this, Enter,InitialOptionKey("ENTER"))];
        }

        private async Task Shortcut()
        {
            await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), Owner.Creature, DynamicVars.Damage, null,null);
            List<CardModel> cards = [.. (await CardSelectCmd.FromDeckForRemoval(
                prefs: new CardSelectorPrefs(CardSelectorPrefs.RemoveSelectionPrompt, 1)
                , player: base.Owner))];
            await CardPileCmd.RemoveFromDeck(cards);
            SetEventFinished(L10NLookup("ZZZLIB-HOLLOW_ZERO.pages.SHORTCUT.description"));
        }

        private async Task Enter()
        {
            CardModel card = Owner.RunState.CreateCard<Demoralized>(Owner);
            CardCmd.PreviewCardPileAdd(await CardPileCmd.Add(card, PileType.Deck), 2f);
            RelicModel relic = RelicFactory.PullNextRelicFromFront(base.Owner, RelicRarity.Common).ToMutable();
            await RelicCmd.Obtain(relic, base.Owner);
            relic = RelicFactory.PullNextRelicFromFront(base.Owner, RelicRarity.Common).ToMutable();
            await RelicCmd.Obtain(relic, base.Owner);
            SetEventFinished(L10NLookup("ZZZLIB-HOLLOW_ZERO.pages.ENTER.description"));
        }
    }
}

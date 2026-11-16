using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Powers
{
    /// <summary>
    /// 战斗开始时，选择1张牌升级
    /// </summary>
    public class Hsos6P : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterSideTurnStartLate(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(Owner) && Owner.IsPlayer)
            {
                

            }
        }

        public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            if (player != base.Owner.Player)
            {
                return;
            }

            var cards = await CardSelectCmd.FromHand(prefs:
                    new CardSelectorPrefs(CardSelectorPrefs.UpgradeSelectionPrompt, 0, Amount),
                    context: choiceContext,
                    player: Owner.Player, filter: a => a.IsUpgradable, source: this);
                foreach (var card in cards)
            {
                CardCmd.Upgrade(card);
            }
        }
    }
}

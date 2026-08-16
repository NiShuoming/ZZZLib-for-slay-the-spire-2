using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Powers
{
    /// <summary>
    /// 人为刀俎 满能量时，攻击牌造成脆弱
    /// </summary>
    public class SteamOvenPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        bool spent = false;
        bool on = false;

        public override Task AfterEnergySpent(CardModel card, int amount)
        {
            if(!Owner.IsPlayer) return Task.CompletedTask;
            if (!spent && card.Type == CardType.Attack)
                on = true;
            if (!spent && amount>0){
                spent = true; 
            }
            return Task.CompletedTask;
        }

        public override async Task BeforeDamageReceived(PlayerChoiceContext choiceContext, Creature target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
        {
            if (on  && target!=null && !target.IsDead && dealer == Owner && props == ValueProp.Move)
            {
                await PowerCmd.Apply<FrailPower>(choiceContext, target, Amount, Owner, cardSource);
            }
        }

        public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (spent)
            {
                on = false;
            }
            return Task.CompletedTask;
        }

        public override Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (side == CombatSide.Player)
            {
                on = false;
                spent = false;
            }
            return Task.CompletedTask;
        }
    }
}

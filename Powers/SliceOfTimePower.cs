using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Powers
{
    /// <summary>
    /// 时光切片 同一回合打出攻击牌、能力牌、技能牌时，获得能量
    /// </summary>
    public class SliceOfTimePower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        bool a = false;
        bool b = false;
        bool c = false;
        bool d = true;

        public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (!Owner.IsPlayer || cardPlay.Card.Owner != Owner.Player) return;
            if(cardPlay.Card.Type == CardType.Attack)a = true;
            else if(cardPlay.Card.Type == CardType.Skill)b=true;
            else if(cardPlay.Card.Type == CardType.Power)c=true;
            if(a && b && c && d)
            {
                d = false;
                await PlayerCmd.GainEnergy(Amount, Owner.Player);
            }
        }

        public override Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if(side == CombatSide.Player)
            {
                a = false;
                b = false;
                c = false;
                d = true;
            }
            return Task.CompletedTask;
        }
    }
}

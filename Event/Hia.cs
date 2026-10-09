using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Other;

namespace ZZZLib.Event
{
    /// <summary>
    /// HIA 1：实战模拟室 与普通敌人战斗，额外获得1次卡牌。2：专业挑战室 与精英战斗，额外获得1次遗物
    /// </summary>
    public class Hia : CustomEventModel
    {
        public override bool IsShared => true;
        public override bool IsAllowed(IRunState runState)
        {
            if (!ZZZConfig.OpenEvent && runState.Players.Count == 1) return false;
            return true;
        }
        protected override IReadOnlyList<EventOption> GenerateInitialOptions()
        {
            return [
            new EventOption(this, CombatSimulation,InitialOptionKey("COMBAT_SIMULATION")),
            new EventOption(this, ExpertChallenge, InitialOptionKey("EXPERT_CHALLENGE"))
            ];
        }
        private Task CombatSimulation()
        {
            return FightEasy();
        }
        private Task ExpertChallenge()
        {
            return FightElite();
        }        

        private Task FightElite()
        {
            EncounterModel encounter = Owner.RunState.Act.AllEliteEncounters.FirstOrDefault();
            if( encounter != null ) 
            EnterCombatWithoutExitingEvent(encounter, new List<Reward>() { new RelicReward(Owner) }, false);
            return Task.CompletedTask;

        }

        private Task FightEasy()
        {
            EncounterModel encounter = Owner.RunState.Act.AllRegularEncounters.FirstOrDefault();
            CardCreationOptions options = CardCreationOptions.ForNonCombatWithUniformOdds(new List<CardPoolModel>() { base.Owner.Character.CardPool })
                .WithFlags(CardCreationFlags.NoRarityModification | CardCreationFlags.NoCardPoolModifications);
            if (encounter != null)
                EnterCombatWithoutExitingEvent(encounter, new List<Reward>() { new CardReward(options,3, Owner)}, false);
            return Task.CompletedTask;
        }
    }
}

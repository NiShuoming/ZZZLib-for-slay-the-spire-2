using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib
{
    /// <summary>
    /// abstract拓展
    /// </summary>
    public interface IZZZSecondEnergyHook
    {
        /// <summary>
        /// 修改第二能量
        /// </summary>
        public abstract int ModifySecondEnergyGain(Player player, int amount, Creature giver, CardModel cardModel);
        /// <summary>
        /// 能量改变结束后
        /// </summary>
        public abstract Task AfterSecondEnergyChanged(Player player, int before, int after, CardModel cardModel);
        /// <summary>
        /// 修改第二能量最大值
        /// </summary>
        public abstract int ModifyMaxSecondEnergy(Player player, int amount);
    }

    public interface IZZZTriggleAnomalyHook
    {
        /// <summary>
        /// 触发属性异常
        /// </summary>
        public abstract Task TriggleAnomaly(PlayerChoiceContext playerChoice, CardKeyword keyWord, Creature target, Creature applier, IEnumerable<DamageResult> results);

    }

    public interface IZZZDrawCardHook
    {
        public abstract int ModifyDrawCard(PlayerChoiceContext choiceContext, decimal count, Player player, bool fromHandDraw);
    }
    /// <summary>
    /// 套在characterModel上，用于释放终结技
    /// </summary>
    public interface IZZZCharacter
    {
        public abstract int MaxSecondEnergy();
        public abstract Task PlayUltimate(PlayerChoiceContext choiceContext, CardPlay cardPlay);
        public abstract LocString energyTitle();
        public abstract LocString energyDescription();
        /// <summary>
        /// res://images/atlases/ui_atlas.sprites/card/energy_<EnergyColorName>.tres 
        /// </summary>
        /// <returns></returns>
        public abstract string energy2Icon();
    }
}

using BaseLib;
using BaseLib.Abstracts;
using BaseLib.Patches.UI;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cmds;

namespace ZZZLib.Other
{
    /*
    public class ZZZSecondEnergy() : CustomResource("ZZZSecondEnergy")
    {
        /// <summary>
        /// 战斗开始时
        /// </summary>
        public override void PrepForCombat()
        {
            Amount = 0;
        }
        /// <summary>
        /// baselib里啥也没有
        /// </summary>
        /// <returns></returns>
        public override ICustomCostVisualsHandler CostVisualsHandler()
        {
            return new BasicCostVisualsHandler(this);
        }
        /// <summary>
        /// baselib里啥也没有
        /// </summary>
        /// <returns></returns>
        public override ICustomResourceVisualsHandler ResourceVisualsHandler()
        {
            return new BasicResourceVisualsHandler(this);
        }
    }*/
    // ==================== 数据类 ====================
    /// <summary>
    /// 在初始遗物中的AfterRoomEntered中添加 Owner.PlayerCombatState.GetSecondEnergy(Owner);来初始化
    /// </summary>
    /// <param name="player"></param>
    public class ZZZSecondEnergy
    {
        private int _energy2 = 0;
        private int _maxEnergy2 = 3;
        private readonly Player _player;

        public ZZZSecondEnergy(Player player, int maxEnergy2)
        {
            _player = player;
            _maxEnergy2 = maxEnergy2;
        }
        public Action<int, int>? SecondEneryChanged;

        public ZZZSecondEnergyCounter? Couter;

        public int Energy2
        {
            get => _energy2;
            set
            {
                int before = _energy2;
                _energy2 = Math.Clamp(value, 0, MaxEnergy2);
                SecondEneryChanged?.Invoke(before, _energy2);
            }
        }

        public int MaxEnergy2
        {
            get => ZZZHook.ModifyMaxSecondEnergy(_player, _maxEnergy2);
        }

        // 添加资源
        public void AddEnergy(int amount)
        {
            Energy2 += amount;
        }

        // 消耗资源（返回是否成功）
        public bool SpendEnergy(int amount)
        {
            if (Energy2 < amount) return false;
            Energy2 -= amount;
            return true;
        }
    }

    // ==================== 扩展类 ====================
    public static class ZZZSecondEnergyPatch
    {
        private static readonly ConditionalWeakTable<PlayerCombatState, ZZZSecondEnergy> _dataTable = new();

        public static ZZZSecondEnergy GetOrCreateData(Player player)
        {
            if (_dataTable.TryGetValue(player.PlayerCombatState, out var data))
                return data;

            if (player.Character is IZZZCharacter izz)
            {
                data = new ZZZSecondEnergy(player, izz.MaxSecondEnergy());
            }
            else
            {
                data = new ZZZSecondEnergy(player,3);
            }
            _dataTable.Add(player.PlayerCombatState, data);

            return data;
        }

        public static ZZZSecondEnergy GetSecondEnergy(this PlayerCombatState combatState, Player player)
        {
            return GetOrCreateData(player);
        }
    }

}

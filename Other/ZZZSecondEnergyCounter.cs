using Godot;
using MegaCrit.Sts2.addons.mega_text;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.HoverTips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Other
{
    /// <summary>
    /// 请放在挂载NEnergyCounter的节点下
    /// </summary>
    public partial class ZZZSecondEnergyCounter:Control
    {
        protected NEnergyCounter? _ene;
        protected MegaLabel? _label;
        protected Player? _pl;
        protected ZZZSecondEnergy? secondEnergy;

        protected HoverTip _hoverTip;

        /// <summary>
        /// 获取子组件或者同级组件请在这里写
        /// </summary>
        public override void _Ready()
        {
            //获取组件
            _label = GetNode<MegaLabel>("%Label2");
            if(_pl.Character is IZZZCharacter zz)
            {
                LocString locString = new LocString(zz.energyDescription().LocTable, zz.energyDescription().LocEntryKey);
                _hoverTip = new HoverTip(new LocString(zz.energyTitle().LocTable, zz.energyTitle().LocEntryKey), locString);
            }
            else
            {
                LocString locString = new LocString("card_keywords", "ZZZLIB-SECOND ENERGY.description");
                locString.Add("energyPrefix", EnergyIconHelper.GetPrefix(_pl.Character.CardPool));
                _hoverTip = new HoverTip(new LocString("card_keywords", "ZZZLIB-SECOND ENERGY.title"), locString);
            }
            Connect(Control.SignalName.MouseEntered, Callable.From(OnHovered));
            Connect(Control.SignalName.MouseExited, Callable.From(OnUnhovered));
            Reflash();
        }

        public override void _EnterTree()
        {
            var prop = typeof(NEnergyCounter).GetField(
                "_player", BindingFlags.NonPublic | BindingFlags.Instance);
            _ene = Owner as NEnergyCounter;
            _pl = prop.GetValue(_ene) as Player;
            secondEnergy = _pl.PlayerCombatState.GetSecondEnergy(_pl);
            secondEnergy.Couter = this;
            secondEnergy.SecondEneryChanged += OnEnergyChanged;
            CombatManager.Instance.StateTracker.CombatStateChanged += OnCombatStateChanged;
        }

        public override void _ExitTree()
        {
            secondEnergy.SecondEneryChanged -= OnEnergyChanged;
            CombatManager.Instance.StateTracker.CombatStateChanged -= OnCombatStateChanged;
        }

        protected virtual void OnCombatStateChanged(CombatState state)
        {
            Reflash();
        }
        protected virtual void Reflash()
        {
            if (_label == null || _pl==null || secondEnergy == null) return;
            _label.SetTextAutoSize($"{secondEnergy.Energy2}/{secondEnergy.MaxEnergy2}");
        }
        /// <summary>
        /// 能量更新时触发
        /// </summary>
        /// <param name="oldEnergy"></param>
        /// <param name="newEnergy"></param>
        protected virtual void OnEnergyChanged(int oldEnergy, int newEnergy)
        {
            
        }

        private void OnHovered()
        {
            NHoverTipSet.CreateAndShow(this, _hoverTip)?.SetGlobalPosition(base.GlobalPosition + new Vector2(-70f, -200f));
        }
        private void OnUnhovered()
        {
            NHoverTipSet.Remove(this);
        }
    }
}

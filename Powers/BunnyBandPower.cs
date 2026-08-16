using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
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
    /// 兔能环p 格挡超过10，获得活力
    /// </summary>
    public class BunnyBandPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(10, ValueProp.Move)];

        public override async Task BeforeBlockGained(Creature creature, decimal amount, ValueProp props, CardModel? cardSource)
        {
            if(creature.Block < DynamicVars.Block.BaseValue && 
                creature.Block + amount >= DynamicVars.Block.BaseValue)
            {
                await PowerCmd.Apply<VigorPower>(new ThrowingPlayerChoiceContext(),
                    Owner, Amount, Owner, null);
            }
        }
    }
}

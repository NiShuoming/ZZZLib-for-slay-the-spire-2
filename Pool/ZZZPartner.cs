using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Pool
{
    internal class ZZZPartner : CustomCharacterModel
    {
        public override bool HideFromVanillaCharacterSelect => true;
        protected override CharacterModel? UnlocksAfterRunAs => null;
        public override string CustomCharacterSelectTransitionPath => "res://materials/transitions/" + "ironclad" + "_transition_mat.tres";

        public override Color NameColor => Colors.White;

        public override CharacterGender Gender => CharacterGender.Neutral;

        public override int StartingHp => 1;

        public override CardPoolModel CardPool => ModelDb.CardPool<ZZZPartnerPool>();

        public override RelicPoolModel RelicPool => ModelDb.RelicPool<ZZZRelicPool>();

        public override PotionPoolModel PotionPool => ModelDb.PotionPool<ZZZPotionPool>();

        public override IEnumerable<CardModel> StartingDeck => [];

        public override IReadOnlyList<RelicModel> StartingRelics => [];

        public override List<string> GetArchitectAttackVfx()
        {
            return new List<string>();
        }
    }
}

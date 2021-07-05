using System;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName("corpo do Orc Paje")]
    public class OrcCacique : BaseCreature
    {
        [Constructable]
        public OrcCacique()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = "Orc Paje";
            this.Body = 1645;
            this.BaseSoundID = 0x45A;

            this.SetStr(140, 160);
            this.SetDex(100, 115);
            this.SetInt(80, 100);

            //this.SetHits(70, 90);

            this.SetDamage(4, 14);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 25, 50);
            this.SetResistance(ResistanceType.Fire, 30, 50);
            this.SetResistance(ResistanceType.Cold, 30, 50);
            this.SetResistance(ResistanceType.Poison, 30, 50);
            this.SetResistance(ResistanceType.Energy, 30, 50);

            this.SetSkill(SkillName.PoderMagico, 80, 100);
            this.SetSkill(SkillName.Arcanismo, 80, 100);
            this.SetSkill(SkillName.ResistenciaMagica, 60.1, 75.0);
            this.SetSkill(SkillName.Anatomia, 50.1, 65.0);
            this.SetSkill(SkillName.Briga, 50, 80);

            Fame = 15000;
            Karma = -15000;

            VirtualArmor = 50;

            Persuadable = true;
            ControlSlots = 2;
            MinPersuadeSkill = 75;

            this.PackReg(6);

			switch (Utility.Random(8))
            {
                case 0: PackItem(new CorpseSkinScroll()); break;
			}

            if (0.05 > Utility.RandomDouble())
                this.PackItem(new OrcishKinMask());

            if (0.5 > Utility.RandomDouble())
                PackItem(new Yeast());
        }

        public OrcCacique(Serial serial)
            : base(serial)
        {
        }

        public override InhumanSpeech SpeechType
        {
            get
            {
                return InhumanSpeech.Orc;
            }
        }
        public override bool CanRummageCorpses
        {
            get
            {
                return true;
            }
        }
        public override int TreasureMapLevel
        {
            get
            {
                return 1;
            }
        }
        public override int Meat
        {
            get
            {
                return 1;
            }
        }

        public override TribeType Tribe { get { return TribeType.Orc; } }

        public override OppositionGroup OppositionGroup
        {
            get
            {
                return OppositionGroup.SavagesAndOrcs;
            }
        }
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Average);
            this.AddLoot(LootPack.LowScrolls);
        }

        public override bool IsEnemy(Mobile m)
        {
            if (m.Player && m.FindItemOnLayer(Layer.Helm) is OrcishKinMask)
                return false;

            return base.IsEnemy(m);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            Item item = aggressor.FindItemOnLayer(Layer.Helm);

            if (item is OrcishKinMask)
            {
                AOS.Damage(aggressor, 50, 0, 100, 0, 0, 0);
                item.Delete();
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}

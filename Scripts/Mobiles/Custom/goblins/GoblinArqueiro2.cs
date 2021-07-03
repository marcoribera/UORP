using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("corpo de um goblin arqueiro elite")]
    public class GoblinArqueiro2 : BaseCreature
    {
        [Constructable]
        public GoblinArqueiro2()
            : base(AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "um goblin arqueiro elite";
            Body = 1783;
            BaseSoundID = 0x600;

            SetStr(50, 70);
            SetDex(40, 70);
            SetInt(15, 20);

            SetHits(100, 120);
            SetStam(60, 80);
            SetMana(30, 30);

            SetDamage(15, 20);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 20, 40);
            SetResistance(ResistanceType.Fire, 20, 40);
            SetResistance(ResistanceType.Cold, 20, 40);
            SetResistance(ResistanceType.Poison, 20, 40);
            SetResistance(ResistanceType.Energy, 20, 40);

            SetSkill(SkillName.ResistenciaMagica, 20, 40);
            SetSkill(SkillName.Anatomia, 60, 80);
            SetSkill(SkillName.Atirar, 40, 60);
            SetSkill(SkillName.Briga, 40, 60);


            Fame = 1500;
            Karma = -1500;

            VirtualArmor = 28;

            this.AddItem(new Bow());
            this.PackItem(new Arrow(Utility.RandomMinMax(10, 30)));            

            PackItem(new ThighBoots());

            switch ( Utility.Random(3) )
            {
                case 0:
                    PackItem(new Ribs());
                    break;
                case 1:
                    PackItem(new Shaft());
                    break;
                case 2:
                    PackItem(new Candle());
                    break;
            }

            if (0.2 > Utility.RandomDouble())
                PackItem(new BolaBall());
        }

        public GoblinArqueiro2(Serial serial)
            : base(serial)
        {
        }

        public override int GetAngerSound() { return 0x600; }
        public override int GetIdleSound() { return 0x600; }
        public override int GetAttackSound() { return 0x5FD; }
        public override int GetHurtSound() { return 0x5FF; }
        public override int GetDeathSound() { return 0x5FE; }

        public override bool CanRummageCorpses { get { return true; } }
        public override int TreasureMapLevel { get { return 1; } }
        public override int Meat { get { return 1; } }
        public override TribeType Tribe { get { return TribeType.GreenGoblin; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.01)
                c.DropItem(new LuckyCoin());
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

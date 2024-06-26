using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("corpo de um goblin")]
    public class TLSGoblinMachado : BaseCreature
    {
        [Constructable]
        public TLSGoblinMachado()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "um goblin";
            Body = 1780;
            BaseSoundID = 0x600;

            SetStr(40, 60);
            SetDex(30, 60);
            SetInt(15, 20);

            //SetHits(90, 100);
            //SetStam(60, 74);
            //SetMana(30, 30);

            SetDamage(1, 7);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 10, 30);
            SetResistance(ResistanceType.Fire, 10, 30);
            SetResistance(ResistanceType.Cold, 10, 30);
            SetResistance(ResistanceType.Poison, 11, 20);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.ResistenciaMagica, 10, 30);
            SetSkill(SkillName.Anatomia, 10, 30);
            SetSkill(SkillName.Atirar, 20, 40);
            SetSkill(SkillName.Briga, 20, 40);
            SetSkill(SkillName.UmaMao, 20, 40);
            SetSkill(SkillName.DuasMaos, 20, 40);


            Fame = 1000;
            Karma = -1000;

            VirtualArmor = 28;

            switch ( Utility.Random(20) )
            {
                case 0:
                    PackItem(new Scimitar());
                    break;
                case 1:
                    PackItem(new Katana());
                    break;
                case 2:
                    PackItem(new WarMace());
                    break;
                case 3:
                    PackItem(new WarHammer());
                    break;
                case 4:
                    PackItem(new Kryss());
                    break;
                case 5:
                    PackItem(new Pitchfork());
                    break;
            }

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
            
            this.PackItem(new Gold(Utility.RandomMinMax(0, 15)));
            if (0.2 > Utility.RandomDouble())
                PackItem(new BolaBall());
        }

        public TLSGoblinMachado(Serial serial)
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
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Meager);
        }
    }
}

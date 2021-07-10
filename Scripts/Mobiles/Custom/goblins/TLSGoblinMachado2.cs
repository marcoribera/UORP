using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("corpo de um goblin elite")]
    public class TLSGoblinMachado2 : BaseCreature
    {
        [Constructable]
        public TLSGoblinMachado2()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "um goblin Elite";

            Body = 1782;
            Hue = 0;
            BaseSoundID = 0x600;

            SetStr(50, 70);
            SetDex(40, 70);
            SetInt(15, 20);

            //SetHits(100, 120);
            //SetStam(60, 80);
            //SetMana(30, 30);

            SetDamage(5, 15);


            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 20, 40);
            SetResistance(ResistanceType.Fire, 20, 40);
            SetResistance(ResistanceType.Cold, 20, 40);
            SetResistance(ResistanceType.Poison, 20, 40);
            SetResistance(ResistanceType.Energy, 20, 40);

            SetSkill(SkillName.ResistenciaMagica, 20, 40);
            SetSkill(SkillName.Anatomia, 30, 50);
            SetSkill(SkillName.Atirar, 40, 60);
            SetSkill(SkillName.Briga, 40, 60);
            SetSkill(SkillName.UmaMao, 40, 60);
            SetSkill(SkillName.DuasMaos,40, 60);

            Fame = 2600;
            Karma = -2600;

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

            if (0.2 > Utility.RandomDouble())
                PackItem(new BolaBall());
        }

        public TLSGoblinMachado2(Serial serial)
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


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            if (version == 0)
            {
                Body = 723;
                Hue = 1900;
            }
        }
    }
}

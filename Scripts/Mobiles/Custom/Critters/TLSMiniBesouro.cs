using System;

namespace Server.Mobiles
{
    [CorpseName("corpo do besourinho")]
    [TypeAlias("Server.Mobiles.TLSMiniBesouro")]
    public class TLSMiniBesouro : BaseCreature
    {
        [Constructable]
        public TLSMiniBesouro()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "um besourinho";
            Body = 1790;
            Hue = Utility.RandomList(0x5AC, 0x5A3, 0x59A, 0x591, 0x588, 0x57F);
            BaseSoundID = 397;

            SetStr(46, 70);
            SetDex(6, 25);
            SetInt(11, 20);

            SetHits(28, 42);
            SetMana(0);

            SetDamage(1, 2);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 5, 10);

            SetSkill(SkillName.ResistenciaMagica, 25.1, 40.0);
            SetSkill(SkillName.Anatomia, 40.1, 60.0);
            SetSkill(SkillName.Briga, 40.1, 60.0);

            Fame = 350;
            Karma = 0;

            VirtualArmor = 6;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 23.1;
        }

        public TLSMiniBesouro(Serial serial)
            : base(serial)
        {
        }

        public override int Meat
        {
            get
            {
                return 1;
            }
        }
        public override FoodType FavoriteFood
        {
            get
            {
                return FoodType.Fish | FoodType.Meat;
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
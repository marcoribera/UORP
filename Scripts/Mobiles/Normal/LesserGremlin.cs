using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a gremlin corpse")]
    public class LesserGremlin : BaseCreature
    {
        [Constructable]
        public LesserGremlin()
            : base(AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Gremlin";
            Body = 724;

            this.SetStr(80, 96);
            this.SetDex(80, 90);
            this.SetInt(26, 40);

            SetHits(150, 180);
            SetMana(80, 100);

            SetDamage(5, 7);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 26);
            SetResistance(ResistanceType.Fire, 36);
            SetResistance(ResistanceType.Cold, 22);
            SetResistance(ResistanceType.Poison, 17);
            SetResistance(ResistanceType.Energy, 30);

            SetSkill(SkillName.Anatomia, 35);
            SetSkill(SkillName.ResistenciaMagica, 35);
            SetSkill(SkillName.Anatomia, 35);

            Persuadable = true;
            ControlSlots = 2;
            MinPersuadeSkill = 35;

            AddItem(new Bow());
            PackItem(new Arrow(Utility.RandomMinMax(60, 80)));
            PackItem(new Apple(5));
        }

        public LesserGremlin(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
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

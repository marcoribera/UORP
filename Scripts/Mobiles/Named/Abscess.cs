using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an abscess's corpse")]
    public class Abscess : BaseCreature
    {
        [Constructable]
        public Abscess()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Abscess";
            Body = 0x109;
            Hue = 0x8FD;
            BaseSoundID = 0x16A;

            SetStr(845, 871);
            SetDex(121, 134);
            SetInt(128, 142);

            SetHits(7470, 7540);

            SetDamage(26, 31);

            SetDamageType(ResistanceType.Physical, 60);
            SetDamageType(ResistanceType.Fire, 10);
            SetDamageType(ResistanceType.Cold, 10);
            SetDamageType(ResistanceType.Poison, 10);
            SetDamageType(ResistanceType.Energy, 10);

            SetResistance(ResistanceType.Physical, 65, 75);
            SetResistance(ResistanceType.Fire, 70, 80);
            SetResistance(ResistanceType.Cold, 25, 35);
            SetResistance(ResistanceType.Poison, 35, 45);
            SetResistance(ResistanceType.Energy, 35, 45);

            SetSkill(SkillName.Briga, 132.3, 143.8);
            SetSkill(SkillName.Anatomia, 121.0, 130.5);
            SetSkill(SkillName.ResistenciaMagica, 102.9, 119.0);
            SetSkill(SkillName.Anatomia, 91.8, 94.3);

            for (int i = 0; i < Utility.RandomMinMax(1, 2); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public Abscess(Serial serial)
            : base(serial)
        {
        }
        public override int Hides
        {
            get
            {
                return 40;
            }
        }
        public override int Meat
        {
            get
            {
                return 19;
            }
        }
        public override bool GivesMLMinorArtifact
        {
            get
            {
                return true;
            }
        }       
        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosUltraRich, 4);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);		
			
            c.DropItem(new AbscessTail());			
			
            if ( Paragon.ChestChance > Utility.RandomDouble() )
            c.DropItem( new ParagonChest( Name, 5 ) );
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
			
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
			
            int version = reader.ReadInt();
        }
    }
}

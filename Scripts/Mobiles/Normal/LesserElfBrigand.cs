using System;
using Server.Items;

namespace Server.Mobiles 
{ 
    [CorpseName("an elf corpse")]
    public class LesserElfBrigand : BaseCreature
    {
        [Constructable]
        public LesserElfBrigand()
            : base(AIType.AI_Spellweaving, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Race = Race.Elf;

            if (Female = Utility.RandomBool())
            {
                Body = 606;
                Name = NameList.RandomName("Elf female");
            }
            else
            {
                Body = 605;
                Name = NameList.RandomName("Elf male");
            }

            Title = "";
            Hue = Race.RandomSkinHue();

            this.SetStr(80, 96);
            this.SetDex(80, 90);
            this.SetInt(26, 40);

            SetHits(150, 180);
            SetMana(80, 100);

            SetDamage(10, 23);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 10, 15);
            SetResistance(ResistanceType.Fire, 10, 15);
            SetResistance(ResistanceType.Poison, 10, 15);
            SetResistance(ResistanceType.Energy, 10, 15);

            SetSkill(SkillName.ResistenciaMagica, 25.0, 47.5);
            SetSkill(SkillName.Anatomia, 65.0, 87.5);
            SetSkill(SkillName.Briga, 15.0, 37.5);
            SetSkill(SkillName.Feiticaria, 15.0, 37.5);
            SetSkill(SkillName.PreparoFisico, 50.0, 75.0);

            Fame = 1000;
            Karma = -1000;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 35;


            // outfit
            AddItem(new Shirt(Utility.RandomNeutralHue()));

            switch (Utility.Random(4))
            {
                case 0:
                    AddItem(new Sandals());
                    break;
                case 1:
                    AddItem(new Shoes());
                    break;
                case 2:
                    AddItem(new Boots());
                    break;
                case 3:
                    AddItem(new ThighBoots());
                    break;
            }

            if (Female)
            {
                if (Utility.RandomBool())
                    AddItem(new Skirt(Utility.RandomNeutralHue()));
                else
                    AddItem(new Kilt(Utility.RandomNeutralHue()));
            }
            else
                AddItem(new ShortPants(Utility.RandomNeutralHue()));

            // hair, facial hair			
            HairItemID = Race.RandomHair(Female);
            HairHue = Race.RandomHairHue();

            // weapon, shield
            Item weapon = Loot.RandomWeapon();

            AddItem(weapon);

            if (weapon.Layer == Layer.OneHanded && Utility.RandomBool())
                AddItem(Loot.RandomShield());

            PackGold(50, 150);
        }

        public LesserElfBrigand(Serial serial)
            : base(serial)
        {
        }

        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }
        public override bool ShowFameTitle
        {
            get
            {
                return false;
            }
        }
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.75)
                c.DropItem(new SeveredElfEars());
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

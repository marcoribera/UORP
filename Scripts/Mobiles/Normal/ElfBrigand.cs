using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("corpo de Elfo")]
    public class ElfBrigand : BaseCreature
    {
        [Constructable]
        public ElfBrigand()
            : base(AIType.AI_Spellweaving, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Race = Race.Elf;

            if (Female = Utility.RandomBool())
            {
                Body = 606;
                Name = NameList.RandomName("Elf female");
                Title = "a Salteadora";
            }
            else
            {
                Body = 605;
                Name = NameList.RandomName("Elf male");
                Title = "o Salteador";
            }

            
            Hue = Race.RandomSkinHue();

            SetStr(70, 100);
            SetDex(65, 95);
            SetInt(50, 75);

            SetDamage(10, 20);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 10, 30);
            SetResistance(ResistanceType.Fire, 10, 15);
            SetResistance(ResistanceType.Poison, 10, 15);
            SetResistance(ResistanceType.Energy, 10, 15);

            SetSkill(SkillName.ResistenciaMagica, 25.0, 47.5);
            SetSkill(SkillName.Anatomia, 15, 35);
            SetSkill(SkillName.Briga, 30.0, 60.0);
            SetSkill(SkillName.Feiticaria, 30.0, 50.0);
            SetSkill(SkillName.PreparoFisico, 30.0, 60.0);
            //SetSkill(SkillName.Contusivo, 30.0, 60.0);
            //SetSkill(SkillName.Cortante, 30.0, 60.0);
            //SetSkill(SkillName.Perfurante, 30.0, 60.0);
            SetSkill(SkillName.DuasMaos, 30.0, 50.0);
            SetSkill(SkillName.UmaMao, 30.0, 50.0);
            SetSkill(SkillName.Bloqueio, 15, 35);

            Fame = 1000;
            Karma = -1000;

            Persuadable = true;
            ControlSlots = 2;
            MinPersuadeSkill = 75;


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

        public ElfBrigand(Serial serial)
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

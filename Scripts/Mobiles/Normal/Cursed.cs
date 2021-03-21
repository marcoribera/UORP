using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an inhuman corpse")]
    public class Cursed : BaseCreature
    {
        [Constructable]
        public Cursed()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Title = "the Cursed";

            this.Hue = Utility.RandomMinMax(0x8596, 0x8599);
            this.Body = 0x190;
            this.Name = NameList.RandomName("male");
            this.BaseSoundID = 471;

            this.AddItem(new ShortPants(Utility.RandomNeutralHue()));
            this.AddItem(new Shirt(Utility.RandomNeutralHue()));

            this.SetStr(91, 100);
            this.SetDex(86, 95);
            this.SetInt(61, 70);

            this.SetHits(91, 120);

            this.SetDamage(5, 13);

            this.SetResistance(ResistanceType.Physical, 15, 25);
            this.SetResistance(ResistanceType.Fire, 5, 10);
            this.SetResistance(ResistanceType.Cold, 25, 35);
            this.SetResistance(ResistanceType.Poison, 5, 10);
            this.SetResistance(ResistanceType.Energy, 5, 10);

            this.SetSkill(SkillName.Perfurante, 46.0, 77.5);
            this.SetSkill(SkillName.Contusivo, 35.0, 57.5);
            this.SetSkill(SkillName.ResistenciaMagica, 53.5, 62.5);
            this.SetSkill(SkillName.Cortante, 55.0, 77.5);
            this.SetSkill(SkillName.Anatomia, 60.0, 82.5);
            this.SetSkill(SkillName.Envenenamento, 60.0, 82.5);

            this.Fame = 1000;
            this.Karma = -2000;

            BaseWeapon weapon = Loot.RandomWeapon();
            weapon.Movable = false;
            this.AddItem(weapon);
        }

        public Cursed(Serial serial)
            : base(serial)
        {
        }

        public override bool ClickTitle
        {
            get
            {
                return false;
            }
        }
        public override bool ShowFameTitle
        {
            get
            {
                return false;
            }
        }
        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }
        public override int GetAttackSound()
        {
            return -1;
        }

        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Meager);
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
using System;
using Server.Items;

namespace Server.Mobiles 
{ 
    [CorpseName("a golem controller corpse")] 
    public class GolemController : BaseCreature 
    { 
        [Constructable] 
        public GolemController()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        { 
            Name = NameList.RandomName("golem controller");
            Title = "the controller";

            Body = 400;
            Hue = 0x455;

            AddArcane(new Robe());
            AddArcane(new ThighBoots());
            AddArcane(new LeatherGloves());
            AddArcane(new Cloak());

            SetStr(126, 150);
            SetDex(96, 120);
            SetInt(151, 175);

            SetHits(76, 90);

            SetDamage(6, 12);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 30, 40);
            SetResistance(ResistanceType.Fire, 25, 35);
            SetResistance(ResistanceType.Cold, 35, 45);
            SetResistance(ResistanceType.Poison, 5, 15);
            SetResistance(ResistanceType.Energy, 15, 25);

            SetSkill(SkillName.PoderMagico, 95.1, 100.0);
            SetSkill(SkillName.Arcanismo, 95.1, 100.0);
            SetSkill(SkillName.ResistenciaMagica, 102.5, 125.0);
            SetSkill(SkillName.Anatomia, 65.0, 87.5);
            SetSkill(SkillName.Briga, 65.0, 87.5);

            Fame = 4000;
            Karma = -4000;

            VirtualArmor = 16;

            if (0.7 > Utility.RandomDouble())
                PackItem(new ArcaneGem());
        }

        public GolemController(Serial serial)
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
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
        }

        public void AddArcane(Item item)
        {
            if (item is IArcaneEquip)
            {
                IArcaneEquip eq = (IArcaneEquip)item;
                eq.CurArcaneCharges = eq.MaxArcaneCharges = 20;
            }

            item.Hue = ArcaneGem.DefaultArcaneHue;
            item.LootType = LootType.Newbied;

            AddItem(item);
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

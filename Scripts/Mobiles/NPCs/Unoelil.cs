using System;
using Server.Items;

namespace Server.Engines.Quests
{ 
    public class Unoelil : MondainQuester
    { 
        [Constructable]
        public Unoelil()
            : base("Unoelil", "the bark weaver")
        { 
            SetSkill(SkillName.PreparoFisico, 60.0, 83.0);
        }

        public Unoelil(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests
        { 
            get
            {
                return new Type[] 
                {
                    typeof(StopHarpingOnMeQuest),
                    typeof(TheFarEyeQuest),
                    typeof(NothingFancyQuest)
                };
            }
        }
        public override void InitBody()
        {
            InitStats(100, 100, 25);
			
            Female = false;
            Race = Race.Elf;
			
            Hue = 0x8362;
            HairItemID = 0x2FCD;
            HairHue = 0x31D;
        }

        public override void InitOutfit()
        {
            AddItem(new ElvenBoots(0x1BB));
            AddItem(new Tunic(0x64F));
            AddItem(new ShortPants(0x1BB));
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

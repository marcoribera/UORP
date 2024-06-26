using System;
using Server.Items;

namespace Server.Engines.Quests
{ 
    public class Aurvidlem : MondainQuester
    {
        [Constructable]
        public Aurvidlem()
            : base("Aurvidlem", "the Artificer")
        {
            SetSkill(SkillName.Erudicao, 60.0, 83.0);
            SetSkill(SkillName.ImbuirMagica, 60.0, 83.0);
        }

        public Aurvidlem(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests
        {
            get
            {
                return new Type[] 
                {
                    typeof(KnowledgeoftheSoulforge)
                };
            }
        }
        public override void InitBody()
        {
            InitStats(100, 100, 25);

            CantWalk = true;
            Race = Race.Gargoyle;

            Hue = 0x86DE;
            HairItemID = 0x4259;
            HairHue = 0x0;
        }

        public override void InitOutfit()
        {
            AddItem(new SerpentStoneStaff());
            AddItem(new GargishClothChest(1307));
            AddItem(new GargishClothArms(1330));
            AddItem(new GargishClothKilt(1307));
        }

        public override void Advertise()
        {
            this.Say(1112525);  // Come to be Artificer. I have a task for you. 
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
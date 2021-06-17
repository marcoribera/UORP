using System;
using Server.Items;

namespace Server.Mobiles
{
    public class Gypsy : BaseCreature
    {
        [Constructable]
        public Gypsy()
            : base(AIType.AI_Melee, FightMode.None, 10, 1, 0.2, 0.4)
        {
            this.InitStats(31, 41, 51);

            this.SpeechHue = Utility.RandomDyedHue();

            this.SetSkill(SkillName.Culinaria, 65, 88);
            this.SetSkill(SkillName.Prestidigitacao, 65, 88);
            this.SetSkill(SkillName.Prestidigitacao, 65, 88);

            this.Hue = Utility.RandomSkinHue();

            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x191;
                this.Name = NameList.RandomName("female");
                this.AddItem(new Kilt(Utility.RandomDyedHue()));
                this.AddItem(new Shirt(Utility.RandomDyedHue()));
                this.AddItem(new ThighBoots());
                this.Title = "the gypsy";
            }
            else
            {
                this.Body = 0x190;
                this.Name = NameList.RandomName("male");
                this.AddItem(new ShortPants(Utility.RandomNeutralHue()));
                this.AddItem(new Shirt(Utility.RandomDyedHue()));
                this.AddItem(new Sandals());
                this.Title = "the gypsy";
            }

            this.AddItem(new Bandana(Utility.RandomDyedHue()));
            this.AddItem(new Dagger());

            Utility.AssignRandomHair(this);

            Container pack = new Backpack();

            pack.DropItem(new Gold(250, 300));

            pack.Movable = false;

            this.AddItem(pack);
        }

        public Gypsy(Serial serial)
            : base(serial)
        {
        }

        public override bool CanTeach
        {
            get
            {
                return true;
            }
        }
        public override bool ClickTitle
        {
            get
            {
                return false;
            }
        }

        public override void OnDeath(Container c)
        {

            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.001) // 1/1000 chance

            switch (Utility.Random(4))
            {
                case 0: c.DropItem(new MoonstoneBracelet()); break;
                case 1: c.DropItem(new MoonstoneEarrings()); break;
                case 2: c.DropItem(new MoonstoneRing()); break;
                case 3: c.DropItem(new MoonstoneNecklace()); break;
        
            }
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

using System;

namespace Server.Items
{
    public class BagOfMysticismReagents : Bag
    {
        [Constructable]
        public BagOfMysticismReagents()
            : this(50)
        {
        }

        [Constructable]
        public BagOfMysticismReagents(int amount)
        {
            this.DropItem(new BlackPearl(amount));
            this.DropItem(new Bloodmoss(amount));
            this.DropItem(new Garlic(amount));
            this.DropItem(new Ginseng(amount));
            this.DropItem(new MandrakeRoot(amount));
            this.DropItem(new Nightshade(amount));
            this.DropItem(new SulfurousAsh(amount));
            this.DropItem(new SpidersSilk(amount));
            this.DropItem(new DaemonBone(amount));
            this.DropItem(new DragonBlood(amount));
            this.DropItem(new Bone(amount));
            this.DropItem(new FertileDirt(amount));
        }

        public BagOfMysticismReagents(Serial serial)
            : base(serial)
        {
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

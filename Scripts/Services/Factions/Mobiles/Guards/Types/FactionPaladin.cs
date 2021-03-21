using System;
using Server.Items;

namespace Server.Factions
{
    public class FactionPaladin : BaseFactionGuard
    {
        [Constructable]
        public FactionPaladin()
            : base("the paladin")
        {
            this.GenerateBody(false, false);

            this.SetStr(151, 175);
            this.SetDex(61, 85);
            this.SetInt(81, 95);

            this.SetResistance(ResistanceType.Physical, 40, 60);
            this.SetResistance(ResistanceType.Fire, 40, 60);
            this.SetResistance(ResistanceType.Cold, 40, 60);
            this.SetResistance(ResistanceType.Energy, 40, 60);
            this.SetResistance(ResistanceType.Poison, 40, 60);

            this.VirtualArmor = 32;

            this.SetSkill(SkillName.Cortante, 110.0, 120.0);
            this.SetSkill(SkillName.Briga, 110.0, 120.0);
            this.SetSkill(SkillName.Anatomia, 110.0, 120.0);
            this.SetSkill(SkillName.ResistenciaMagica, 110.0, 120.0);
            this.SetSkill(SkillName.Medicina, 110.0, 120.0);
            this.SetSkill(SkillName.Anatomia, 110.0, 120.0);

            this.SetSkill(SkillName.Arcanismo, 110.0, 120.0);
            this.SetSkill(SkillName.PoderMagico, 110.0, 120.0);

            this.AddItem(this.Immovable(this.Rehued(new PlateChest(), 2125)));
            this.AddItem(this.Immovable(this.Rehued(new PlateLegs(), 2125)));
            this.AddItem(this.Immovable(this.Rehued(new PlateHelm(), 2125)));
            this.AddItem(this.Immovable(this.Rehued(new PlateGorget(), 2125)));
            this.AddItem(this.Immovable(this.Rehued(new PlateArms(), 2125)));
            this.AddItem(this.Immovable(this.Rehued(new PlateGloves(), 2125)));

            this.AddItem(this.Immovable(this.Rehued(new BodySash(), 1254)));
            this.AddItem(this.Immovable(this.Rehued(new Cloak(), 1254)));

            this.AddItem(this.Newbied(new Halberd()));

            this.AddItem(this.Immovable(this.Rehued(new VirtualMountItem(this), 1254)));

            this.PackItem(new Bandage(Utility.RandomMinMax(30, 40)));
            this.PackStrongPotions(6, 12);
        }

        public FactionPaladin(Serial serial)
            : base(serial)
        {
        }

        public override GuardAI GuardAI
        {
            get
            {
                return GuardAI.Magic | GuardAI.Melee | GuardAI.Smart | GuardAI.Curse | GuardAI.Bless;
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

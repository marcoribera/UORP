using System;

namespace Server.Items
{
    public class ConjurersGrimoire : Spellbook
    {
        public override bool IsArtifact { get { return true; } }
        public override int LabelNumber { get { return 1094799; } } // Conjurer's Grimoire

        [Constructable]
        public ConjurersGrimoire()
            : base()
        {
            Hue = 1157;
            Slayer = SlayerName.Silver;
            Attributes.LowerManaCost = 10;
            Attributes.BonusInt = 8;
            Attributes.SpellDamage = 15;
            SkillBonuses.SetValues(0, SkillName.Arcanismo, 15.0);
        }

        public ConjurersGrimoire(Serial serial)
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
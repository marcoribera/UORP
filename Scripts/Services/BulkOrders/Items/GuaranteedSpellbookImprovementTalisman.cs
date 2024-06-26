using System;
using Server.Mobiles;

namespace Server.Items
{
    public class GuaranteedSpellbookImprovementTalisman : BaseTalisman
    {
        [Constructable]
        public GuaranteedSpellbookImprovementTalisman()
            : this(10)
        {
        }

        [Constructable]
        public GuaranteedSpellbookImprovementTalisman(int charges)
            : base(0x9E28)
        {
            Charges = charges;

            Skill = TalismanSkill.Erudicao;
            SuccessBonus = BaseTalisman.GetRandomSuccessful();
            ExceptionalBonus = BaseTalisman.GetRandomExceptional();
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (Charges > 0)
                list.Add(1049116, Charges.ToString()); // [ Charges: ~1_CHARGES~ ]

            list.Add(1157212); // Crafting Failure Protection
        }

        public GuaranteedSpellbookImprovementTalisman(Serial serial)
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

////////Animal Crackers//////////
///////////Was Here/////////////
// Cleaned up by GreyWolf79 (Nov. 19, 2008)
using System;
using Server.Network;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a corpse of a cock roach")]
    public class TLSCockRoach : BaseCreature
    {

        [Constructable]
        public TLSCockRoach() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.15, 0.4 )
        {

	    Body = 169;
            Name = "a cock roach";
            Hue = 1175;

            SetStr(100,300);
            SetDex(150, 300);
            SetInt(50, 100);

            SetHits(200, 350);

            SetDamage( 9, 19 );

            SetDamageType(ResistanceType.Physical, 22);
            SetDamageType(ResistanceType.Cold, 22);
            SetDamageType(ResistanceType.Fire, 22);
            SetDamageType(ResistanceType.Energy, 22);
            SetDamageType(ResistanceType.Poison, 22);

            SetResistance(ResistanceType.Physical, 10, 30);
            SetResistance(ResistanceType.Cold, 10, 30);
            SetResistance(ResistanceType.Fire, 10, 30);
            SetResistance(ResistanceType.Energy, 10, 30);
            SetResistance(ResistanceType.Poison, 10, 30);

            Fame = 2200;
            Karma = -2200;


            Tamable = true; // added to make it tamable - GreyWolf79
            ControlSlots = 2; // original slots AnimalCrackers had in script
            MinTameSkill = 96.1; // added so tamable - GreyWolf79
   
        }

        public override bool AlwaysMurderer { get { return false; } } // if going to be tamable false works best - GreyWolf79
        public override FoodType FavoriteFood { get { return FoodType.Fish | FoodType.Meat; } } // added so it can eat - GreyWolf79
        public override int Meat { get { return 1; } } // added so you get meat when it is killed - GreyWolf79

        public TLSCockRoach(Serial serial) : base(serial)
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

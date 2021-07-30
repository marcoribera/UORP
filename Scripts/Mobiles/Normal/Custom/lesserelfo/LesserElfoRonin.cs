using System;
using System.Collections;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a ronin corpse" )]
	public class LesserElfoRonin : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }

        private DateTime m_NextWeaponChange;

		[Constructable]
		public LesserElfoRonin() : base( AIType.AI_Samurai, FightMode.Closest, 10, 1, 0.3, 0.6 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Hue = Utility.RandomSkinHue();
			Name = "";
			Body = (( this.Female = Utility.RandomBool() ) ? Body = 0x191 : Body = 0x190);

            {
                InitStats(100, 100, 25);
                this.Race = Race.Elf;
          
                SpeechHue = Utility.RandomDyedHue();

                if (Female)
                {
                    Body = 0x25E;
                    Name = NameList.RandomName("female");
                }
                else
                {
                    Body = 0x25D;
                    Name = NameList.RandomName("male");
                }

                Hue = Utility.RandomSkinHue();
                Utility.AssignRandomHair(this);
                Utility.AssignRandomFacialHair(this);
            }

            Hue = Utility.RandomSkinHue();

            this.SetStr(80, 96);
            this.SetDex(80, 90);
            this.SetInt(26, 40);

            SetHits(150, 180);
            SetMana(80, 100);

            SetDamage( 17, 25 );

			SetDamageType( ResistanceType.Physical, 90 );
			SetDamageType( ResistanceType.Poison, 10 );

			SetResistance( ResistanceType.Physical, 55, 75 );
			SetResistance( ResistanceType.Fire, 40, 60 );
			SetResistance( ResistanceType.Cold, 35, 55 );
			SetResistance( ResistanceType.Poison, 50, 70 );
			SetResistance( ResistanceType.Energy, 55, 75 );

			SetSkill( SkillName.ResistenciaMagica, 20.0, 40.0);
			SetSkill( SkillName.Anatomia, 20.0, 40.0);
			SetSkill( SkillName.Briga, 20.0, 40.0);
			SetSkill( SkillName.Anatomia, 20.0, 40.0);

			SetSkill( SkillName.Perfurante, 20.0, 40.0);
			SetSkill( SkillName.Contusivo, 20.0, 40.0);
			SetSkill( SkillName.Cortante, 20.0, 40.0);

            SetSkill(SkillName.Bushido, 20.0, 40.0);

			Fame = 8500;
			Karma = -8500;

			Persuadable = true;
			ControlSlots = 2;
			MinPersuadeSkill = 40;
            IdiomaNativo = Mobiles.SpeechType.Avlitir;


            AddItem( new SamuraiTabi() );
			AddItem( new LeatherHiroSode());
			AddItem( new LeatherDo());

			switch ( Utility.Random( 4 ))
			{
				case 0: AddItem( new LightPlateJingasa()); break;
				case 1: AddItem( new ChainHatsuburi() ); break;
				case 2: AddItem( new DecorativePlateKabuto() ); break;
				case 3: AddItem( new LeatherJingasa()); break;
			}

			switch ( Utility.Random( 3 ))
			{
				case 0: AddItem( new StuddedHaidate()); break;
				case 1: AddItem( new LeatherSuneate() ); break;
				case 2: AddItem( new PlateSuneate() ); break;
			}

			if( Utility.RandomDouble() > .2 )
				AddItem( new NoDachi() );
			else
				AddItem( new Halberd() );

			PackItem( new Wakizashi() );
			PackItem( new Longsword() );

			Utility.AssignRandomHair( this );

            SetWeaponAbility(WeaponAbility.RidingSwipe);
		}

		public override void OnDeath( Container c )
 		{
			base.OnDeath( c );
	 		c.DropItem( new BookOfBushido() );
 		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems, 2 );
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool CanRummageCorpses{ get{ return true; } }

        public override double WeaponAbilityChance
        {
            get
            {
                if(Combatant is Mobile && ((Mobile)Combatant).Mounted)
                    return 0.8;

                return base.WeaponAbilityChance;
            }
        }

        private void ChangeWeapon()
        {
            if (Backpack == null)
                return;

            Item item = FindItemOnLayer(Layer.OneHanded);

            if (item == null)
                item = FindItemOnLayer(Layer.TwoHanded);

            System.Collections.Generic.List<BaseWeapon> weapons = new System.Collections.Generic.List<BaseWeapon>();

            foreach (Item i in Backpack.Items)
            {
                if (i is BaseWeapon && i != item)
                    weapons.Add((BaseWeapon)i);
            }

            if (weapons.Count > 0)
            {
                if (item != null)
                    Backpack.DropItem(item);

                AddItem(weapons[Utility.Random(weapons.Count)]);

                m_NextWeaponChange = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 60));
            }
        }

        public override void OnThink()
        {
            base.OnThink();

            if (Combatant != null && m_NextWeaponChange < DateTime.UtcNow)
                ChangeWeapon();
        }

		public LesserElfoRonin( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            m_NextWeaponChange = DateTime.UtcNow;
		}
       
        
    }
}

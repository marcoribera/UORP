// Created by Script Creator

using System;
using Server.Items;

namespace Server.Mobiles

              {
              [CorpseName( " corpse of the Caveman" )]
              public class Caveman : BaseCreature
              {
				private Timer m_Timer;
                                 [Constructable]
                                    public Caveman() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
                            {
                                               
					                           Body = 400;
						                       Female = false; 
						                       Hue = 33779;
						                       Title = "The Caveman";
                                               //Body = 149; // Uncomment these lines and input values
                                               //BaseSoundID = 0x4B0; // To use your own custom body and sound.
                                               SetStr( 50, 80 );
                                               SetDex( 50, 60 );
                                               SetInt( 50, 60 );
                                               SetHits( 150, 200 );
                                               SetDamage( 20, 25 );
                                               SetDamageType( ResistanceType.Cold, 19 );
                                               SetDamageType( ResistanceType.Fire, 19 );
                                               SetDamageType( ResistanceType.Energy, 19 );
                                               SetDamageType( ResistanceType.Poison, 19 );

                                               SetResistance( ResistanceType.Physical, 60 );
                                               SetResistance( ResistanceType.Cold, 10 );
                                               SetResistance( ResistanceType.Fire, 10);
                                               SetResistance( ResistanceType.Energy, 10);
                                               SetResistance( ResistanceType.Poison, 10);

			SetSkill( SkillName.Perfurante, 60.0, 80.0 );
			SetSkill( SkillName.Contusivo, 60.0, 80.0 );
			SetSkill( SkillName.ResistenciaMagica, 60.0, 80.0 );
			SetSkill( SkillName.Cortante, 60.0, 80.0 );
			SetSkill( SkillName.Anatomia, 60.0, 80.0 );
			SetSkill( SkillName.Briga, 60.0, 80.0 );


			m_Timer = new TeleportTimer( this );
			m_Timer.Start();



            Fame = 150;
            Karma = -150;
            VirtualArmor = 60;

			PackGold( 51, 65 );


			

			Item Shirt = new Shirt(); 
			Shirt.Movable = false;
			Shirt.Hue = 351; 
			AddItem( Shirt );

			Item FurSarong = new FurSarong(); 
			FurSarong.Movable = false;
			FurSarong.Hue = 351; 
			AddItem( FurSarong );

			Item Club = new Club(); 
			Club.Movable = false;
			Club.Hue = 0; 
			AddItem( Club );

			


           



                            }
        
		
		        public virtual bool HasBreath{ get{ return true ; } }
				public virtual int BreathFireDamage{ get{ return 11; } }
				public virtual int BreathColdDamage{ get{ return 11; } }
//                public override bool IsScaryToPets{ get{ return true; } }
				public override bool AutoDispel{ get{ return true; } }
                public override bool BardImmune{ get{ return true; } }
                public override bool Unprovokable{ get{ return true; } }
                public override Poison HitPoison{ get{ return Poison. Lesser ; } }
                public override bool AlwaysMurderer{ get{ return true; } }
//				public override bool IsScaredOfScaryThings{ get{ return false; } }






		public override void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{
			if ( from is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)from;

				if ( bc.Controlled || bc.BardTarget == this )
					damage = 0; // Immune to pets and provoked creatures
			}
		}
		private class TeleportTimer : Timer
		{
			private Mobile m_Owner;

			private static int[] m_Offsets = new int[]
			{
				-1, -1,
				-1,  0,
				-1,  1,
				0, -1,
				0,  1,
				1, -1,
				1,  0,
				1,  1
			};

			public TeleportTimer( Mobile owner ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.1 ) )
			{
				m_Owner = owner;
			}

			protected override void OnTick()
			{
				if ( m_Owner.Deleted )
				{
					Stop();
					return;
				}

				Map map = m_Owner.Map;

				if ( map == null )
					return;

				if ( 0.5 < Utility.RandomDouble() )
					return;

				Mobile toTeleport = null;

				foreach ( Mobile m in m_Owner.GetMobilesInRange( 16 ) )
				{
					if ( m != m_Owner && m.Player && m_Owner.CanBeHarmful( m ) && m_Owner.CanSee( m ) )
					{
						toTeleport = m;
						break;
					}
				}

				if ( toTeleport != null )
				{
					int offset = Utility.Random( 8 ) * 2;

					Point3D to = m_Owner.Location;

					for ( int i = 0; i < m_Offsets.Length; i += 2 )
					{
						int x = m_Owner.X + m_Offsets[(offset + i) % m_Offsets.Length];
						int y = m_Owner.Y + m_Offsets[(offset + i + 1) % m_Offsets.Length];

						if ( map.CanSpawnMobile( x, y, m_Owner.Z ) )
						{
							to = new Point3D( x, y, m_Owner.Z );
							break;
						}
						else
						{
							int z = map.GetAverageZ( x, y );

							if ( map.CanSpawnMobile( x, y, z ) )
							{
								to = new Point3D( x, y, z );
								break;
							}
						}
					}

					Mobile m = toTeleport;

					Point3D from = m.Location;

					m.Location = to;

					Server.Spells.SpellHelper.Turn( m_Owner, toTeleport );
					Server.Spells.SpellHelper.Turn( toTeleport, m_Owner );

					m.ProcessDelta();

					Effects.SendLocationParticles( EffectItem.Create( from, m.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
					Effects.SendLocationParticles( EffectItem.Create(   to, m.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 5023 );

					m.PlaySound( 0x1FE );

					m_Owner.Combatant = toTeleport;
				}
			}
		}


public Caveman( Serial serial ) : base( serial )
                      {
                      }

public override void Serialize( GenericWriter writer )
                      {
                                        base.Serialize( writer );
                                        writer.Write( (int) 0 );
                      }

        public override void Deserialize( GenericReader reader )
                      {
                                        base.Deserialize( reader );
                                        int version = reader.ReadInt();
                      }
    }
}

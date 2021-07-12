using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Spells;
using Server.Spells.Third;
using Server.Spells.Sixth;
using Server.Spells.Seventh;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "the ice king's corpse" )]
	public class IceKing : BaseCreature
	{

		public override WeaponAbility GetWeaponAbility()
		{
			return Utility.RandomBool() ? WeaponAbility.ConcussionBlow : WeaponAbility.CrushingBlow;
		}

		public override bool IgnoreYoungProtection { get { return Core.ML; } }
		public override bool AlwaysMurderer{ get{ return true; } }

		[Constructable]
		public IceKing() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.1, 0.4 )
		{
			Name = "Set";
			Title = "The Ice King";
			Body = 400;
			Hue = 1151;
			BaseSoundID = 377;

			AddItem( new ThighBoots( 0x51D ) );
			
			Item item;
			
			item = new BodySash();
			item.Hue = 1151;
			AddItem( item );
			
			item = new Cloak();
			item.Hue = 1151;
			AddItem( item );			

			PlateArms arms = new PlateArms();
			arms.Hue = 1151;
                        arms.Movable = false;
			AddItem(arms);

			PlateChest chest = new PlateChest();
			chest.Hue = 1151;
                        chest.Movable = false;
			AddItem(chest);

			PlateGloves gloves = new PlateGloves();
			gloves.Hue = 1151;
                        gloves.Movable = false;
			AddItem(gloves);

			PlateGorget gorget = new PlateGorget();
			gorget.Hue = 1151;
                        gorget.Movable = false;
			AddItem(gorget);

			PlateHelm helm = new PlateHelm();
			helm.Hue = 1151;
                        helm.Movable = false;
			AddItem(helm);

			PlateLegs legs = new PlateLegs();
			legs.Hue = 1151;
                        legs.Movable = false;
			AddItem(legs);

			SetStr( 1198, 1207 );
			SetDex( 127, 135 );
			SetInt( 495, 546 );

			SetHits( 25000 );

			SetDamage( 27, 31 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Cold, 20 );
			
			SetResistance( ResistanceType.Physical, 78, 89 );
			SetResistance( ResistanceType.Fire, 30, 46 );
			SetResistance( ResistanceType.Cold, 97, 100 );
			SetResistance( ResistanceType.Poison, 71, 80 );
			SetResistance( ResistanceType.Energy, 70, 80 );

			SetSkill( SkillName.Briga, 110.4, 112.3 );
			SetSkill( SkillName.Anatomia, 114.0, 118.5 );
			SetSkill( SkillName.ResistenciaMagica, 114.1, 132.0 );
			SetSkill( SkillName.Arcanismo, 90.9, 99.8 );
			SetSkill( SkillName.PoderMagico, 90.7, 98.7 );
			
			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 80;

			PackGold( 9500, 16500 );

			if( Utility.RandomDouble() <= 0.15 ) PackItem( new RarewoodChest() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 8 );
			AddLoot( LootPack.Gems, 13 );
			AddLoot( LootPack.MedScrolls, 11 );
		}

		public IceKing( Serial serial ) : base( serial )
		{
		}

		public override int Meat{ get{ return 8; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool Uncalmable{ get{ return Core.SE; } }
		public override bool AutoDispel{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }

		public void DrainLife()
		{
			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 10 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					list.Add( m );
				else if ( m.Player )
					list.Add( m );
			}

			foreach ( Mobile m in list )
			{
				DoHarmful( m );

				m.FixedParticles( 0x374A, 10, 15, 5013, 0x496, 0, EffectLayer.Waist );
				m.PlaySound( 0x231 );

				m.SendMessage( "Your Soul is Mine!" );

				int toDrain = Utility.RandomMinMax( 10, 40 );

				Hits += toDrain;
				m.Damage( toDrain, this );
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();
		}
		
		private Timer m_Timer;
		private string m_Name;
		private int m_Hue;

		public override void OnThink()
		{
			base.OnThink();
			
			if ( Combatant == null )
				return;	
				
			if ( Hits > 0.8 * HitsMax && Utility.RandomDouble() < 0.05 )
				FireRing();				
				
		
		}
		
		
		
		public void DeleteItems()
		{		
			for ( int i = Items.Count - 1; i >= 0; i -- )
				if ( Items[ i ] is ClonedItem )
					Items[ i ].Delete();
					
			if ( Backpack != null )
			{
				for ( int i = Backpack.Items.Count - 1; i >= 0; i -- )
					if ( Backpack.Items[ i ] is ClonedItem )
						Backpack.Items[ i ].Delete();
			}
		}
		
		public virtual void EndMorph()
		{
			if ( Combatant != null && Name == Combatant.Name )
				return;
		
			DeleteItems();
			
			if ( m_Timer != null )
			{
				m_Timer.Stop();		
				m_Timer = null;	
			}
				
			
			
			Body = 400;
			Title = null;
			Name = m_Name;
			Hue = m_Hue;
			
			PlaySound( 0x511 );
			FixedParticles( 0x376A, 1, 14, 5045, EffectLayer.Waist );
		}
		
		private static int[] m_North = new int[]
		{
			-1, -1, 
			1, -1,
			-1, 2,
			1, 2
		};
		
		private static int[] m_East = new int[]
		{
			-1, 0,
			2, 0
		};
		
		public virtual void FireRing()
		{
			for ( int i = 0; i < m_North.Length; i += 2 ) 
			{
				Point3D p = Location;
				
				p.X += m_North[ i ];
				p.Y += m_North[ i + 1 ];
				
				IPoint3D po = p as IPoint3D;
				
				SpellHelper.GetSurfaceTop( ref po );
				
				Effects.SendLocationEffect( po, Map, 0x3E27, 50 );
			}
			
			for ( int i = 0; i < m_East.Length; i += 2 ) 
			{
				Point3D p = Location;
				
				p.X += m_East[ i ];
				p.Y += m_East[ i + 1 ];
				
				IPoint3D po = p as IPoint3D;
				
				SpellHelper.GetSurfaceTop( ref po );
				
				Effects.SendLocationEffect( po, Map, 0x3E31, 50 );
			}
		}
		
		public override bool OnBeforeDeath()
		{			
			if ( m_Timer != null )
				m_Timer.Stop();
					
			return base.OnBeforeDeath();
		}
		
		public override void OnAfterDelete()
		{					
			if ( m_Timer != null )
				m_Timer.Stop();
				
			base.OnAfterDelete();	
		}
		
		public virtual void Damage( int amount, Mobile from )
		{
			base.Damage( amount, from );
						
			if ( Combatant == null || Hits > HitsMax * 0.2 || Utility.RandomBool() )
				return;	
							
			new InvisibilitySpell( this, null ).Cast();
			
			Target target = Target;
			
			if ( target != null )
				target.Invoke( this, this );
				
			Timer.DelayCall( TimeSpan.FromSeconds( 1 ), new TimerCallback( Teleport ) );
		}
		
		public virtual void Teleport()
		{				
			if ( Combatant == null )
				return;
						
			// 20 tries to teleport
			for ( int tries = 0; tries < 20; tries ++ )
			{
				int x = Utility.RandomMinMax( 5, 7 ); 
				int y = Utility.RandomMinMax( 5, 7 );
				
				if ( Utility.RandomBool() )
					x *= -1;
					
				if ( Utility.RandomBool() )
					y *= -1;
				
				Point3D p = new Point3D( X + x, Y + y, 0 );
				IPoint3D po = new LandTarget( p, Map ) as IPoint3D;
				
				if ( po == null )
					continue;
					
				SpellHelper.GetSurfaceTop( ref po );

				if ( InRange( p, 12 ) && InLOS( p ) && Map.CanSpawnMobile( po.X, po.Y, po.Z ) )
				{					
					
					Point3D from = Location;
					Point3D to = new Point3D( po );
	
					Location = to;
					ProcessDelta();
					
					FixedParticles( 0x376A, 9, 32, 0x13AF, EffectLayer.Waist );
					PlaySound( 0x1FE );
										
					return;					
				}
			}		
			
			RevealingAction();
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
		}

		private class ClonedItem : Item
		{	
			public ClonedItem( Item oItem ) : base( oItem.ItemID )
			{
				Name = oItem.Name;
				Weight = oItem.Weight;
				Hue = oItem.Hue;
				Layer = oItem.Layer;
			}
	
			public override DeathMoveResult OnParentDeath( Mobile parent )
			{
			        return DeathMoveResult.RemainEquiped;
			}
			
			public override DeathMoveResult OnInventoryDeath( Mobile parent )
			{
			        Delete();
			        return base.OnInventoryDeath( parent );
			}
	
			public ClonedItem( Serial serial ) : base( serial )
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
			}
		}
	}
}


using System;
using Server;
using System.Collections;
using Server.Targeting;

namespace Server.Items
{

	public class CorkScrew : Item, IUsesRemaining
	{
		
		private int m_UsesRemaining;

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		public bool ShowUsesRemaining{ get{ return true; } set{} }
		
		[Constructable]
		public CorkScrew() : base( 0x1029 )
		{
	     	Name = "a corkscrew";
	     	Weight = 1.0;
	     	UsesRemaining = 50;
	     	
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
		
			list.Add( 1060584, m_UsesRemaining.ToString() ); // uses remaining: ~1_val~
					
		}
		
		
		public override void OnDoubleClick( Mobile from )
		{
			
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if ( UsesRemaining <= 0 )
				from.SendLocalizedMessage( 1019073 ); // This item is out of charges.
			else
			{
				from.SendMessage( "Select the potion or container of potions to empty." );
				from.Target = new ScrewTarget( this );
				
			}
		}
		
		public CorkScrew( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_UsesRemaining );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_UsesRemaining = reader.ReadInt();
		}
		
		public static void ConsumeCharge( Mobile from, CorkScrew screw )
		{
			
			screw.UsesRemaining -= 1;

			if ( screw.UsesRemaining == 0 )
			{
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool!.
				screw.Delete();
			}
		}
	
	
	private class ScrewTarget : Target
	{
		CorkScrew m_screw;
		
		public ScrewTarget( CorkScrew screw ) : base( 2, false, TargetFlags.None )
		{
			m_screw = screw;
		}
		protected override void OnTarget( Mobile from, object target ) 
		{
			if ( target is BasePotion )
			{
				BasePotion p = (BasePotion)target;
									
				Bottle bottle = new Bottle();
					from.AddToBackpack( bottle );
					p.Delete();
					from.SendMessage( "You have dumped the potion out, and put the empty bottle in your pack." );
					ConsumeCharge( from, m_screw );
				
				
				
			}
			else if ( target is Container )
			{
				Container c = (Container)target;
				BasePotion p;
				bool cont = true;
				int amount = 0;
				if ( c.FindItemByType( typeof (BasePotion) ) != null )
				 {
					amount = c.GetAmount( typeof (BasePotion) );
					while ( cont )
				    	{
							p = (BasePotion)c.FindItemByType( typeof (BasePotion) );
							p.Delete();
				    		if ( c.FindItemByType( typeof (BasePotion) ) == null )
				    			cont = false;
				    	}
				    	c.DropItem( new Bottle( amount ) );
						from.SendMessage( "The potions have been emptied." );
				    	ConsumeCharge( from, m_screw );
				}
				else 
					from.SendMessage( "That container holds no potions!" );
			}
			else
				from.SendMessage( "You may only use this on a full potion or a container with potions in it!" );
			
		}
		
	}
	
	
  }
}

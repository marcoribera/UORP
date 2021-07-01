using System;
using Server;
using Server.Mobiles;
using Server.Network;
using System.Collections;

namespace Server.Items
{
	public class RingOfTheDruid : GoldRing
	{
		//public override int LabelNumber{ get{ return 1061102; } } // Ring of the Vile
		public override int ArtifactRarity{ get{ return 50; } }
		
		private Mobile      m_Owner;  // i added for ownership

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}
		[Constructable]
		public RingOfTheDruid()
		{
			
			Name = "Anel do Druida";
			Hue = 1274;
			SkillBonuses.SetValues( 0, SkillName.Adestramento, 0.0 );
		
		}

		public RingOfTheDruid( Serial serial ) : base( serial )
		{
		}
		public override bool OnEquip( Mobile from ) 
	{
 
                if( m_Owner == null )
			{
				m_Owner = from;
				base.OnEquip( from );
				from.FollowersMax = 7;
				from.SendMessage( "Esse anel agora pertence a você!" );
			}
			else if( from == m_Owner || from.AccessLevel >= AccessLevel.GameMaster )

			{
				base.OnEquip( from );
				from.FollowersMax = 5;
				from.SendMessage( "Você tem 5 seguidores" );
			}
			else
			{
				from.SendMessage( "Esse anel não pertence a você." );
				return false;
			}
			return true;
	}
		public override void OnRemoved( object parent )
		{ 

      if ( parent is Mobile ) 		                      
            {       
			((Mobile)parent).FollowersMax = 5;
            ((Mobile)parent).SendMessage("Você tem 5 seguidores");	
			} 
   

                        
		 } 

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}

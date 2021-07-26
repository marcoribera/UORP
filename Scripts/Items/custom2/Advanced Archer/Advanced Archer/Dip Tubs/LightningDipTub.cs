using System;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.ContextMenus;
using System.Collections.Generic;

namespace Server.Items
{
    public class LightningDipTub : Item
	{
		private int i_Owner, i_Charges;
		
		public int Owner { get{ return i_Owner; } set{ i_Owner = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges { get{ return i_Charges; } set{ i_Charges = value; InvalidateProperties(); } }
		
		[Constructable]
		public LightningDipTub() : this ( 50, 0 )
		{
		}
		
		[Constructable]
		public LightningDipTub( int charges, int owner ) : base ( 0xFAB )
		{
			Name = "Lightning Dip Tub";
			Charges = charges;
			Owner = owner;
			Hue = 1174;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				if (i_Charges >= 1 )
					from.Target = new InternalTarget( this );
				
				else if ( i_Charges < 1 )
					from.SendMessage( "You don't have enough charges." );
			}
		}
		
		public LightningDipTub( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			
			writer.Write( (int) i_Charges );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			i_Charges = reader.ReadInt();
		}
		
		public override void AddNameProperties( ObjectPropertyList list )
		{
			AddNameProperty( list );
			
			if (Charges < 1)
				list.Add("{0} without charges", Name);
			else
				list.Add("{0} with {1} charges", Name, Charges);
			
			if ( IsSecure )
				AddSecureProperty( list );
			else if ( IsLockedDown )
				AddLockedDownProperty( list );
			
			if ( DisplayLootType )
				AddLootTypeProperty( list );
		}
		
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
		}
		
		
		private class InternalTarget : Target
		{
			private LightningDipTub it_Tub;
			
			public InternalTarget( LightningDipTub tub ) : base( 1, false, TargetFlags.None )
			{
				it_Tub = tub;
			}
			
			protected override void OnTarget( Mobile from, object targeted )
			{
				Item item = targeted as Item;
				Arrow arrow = targeted as Arrow;
				Bolt bolt = targeted as Bolt;

                if (targeted is PlayerMobile || targeted is BaseCreature)
                {
                    from.SendMessage("You cannot target that.");
                    return;
                }
                if (targeted != null && targeted is ALightningArrow || targeted is LightningBolt)
                {
                    from.SendMessage("You cannot add any more electrical material.");
                    return;
                }
                if (targeted != null && targeted is Arrow)
                {
                    if (it_Tub != null)
                    {
                        if (!(item.IsChildOf(from.Backpack)))
                        {
                            from.SendMessage("This must be in your backpack.");
                        }
                        else
                        {
                            it_Tub.Charges--;
                            int amount = arrow.Amount;
                            int amarrow = amount;
                            from.AddToBackpack(new ALightningArrow(amarrow));
                            from.SendMessage("You carefully apply the electrical material to your arrows.");
                            arrow.Delete();
                        }
                    }
                    else
                        return;
                }
                else if (targeted != null && targeted is Bolt)
                {
                    if (it_Tub != null)
                    {
                        if (!(item.IsChildOf(from.Backpack)))
                        {
                            from.SendMessage("This must be in your backpack.");
                        }
                        else
                        {
                            it_Tub.Charges--;
                            int amount = bolt.Amount;
                            int ambolt = amount;
                            from.AddToBackpack(new LightningBolt(ambolt));
                            from.SendMessage("You carefully apply the electrical material to your bolts.");
                            bolt.Delete();
                        }
                    }
                    else
                        return;
                }
                else
                    return;
			}
		}
	}
}

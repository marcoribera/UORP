using System;
using System.Collections;
using System.Reflection;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Regions;

namespace Server.Commands
{
	public class CloneMe
	{
		public static void Initialize()
		{
			CommandSystem.Register( "CloneMe", AccessLevel.GameMaster, new CommandEventHandler( CloneMe_OnCommand ) );
                        CommandSystem.Register( "Refresh", AccessLevel.GameMaster, new CommandEventHandler( Refresh_OnCommand ) );
                }
		
		[Usage( "CloneMe" )]
		[Description( "Makes an exact duplicate of you at your present location and hides you" )]
                 public static void CloneMe_OnCommand( CommandEventArgs e )
		{
                    Cloner m = new Cloner();
                    e.Mobile.Hidden = true;
                    m.Dex = e.Mobile.Dex;
                    m.Int = e.Mobile.Int;
                    m.Str = e.Mobile.Str;
                    m.Fame = e.Mobile.Fame;
                    m.Karma = e.Mobile.Karma;
                    m.NameHue = e.Mobile.NameHue;
                    m.SpeechHue = e.Mobile.SpeechHue;
                    m.Criminal = e.Mobile.Criminal;
                    m.Name = e.Mobile.Name;
                    m.Title = e.Mobile.Title;
                    m.Female = e.Mobile.Female;
                    m.Body = e.Mobile.Body;
                    m.Hue = e.Mobile.Hue;
                    m.Hits = e.Mobile.HitsMax;
                    m.Mana = e.Mobile.ManaMax;
                    m.Stam = e.Mobile.StamMax;
                    m.BodyMod = e.Mobile.Body;
                    //m.Controled = true;  <-----this can be uncommented (and next line) and it will become tame to you (although it says tame above it)
                    //m.ControlMaster = e.Mobile;  <-----this can be uncommented and it will become tame to  you
                    m.Map = e.Mobile.Map;
                    m.Location = e.Mobile.Location;
                    m.Direction = e.Mobile.Direction;
                    
                    for (int i=0; i<e.Mobile.Skills.Length; i++)
                     m.Skills[i].Base = e.Mobile.Skills[i].Base;

                  // This code block duplicates all equiped items
                  ArrayList items = new ArrayList( e.Mobile.Items );
                  for (int i=0; i<items.Count; i++)
                  {
                     Item item = (Item)items[i]; 
                     if((( item != null ) && ( item.Parent == e.Mobile ) && ( item != e.Mobile.Backpack )))
                     {
                        Type type = item.GetType();
                        Item newitem = Loot.Construct( type );
                        CopyProperties( newitem, item );
                        m.AddItem( newitem );
                     }
                  }
                  // This code block duplicates all pack items
                     /*Container pack = e.Mobile.Backpack;
                     if( pack != null )
                     {
                        ArrayList t_items = new ArrayList( pack.Items );
                        for (int i=0; i<t_items.Count; i++)
                        {
                           Item item = (Item)t_items[i];
                           if(( item != null ) && ( item.IsChildOf( pack )))
                           {
                              Type type = item.GetType();
                              Item newitem = Loot.Construct( type );
                              CopyProperties( newitem, item );
                              m.PlaceInBackpack( newitem );
                           }
                        }
                     }*/
                  }

private static void CopyProperties ( Item dest, Item src )
   {
      PropertyInfo[] props = src.GetType().GetProperties();

      for ( int i = 0; i < props.Length; i++ )
     {
         try
         {
            if ( props[i].CanRead && props[i].CanWrite )
            {
               props[i].SetValue( dest, props[i].GetValue( src, null ), null );
            }
         }
         catch
         {
         }
      }
   }
   
                [Usage( "Refresh" )]
		[Description( "Sets all targets stats to full." )]
  public static void Refresh_OnCommand( CommandEventArgs e )
		{
                        e.Mobile.Target = new freshTarget();
                }
        public class freshTarget : Target
        {
          public freshTarget() : base( 12, false, TargetFlags.None )
        {
        }
        protected override void OnTarget( Mobile from, object targeted )
        {
           if ( targeted is Mobile )
            {
                Mobile targ = (Mobile)targeted;
            if ( !from.CanSee( targ ) )
            {
           from.SendMessage( "The target is not in your line of sight!" );
            }
            else
            {
                targ.Hits = targ.HitsMax ;
                targ.Mana = targ.ManaMax ;
                targ.Stam = targ.StamMax ;
                targ.Hunger = 20 ;
                targ.Thirst = 20 ;
           }
           }
       }
    }
   }
}



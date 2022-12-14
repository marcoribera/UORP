using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Regions;
using Server.Mobiles;

namespace Server.Spells.Cosmos
{
	public class ReplicarSpell : CosmosSpell
	{
        private static readonly SpellInfo m_Info = new SpellInfo(
           "Replicar", "Replicare",
           203,
           0,
           Reagent.DragonBlood, 
           Reagent.Vela,
           Reagent.Incenso,
           Reagent.PenaETinteiro);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias


        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(0.5); } }



        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Eighth;
            }
        }

        public ReplicarSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

        public override void OnCast()
		{
			if ( CheckFizzle() )
			{
				if (Caster.Backpack == null)
					return;

				ArrayList targets = new ArrayList();
				foreach ( Item item in Caster.Backpack.Items )
				if ( item is SoulOrb )
				{
					SoulOrb myOrb = (SoulOrb)item;
					if ( myOrb.m_Owner == Caster )
					{
						targets.Add( item );
					}
				}
				for ( int i = 0; i < targets.Count; ++i )
				{
					Item item = ( Item )targets[ i ];
					item.Delete();
				}

				Caster.PlaySound( 0x244 );
				Effects.SendLocationEffect(Caster.Location, Caster.Map, 0x373A, 15, 0, 0);
				Caster.SendMessage("Você cria um cristal de replicação com seu essência cósmica.");
				SoulOrb iOrb = new SoulOrb();
				iOrb.m_Owner = Caster;
				iOrb.Name = "Cristal de Replicação";
				iOrb.ItemID = 0x1F1C;
				Caster.AddToBackpack( iOrb );
				Server.Items.SoulOrb.OnSummoned( Caster, iOrb );
			}
		}
	}
}

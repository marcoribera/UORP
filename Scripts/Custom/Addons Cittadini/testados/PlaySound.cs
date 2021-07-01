using System;
using System.Reflection;
using Server.Items;
using Server.Targeting;

namespace Server.Commands
{
    public class PlaySoundCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register( "PlaySound", AccessLevel.GameMaster, new CommandEventHandler( PlaySound_OnCommand ) );
		}

		[Usage( "PlaySound [soundid]" )]
		[Description( "Plays a sound." )]
		private static void PlaySound_OnCommand( CommandEventArgs e )
		{
			int soundId = 1;
			if ( e.Length >= 1 )
                soundId = e.GetInt32(0);
            e.Mobile.Target = new PlaySoundTarget(soundId);
			e.Mobile.SendMessage( "Play sound on who?" );
		}

		private class PlaySoundTarget : Target
		{
			private int m_SoundId;

            public PlaySoundTarget(int soundId)
				: base( 15, false, TargetFlags.None )
			{
                m_SoundId = soundId;
			}

			protected override void OnTarget( Mobile from, object targ )
			{
                if (!(targ is Mobile))
                {
                    from.SendMessage("You can only play sounds on mobiles.");
                    return;
                }
                else
                {
                    Mobile mob = targ as Mobile;

                    mob.PlaySound(m_SoundId);
                }
			}
		}
	}
}

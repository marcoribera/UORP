using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
    [Flipable(0xF61, 0xF60)]
    public class espadafalante : BaseSword
    {
    private DateTime timeTalk = DateTime.Now;

        [Constructable]
        public espadafalante()
            : base(0xF61)
        {
            Name = "Lilarcor";
	 	 	EngravedText = "You talking to me?";
	 	 	Hue = 2406;
	 	 	Slayer = SlayerName.DragonSlaying;
	 	 	Attributes.AttackChance = 10;
	 	 	Attributes.DefendChance = 10;
	 	 	Attributes.WeaponDamage = 50;
	 	 	Attributes.WeaponSpeed = 35;
			
			WeaponAttributes.HitDispel = 100;
	 	 	WeaponAttributes.HitLowerDefend = 25;
			WeaponAttributes.HitLowerAttack = 25;
	 	 	WeaponAttributes.HitFireball = 10;
            Weight = 7.0;
        }

        public espadafalante(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.ArmorIgnore;
        public override WeaponAbility SecondaryAbility => WeaponAbility.ConcussionBlow;
        public override int StrengthReq => 35;
        public override int MinDamage => 14;
        public override int MaxDamage => 18;
        public override float Speed => 3.50f;

        public override int DefHitSound => 0x237;
        public override int DefMissSound => 0x23A;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 110;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

                public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

		public override void OnHit(Mobile attacker, IDamageable defender, double damage)
			{ 
				base.OnHit( attacker, defender, damage ); 
				//Speech Trigger
				if( DateTime.Now >= timeTalk ) 
					{ 
						switch ( Utility.Random( 31 )) 
						{ 
							case 0: attacker.Say( "So, are we gonna kill something now?" ); break; 
							case 1: attacker.Say( "I am invincible, INVINCIBLE I say! *laughs*" ); break; 
							case 2: attacker.Say( "I know! Find someone rich, and kill them! Then find someone richer, and kill them too! Hack and slash your way to fortune! Whoo-hoo!!" ); break; 
							case 3: attacker.Say( "I'm sharp, I can come up with something... OK... find someone who knows what you want to know and threaten to kill them! Yeah! Then kill them! Woo-hoo!!!" ); break; 
							case 4: attacker.Say( "I know! Start swinging! Eventually you'll lop off the head of someone important and then the good fights will REALLY start!" ); break; 
							case 5: attacker.Say( "You know, once, long time ago, I was, like, a Moonblade." ); break; 
							case 6: attacker.Say( "Err... find that wizard guy. Yeah... find him and kill him. Kill kill kill kill KILL!! Whoo-hoo!!" ); break; 
							case 7: attacker.Say( "My brother's a +12 Hackmaster!" ); break; 
							case 8: attacker.Say( "Choke up, dolt, your grip's all-wrong!" ); break; 
							case 9: attacker.Say( "What's my status? Since when do you care about me unless I'm impaled in something's guts? Oh well, fine, let me think for a minute... Well, as a matter of fact I would like to register a complaint." ); break;
							case 10: attacker.Say( "Murder! Death! Kill! Murder! Death! Kill! Bouah-ha-ha-ha!" ); break; 
							case 11: attacker.Say( "Hands up, kiddies, who wants to die?" ); break; 
							case 12: attacker.Say( "(sigh) ...come on..." ); break; 
							case 13: attacker.Say( "(double sigh) Rassa-frackin' (grumbling) c'mon-c'mon-C'MON!!!" ); break; 
							case 14: attacker.Say( "Come get some! Boo-yah!" ); break; 
							case 15: attacker.Say( "YOINK!!! Got your nose!" ); break; 
							case 16: attacker.Say( "You really need to clean me. I like to shine! Ha-ha-ha!" ); break; 
							case 17: attacker.Say( "Kill it! Kill it quick before they're all gone!" ); break; 
							case 18: attacker.Say( "Mwoo-ha-ha-ha-ha-ha-ha!" ); break;
							case 19: attacker.Say( "Swish! Hot butta!" ); break;  
							case 20: attacker.Say( "I refuse to answer any more questions until I'm cleaned and polished thoroughly. Grab a rag already!" ); break;
							case 21: attacker.Say( "I'm the best at what I do, and what I do ain't pretty! *laughs*" ); break;
							case 22: attacker.Say( "I think you need to take better care of me. I've got more chips than a blind beaver! I look like a second rate pig-poker!" ); break;
							case 23: attacker.Say( "You talking to me?" ); break;
							case 24: attacker.Say( "I love the smell of daisies in the morning!" ); break;
							case 25: attacker.Say( "Listen beefy, I may be an intelligent sword, but I've had no formal edjumacation." ); break;
							case 26: attacker.Say( "You know, my last owner always said I was 'sharp' and 'edgy'. He was such an ass." ); break;
							case 27: attacker.Say( "And that's for grandma, who said I'd never amount to anything more than a butterknife!" ); break;
							case 28: attacker.Say( "Kill! Kill! Kill! Yeah cool!!" ); break;
							case 29: attacker.Say( "I don't know what you have expected, but as a sword I'm pretty one-dimensional in what I waaant!!!" ); break;
							case 30: attacker.Say( "I don't chop wood, OK? I'm not an axe!" ); break;
							case 31: attacker.Say( "I want to kill a dragon. Right now. Go find one and kill it. That would be SO cool." ); break;						
						}
					
					timeTalk = DateTime.Now + TimeSpan.FromSeconds(7.0);
                  
                 }
            }
        }
    }
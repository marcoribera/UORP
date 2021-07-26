using System;
using Server.Items;
using Server.Network;
using Server.Spells;
using Server.Mobiles;

namespace Server.Items
{
	public abstract class CBaseRanged : BaseMeleeWeapon
	{
		public static readonly TimeSpan PlayerFreezeDuration = TimeSpan.FromSeconds(3.0);
		public static readonly TimeSpan NPCFreezeDuration = TimeSpan.FromSeconds(6.0);
		
		public abstract int EffectID { get; }
		public abstract Type AmmoType { get; }
		public abstract Item Ammo { get; }
		public override int DefHitSound { get { return 0x234; } }
		public override int DefMissSound { get { return 0x238; } }
		public override SkillName DefSkill { get { return SkillName.Atirar; } }
		public override WeaponType DefType { get { return WeaponType.Ranged; } }
		public override WeaponAnimation DefAnimation { get { return WeaponAnimation.ShootXBow; } }
		public override SkillName AccuracySkill { get { return SkillName.Atirar; } }
        private Timer m_RecoveryTimer;
        private bool m_Balanced;
        private int m_Velocity;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Balanced
        {
            get { return m_Balanced; }
            set
            {
                m_Balanced = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Velocity
        {
            get { return m_Velocity; }
            set
            {
                m_Velocity = value;
                InvalidateProperties();
            }
        }

		public CBaseRanged( int itemID ) : base( itemID )
		{
            #region//Colored Item Name Mod Start
            Resource = CraftResource.RegularWood;
            #endregion//Colored Item Name Mod Start[/B]
		}
		
		public CBaseRanged(Serial serial) : base( serial )
		{
		}
		
		public virtual TimeSpan OnSwing(Mobile attacker, Mobile defender)
		{
                // Make sure we've been standing still for one second
                if (Core.TickCount - attacker.LastMoveTime >= (Core.SE ? 250 : Core.AOS ? 500 : 1000) ||
                    (Core.AOS && WeaponAbility.GetCurrentAbility(attacker) is MovingShot))
                {
                    bool canSwing = true;

                    if (Core.AOS)
                    {
                        canSwing = (!attacker.Paralyzed && !attacker.Frozen);

                        if (canSwing)
                        {
                            Spell sp = attacker.Spell as Spell;

                            canSwing = (sp == null || !sp.IsCasting || !sp.BlocksMovement);
                        }
                    }

                   

                    if (canSwing && attacker.HarmfulCheck(defender))
                    {
                        attacker.DisruptiveAction();
                        attacker.Send(new Swing(0, attacker, defender));

                        if (OnFired(attacker, defender))
                        {
                            if (CheckHit(attacker, defender))
                            {
                                OnHit(attacker, defender);
                            }
                            else
                            {
                                OnMiss(attacker, defender);
                            }
                        }
                    }

                    attacker.RevealingAction();

                    return GetDelay(attacker);
                }

                attacker.RevealingAction();

                return TimeSpan.FromSeconds(0.25);
		}
		
        public virtual void OnHit(Mobile attacker, Mobile defender, double damageBonus)
		{
			if ( attacker == null || defender == null )
				return;
	
			if (AmmoType == typeof( PoisonArrow ) )
			{
                if( attacker.Skills[SkillName.Atirar].Value >= 120 )
				{
					if ( 0.30 > Utility.RandomDouble() )
					{
						defender.ApplyPoison(defender, Poison.Deadly);
						defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
					}
					else if ( 0.50 > Utility.RandomDouble() )
					{
						defender.ApplyPoison(defender, Poison.Greater);
						defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
					}
					else if ( 0.40 > Utility.RandomDouble() )
					{
						defender.ApplyPoison(defender, Poison.Regular);
						defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
					}
				}				
				else if( attacker.Skills[SkillName.Atirar].Value >= 100 )
				{
					if ( 0.30 > Utility.RandomDouble() )
					{
						defender.ApplyPoison(defender, Poison.Greater);
						defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
					}
					else if ( 0.60 > Utility.RandomDouble() )
					{
						defender.ApplyPoison(defender, Poison.Regular);
						defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
					}
				}
				
				else if( attacker.Skills[SkillName.Atirar].Value >= 70 )
				{
					if ( 0.10 > Utility.RandomDouble() )
					{
						defender.ApplyPoison(defender, Poison.Greater);
						defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
					}
					else if ( 0.30 > Utility.RandomDouble() )
					{
						defender.ApplyPoison(defender, Poison.Regular);
						defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
					}
				}
				
				else if( attacker.Skills[SkillName.Atirar].Value >= 50 )
				{
					if ( 0.50 > Utility.RandomDouble() )
					{
						defender.ApplyPoison(defender, Poison.Regular);
						defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
					}
					else if ( 0.50 > Utility.RandomDouble() )
					{
						defender.ApplyPoison(defender, Poison.Lesser);
						defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
					}
				}
				
				else if( attacker.Skills[SkillName.Atirar].Value < 50 )
				{
					defender.ApplyPoison(defender, Poison.Lesser);
					defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
				}
                /*
				switch (Utility.Random(4))
				{
						case 0: defender.ApplyPoison(defender, Poison.Deadly);
						defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist); break;
						case 1: defender.ApplyPoison(defender, Poison.Greater);
						defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist); break;
						case 2: defender.ApplyPoison(defender, Poison.Regular);
						defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist); break;
						case 3: defender.ApplyPoison(defender, Poison.Lesser);
						defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist); break;
				}
                 */
			}
			else if (AmmoType == typeof( PoisonBolt ) )
			{
                if (attacker.Skills[SkillName.Atirar].Value >= 120)
                {
                    if (0.30 > Utility.RandomDouble())
                    {
                        defender.ApplyPoison(defender, Poison.Deadly);
                        defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
                    }
                    else if (0.40 > Utility.RandomDouble())
                    {
                        defender.ApplyPoison(defender, Poison.Greater);
                        defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
                    }
                    else if (0.30 > Utility.RandomDouble())
                    {
                        defender.ApplyPoison(defender, Poison.Regular);
                        defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
                    }
                }

                else if (attacker.Skills[SkillName.Atirar].Value >= 100)
                {
                    if (0.30 > Utility.RandomDouble())
                    {
                        defender.ApplyPoison(defender, Poison.Greater);
                        defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
                    }
                    else if (0.60 > Utility.RandomDouble())
                    {
                        defender.ApplyPoison(defender, Poison.Regular);
                        defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
                    }
                }

                else if (attacker.Skills[SkillName.Atirar].Value >= 70)
                {
                    if (0.10 > Utility.RandomDouble())
                    {
                        defender.ApplyPoison(defender, Poison.Greater);
                        defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
                    }
                    else if (0.30 > Utility.RandomDouble())
                    {
                        defender.ApplyPoison(defender, Poison.Regular);
                        defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
                    }
                    else if (0.60 > Utility.RandomDouble())
                    {
                        defender.ApplyPoison(defender, Poison.Lesser);
                        defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
                    }
                }

                else if (attacker.Skills[SkillName.Atirar].Value >= 50)
                {
                    if (0.50 > Utility.RandomDouble())
                    {
                        defender.ApplyPoison(defender, Poison.Regular);
                        defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
                    }
                    else if (0.50 > Utility.RandomDouble())
                    {
                        defender.ApplyPoison(defender, Poison.Lesser);
                        defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
                    }
                }

                else if (attacker.Skills[SkillName.Atirar].Value < 50)
                {
                    defender.ApplyPoison(defender, Poison.Lesser);
                    defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist);
                }
                /*
				switch (Utility.Random(4))
				{
						case 0: defender.ApplyPoison(defender, Poison.Deadly);
						defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist); break;
						case 1: defender.ApplyPoison(defender, Poison.Greater);
						defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist); break;
						case 2: defender.ApplyPoison(defender, Poison.Regular);
						defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist); break;
						case 3: defender.ApplyPoison(defender, Poison.Lesser);
						defender.FixedParticles(0x3728, 200, 25, 69, EffectLayer.Waist); break;
				}
                 */
			}
			else if (AmmoType == typeof( ExplosiveArrow ) )
			{
                if (attacker.Skills[SkillName.Atirar].Value >= 120)
                {
                    defender.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Waist);
                    defender.PlaySound(0x307);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(20, 80), 0, 100, 0, 0, 0);
                }

                if (attacker.Skills[SkillName.Atirar].Value >= 60)
                {
                    defender.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Waist);
                    defender.PlaySound(0x307);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(10, 40), 0, 100, 0, 0, 0);
                }

                if (attacker.Skills[SkillName.Atirar].Value < 60)
                {
                    defender.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Waist);
                    defender.PlaySound(0x307);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(5, 20), 0, 100, 0, 0, 0);
                }
                /*
				switch (Utility.Random(3))
				{
						case 0: defender.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Waist);
						defender.PlaySound(0x307);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(10, 50), 0, 100, 0, 0, 0); break;
						
						case 1: defender.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Waist);
						defender.PlaySound(0x307);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(30, 70), 0, 100, 0, 0, 0); break;
						
						case 2: defender.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Waist);
						defender.PlaySound(0x307);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(50, 100), 0, 100, 0, 0, 0); break;
				}
                 */
				
			}
			else if (AmmoType == typeof( ExplosiveBolt ) )
			{
                if (attacker.Skills[SkillName.Atirar].Value >= 120)
                {
                    defender.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Waist);
                    defender.PlaySound(0x307);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(20, 80), 0, 100, 0, 0, 0);
                }

                else if (attacker.Skills[SkillName.Atirar].Value >= 60)
                {
                    defender.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Waist);
                    defender.PlaySound(0x307);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(10, 40), 0, 100, 0, 0, 0);
                }

                else if (attacker.Skills[SkillName.Atirar].Value < 60)
                {
                    defender.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Waist);
                    defender.PlaySound(0x307);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(5, 20), 0, 100, 0, 0, 0);
                }
                /*
				switch (Utility.Random(3))
				{
						case 0: defender.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Waist);
						defender.PlaySound(0x307);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(10, 50), 0, 100, 0, 0, 0); break;
						
						case 1: defender.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Waist);
						defender.PlaySound(0x307);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(30, 70), 0, 100, 0, 0, 0); break;
						
						case 2: defender.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Waist);
						defender.PlaySound(0x307);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(50, 100), 0, 100, 0, 0, 0); break;
				}
				*/
			}
			else if (AmmoType == typeof( ArmorPiercingArrow ) )
			{
                if (attacker.Skills[SkillName.Atirar].Value >= 120)
                {
                    defender.PlaySound(0x56);
                    defender.FixedParticles(0x3728, 200, 25, 9942, EffectLayer.Waist);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(20, 80), 100, 0, 0, 0, 0);
                }

                else if (attacker.Skills[SkillName.Atirar].Value >= 60)
                {
                    defender.PlaySound(0x56);
                    defender.FixedParticles(0x3728, 200, 25, 9942, EffectLayer.Waist);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(10, 40), 100, 0, 0, 0, 0);
                }

                else if (attacker.Skills[SkillName.Atirar].Value < 60)
                {
                    defender.PlaySound(0x56);
                    defender.FixedParticles(0x3728, 200, 25, 9942, EffectLayer.Waist);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(5, 20), 100, 0, 0, 0, 0);
                }
                /*
				switch (Utility.Random(3))
				{
						case 0: defender.PlaySound(0x56);
						defender.FixedParticles(0x3728, 200, 25, 9942, EffectLayer.Waist);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(10, 50), 100, 0, 0, 0, 0); break;
						
						case 1: defender.PlaySound(0x56);
						defender.FixedParticles(0x3728, 200, 25, 9942, EffectLayer.Waist);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(30, 70), 100, 0, 0, 0, 0); break;
						
						case 2: defender.PlaySound(0x56);
						defender.FixedParticles(0x3728, 200, 25, 9942, EffectLayer.Waist);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(50, 100), 100, 0, 0, 0, 0); break;
				}
                 */
			}
			else if (AmmoType == typeof( ArmorPiercingBolt ) )
			{
                if (attacker.Skills[SkillName.Atirar].Value >= 120)
                {
                    defender.PlaySound(0x56);
                    defender.FixedParticles(0x3728, 200, 25, 9942, EffectLayer.Waist);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(20, 80), 100, 0, 0, 0, 0);
                }

                else if (attacker.Skills[SkillName.Atirar].Value >= 60)
                {
                    defender.PlaySound(0x56);
                    defender.FixedParticles(0x3728, 200, 25, 9942, EffectLayer.Waist);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(10, 40), 100, 0, 0, 0, 0);
                }

                else if (attacker.Skills[SkillName.Atirar].Value < 60)
                {
                    defender.PlaySound(0x56);
                    defender.FixedParticles(0x3728, 200, 25, 9942, EffectLayer.Waist);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(5, 20), 100, 0, 0, 0, 0);
                }
                /*
				switch (Utility.Random(3))
				{
						case 0: defender.PlaySound(0x56);
						defender.FixedParticles(0x3728, 200, 25, 9942, EffectLayer.Waist);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(10, 50), 100, 0, 0, 0, 0); break;
						
						case 1: defender.PlaySound(0x56);
						defender.FixedParticles(0x3728, 200, 25, 9942, EffectLayer.Waist);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(30, 70), 100, 0, 0, 0, 0); break;
						
						case 2: defender.PlaySound(0x56);
						defender.FixedParticles(0x3728, 200, 25, 9942, EffectLayer.Waist);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(50, 100), 100, 0, 0, 0, 0); break;
				}
                 */
			}
			else if (AmmoType == typeof( FreezeArrow ) )
			{
                if (attacker.Skills[SkillName.Atirar].Value >= 120)
                {
                    defender.PlaySound(0x204);
                    defender.Freeze(defender.Player ? PlayerFreezeDuration : NPCFreezeDuration);
                    defender.FixedEffect(0x376A, 9, 32);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(20, 80), 0, 0, 100, 0, 0);
                }

                else if (attacker.Skills[SkillName.Atirar].Value >= 60)
                {
                    defender.PlaySound(0x204);
                    defender.Freeze(defender.Player ? PlayerFreezeDuration : NPCFreezeDuration);
                    defender.FixedEffect(0x376A, 9, 32);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(10, 40), 0, 0, 100, 0, 0);
                }

                else if (attacker.Skills[SkillName.Atirar].Value < 60)
                {
                    defender.PlaySound(0x204);
                    defender.Freeze(defender.Player ? PlayerFreezeDuration : NPCFreezeDuration);
                    defender.FixedEffect(0x376A, 9, 32);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(5, 20), 0, 0, 100, 0, 0);
                }
                /*
				switch (Utility.Random(3))
				{
						case 0: defender.PlaySound(0x204);
						defender.Freeze(defender.Player ? PlayerFreezeDuration : NPCFreezeDuration);
						defender.FixedEffect(0x376A, 9, 32);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(10, 50), 0, 0, 100, 0, 0); break;
						
						case 1: defender.PlaySound(0x204);
						defender.Freeze(defender.Player ? PlayerFreezeDuration : NPCFreezeDuration);
						defender.FixedEffect(0x376A, 9, 32);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(30, 70), 0, 0, 100, 0, 0); break;
						
						case 2: defender.PlaySound(0x204);
						defender.Freeze(defender.Player ? PlayerFreezeDuration : NPCFreezeDuration);
						defender.FixedEffect(0x376A, 9, 32);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(50, 100), 0, 0, 100, 0, 0); break;
				}
                 */
			}
			else if (AmmoType == typeof( FreezeBolt ) )
			{
                if (attacker.Skills[SkillName.Atirar].Value >= 120)
                {
                    defender.PlaySound(0x204);
                    defender.Freeze(defender.Player ? PlayerFreezeDuration : NPCFreezeDuration);
                    defender.FixedEffect(0x376A, 9, 32);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(20, 80), 0, 0, 100, 0, 0);
                }

                else if (attacker.Skills[SkillName.Atirar].Value >= 60)
                {
                    defender.PlaySound(0x204);
                    defender.Freeze(defender.Player ? PlayerFreezeDuration : NPCFreezeDuration);
                    defender.FixedEffect(0x376A, 9, 32);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(10, 40), 0, 0, 100, 0, 0);
                }

                else if (attacker.Skills[SkillName.Atirar].Value < 60)
                {
                    defender.PlaySound(0x204);
                    defender.Freeze(defender.Player ? PlayerFreezeDuration : NPCFreezeDuration);
                    defender.FixedEffect(0x376A, 9, 32);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(5, 20), 0, 0, 100, 0, 0);
                }
                /*
				switch (Utility.Random(3))
				{
						case 0: defender.PlaySound(0x204);
						defender.Freeze(defender.Player ? PlayerFreezeDuration : NPCFreezeDuration);
						defender.FixedEffect(0x376A, 9, 32);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(10, 50), 0, 0, 100, 0, 0); break;
						
						case 1: defender.PlaySound(0x204);
						defender.Freeze(defender.Player ? PlayerFreezeDuration : NPCFreezeDuration);
						defender.FixedEffect(0x376A, 9, 32);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(30, 70), 0, 0, 100, 0, 0); break;
						
						case 2: defender.PlaySound(0x204);
						defender.Freeze(defender.Player ? PlayerFreezeDuration : NPCFreezeDuration);
						defender.FixedEffect(0x376A, 9, 32);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(50, 100), 0, 0, 100, 0, 0); break;
				}
                 */
			}
			else if (AmmoType == typeof( LightningArrow ) )
			{
                if (attacker.Skills[SkillName.Atirar].Value >= 120)
                {
                    defender.PlaySound(1471);
                    defender.BoltEffect(0);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(20, 80), 100, 0, 0, 0, 0);
                }

                else if (attacker.Skills[SkillName.Atirar].Value >= 60)
                {
                    defender.PlaySound(1471);
                    defender.BoltEffect(0);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(10, 40), 100, 0, 0, 0, 0);
                }

                else if (attacker.Skills[SkillName.Atirar].Value < 60)
                {
                    defender.PlaySound(1471);
                    defender.BoltEffect(0);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(5, 20), 100, 0, 0, 0, 0);
                }
                /*
				switch (Utility.Random(3))
				{
						case 0: defender.PlaySound(1471);
						defender.BoltEffect(0);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(10, 50), 100, 0, 0, 0, 0); break;
						
						case 1: defender.PlaySound(1471);
						defender.BoltEffect(0);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(30, 70), 100, 0, 0, 0, 0); break;
						
						case 2: defender.PlaySound(1471);
						defender.BoltEffect(0);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(50, 100), 100, 0, 0, 0, 0); break;
				}
                 */
			}
			else if (AmmoType == typeof( LightningBolt ) )
			{
                if (attacker.Skills[SkillName.Atirar].Value >= 120)
                {
                    defender.PlaySound(1471);
                    defender.BoltEffect(0);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(20, 80), 100, 0, 0, 0, 0);
                }

                else if (attacker.Skills[SkillName.Atirar].Value >= 60)
                {
                    defender.PlaySound(1471);
                    defender.BoltEffect(0);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(10, 40), 100, 0, 0, 0, 0);
                }

                else if (attacker.Skills[SkillName.Atirar].Value < 60)
                {
                    defender.PlaySound(1471);
                    defender.BoltEffect(0);
                    attacker.DoHarmful(defender);
                    AOS.Damage(defender, attacker, Utility.RandomMinMax(5, 20), 100, 0, 0, 0, 0);
                }
                /*
				switch (Utility.Random(3))
				{
						case 0: defender.PlaySound(1471);
						defender.BoltEffect(0);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(10, 50), 100, 0, 0, 0, 0); break;
						
						case 1: defender.PlaySound(1471);
						defender.BoltEffect(0);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(30, 70), 100, 0, 0, 0, 0); break;
						
						case 2: defender.PlaySound(1471);
						defender.BoltEffect(0);
						attacker.DoHarmful(defender);
						AOS.Damage(defender, attacker, Utility.RandomMinMax(50, 100), 100, 0, 0, 0, 0); break;
				}
                 */
			}
			
			else if (Ammo == null)
				attacker.SendMessage("You are out of arrows, or may have to choose a different type of arrow by double clicking the bow.");
			
			//if (attacker.Player && !defender.Player && (defender.Body.IsAnimal || defender.Body.IsMonster) && 0.4 >= Utility.RandomDouble())
				//defender.AddToBackpack(Ammo);

            if (AmmoType != null && attacker.Player && !defender.Player && (defender.Body.IsAnimal || defender.Body.IsMonster) && 0.4 >= Utility.RandomDouble())
            {
                defender.AddToBackpack(Ammo);
            }

            if (Core.ML && m_Velocity > 0)
            {
                int bonus = (int)attacker.GetDistanceToSqrt(defender);

                if (bonus > 0 && m_Velocity > Utility.Random(100))
                {
                    AOS.Damage(defender, attacker, bonus * 3, 100, 0, 0, 0, 0);

                    if (attacker.Player)
                    {
                        attacker.SendLocalizedMessage(1072794); // Your arrow hits its mark with velocity!
                    }

                    if (defender.Player)
                    {
                        defender.SendLocalizedMessage(1072795); // You have been hit by an arrow with velocity!
                    }
                }
            }

            base.OnHit(attacker, defender, damageBonus);
		}

        public virtual void OnMiss(Mobile attacker, Mobile defender)
        {
            if (attacker.Player && 0.4 >= Utility.RandomDouble())
            {
                if (Core.SE)
                {
                    PlayerMobile p = attacker as PlayerMobile;

                    if (p != null && AmmoType != null)
                    {
                        Type ammo = AmmoType;

                        if (p.RecoverableAmmo.ContainsKey(ammo))
                        {
                            p.RecoverableAmmo[ammo]++;
                        }
                        else
                        {
                            p.RecoverableAmmo.Add(ammo, 1);
                        }

                        if (!p.Warmode)
                        {
                            if (m_RecoveryTimer == null)
                            {
                                m_RecoveryTimer = Timer.DelayCall(TimeSpan.FromSeconds(10), p.RecoverAmmo);
                            }

                            if (!m_RecoveryTimer.Running)
                            {
                                m_RecoveryTimer.Start();
                            }
                        }
                    }
                }
                else
                {
                    Ammo.MoveToWorld(
                        new Point3D(defender.X + Utility.RandomMinMax(-1, 1), defender.Y + Utility.RandomMinMax(-1, 1), defender.Z),
                        defender.Map);
                }
            }

            base.OnMiss(attacker, defender);
        }

        public virtual bool OnFired(Mobile attacker, Mobile defender)
        {
            if (attacker.Player)
            {
                BaseQuiver quiver = attacker.FindItemOnLayer(Layer.Cloak) as BaseQuiver;
                Container pack = attacker.Backpack;

                if (quiver == null || Utility.Random(100) >= quiver.LowerAmmoCost)
                {
                    // consume ammo
                    if (quiver != null && quiver.ConsumeTotal(AmmoType, 1))
                    {
                        quiver.InvalidateWeight();
                    }
                    else if (pack == null || !pack.ConsumeTotal(AmmoType, 1))
                    {
                        return false;
                    }
                }
                else if (quiver.FindItemByType(AmmoType) == null && (pack == null || pack.FindItemByType(AmmoType) == null))
                {
                    // lower ammo cost should not work when we have no ammo at all
                    return false;
                }
            }

            attacker.MovingEffect(defender, EffectID, 18, 1, false, false);

            return true;
        }
		
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)3); // version
            writer.Write(m_Balanced);
            writer.Write(m_Velocity);
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			
			switch (version)
			{
                case 3:
                    {
                        m_Balanced = reader.ReadBool();
                        m_Velocity = reader.ReadInt();

                        goto case 2;
                    }
				case 2:
				case 1:
					{
						break;
					}
				case 0:
					{
						/*m_EffectID =*/
						reader.ReadInt();
						break;
					}
			}
			
			if (version < 2)
			{
				WeaponAttributes.MageWeapon = 0;
				WeaponAttributes.UseBestSkill = 0;
			}
		}
	}
}

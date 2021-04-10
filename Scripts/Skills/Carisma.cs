#region References

//Usados no begging
/*using System;

using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Targeting;
*/

using System;
using System.Collections;

using Server.Engines.XmlSpawner2;
using Server.Factions;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Spells.Spellweaving;
using Server.Targeting;

#endregion

namespace Server.SkillHandlers
{
    public class Carisma
    {
        //Marcknight: TODO: Fazer funcionar como animaltaming, mas para NPCs humanos e similares
        //Marcknight: TODO: Colocar no patch as traduções propostas
        private static readonly Hashtable m_BeingTamed = new Hashtable();

        public static bool DisableMessage { get; set; }
        public static bool DeferredTarget { get; set; }

        static Carisma()
        {
            DeferredTarget = true;
            DisableMessage = false;
        }

        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.Carisma].Callback = OnUse;
        }

        public static TimeSpan OnUse(Mobile m)
        {
            m.RevealingAction();

            if (!DisableMessage)
            {
                m.SendLocalizedMessage(502789); // Tame which animal? //Marcknight: TODO: Criar um texto especifico pra essa skill
            }

            if (DeferredTarget)
            {
                Timer.DelayCall(() => m.Target = new InternalTarget(m));
            }
            else
            {
                m.Target = new InternalTarget(m);
            }

            // We're not sure why this is getting hung up. Now, its 30 second timeout + 10 seconds (max) to tame
            return TimeSpan.FromSeconds(40.0);
        }

        public static bool MustBeSubdued(BaseCreature bc)
        {
            if (bc.Owners.Count > 0)
            {
                return false;
            } //Checks to see if the animal has been tamed before
            return bc.SubdueBeforeTame && (bc.Hits > ((double)bc.HitsMax / 10));
        }

        public static void ScaleStats(BaseCreature bc, double scalar)
        {
            if (bc.RawStr > 0)
            {
                bc.RawStr = (int)Math.Max(1, bc.RawStr * scalar);
            }

            if (bc.RawDex > 0)
            {
                bc.RawDex = (int)Math.Max(1, bc.RawDex * scalar);
            }

            if (bc.RawInt > 0)
            {
                bc.RawInt = (int)Math.Max(1, bc.RawInt * scalar);
            }

            if (bc.HitsMaxSeed > 0)
            {
                bc.HitsMaxSeed = (int)Math.Max(1, bc.HitsMaxSeed * scalar);
                bc.Hits = bc.Hits;
            }

            if (bc.StamMaxSeed > 0)
            {
                bc.StamMaxSeed = (int)Math.Max(1, bc.StamMaxSeed * scalar);
                bc.Stam = bc.Stam;
            }
        }

        public static void ScaleSkills(BaseCreature bc, double scalar, bool firstTame)
        {
            ScaleSkills(bc, scalar, scalar, firstTame);
        }

        public static void ScaleSkills(BaseCreature bc, double scalar, double capScalar, bool firstTame)
        {
            for (int i = 0; i < bc.Skills.Length; ++i)
            {
                if (!Core.TOL || firstTame)
                {
                    bc.Skills[i].Cap = Math.Max(100.0, bc.Skills[i].Base * capScalar);
                }

                bc.Skills[i].Base *= scalar;

                if (bc.Skills[i].Base > bc.Skills[i].Cap)
                {
                    bc.Skills[i].Cap = bc.Skills[i].Base;
                }
            }
        }

        private class InternalTarget : Target
        {
            private bool m_SetSkillTime = true;

            public InternalTarget(Mobile m)
                : base(Core.AOS ? 3 : 2, false, TargetFlags.None)
            {
                BeginTimeout(m, TimeSpan.FromSeconds(30.0));
            }

            protected override void OnTargetFinish(Mobile from)
            {
                if (m_SetSkillTime)
                {
                    from.NextSkillTime = Core.TickCount;
                }
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                from.RevealingAction();

                int number = -1;

                if (targeted is Mobile)
                {
                    Mobile targ = (Mobile)targeted;

                    if (targ.Player)
                    {
                        number = 500398; // Perhaps just asking would work better. //Tente conversar com ele para convencê-lo a se aliar.
                    }
                    else if (!targ.Body.IsHuman && !targ.Body.IsGargoyle) // Make sure the NPC is human
                    {
                        number = 500399; // There is little chance of getting money from that! //Não dá pra convencer isso a ser seu aliado com Carisma
                    }
                    else if (targ is BaseVendor)
                    {
                        number = 513414; //Essa pessoa não vai largar o emprego por você!
                        // Face eachother
                        from.Direction = from.GetDirectionTo(targ);
                        targ.Direction = targ.GetDirectionTo(from);

                        from.Animate(32, 5, 1, true, false, 0); // Bow

                        new InternalTimerVendor(from, targ).Start();

                        m_SetSkillTime = false;
                    }
                    else if (!from.InRange(targ, 2))
                    {
                        if (!targ.Female)
                        {
                            number = 500401; // You are too far away to beg from him. //Você está longe demais pra argumentar direito com ele.
                        }
                        else
                        {
                            number = 500402; // You are too far away to beg from her. //Você está longe demais pra argumentar direito com ela.
                        }
                    }
                    else if (number != -1)
                    {
                        from.SendLocalizedMessage(number);
                    }
                    else if (targeted is BaseCreature)
                    {
                        BaseCreature creature = (BaseCreature)targeted;

                        /*
                        if (!creature.Tamable)  //Só colocar pra fazer essa verificação se decidir setar quais NPC humanos são domáveis
                        {
                            creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1049655, from.NetState);
                            // That creature cannot be tamed. //Novo numero de cliloc: Esta pessoa não tem interesse em servir a ninguém.
                        }
                        */
                        if (creature.Controlled)
                        {
                            creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502804, from.NetState);
                            // That animal looks tame already. //Novo numero de cliloc: Parece que esta pessoa já serve a alguém.
                        }
                        else if (from.Female && !creature.AllowFemaleTamer)
                        {
                            creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1049653, from.NetState);
                            // That creature can only be tamed by males. //Novo numero de cliloc: Parece que esta pessoa jamais seguiria uma mulher.
                        }
                        else if (!from.Female && !creature.AllowMaleTamer)
                        {
                            creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1049652, from.NetState);
                            // That creature can only be tamed by females. //Novo numero de cliloc: Parece que esta pessoa jamais seguiria um homem.
                        }
                        else if (from.Followers + creature.ControlSlots > from.FollowersMax)
                        {
                            from.SendLocalizedMessage(1049611); // You have too many followers to tame that creature. //Novo numero de cliloc: Você tem seguidores demais pra liderar mais esse.
                        }
                        else if (creature.Owners.Count >= BaseCreature.MaxOwners && !creature.Owners.Contains(from))
                        {
                            creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1005615, from.NetState);
                            // This animal has had too many owners and is too upset for you to tame. //Novo numero de cliloc: Esta pessoa já foi abandonada por muitos mestres e não quer seguir um novo.
                        }
                        else if (MustBeSubdued(creature))
                        {
                            creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1054025, from.NetState);
                            // You must subdue this creature before you can tame it! // Novo numero de cliloc: Você deve subjugar esta pessoa antes de persuadí-la!
                        }
                        else if (from.Skills[SkillName.Carisma].Value >= creature.CurrentTameSkill)
                        {
                            if (m_BeingTamed.Contains(targeted))
                            {
                                creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502802, from.NetState);
                                // Someone else is already taming this. // Novo numero de cliloc: Alguém já está tentando persuadir essa pessoa!
                            }
                            else if (creature.CanAngerOnTame && 0.90 >= Utility.RandomDouble())
                            {
                                creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502805, from.NetState);
                                // You seem to anger the beast! //Novo numero de cliloc: A pessoa se sentiu ofendida e está furiosa!
                                creature.PlaySound(creature.GetAngerSound());
                                creature.Direction = creature.GetDirectionTo(from);

                                if (!Core.SA)
                                {
                                    if (creature.BardPacified && Utility.RandomDouble() > .24)
                                    {
                                        Timer.DelayCall(TimeSpan.FromSeconds(2.0), () => creature.BardPacified = true);
                                    }
                                    else
                                    {
                                        creature.BardEndTime = DateTime.UtcNow;
                                    }

                                    creature.BardPacified = false;
                                }

                                if (creature.AIObject != null)
                                {
                                    creature.AIObject.DoMove(creature.Direction);
                                }

                                if (from is PlayerMobile &&
                                    !(((PlayerMobile)from).HonorActive ||
                                      TransformationSpellHelper.UnderTransformation(from, typeof(EtherealVoyageSpell))))
                                {
                                    creature.Combatant = from;
                                }
                            }
                            else
                            {
                                // Face eachother
                                from.Direction = from.GetDirectionTo(targ);
                                targ.Direction = targ.GetDirectionTo(from);

                                from.Animate(33, 5, 1, true, false, 0); // Salute
                                targ.Animate(33, 5, 1, true, false, 0); // Salute

                                m_SetSkillTime = false;

                                m_BeingTamed[targeted] = from;

                                from.LocalOverheadMessage(MessageType.Emote, 0x59, 1010597); // You start to tame the creature. //Novo numero de cliloc: Tentando persuadir a pessoa.
                                from.NonlocalOverheadMessage(MessageType.Emote, 0x59, 1010598); // *begins taming a creature.* //Novo numero de cliloc: *conversando com a pessoa*

                                new InternalTimer(from, creature, Utility.Random(3, 2)).Start();
                            }
                        }
                        else
                        {
                            creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502806, from.NetState);
                            // You have no chance of taming this creature. //Novo numero de cliloc: Seu papo não é bom o bastante pra convencer esta pessoa.
                        }
                    }
                    else
                    {
                        ((Mobile)targeted).PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502469, from.NetState);
                        // That being cannot be tamed. //Novo numero de cliloc: Impossível dialogar com essa criatura com Carisma.
                    }
                }
                else // Not a Mobile
                {
                    from.SendLocalizedMessage(500399); // There is little chance of getting money from that! // Não tem como dialogar com ISTO!
                }
            }

            private class InternalTimer : Timer
            {
                private readonly Mobile m_Tamer;
                private readonly BaseCreature m_Creature;
                private readonly int m_MaxCount;
                private readonly DateTime m_StartTime;
                private int m_Count;
                private bool m_Paralyzed;

                public InternalTimer(Mobile tamer, BaseCreature creature, int count)
                    : base(TimeSpan.FromSeconds(3.0), TimeSpan.FromSeconds(3.0), count)
                {
                    m_Tamer = tamer;
                    m_Creature = creature;
                    m_MaxCount = count;
                    m_Paralyzed = creature.Paralyzed;
                    m_StartTime = DateTime.UtcNow;
                    Priority = TimerPriority.TwoFiftyMS;
                }

                protected override void OnTick()
                {
                    m_Count++;

                    DamageEntry de = m_Creature.FindMostRecentDamageEntry(false);
                    bool alreadyOwned = m_Creature.Owners.Contains(m_Tamer);

                    if (!m_Tamer.InRange(m_Creature, Core.AOS ? 7 : 6))
                    {
                        m_BeingTamed.Remove(m_Creature);
                        m_Tamer.NextSkillTime = Core.TickCount;
                        m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502795, m_Tamer.NetState);
                        // You are too far away to continue taming.
                        Stop();
                    }
                    else if (!m_Tamer.CheckAlive())
                    {
                        m_BeingTamed.Remove(m_Creature);
                        m_Tamer.NextSkillTime = Core.TickCount;
                        m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502796, m_Tamer.NetState);
                        // You are dead, and cannot continue taming.
                        Stop();
                    }
                    else if (!m_Tamer.CanSee(m_Creature) || !m_Tamer.InLOS(m_Creature) || !CanPath())
                    {
                        m_BeingTamed.Remove(m_Creature);
                        m_Tamer.NextSkillTime = Core.TickCount;
                        m_Tamer.SendLocalizedMessage(1049654);
                        // You do not have a clear path to the animal you are taming, and must cease your attempt.
                        Stop();
                    }
                    /* //Só colocar pra fazer essa verificação se decidir setar quais NPC humanos são domáveis
                    else if (!m_Creature.Tamable)
                    {
                        m_BeingTamed.Remove(m_Creature);
                        m_Tamer.NextSkillTime = Core.TickCount;
                        m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1049655, m_Tamer.NetState);
                        // That creature cannot be tamed.
                        Stop();
                    }
                    */
                    else if (m_Creature.Controlled)
                    {
                        m_BeingTamed.Remove(m_Creature);
                        m_Tamer.NextSkillTime = Core.TickCount;
                        m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502804, m_Tamer.NetState);
                        // That animal looks tame already.
                        Stop();
                    }
                    else if (m_Creature.Owners.Count >= BaseCreature.MaxOwners && !m_Creature.Owners.Contains(m_Tamer))
                    {
                        m_BeingTamed.Remove(m_Creature);
                        m_Tamer.NextSkillTime = Core.TickCount;
                        m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1005615, m_Tamer.NetState);
                        // This animal has had too many owners and is too upset for you to tame.
                        Stop();
                    }
                    else if (MustBeSubdued(m_Creature))
                    {
                        m_BeingTamed.Remove(m_Creature);
                        m_Tamer.NextSkillTime = Core.TickCount;
                        m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1054025, m_Tamer.NetState);
                        // You must subdue this creature before you can tame it!
                        Stop();
                    }
                    else if (de != null && de.LastDamage > m_StartTime)
                    {
                        m_BeingTamed.Remove(m_Creature);
                        m_Tamer.NextSkillTime = Core.TickCount;
                        m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502794, m_Tamer.NetState);
                        // The animal is too angry to continue taming.
                        Stop();
                    }
                    else if (m_Count < m_MaxCount)
                    {
                        m_Tamer.RevealingAction();

                        switch (Utility.Random(3))
                        {
                            case 0:
                                m_Tamer.PublicOverheadMessage(MessageType.Regular, 0x3B2, Utility.Random(502790, 4));
                                break;
                            case 1:
                                m_Tamer.PublicOverheadMessage(MessageType.Regular, 0x3B2, Utility.Random(1005608, 6));
                                break;
                            case 2:
                                m_Tamer.PublicOverheadMessage(MessageType.Regular, 0x3B2, Utility.Random(1010593, 4));
                                break;
                        }

                        if (!alreadyOwned)
                        {
                            m_Tamer.CheckTargetSkill(SkillName.Carisma, m_Creature, m_Creature.CurrentTameSkill, m_Creature.CurrentTameSkill + 20.0);
                        }

                        if (m_Creature.Paralyzed)
                        {
                            m_Paralyzed = true;
                        }
                    }
                    else
                    {
                        m_Tamer.RevealingAction();
                        m_Tamer.NextSkillTime = Core.TickCount;
                        m_BeingTamed.Remove(m_Creature);

                        if (m_Creature.Paralyzed)
                        {
                            m_Paralyzed = true;
                        }

                        //if (!alreadyOwned) // Passively check animal lore for gain
                        //{
                        //     m_Tamer.CheckTargetSkill(SkillName.Carisma, m_Creature, m_Creature.CurrentTameSkill- 10.0, m_Creature.CurrentTameSkill + 10.0);
                        //}

                        double minSkill = m_Creature.CurrentTameSkill + (m_Creature.Owners.Count * 6.0);
                        
                        //minSkill += 24.9;

                        if (alreadyOwned || m_Tamer.CheckTargetSkill(SkillName.Carisma, m_Creature, minSkill, minSkill + 20.0))
                        {
                            if (m_Creature.Owners.Count == 0) // First tame
                            {
                                if (m_Paralyzed)
                                {
                                    ScaleSkills(m_Creature, 0.86, true); // 86% of original skills if they were paralyzed during the taming
                                }
                                else
                                {
                                    ScaleSkills(m_Creature, 0.90, true); // 90% of original skills
                                }
                            }
                            else
                            {
                                ScaleSkills(m_Creature, 0.90, false); // 90% of original skills
                            }

                            if (alreadyOwned)
                            {
                                m_Tamer.SendLocalizedMessage(502797); // That wasn't even challenging.
                            }
                            else
                            {
                                m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502799, m_Tamer.NetState);
                                // It seems to accept you as master.
                            }

                            m_Creature.SetControlMaster(m_Tamer);
                            m_Creature.IsBonded = false;

                            m_Creature.OnAfterTame(m_Tamer);

                            if (!m_Creature.Owners.Contains(m_Tamer))
                            {
                                m_Creature.Owners.Add(m_Tamer);
                            }

                            PetTrainingHelper.GetAbilityProfile(m_Creature, true).OnTame();

                            EventSink.InvokeTameCreature(new TameCreatureEventArgs(m_Tamer, m_Creature));

                        }
                        else
                        {
                            m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502798, m_Tamer.NetState);
                            // You fail to tame the creature.
                        }
                    }
                }

                private bool CanPath()
                {
                    IPoint3D p = m_Tamer;

                    if (p == null)
                    {
                        return false;
                    }

                    if (m_Creature.InRange(new Point3D(p), 1))
                    {
                        return true;
                    }

                    MovementPath path = new MovementPath(m_Creature, new Point3D(p));
                    return path.Success;
                }
            }
        }

        private class InternalTimerVendor : Timer
        {
            private readonly Mobile m_From;
            private readonly Mobile m_Target;

            public InternalTimerVendor(Mobile from, Mobile target)
                : base(TimeSpan.FromSeconds(2.0))
            {
                m_From = from;
                m_Target = target;
                Priority = TimerPriority.TwoFiftyMS;
            }

            protected override void OnTick()
            {
                Container theirPack = m_Target.Backpack;

                double badKarmaChance = 0.5 - ((double)m_From.Karma / 8570);

                if (theirPack == null && m_Target.Race != Race.Elf)
                {
                    m_From.SendLocalizedMessage(500404); // They seem unwilling to give you any money.
                }
                else if (m_From.Karma < 0 && badKarmaChance > Utility.RandomDouble())
                {
                    m_Target.PublicOverheadMessage(MessageType.Regular, m_Target.SpeechHue, 500406);
                    // Thou dost not look trustworthy... no gold for thee today!
                }
                else if (m_From.CheckTargetSkill(SkillName.Carisma, m_Target, 0.0, 100.0))
                {
                    if (m_Target.Race != Race.Elf)
                    {
                        int toConsume = theirPack.GetAmount(typeof(Gold)) / 10;
                        int max = 10 + (m_From.Fame / 2500);

                        if (max > 14)
                        {
                            max = 14;
                        }
                        else if (max < 10)
                        {
                            max = 10;
                        }

                        if (toConsume > max)
                        {
                            toConsume = max;
                        }

                        if (toConsume > 0)
                        {
                            int consumed = theirPack.ConsumeUpTo(typeof(Gold), toConsume);

                            if (consumed > 0)
                            {
                                m_Target.PublicOverheadMessage(MessageType.Regular, m_Target.SpeechHue, 500405);
                                // I feel sorry for thee...

                                Gold gold = new Gold(consumed);

                                m_From.AddToBackpack(gold);
                                m_From.PlaySound(gold.GetDropSound());

                                if (m_From.Karma > -3000)
                                {
                                    int toLose = m_From.Karma + 3000;

                                    if (toLose > 40)
                                    {
                                        toLose = 40;
                                    }

                                    Titles.AwardKarma(m_From, -toLose, true);
                                }
                            }
                            else
                            {
                                m_Target.PublicOverheadMessage(MessageType.Regular, m_Target.SpeechHue, 500407);
                                // I have not enough money to give thee any!
                            }
                        }
                        else
                        {
                            m_Target.PublicOverheadMessage(MessageType.Regular, m_Target.SpeechHue, 500407);
                            // I have not enough money to give thee any!
                        }
                    }
                    else
                    {
                        double chance = Utility.RandomDouble();
                        Item reward = null;
                        string rewardName = "";
                        if (chance >= .99)
                        {
                            int rand = Utility.Random(8);

                            if (rand == 0)
                            {
                                reward = new BegBedRoll();
                                rewardName = "a bedroll";
                            }
                            else if (rand == 1)
                            {
                                reward = new BegCookies();
                                rewardName = "a plate of cookies.";
                            }
                            else if (rand == 2)
                            {
                                reward = new BegFishSteak();
                                rewardName = "a fish steak.";
                            }
                            else if (rand == 3)
                            {
                                reward = new BegFishingPole();
                                rewardName = "a fishing pole.";
                            }
                            else if (rand == 4)
                            {
                                reward = new BegFlowerGarland();
                                rewardName = "a flower garland.";
                            }
                            else if (rand == 5)
                            {
                                reward = new BegSake();
                                rewardName = "a bottle of Sake.";
                            }
                            else if (rand == 6)
                            {
                                reward = new BegTurnip();
                                rewardName = "a turnip.";
                            }
                            else if (rand == 7)
                            {
                                reward = new BegWine();
                                rewardName = "a Bottle of wine.";
                            }
                            else if (rand == 8)
                            {
                                reward = new BegWinePitcher();
                                rewardName = "a Pitcher of wine.";
                            }
                        }
                        else if (chance >= .76)
                        {
                            int rand = Utility.Random(6);

                            if (rand == 0)
                            {
                                reward = new BegStew();
                                rewardName = "a bowl of stew.";
                            }
                            else if (rand == 1)
                            {
                                reward = new BegCheeseWedge();
                                rewardName = "a wedge of cheese.";
                            }
                            else if (rand == 2)
                            {
                                reward = new BegDates();
                                rewardName = "a bunch of dates.";
                            }
                            else if (rand == 3)
                            {
                                reward = new BegLantern();
                                rewardName = "a lantern.";
                            }
                            else if (rand == 4)
                            {
                                reward = new BegLiquorPitcher();
                                rewardName = "a Pitcher of liquor";
                            }
                            else if (rand == 5)
                            {
                                reward = new BegPizza();
                                rewardName = "pizza";
                            }
                            else if (rand == 6)
                            {
                                reward = new BegShirt();
                                rewardName = "a shirt.";
                            }
                        }
                        else if (chance >= .25)
                        {
                            int rand = Utility.Random(1);

                            if (rand == 0)
                            {
                                reward = new BegFrenchBread();
                                rewardName = "french bread.";
                            }
                            else
                            {
                                reward = new BegWaterPitcher();
                                rewardName = "a Pitcher of water.";
                            }
                        }

                        if (reward == null)
                        {
                            reward = new Gold(1);
                        }

                        m_Target.Say(1074854); // Here, take this...
                        m_From.AddToBackpack(reward);
                        m_From.SendLocalizedMessage(1074853, rewardName); // You have been given ~1_name~

                        if (m_From.Karma > -3000)
                        {
                            int toLose = m_From.Karma + 3000;

                            if (toLose > 40)
                            {
                                toLose = 40;
                            }

                            Titles.AwardKarma(m_From, -toLose, true);
                        }

                    }
                }

                else
                {
                    m_Target.SendLocalizedMessage(500404); // They seem unwilling to give you any money.
                }


                m_From.NextSkillTime = Core.TickCount + 10000;
            }
        }
    }




    /*
    public static void Initialize()
    {
        SkillInfo.Table[(int)SkillName.Carisma].Callback = OnUse;
    }

    public static TimeSpan OnUse(Mobile m)
    {
        m.RevealingAction();

        m.SendLocalizedMessage(500397); // To whom do you wish to grovel?

        Timer.DelayCall(() => m.Target = new InternalTarget());

        return TimeSpan.FromHours(1.0);
    }

    private class InternalTarget : Target
    {
        private bool m_SetSkillTime = true;

        public InternalTarget()
            : base(12, false, TargetFlags.None)
        { }

        protected override void OnTargetFinish(Mobile from)
        {
            if (m_SetSkillTime)
            {
                from.NextSkillTime = Core.TickCount;
            }
        }

        protected override void OnTarget(Mobile from, object targeted)
        {
            from.RevealingAction();

            int number = -1;

            if (targeted is Mobile)
            {
                Mobile targ = (Mobile)targeted;

                if (targ.Player) // We can't beg from players
                {
                    number = 500398; // Perhaps just asking would work better.
                }
                else if (!targ.Body.IsHuman) // Make sure the NPC is human
                {
                    number = 500399; // There is little chance of getting money from that!
                }
                else if (!from.InRange(targ, 2))
                {
                    if (!targ.Female)
                    {
                        number = 500401; // You are too far away to beg from him.
                    }
                    else
                    {
                        number = 500402; // You are too far away to beg from her.
                    }
                }
                else if (!Core.ML && from.Mounted) // If we're on a mount, who would give us money? TODO: guessed it's removed since ML
                {
                    number = 500404; // They seem unwilling to give you any money.
                }
                else
                {
                    // Face eachother
                    from.Direction = from.GetDirectionTo(targ);
                    targ.Direction = targ.GetDirectionTo(from);

                    from.Animate(32, 5, 1, true, false, 0); // Bow

                    new InternalTimer(from, targ).Start();

                    m_SetSkillTime = false;
                }
            }
            else // Not a Mobile
            {
                number = 500399; // There is little chance of getting money from that!
            }

            if (number != -1)
            {
                from.SendLocalizedMessage(number);
            }
        }

        private class InternalTimer : Timer
        {
            private readonly Mobile m_From;
            private readonly Mobile m_Target;

            public InternalTimer(Mobile from, Mobile target)
                : base(TimeSpan.FromSeconds(2.0))
            {
                m_From = from;
                m_Target = target;
                Priority = TimerPriority.TwoFiftyMS;
            }

            protected override void OnTick()
            {
                Container theirPack = m_Target.Backpack;

                double badKarmaChance = 0.5 - ((double)m_From.Karma / 8570);

                if (theirPack == null && m_Target.Race != Race.Elf)
                {
                    m_From.SendLocalizedMessage(500404); // They seem unwilling to give you any money.
                }
                else if (m_From.Karma < 0 && badKarmaChance > Utility.RandomDouble())
                {
                    m_Target.PublicOverheadMessage(MessageType.Regular, m_Target.SpeechHue, 500406);
                    // Thou dost not look trustworthy... no gold for thee today!
                }
                else if (m_From.CheckTargetSkill(SkillName.Carisma, m_Target, 0.0, 100.0))
                {
                    if (m_Target.Race != Race.Elf)
                    {
                        int toConsume = theirPack.GetAmount(typeof(Gold)) / 10;
                        int max = 10 + (m_From.Fame / 2500);

                        if (max > 14)
                        {
                            max = 14;
                        }
                        else if (max < 10)
                        {
                            max = 10;
                        }

                        if (toConsume > max)
                        {
                            toConsume = max;
                        }

                        if (toConsume > 0)
                        {
                            int consumed = theirPack.ConsumeUpTo(typeof(Gold), toConsume);

                            if (consumed > 0)
                            {
                                m_Target.PublicOverheadMessage(MessageType.Regular, m_Target.SpeechHue, 500405);
                                // I feel sorry for thee...

                                Gold gold = new Gold(consumed);

                                m_From.AddToBackpack(gold);
                                m_From.PlaySound(gold.GetDropSound());

                                if (m_From.Karma > -3000)
                                {
                                    int toLose = m_From.Karma + 3000;

                                    if (toLose > 40)
                                    {
                                        toLose = 40;
                                    }

                                    Titles.AwardKarma(m_From, -toLose, true);
                                }
                            }
                            else
                            {
                                m_Target.PublicOverheadMessage(MessageType.Regular, m_Target.SpeechHue, 500407);
                                // I have not enough money to give thee any!
                            }
                        }
                        else
                        {
                            m_Target.PublicOverheadMessage(MessageType.Regular, m_Target.SpeechHue, 500407);
                            // I have not enough money to give thee any!
                        }
                    }
                    else
                    {
                        double chance = Utility.RandomDouble();
                        Item reward = null;
                        string rewardName = "";
                        if (chance >= .99)
                        {
                            int rand = Utility.Random(8);

                            if (rand == 0)
                            {
                                reward = new BegBedRoll();
                                rewardName = "a bedroll";
                            }
                            else if (rand == 1)
                            {
                                reward = new BegCookies();
                                rewardName = "a plate of cookies.";
                            }
                            else if (rand == 2)
                            {
                                reward = new BegFishSteak();
                                rewardName = "a fish steak.";
                            }
                            else if (rand == 3)
                            {
                                reward = new BegFishingPole();
                                rewardName = "a fishing pole.";
                            }
                            else if (rand == 4)
                            {
                                reward = new BegFlowerGarland();
                                rewardName = "a flower garland.";
                            }
                            else if (rand == 5)
                            {
                                reward = new BegSake();
                                rewardName = "a bottle of Sake.";
                            }
                            else if (rand == 6)
                            {
                                reward = new BegTurnip();
                                rewardName = "a turnip.";
                            }
                            else if (rand == 7)
                            {
                                reward = new BegWine();
                                rewardName = "a Bottle of wine.";
                            }
                            else if (rand == 8)
                            {
                                reward = new BegWinePitcher();
                                rewardName = "a Pitcher of wine.";
                            }
                        }
                        else if (chance >= .76)
                        {
                            int rand = Utility.Random(6);

                            if (rand == 0)
                            {
                                reward = new BegStew();
                                rewardName = "a bowl of stew.";
                            }
                            else if (rand == 1)
                            {
                                reward = new BegCheeseWedge();
                                rewardName = "a wedge of cheese.";
                            }
                            else if (rand == 2)
                            {
                                reward = new BegDates();
                                rewardName = "a bunch of dates.";
                            }
                            else if (rand == 3)
                            {
                                reward = new BegLantern();
                                rewardName = "a lantern.";
                            }
                            else if (rand == 4)
                            {
                                reward = new BegLiquorPitcher();
                                rewardName = "a Pitcher of liquor";
                            }
                            else if (rand == 5)
                            {
                                reward = new BegPizza();
                                rewardName = "pizza";
                            }
                            else if (rand == 6)
                            {
                                reward = new BegShirt();
                                rewardName = "a shirt.";
                            }
                        }
                        else if (chance >= .25)
                        {
                            int rand = Utility.Random(1);

                            if (rand == 0)
                            {
                                reward = new BegFrenchBread();
                                rewardName = "french bread.";
                            }
                            else
                            {
                                reward = new BegWaterPitcher();
                                rewardName = "a Pitcher of water.";
                            }
                        }

                        if (reward == null)
                        {
                            reward = new Gold(1);
                        }

                        m_Target.Say(1074854); // Here, take this...
                        m_From.AddToBackpack(reward);
                        m_From.SendLocalizedMessage(1074853, rewardName); // You have been given ~1_name~

                        if (m_From.Karma > -3000)
                        {
                            int toLose = m_From.Karma + 3000;

                            if (toLose > 40)
                            {
                                toLose = 40;
                            }

                            Titles.AwardKarma(m_From, -toLose, true);
                        }

                    }
                }

                else
                {
                    m_Target.SendLocalizedMessage(500404); // They seem unwilling to give you any money.
                }


                m_From.NextSkillTime = Core.TickCount + 10000;
            }
        }
    }
}
    */
}

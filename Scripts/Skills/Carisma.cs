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

using Server.Gumps;
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
        public enum Buttons
        {
            Fechar,
            Intimidar,
            Esmolar,
            Recrutar
        }
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
                m.SendLocalizedMessage(503414); // Em quem gostaria de usar seu Carisma?
            }

            if (DeferredTarget)
            {
                Timer.DelayCall(() => m.Target = new CarismaTarget(m));
            }
            else
            {
                m.Target = new CarismaTarget(m);
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

        private class CarismaTarget : Target
        {
            private bool m_SetSkillTime = true;

            public CarismaTarget(Mobile m)
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

                CarismaGump gump = new CarismaGump(from, targeted);
                from.SendGump(gump);

                m_SetSkillTime = false;

            }

            public class CarismaGump : Gump
            {
                Mobile m_eu;
                object m_alvo;
                public CarismaGump(Mobile from, object targ)
                    : base(120, 50)
                {
                    m_eu = from;
                    m_alvo = targ;

                    Closable = true;
                    Disposable = true;
                    Dragable = true;
                    Resizable = false;

                    AddPage(0);
                    AddImage(0, 0, 0x24F4); //largura: 250 | altura: 177
                    AddHtmlLocalized(25, 5, 200, 20, 503472, false, false); //MENU CARISMA
                    AddHtmlLocalized(25, 35, 200, 60, 503473, false, false); // Como deseja interagir?


                    AddButton(35, 65, 0x7583, 0x7584, (int)Buttons.Intimidar, GumpButtonType.Reply, 0);
                    AddHtmlLocalized(60, 65, 300, 120, 503474, false, false); //Intimidar

                    AddButton(35, 90, 0x7583, 0x7584, (int)Buttons.Esmolar, GumpButtonType.Reply, 0);
                    AddHtmlLocalized(60, 90, 300, 120, 503475, false, false); //Pedir Esmola

                    AddButton(35, 115, 0x7583, 0x7584, (int)Buttons.Recrutar, GumpButtonType.Reply, 0);
                    AddHtmlLocalized(60, 115, 300, 120, 503476, false, false); //Recrutar para o grupo
                }

                public override void OnResponse(NetState state, RelayInfo info)
                {
                    int resposta = info.ButtonID;

                    if (resposta == (int)Buttons.Intimidar)
                    {
                        if (m_alvo is PlayerMobile)
                        {
                            m_eu.SendLocalizedMessage(500398);
                        }
                        else if (m_alvo is Mobile)
                        {
                            Intimidar();
                        }
                        else
                        {
                            m_eu.SendLocalizedMessage(503428);
                        }
                    }
                    else if (resposta == (int)Buttons.Esmolar)
                    {
                        if (m_alvo is PlayerMobile)
                        {
                            m_eu.SendLocalizedMessage(500398);
                        }
                        else if (m_alvo is Mobile)
                        {
                            PedirEsmola();
                        }
                        else
                        {
                            m_eu.SendLocalizedMessage(503428);
                        }
                    }
                    else if (resposta == (int)Buttons.Recrutar)
                    {
                        int number = -1;

                        if (m_alvo is Mobile)
                        {
                            Mobile targ = (Mobile)m_alvo;

                            if (targ.Player)
                            {
                                number = 500398; // Tente conversar com essa pessoa pra convencê-la.
                            }
                            /*
                            else if (!targ.Body.IsHuman && !targ.Body.IsGargoyle) // Make sure the NPC is human
                            {
                                number = 500399; // Não dá pra convencer isso a ser seu aliado com Carisma
                            }
                            */
                            /*
                            else if (targ is BaseVendor)
                            {
                                number = 503415; //Essa pessoa não vai largar o emprego por você!
                                                 // Face eachother
                                m_eu.Direction = m_eu.GetDirectionTo(targ);
                                targ.Direction = targ.GetDirectionTo(m_eu);

                                m_eu.Animate(32, 5, 1, true, false, 0); // Bow

                            }
                            */
                            else if (!m_eu.InRange(targ, 2))
                            {
                                if (!targ.Female)
                                {
                                    number = 500401; //Você está longe demais pra argumentar direito com ele.
                                }
                                else
                                {
                                    number = 500402; //Você está longe demais pra argumentar direito com ela.
                                }
                            }
                            else if (number != -1)
                            {
                                m_eu.SendLocalizedMessage(number);
                            }
                            else if (m_alvo is BaseCreature)
                            {
                                BaseCreature creature = (BaseCreature)m_alvo;

                                if (!creature.Persuadable)
                                {
                                    creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1049655, m_eu.NetState);
                                    // That creature cannot be tamed. //Novo numero de cliloc: Esta pessoa não tem interesse em servir a ninguém.
                                }

                                if (creature.Controlled)
                                {
                                    creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 503416, m_eu.NetState);
                                    // Parece que esta pessoa já serve a alguém.
                                }
                                else if (m_eu.Female && !creature.AllowFemaleTamer)
                                {
                                    creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 503417, m_eu.NetState);
                                    // Parece que esta pessoa jamais seguiria uma mulher.
                                }
                                else if (!m_eu.Female && !creature.AllowMaleTamer)
                                {
                                    creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 503418, m_eu.NetState);
                                    // Parece que esta pessoa jamais seguiria um homem.
                                }
                                else if (m_eu.Followers + creature.ControlSlots > m_eu.FollowersMax)
                                {
                                    m_eu.SendLocalizedMessage(503419); // You have too many followers to tame that creature. //Novo numero de cliloc: Você tem seguidores demais pra liderar mais esse.
                                }
                                else if (creature.Owners.Count >= BaseCreature.MaxOwners && !creature.Owners.Contains(m_eu))
                                {
                                    creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 503420, m_eu.NetState);
                                    // Esta pessoa já foi abandonada por muitos mestres e não quer seguir um novo.
                                }
                                else if (MustBeSubdued(creature))
                                {
                                    creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 503421, m_eu.NetState);
                                    // Você deve subjugar esta pessoa antes de persuadí-la!
                                }
                                else if (m_eu.Skills[SkillName.Carisma].Value >= creature.CurrentTameSkill)
                                {
                                    if (m_BeingTamed.Contains(m_alvo))
                                    {
                                        creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 503422, m_eu.NetState);
                                        // Alguém já está tentando persuadir essa pessoa!
                                    }
                                    else if (creature.CanAngerOnTame && 0.90 >= Utility.RandomDouble())
                                    {
                                        creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 503423, m_eu.NetState);
                                        // A pessoa se sentiu ofendida e está furiosa!
                                        creature.PlaySound(creature.GetAngerSound());
                                        creature.Direction = creature.GetDirectionTo(m_eu);

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

                                        if (m_eu is PlayerMobile &&
                                            !(((PlayerMobile)m_eu).HonorActive ||
                                              TransformationSpellHelper.UnderTransformation(m_eu, typeof(EtherealVoyageSpell))))
                                        {
                                            creature.Combatant = m_eu;
                                        }
                                    }
                                    else
                                    {
                                        // Face eachother
                                        m_eu.Direction = m_eu.GetDirectionTo(targ);
                                        targ.Direction = targ.GetDirectionTo(m_eu);

                                        m_eu.Animate(33, 5, 1, true, false, 0); // Salute
                                        targ.Animate(33, 5, 1, true, false, 0); // Salute

                                        m_BeingTamed[m_alvo] = m_eu;

                                        m_eu.LocalOverheadMessage(MessageType.Emote, m_eu.EmoteHue, 503424); // *Tentando persuadir a pessoa*.
                                        m_eu.NonlocalOverheadMessage(MessageType.Emote, creature.EmoteHue, 503425); // *conversando com a pessoa*

                                        new InternalTimer(m_eu, creature, Utility.Random(5, 9)).Start();
                                    }
                                }
                                else
                                {
                                    creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 503426, m_eu.NetState);
                                    // Seu papo não é bom o bastante pra convencer esta pessoa.
                                }
                            }
                            else
                            {
                                ((Mobile)m_alvo).PrivateOverheadMessage(MessageType.Regular, 0x3B2, 503432, m_eu.NetState);
                                // Impossível convencer essa criatura com Carisma.
                            }
                        }
                        else // Not a Mobile
                        {
                            m_eu.SendLocalizedMessage(503429); // Não tem como dialogar com ISTO!
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                protected void Intimidar()
                {
                    if (m_alvo is Mobile)
                    {
                        Mobile targ = (Mobile)m_alvo;

                        if (!m_eu.InRange(targ, 2))
                        {
                            if (!targ.Female)
                            {
                                targ.PublicOverheadMessage(MessageType.Regular, targ.SpeechHue, 500401); //Você está longe demais pra argumentar direito com ele.
                            }
                            else
                            {
                                targ.PublicOverheadMessage(MessageType.Regular, targ.SpeechHue, 500402); //Você está longe demais pra argumentar direito com ela.
                            }
                            return;
                        }

                            double carismaPower = m_eu.Skills.Carisma.Value / 5.0;
                        int resposta = Math.Min(12, 3 * (int)(m_eu.Skills.Carisma.Value / 20.0)); //calcula posição relativa da mensagem no cliloc com base na skill Carisma
                        Container theirPack = targ.Backpack;

                        double badKarmaChance = 0.9;

                        m_eu.PublicOverheadMessage(MessageType.Emote, m_eu.EmoteHue, 503492 + resposta); //Emote de Intimidar

                        Timer.DelayCall(TimeSpan.FromSeconds(2), () =>
                        {
                            m_eu.PublicOverheadMessage(MessageType.Regular, m_eu.SpeechHue, Utility.Random(503493 + resposta, 2)); //Mensagens de Intimidar

                            Timer.DelayCall(TimeSpan.FromSeconds(2), () =>
                            {
                                if (theirPack == null)
                                {
                                    m_eu.SendLocalizedMessage(500404); // They seem unwilling to give you any money.
                                }
                                else if (m_eu.CheckTargetSkill(SkillName.Carisma, targ, 0.0, 100.0))
                                {
                                    int disponivel = theirPack.GetAmount(typeof(Gold));
                                    int toConsume = (int)((double)disponivel * (carismaPower / 20.0)); //Se tiver mais de 100 de skill, o NPC tinha dinheiro extra de algum lugar e dá um extra.
                                    int max = (int)(10.0 * carismaPower);

                                    if (toConsume > max)
                                    {
                                        toConsume = max;
                                    }

                                    if (toConsume > 0)
                                    {
                                        int consumed = theirPack.ConsumeUpTo(typeof(Gold), Math.Min(toConsume, disponivel));

                                        if (consumed > 0)
                                        {
                                            targ.PublicOverheadMessage(MessageType.Regular, targ.SpeechHue, 503515);

                                            Gold gold = new Gold(toConsume);

                                            m_eu.AddToBackpack(gold);
                                            m_eu.PlaySound(gold.GetDropSound());
                                        }
                                        else
                                        {
                                            targ.PublicOverheadMessage(MessageType.Emote, targ.SpeechHue, 503517);
                                        }
                                    }
                                    else
                                    {
                                        targ.PublicOverheadMessage(MessageType.Regular, targ.SpeechHue, 503516);
                                    }
                                }
                                else
                                {
                                    targ.PublicOverheadMessage(MessageType.Regular, targ.SpeechHue, Utility.Random(503507, 4));
                                    if (m_eu.Karma > -3000 && (Utility.RandomDouble() <= badKarmaChance))
                                    {
                                        int toLose = m_eu.Karma + 3000;

                                        if (toLose > 100)
                                        {
                                            toLose = 100;
                                        }

                                        Titles.AwardKarma(m_eu, -toLose, true);
                                    }
                                }
                            });
                        });
                    }
                    else // Not a Mobile
                    {
                        m_eu.SendLocalizedMessage(503429); // Não tem como dialogar com ISTO!
                    }
                }
                protected void PedirEsmola()
                {
                    if (m_alvo is Mobile)
                    {
                        Mobile targ = (Mobile)m_alvo;

                        if (!m_eu.InRange(targ, 2))
                        {
                            if (!targ.Female)
                            {
                                targ.PublicOverheadMessage(MessageType.Regular, targ.SpeechHue, 500401); //Você está longe demais pra argumentar direito com ele.
                            }
                            else
                            {
                                targ.PublicOverheadMessage(MessageType.Regular, targ.SpeechHue, 500402); //Você está longe demais pra argumentar direito com ela.
                            }
                            return;
                        }

                        double carismaPower = m_eu.Skills.Carisma.Value / 5.0;
                        int resposta = Math.Min(12, 3 * (int)(m_eu.Skills.Carisma.Value / 20.0)); //calcula posição relativa da mensagem no cliloc com base na skill Carisma

                        Container theirPack = targ.Backpack;

                        double badKarmaChance = 0.05;

                        m_eu.PublicOverheadMessage(MessageType.Emote, m_eu.EmoteHue, 503477 + resposta); //Emote de Pedir esmola

                        Timer.DelayCall(TimeSpan.FromSeconds(2), () =>
                        {
                            m_eu.PublicOverheadMessage(MessageType.Regular, m_eu.SpeechHue, Utility.Random(503478 + resposta, 2)); //Mensagemde Pedir esmola

                            Timer.DelayCall(TimeSpan.FromSeconds(2), () =>
                            {
                                if (theirPack == null)
                                {
                                    m_eu.SendLocalizedMessage(500404);
                                }
                                else if (m_eu.CheckTargetSkill(SkillName.Carisma, targ, 0.0, 100.0))
                                {
                                    int disponivel = theirPack.GetAmount(typeof(Gold));
                                    int toConsume = (int)((double)disponivel * (carismaPower / 40.0));
                                    int max = (int)(carismaPower);

                                    if (toConsume > max)
                                    {
                                        toConsume = max;
                                    }

                                    double chance = Utility.RandomDouble();
                                    Item reward = null;
                                    string rewardName = "";

                                    if (chance >= .99)
                                    {
                                        int rand = Utility.Random(8);

                                        if (rand == 0)
                                        {
                                            reward = new BegBedRoll();
                                            rewardName = "um Saco de dormir";
                                        }
                                        else if (rand == 1)
                                        {
                                            reward = new BegCookies();
                                            rewardName = "uma Travessa de biscoitos";
                                        }
                                        else if (rand == 2)
                                        {
                                            reward = new BegFishSteak();
                                            rewardName = "uma Posta de peixe";
                                        }
                                        else if (rand == 3)
                                        {
                                            reward = new BegFishingPole();
                                            rewardName = "uma Vara de pescar";
                                        }
                                        else if (rand == 4)
                                        {
                                            reward = new BegFlowerGarland();
                                            rewardName = "uma Guirlanda de flores";
                                        }
                                        else if (rand == 5)
                                        {
                                            reward = new BegSake();
                                            rewardName = "uma Garrafa de sake";
                                        }
                                        else if (rand == 6)
                                        {
                                            reward = new BegTurnip();
                                            rewardName = "um Nabo";
                                        }
                                        else if (rand == 7)
                                        {
                                            reward = new BegWine();
                                            rewardName = "uma Garrafa de vinho";
                                        }
                                        else if (rand == 8)
                                        {
                                            reward = new BegWinePitcher();
                                            rewardName = "uma Jarra de vinho";
                                        }
                                    }
                                    else if (chance >= .76)
                                    {
                                        int rand = Utility.Random(6);

                                        if (rand == 0)
                                        {
                                            reward = new BegStew();
                                            rewardName = "uma Tigela de sopa";
                                        }
                                        else if (rand == 1)
                                        {
                                            reward = new BegCheeseWedge();
                                            rewardName = "uma Fatia de queijo";
                                        }
                                        else if (rand == 2)
                                        {
                                            reward = new BegDates();
                                            rewardName = "uma Porção de tâmaras";
                                        }
                                        else if (rand == 3)
                                        {
                                            reward = new BegLantern();
                                            rewardName = "uma Lanterna";
                                        }
                                        else if (rand == 4)
                                        {
                                            reward = new BegLiquorPitcher();
                                            rewardName = "uma Jarra de Licor";
                                        }
                                        else if (rand == 5)
                                        {
                                            reward = new BegPizza();
                                            rewardName = "uma Pizza";
                                        }
                                        else if (rand == 6)
                                        {
                                            reward = new BegShirt();
                                            rewardName = "uma Camiseta";
                                        }
                                    }
                                    else if (chance >= .25)
                                    {
                                        int rand = Utility.Random(1);

                                        if (rand == 0)
                                        {
                                            reward = new BegFrenchBread();
                                            rewardName = "um Pão francês";
                                        }
                                        else
                                        {
                                            reward = new BegWaterPitcher();
                                            rewardName = "uma Jarra de água";
                                        }
                                    }

                                    if (toConsume > 0)
                                    {
                                        int consumed = theirPack.ConsumeUpTo(typeof(Gold), Math.Min(toConsume, disponivel));

                                        if (consumed > 0)
                                        {
                                            targ.PublicOverheadMessage(MessageType.Regular, targ.SpeechHue, 500405);

                                            Gold gold = new Gold(toConsume);

                                            m_eu.AddToBackpack(gold);
                                            m_eu.PlaySound(gold.GetDropSound());
                                        }
                                    }

                                    if (rewardName != "" && toConsume > 0)
                                    {
                                        rewardName = String.Concat(rewardName, " e ", (toConsume > 1) ? "uma Moeda." : "Moedas.");
                                    }
                                    else if (rewardName != "")
                                    {
                                        rewardName = String.Concat(rewardName, ".");
                                    }
                                    if (toConsume > 0)
                                    {
                                        rewardName = (toConsume > 1) ? "uma Moeda." : "Moedas.";
                                    }
                                    else
                                    {
                                        rewardName = "absolutamente nada.";
                                    }
                                    m_eu.SendLocalizedMessage(1074853, rewardName); // You have been given ~1_name~
                                }
                                else
                                {
                                    targ.PublicOverheadMessage(MessageType.Regular, targ.SpeechHue, Utility.Random(503511, 4));
                                    if (m_eu.Karma > -3000 && (Utility.RandomDouble() <= badKarmaChance))
                                    {
                                        int toLose = m_eu.Karma + 3000;

                                        if (toLose > 100)
                                        {
                                            toLose = 100;
                                        }

                                        Titles.AwardKarma(m_eu, -toLose, true);
                                    }
                                }
                            });
                        });
                        m_eu.NextSkillTime = Core.TickCount + 10000;
                    }
                    else // Not a Mobile
                    {
                        m_eu.SendLocalizedMessage(503429); // Não tem como dialogar com ISTO!
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
                        : base(TimeSpan.FromSeconds(4.0), TimeSpan.FromSeconds(4.0), count)
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
                            m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 503429, m_Tamer.NetState);
                            // Você se afastou demais pra continuar persuadindo direito.
                            Stop();
                        }
                        else if (!m_Tamer.CheckAlive())
                        {
                            m_BeingTamed.Remove(m_Creature);
                            m_Tamer.NextSkillTime = Core.TickCount;
                            m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 503430, m_Tamer.NetState);
                            // Não dá pra continuar a persuadir estando inconsciente.
                            Stop();
                        }
                        else if (!m_Tamer.CanSee(m_Creature) || !m_Tamer.InLOS(m_Creature) || !CanPath())
                        {
                            m_BeingTamed.Remove(m_Creature);
                            m_Tamer.NextSkillTime = Core.TickCount;
                            m_Tamer.SendLocalizedMessage(503431);
                            // Você não tem um caminho livre até a pessoa que tenta persuadir e teve que interromper a tentativa.
                            Stop();
                        }
                        //Só colocar pra fazer essa verificação se decidir setar quais NPC humanos são domáveis
                        else if (!m_Creature.Persuadable)
                        {
                            m_BeingTamed.Remove(m_Creature);
                            m_Tamer.NextSkillTime = Core.TickCount;
                            m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 503432, m_Tamer.NetState);
                            // Impossível convencer esta criatura com Carisma.
                            Stop();
                        }
                        else if (m_Creature.Controlled)
                        {
                            m_BeingTamed.Remove(m_Creature);
                            m_Tamer.NextSkillTime = Core.TickCount;
                            m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 503416, m_Tamer.NetState);
                            // Parece que esta pessoa já serve a alguém.
                            Stop();
                        }
                        else if (m_Creature.Owners.Count >= BaseCreature.MaxOwners && !m_Creature.Owners.Contains(m_Tamer))
                        {
                            m_BeingTamed.Remove(m_Creature);
                            m_Tamer.NextSkillTime = Core.TickCount;
                            m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 503420, m_Tamer.NetState);
                            // Esta pessoa já foi abandonada por muitos mestres e não quer seguir um novo.
                            Stop();
                        }
                        else if (MustBeSubdued(m_Creature))
                        {
                            m_BeingTamed.Remove(m_Creature);
                            m_Tamer.NextSkillTime = Core.TickCount;
                            m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 503421, m_Tamer.NetState);
                            // Você deve subjugar esta pessoa antes de persuadí-la!
                            Stop();
                        }
                        else if (de != null && de.LastDamage > m_StartTime)
                        {
                            m_BeingTamed.Remove(m_Creature);
                            m_Tamer.NextSkillTime = Core.TickCount;
                            m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 503423, m_Tamer.NetState);
                            // A pessoa se sentiu ofendida e está furiosa!
                            Stop();
                        }
                        else if (m_Count < m_MaxCount)
                        {
                            m_Tamer.RevealingAction();

                            //Marcknight: Deixei espaço reservado no Cliloc.enu para mais 4 falas de cada interlocutor.

                            //Fala do personagem que tenta convencer
                            m_Tamer.PublicOverheadMessage(MessageType.Regular, m_Tamer.SpeechHue, Utility.Random(503433, 11));

                            Timer.DelayCall(TimeSpan.FromSeconds(2.0), () =>
                            {
                            //Fala do personagem que tenta não ser convencido
                            m_Creature.PublicOverheadMessage(MessageType.Regular, 0x3B2, Utility.Random(503448, 11));
                            });

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

                            double minSkill = m_Creature.CurrentPersuadeSkill + (m_Creature.Owners.Count * 6.0);

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
                                    m_Tamer.SendLocalizedMessage(502797); // Nem foi desafiador.
                                }
                                else
                                {
                                    //Marcknight: Sucesso em domar

                                    // Face eachother
                                    m_Tamer.Direction = m_Tamer.GetDirectionTo(m_Creature);
                                    m_Creature.Direction = m_Creature.GetDirectionTo(m_Tamer);

                                    m_Tamer.PublicOverheadMessage(MessageType.Regular, m_Tamer.SpeechHue, Utility.Random(503463, 3));
                                    Timer.DelayCall(TimeSpan.FromSeconds(2.0), () =>
                                    {
                                        m_Creature.PublicOverheadMessage(MessageType.Regular, 0x3B2, Utility.Random(503466, 3));
                                        m_Tamer.Animate(33, 5, 1, true, false, 0); // Salute
                                    m_Creature.Animate(33, 5, 1, true, false, 0); // Salute
                                });
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
                                m_Tamer.PublicOverheadMessage(MessageType.Regular, m_Tamer.SpeechHue, Utility.Random(503463, 3));

                                Timer.DelayCall(TimeSpan.FromSeconds(2), () =>
                                {
                                    m_Creature.PublicOverheadMessage(MessageType.Regular, m_Creature.SpeechHue, Utility.Random(503469, 3));
                                });
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
        }
    }
}

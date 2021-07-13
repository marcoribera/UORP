using System;

using Server.Items;
using Server.Mobiles.Custom.MiniBosses;
using Server.Network;
using Server.Services;

namespace Server.Mobiles
{
    public class CabecaAnthony : Bag
    {
        [Constructable]
        public CabecaAnthony()
        {
            Name = "Percentes de Anthony";
            Hue = 38;
        }

        public CabecaAnthony(Serial s) : base(s) { }

        public override void OnDoubleClick(Mobile from)
        {
            from.SendMessage("Quem sabe mostrando isso ao orc lider ele nao te da uma recompensa ?");
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }

    public class AnthonyErtsem : BaseCreature
    {
        public override bool ClickTitle { get { return false; } }
        public override bool CanStealth { get { return true; } }
        public override bool ReduceSpeedWithDamage { get { return false; } }

        private DateTime m_NextWeaponChange;

        [Constructable]
        public AnthonyErtsem() : base(AIType.AI_Ninja, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            Name = "Anthony Ertsem";

            Body = 0x190;

            SetHits(5000, 5000);

            SetStr(90, 120);
            SetDex(81, 95);
            SetInt(151, 165);
            SetStam(200);

            SetDamage(2, 10);

            SetDamageType(ResistanceType.Physical, 65);
            SetDamageType(ResistanceType.Fire, 15);
            SetDamageType(ResistanceType.Poison, 15);
            SetDamageType(ResistanceType.Energy, 5);

            SetResistance(ResistanceType.Physical, 35, 65);
            SetResistance(ResistanceType.Fire, 40, 60);
            SetResistance(ResistanceType.Cold, 25, 45);
            SetResistance(ResistanceType.Poison, 40, 60);
            SetResistance(ResistanceType.Energy, 35, 55);

            SetSkill(SkillName.Anatomia, 105.0, 120.0);
            SetSkill(SkillName.ResistenciaMagica, 0, 0);
            SetSkill(SkillName.Briga, 95.0, 120.0);
            SetSkill(SkillName.Cortante, 95.0, 120.0);
            SetSkill(SkillName.Contusivo, 95.0, 120.0);
            SetSkill(SkillName.Perfurante, 95.0, 120.0);

            SetSkill(SkillName.Ninjitsu, 60, 70);
            SetSkill(SkillName.Furtividade, 100.0);
            //SetSkill(SkillName.Stealth, 120.0);

            Item LeatherChest = new LeatherChest();
            LeatherChest.Movable = false;
            LeatherChest.Hue = 1175;
            AddItem(LeatherChest);

            Item LeatherArms = new LeatherArms();
            LeatherArms.Movable = false;
            LeatherArms.Hue = 1175;
            AddItem(LeatherArms);

            Item LeatherLegs = new LeatherLegs();
            LeatherLegs.Movable = false;
            LeatherLegs.Hue = 1175;
            AddItem(LeatherLegs);

            Item LeatherGloves = new LeatherGloves();
            LeatherGloves.Movable = false;
            LeatherGloves.Hue = 1175;
            AddItem(LeatherGloves);

            Item LeatherGorget = new LeatherGorget();
            LeatherGorget.Movable = false;
            LeatherGorget.Hue = 1175;
            AddItem(LeatherGorget);

            var arma = new Dagger();
            arma.Hue = 1175;
            arma.Name = "Adaga de Anthony";
            arma.Attributes.BonusDex = 5;
            arma.Movable = false;
            arma.Poison = Poison.Regular;
            arma.PoisonCharges = 9999;
            AddItem(arma);

            //PackItem(Decos.RandomDeco());
            SetWeaponAbility(WeaponAbility.BleedAttack);

            Fame = 8500;
            Karma = -8500;
            AddItem(new Gold(3000));
            Utility.AssignRandomHair(this);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
        }

        public override bool BardImmune { get { return true; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Gems, 2);
        }
        public class PotTimer : Timer
        {
            private BaseCreature m_Defender;
            private Mobile m_Target;
            private int count = 3;

            public PotTimer(BaseCreature defender, Mobile target)
                : base(TimeSpan.FromSeconds(0.65), TimeSpan.FromSeconds(0.65), 4)
            {
                m_Defender = defender;
                m_Target = target;
                Start();
            }

            protected override void OnTick()
            {
                if (m_Defender == null || m_Target == null || !m_Defender.Alive || !m_Target.Alive)
                {
                    Stop();
                    return;
                }

                m_Defender.PublicOverheadMessage(MessageType.Regular, 23, true, "" + count);
                count--;
                if (count < 0)
                {
                    Stop();
                    var distancia = m_Defender.GetDistanceToSqrt(m_Target.Location);

                    if (m_Defender.Paralyzed || !m_Defender.InLOS(m_Target) || distancia > 16)
                    {
                        m_Defender.OverheadMessage("* ... *");
                    }
                    else
                    {
                        Effects.SendMovingEffect(m_Defender, m_Target, 0xF0D, 12, 0, false, false, 0, 0);
                        Timer.DelayCall(TimeSpan.FromMilliseconds(500), () => {

                            if (m_Target == null || m_Target.Map == null)
                                return;

                            var dmg = 15 + Utility.Random(25);
                            m_Target.Damage(dmg);
                          
                            Effects.PlaySound(m_Target.Location, m_Target.Map, 0x307);
                            Effects.SendLocationEffect(m_Target.Location, m_Target.Map, 0x36B0, 9, 10, 0, 0);

                            if (m_Target == null || !m_Target.Alive || m_Target.Map == Map.Internal)
                                return;

                            var alvos = m_Target.Map.GetClientsInRange(m_Target.Location, 12);
                            foreach (var alvo in alvos)
                            {
                                if (m_Target != alvo.Mobile && m_Target.InLOS(alvo.Mobile))
                                {
                                    m_Target.MovingParticles(alvo.Mobile, 0x10D3, 15, 0, false, false, 9502, 4019, 0x160);
                                    var teia = new Teia(alvo.Mobile);
                                    alvo.Mobile.SendMessage("Voce foi preso por uma teia e nao consegue se soltar. Talvez alguem consiga te soltar clicando na teia.");
                                    alvo.Mobile.OverheadMessage("* preso *");
                                    teia.MoveToWorld(alvo.Mobile.Location, alvo.Mobile.Map);
                                    alvo.Mobile.Freeze(TimeSpan.FromSeconds(20));
                                    Timer.DelayCall(TimeSpan.FromSeconds(20), () =>
                                    {
                                        if (alvo == null || alvo.Mobile == null)
                                            return;

                                        if (teia != null && !teia.Deleted)
                                            teia.Delete();
                                        alvo.Mobile.Frozen = false;
                                    });
                                }
                            }
                            alvos.Free();

                        });
                    }

                }
            }
        }

        public override void OnThink()
        {
            if (this.Combatant != null && this.Combatant is Mobile)
            {
                if (!IsCooldown("pot"))
                {
                    OverheadMessage("* pega uma pocao pegajosa *");
                    SetCooldown("pot", TimeSpan.FromSeconds(15));
                    new PotTimer(this, (Mobile)this.Combatant);
                }
            }
            base.OnThink();
        }


        public override bool OnBeforeDeath()
        {
            GolemMecanico.JorraOuro(this.Location, this.Map, 150);
            return base.OnBeforeDeath();
        }

        public override bool AlwaysMurderer { get { return true; } }

        public AnthonyErtsem(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

        }
    }
}

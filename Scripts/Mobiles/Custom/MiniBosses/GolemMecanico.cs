using System;
using System.Collections.Generic;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("a golem corpse")]
    public class GolemMecanico : BaseCreature
    {

        public bool Carregando = false;
        public bool Tatunado = false;

        public double GetDelay()
        {
            return Tatunado ? 0.4 : 1;
        }

        public static List<GolemMecanico> Instances = new List<GolemMecanico>();

        public void SetSpeed(double speed)
        {
            var dActiveSpeed = speed;
            var dPassiveSpeed = speed / 2;
            //SpeedInfo.GetSpeeds(this, ref dActiveSpeed, ref dPassiveSpeed);
            ActiveSpeed = dActiveSpeed;
            PassiveSpeed = dPassiveSpeed;
            CurrentSpeed = dActiveSpeed;
        }

        [Constructable]
        public GolemMecanico()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.8)
        {
            Name = "Goblozord Mechnotron";
            Body = 763;
            //Hue = 1157;

            SetStr(100, 100);
            SetDex(100, 100);
            SetHits(7000);

            SetResistance(ResistanceType.Fire, 100);
            SetResistance(ResistanceType.Poison, 10, 25);

            SetSkill(SkillName.ResistenciaMagica, 0, 0);
            SetSkill(SkillName.Anatomia, 60.0, 100.0);
            SetSkill(SkillName.Briga, 150.0, 190.0);
            //SetSkill(SkillName.DetectHidden, 45.0, 50.0);
            SetSkill(SkillName.Bloqueio, 60);

            Fame = 15000;
            Karma = -15000;

            VirtualArmor = 20;

            SetDamage(19, 20);

            if (Instances.Count > 0)
            {
                Instances.ForEach(i => i.Delete());
            }

            this.PackItem(new AutomatonActuator());
            //this.PackItem(Decos.RandomDeco());
            Instances.Add(this);

            Item pot = null;

            switch (Utility.Random(4))
            {
                case 0: pot = new HealPotion(); break;
                case 1: pot = new RefreshPotion(); break;
                case 2: pot = new RefreshPotion(); break;
                case 3: pot = new CurePotion(); break;
            }
            pot.Amount = 50;
            PackItem(pot);
            SetSpecialAbility(SpecialAbility.ManaDrain);
            SetWeaponAbility(WeaponAbility.ParalyzingBlow);

            PackItem(new PowerCrystal());
            if (Utility.RandomBool())
                PackItem(new PowerCrystal());
            PackItem(new ClockworkAssembly());
            if (Utility.RandomBool())
                PackItem(new ClockworkAssembly());
            PackItem(new Gold(2000));
            PackItem(new MechanicalLifeManual());
            this.PackItem(new Item(0xA517));
        }

        public GolemMecanico(Serial serial)
            : base(serial)
        {
        }

        public void Raio()
        {
            this.PublicOverheadMessage(MessageType.Regular, 8, true, "* carregando energia *");
            //this.OverheadMessage("* carregando energia *");
            new RaioTimer(this).Start();
        }

        public void Turbo()
        {
            //this.OverheadMessage("* aquecendo motores *");
            this.PublicOverheadMessage(MessageType.Regular, 8, true, "* aquecendo motores *");
            new TurboTimer(this).Start();
        }

        private class TurboTimer : Timer
        {
            private int ct = 10;
            private GolemMecanico bixo;

            public TurboTimer(GolemMecanico bixo) : base(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), 11)
            {
                this.bixo = bixo;
                bixo.Carregando = true;
            }

            protected override void OnTick()
            {
                if (!bixo.Carregando)
                {
                    Stop();
                    return;
                }
                var pct = 100 - ct * 10;
                bixo.PublicOverheadMessage(MessageType.Regular, 8, true, pct + "%");
                ct--;
                if (ct < 0)
                {
                    bixo.PublicOverheadMessage(MessageType.Regular, 8, true, pct + "* motor quente *");
                    bixo.SetSpeed(0.2);
                    bixo.ProcessDelta();
                    bixo.Hue = 32;
                    bixo.Tatunado = true;
                    bixo.PlaySound(0x20A);
                    bixo.Carregando = false;
                    bixo.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot);
                    Timer.DelayCall(TimeSpan.FromSeconds(10), () =>
                    {
                        bixo.Hue = 0;
                        bixo.PublicOverheadMessage(MessageType.Regular, 8, true, "* motor esfria *");
                        bixo.SetSpeed(0.8);
                        bixo.Tatunado = false;
                    });
                    Stop();

                }
            }
        }

        private class RaioTimer : Timer
        {
            private int ct = 10;
            private GolemMecanico bixo;

            public RaioTimer(GolemMecanico bixo) : base(TimeSpan.FromSeconds(bixo.GetDelay()), TimeSpan.FromSeconds(bixo.GetDelay()), 11)
            {
                this.bixo = bixo;
                bixo.Carregando = true;
            }

            protected override void OnTick()
            {
                if (!bixo.Carregando)
                {
                    Stop();
                    return;
                }
                var pct = 100 - ct * 10;
                bixo.PublicOverheadMessage(MessageType.Regular, 8, true, pct + "%");
                // bixo.OverheadMessage(pct+"%");
                ct--;
                if (ct < 0)
                {
                    bixo.PublicOverheadMessage(MessageType.Regular, 8, true, "* descarrega energia *");
                    bixo.PlaySound(0x20A);
                    var mobiles = bixo.GetMobilesInRange(8);
                    foreach (var m in mobiles)
                    {
                        if (m != bixo)
                        {
                            if (m is PlayerMobile)
                            {
                                bixo.MovingParticles(m, 0x379F, 7, 0, false, true, 3043, 4043, 0x211);
                                Effects.SendBoltEffect(m, true, 0, false);
                                AOS.Damage(m, 40 + Utility.Random(30), 0, 0, 0, 0, 100);
                            }
                        }
                        else
                        {
                            bixo.Heal(40 + Utility.Random(30));
                        }
                    }
                    mobiles.Free();
                    Stop();
                    bixo.Carregando = false;
                }
            }
        }

        public static void JorraOuro(Point3D loc, Map map, int amount, int size = 8)
        {
            if (map != null)
            {
                for (int x = -size; x <= size; ++x)
                {
                    for (int y = -size; y <= size; ++y)
                    {
                        double dist = Math.Sqrt(x * x + y * y);

                        if (dist <= size && Utility.RandomBool())
                            new GoldTimer(map, loc.X + x, loc.Y + y, loc.Z, amount).Start();
                    }
                }
            }

        }

        private class GoldTimer : Timer
        {
            private Map m_Map;
            private int m_X, m_Y;
            private int z;
            private int qtd;

            public GoldTimer(Map map, int x, int y, int z, int qtd) : base(TimeSpan.FromSeconds(Utility.RandomDouble() * 10.0))
            {
                m_Map = map;
                m_X = x;
                m_Y = y;
                this.z = z;
                this.qtd = qtd;
            }

            protected override void OnTick()
            {
                //int z = m_Map.GetAverageZ(m_X, m_Y);
                bool canFit = m_Map.CanFit(m_X, m_Y, z, z, false, false);

                for (int i = -3; !canFit && i <= 3; ++i)
                {
                    canFit = m_Map.CanFit(m_X, m_Y, z + i, z, false, false);

                    if (canFit)
                        z += i;
                }

                if (!canFit)
                    return;

                Gold g = new Gold(qtd, qtd * 2);
                g.MoveToWorld(new Point3D(m_X, m_Y, z), m_Map);

                if (0.3 >= Utility.RandomDouble())
                {
                    Effects.SendLocationParticles(EffectItem.Create(g.Location, g.Map, EffectItem.DefaultDuration), 0x3709, 10, 30, 5052);
                    Effects.PlaySound(g, g.Map, 0x208);
                }
            }
        }


        public override bool OnBeforeDeath()
        {
            if (Instances != null && Instances.Contains(this))
                Instances.Remove(this);
            return base.OnBeforeDeath();
        }

        public override void OnThink()
        {
            if (Tatunado && !IsCooldown("fuma"))
            {
                SetCooldown("fuma", TimeSpan.FromSeconds(2));
                FixedEffect(0x3728, 10, 20);
            }

            if (Tatunado && !IsCooldown("tp") && Combatant != null)
            {
                SetCooldown("tp", TimeSpan.FromSeconds(4));
                Effects.SendBoltEffect(this, true, 0, false);
                FixedEffect(0x3728, 10, 20);
                MoveToWorld(Combatant.Location, Combatant.Map);
                FixedEffect(0x3728, 10, 20);
            }

            if (IsCooldown("skill"))
                return;

            SetCooldown("skill", TimeSpan.FromSeconds(13));
            if (Utility.RandomDouble() < 0.8)
                Raio();
            else
                Turbo();
            base.OnThink();
        }

        public override bool IsScaredOfScaryThings { get { return false; } }
        public override bool IsScaryToPets { get { return true; } }
        public override bool IsBondable { get { return false; } }
        public override FoodType FavoriteFood { get { return FoodType.None; } }
        public override bool CanBeDistracted { get { return false; } }
        public override bool DeleteOnRelease { get { return true; } }
        public override bool AutoDispel { get { return !Controlled; } }
        public override bool BleedImmune { get { return true; } }
        public override bool BardImmune { get { return !Core.AOS || Controlled; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            if (0.75 > Utility.RandomDouble())
                c.DropItem(DawnsMusicGear.RandomCommon);
            else
                c.DropItem(DawnsMusicGear.RandomUncommon);

        }

        public override int GetAngerSound()
        {
            return 541;
        }

        public override int GetIdleSound()
        {
            if (!Controlled)
                return 542;

            return base.GetIdleSound();
        }

        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.FilthyRich, 2);
        }


        public override int GetDeathSound()
        {
            if (!Controlled)
                return 545;

            return base.GetDeathSound();
        }

        public override int GetAttackSound()
        {
            return 562;
        }

        public override int GetHurtSound()
        {
            if (Controlled)
                return 320;

            return base.GetHurtSound();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            Instances.Add(this);
        }
    }
}

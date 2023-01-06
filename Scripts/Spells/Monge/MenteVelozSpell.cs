using System;
using System.Web.UI.WebControls;
using Server.Targeting;

namespace Server.Spells.Monge
{
    public class MenteVelozSpell : MongeSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Mente veloz", "Celeriter Anima",
            212,
            9061,
            Reagent.MandrakeRoot,
            Reagent.Nightshade);

        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        public MenteVelozSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override double RequiredSkill
        {
            get
            {
                return 10.0;
            }
        }
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.First;
            }
        }


      /*  public static void EndCunningEffect(Mobile m)  //primeiro preciso remover o bonus
        {
            if (m_Table.Contains(m))   // preciso achar o m.table
            {
                StatusBonus[] status = (StatusBonus[])m_Table[m];

                if (status != null)
                {
                    for (int i = 0; i < status.Length; ++i)
                        m.RemoveStatusBonus(status[i]);
                }

                m_Table.Remove(m);
                BuffInfo.RemoveBuff(m, BuffIcon.Cunning);
            }
        }


        public override bool CheckCast()   // preciso adicionar um check para dizer se a magia já está em efeito
        {
            if (Core.AOS)
                return true;

            if (this.Caster.newInt > 0) //não sei como declarar a variavel que vai dizer que a magia já está em efeito
            {
                this.Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
                return false;
            }
            else if (!this.Caster.CanBeginAction(typeof(DefensiveSpell)))
            {
                this.Caster.SendLocalizedMessage(1005385); // The spell will not adhere to you at this time.
                return false;
            }

            return true;
        }*/



        public override void OnCast()
        {
            Mobile m = this.Caster;
       
            if (this.CheckBSequence(m))
            {
                int oldInt = SpellHelper.GetBuffOffset(m, StatType.Int);
                int newInt = SpellHelper.GetOffset(this,Caster, m, StatType.Int, false, true);

                if (newInt < oldInt || newInt == 0)
                {
                    DoHurtFizzle();
                }
                else
                {
                    SpellHelper.Turn(this.Caster, m);

                    SpellHelper.AddStatBonus(this, this.Caster, m, false, StatType.Int);
                    int percentage = (int)(SpellHelper.GetOffsetScalar(this,this.Caster, m, false) * 100);
                    TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
                    BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Cunning, 1075843, length, m, percentage.ToString()));

                    m.FixedParticles(0x375A, 10, 15, 5011, EffectLayer.Head);
                    m.PlaySound(0x1EB);
                }
            }

            this.FinishSequence();
        }

    }
}

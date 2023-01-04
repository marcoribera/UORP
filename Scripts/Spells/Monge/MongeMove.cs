using System;

namespace Server.Spells
{
    public class MongeMove : SpecialMove
    {
        public override SkillName MoveSkill
        {
            get
            {
                return SkillName.Misticismo;
            }
        }
        public virtual int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias

        public virtual SpellCircle Circle
        {
            get
            {
                return SpellCircle.Sixth;
            }
        }
        public override void CheckGain(Mobile m)
        {
            m.CheckSkill(this.MoveSkill, this.RequiredSkill - 12.5, this.RequiredSkill + 37.5);	//Per five on friday 02/16/07
        }
    }
}

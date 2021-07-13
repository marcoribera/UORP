using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Custom.Classes
{
    public enum TierClasse
    {
        SemClasse,
        ClasseBasica,
        ClasseIntermediaria,
        ClasseAvancada,
        ClasseLendaria
    }

    public class ClassePersonagem
    {
        public int ID;
        public string Nome;
        public string Desc;
        public TierClasse Tier;
        Dictionary<SkillName, double> Requisitos;
        Dictionary<SkillName, double> Beneficios;

        public ClassePersonagem(int identificador, string nome, string descricao, TierClasse tier, Dictionary<SkillName, double> requisitos, Dictionary<SkillName, double> beneficios)
        {
            this.Nome = nome;
            this.Desc = descricao;
            this.Tier = tier;
            this.Requisitos = requisitos;
            this.Beneficios = beneficios;
            this.ID = identificador;
        }

        public bool AplicaClasse(PlayerMobile player)
        {
            bool classeAplicada = true;
            int atendidos = 0;
            switch (Tier)
            {
                case TierClasse.ClasseBasica:
                    player.SendMessage("Requisitos:");
                    foreach (KeyValuePair<SkillName, double> requisito in Requisitos)
                    {
                        player.SendMessage(String.Format("({0}/{1}) {2}", player.Skills[requisito.Key].Base, requisito.Value, player.Skills[requisito.Key].Name));
                        //Console.WriteLine("-> "+requisito.Key + " " + player.Skills[requisito.Key].Name);
                        if (player.Skills[requisito.Key].Base >= requisito.Value)
                        {
                            atendidos++;
                        }
                    }
                    player.SendMessage(String.Format("{0}/4 dos requisitos.", atendidos));
                    if (atendidos >= 4)
                    {
                        foreach (KeyValuePair<SkillName, double> beneficio in Beneficios) //Aplica os bonus de Cap
                        {
                            player.SendMessage(String.Format("Benefício Aplicado: Cap de {0} ({1}+{2})", player.Skills[beneficio.Key].Name, player.Skills[beneficio.Key].Cap, beneficio.Value));
                            player.Skills[beneficio.Key].Cap = 50.0 + beneficio.Value;  //player.Skills[beneficio.Key].Cap += beneficio.Value;
                        }
                        player.ClasseBasicaID = ID; //Define a classe do personagem
                        player.ClasseAbandonavel = TierClasse.ClasseBasica;
                        player.SendMessage(String.Format("Agora você é um membro da classe {0}, Paranbéns!", this.Nome));
                        classeAplicada = true;
                    }
                    else
                    {
                        classeAplicada = false;
                    }
                    break;
                case TierClasse.ClasseIntermediaria:
                    classeAplicada = false;
                    break;
                case TierClasse.ClasseAvancada:
                    classeAplicada = false;
                    break;
                case TierClasse.ClasseLendaria:
                    classeAplicada = false;
                    break;
                default:
                    classeAplicada = false;
                    break;
            }
            return classeAplicada;
        }
        public bool EsqueceClasse(PlayerMobile player)
        {
            bool esquece = true;
            int atendidos = 0;
            switch (Tier)
            {
                case TierClasse.ClasseBasica:
                    player.SendMessage("Skills máximas para esquecer classe:");
                    foreach (KeyValuePair<SkillName, double> beneficio in Beneficios)
                    {
                        player.SendMessage(String.Format("({2}/{1}) {0}", player.Skills[beneficio.Key].Name, player.Skills[beneficio.Key].Cap - beneficio.Value, player.Skills[beneficio.Key].Value));
                        if (player.Skills[beneficio.Key].Base > player.Skills[beneficio.Key].Cap - beneficio.Value)
                        {
                            esquece = false;
                        }
                        else
                        {
                            atendidos++;
                        }
                    }
                    player.SendMessage(String.Format("{0}/{1} dos requisitos.", atendidos, Beneficios.Count()));
                    if (atendidos == Beneficios.Count())
                    {
                        //Remove os beneficios da classe
                        foreach (KeyValuePair<SkillName, double> beneficio in Beneficios)
                        {
                            player.SendMessage(String.Format("Benefício Removido: Cap de {0} ({1}-{2})", player.Skills[beneficio.Key].Name, player.Skills[beneficio.Key].Cap, beneficio.Value));
                            player.Skills[beneficio.Key].Cap = 50.0;  //player.Skills[beneficio.Key].Cap -= beneficio.Value;
                        }

                        player.ClasseBasicaID = 0;
                        player.ClasseAbandonavel = TierClasse.SemClasse;
                        player.SendMessage(String.Format("Agora abandonou o caminho da classe {0}. Boa sorte!", this.Nome));
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case TierClasse.ClasseIntermediaria:
                    return false;
                    break;
                case TierClasse.ClasseAvancada:
                    return false;
                    break;
                case TierClasse.ClasseLendaria:
                    return false;
                    break;
                default:
                    return false;
                    break;
            }
        }
    }
}

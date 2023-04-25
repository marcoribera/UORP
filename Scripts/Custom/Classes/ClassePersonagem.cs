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

        public Dictionary<SkillName, double> getRequisitos()
        {
            return Requisitos;
        }
        public Dictionary<SkillName, double> getBeneficios()
        {
            return Beneficios;
        }
        public override string ToString()
        {
            //Retorna o nome da classe seguido do tier da classe
            return (Nome + " ("+(Tier.ToString()).Remove(0,6)+")");
        }

        public string DetalhesClasse()
        {
            string saida = "<B>Classe:</B> " + this.ToString() + "<BR><B>Descrição:</B> " + Desc + "<BR><B>Requisitos:</B>";
            //Aqui ele insere as informações com as regras para pegar a classe
            switch (Tier)
            {
                case TierClasse.ClasseBasica:
                    saida += "Pelo menos 4 desses<BR>";
                    foreach (KeyValuePair<SkillName, double> requisito in Requisitos)
                    {
                        saida += String.Format("{0} {1}<BR>", requisito.Value, requisito.Key.ToString());
                    }
                    saida += "<BR><B>Benefícios:</B><BR>Aumentos de cap<BR>";
                    foreach (KeyValuePair<SkillName, double> beneficio in Beneficios)
                        {
                        saida += String.Format("+{0} {1}<BR>", beneficio.Value, beneficio.Key.ToString());
                    }
                    break;
                case TierClasse.ClasseIntermediaria:
                    break;
                case TierClasse.ClasseAvancada:
                    break;
                case TierClasse.ClasseLendaria:
                    break;
            }
            return saida;
        }

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
                    if (player.ClasseBasicaID != 0)
                    {
                        player.SendMessage(32, "Esse personagem já tem Classe Básica");
                        return false;
                    }
                    else
                    {
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
                                player.Skills[beneficio.Key].Cap += beneficio.Value;  //player.Skills[beneficio.Key].Cap += beneficio.Value;
                            }
                            player.ClasseBasicaID = ID; //Define a classe do personagem
                            player.ClasseAbandonavel = TierClasse.ClasseBasica;
                            player.SendMessage(String.Format("Agora você é um membro da classe {0}, Parabéns!", this.Nome));
                            classeAplicada = true;
                        }
                        else
                        {
                            classeAplicada = false;
                        }
                    }
                    break;
                case TierClasse.ClasseIntermediaria:
                    if (player.ClasseIntermediariaID != 0)
                    {
                        player.SendMessage(32, "Esse personagem já tem Classe Intermediária");
                        return false;
                    }
                    else
                    {
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
                            player.ClasseIntermediariaID = ID; //Define a classe do personagem
                            player.ClasseAbandonavel = TierClasse.ClasseIntermediaria;
                            player.SendMessage(String.Format("Agora você é um membro da classe {0}, Parabéns!", this.Nome));
                            classeAplicada = true;
                        }
                        else
                        {
                            classeAplicada = false;
                        }
                    }
                    break;
                case TierClasse.ClasseAvancada:
                    if (player.ClasseAvancadaID != 0)
                    {
                        player.SendMessage(32, "Esse personagem já tem Classe Avançada");
                        return false;
                    }
                    else
                    {
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
                            player.ClasseAvancadaID = ID; //Define a classe do personagem
                            player.ClasseAbandonavel = TierClasse.ClasseAvancada;
                            player.SendMessage(String.Format("Agora você é um membro da classe {0}, Parabéns!", this.Nome));
                            classeAplicada = true;
                        }
                        else
                        {
                            classeAplicada = false;
                        }
                    }
                    break;
                case TierClasse.ClasseLendaria:
                    if (player.ClasseLendariaID != 0)
                    {
                        player.SendMessage(32, "Esse personagem já tem Classe Lendária");
                        return false;
                    }
                    else
                    {
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
                            player.ClasseLendariaID = ID; //Define a classe do personagem
                            player.ClasseAbandonavel = TierClasse.ClasseLendaria;
                            player.SendMessage(String.Format("Agora você é um membro da classe {0}, Parabéns!", this.Nome));
                            classeAplicada = true;
                        }
                        else
                        {
                            classeAplicada = false;
                        }
                    }
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
                    if (player.ClasseBasicaID == 0)
                    {
                        player.SendMessage(32, "Esse personagem não tem Classe Básica");
                        return false;
                    }
                    else
                    {
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
                            //player.ClasseAbandonavel = TierClasse.SemClasse; //Aqui so serve se quisermos liberar pra poder voltar mais de um tier de classe.
                            player.SendMessage(String.Format("Agora abandonou o caminho da classe {0}. Boa sorte!", this.Nome));
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    break;
                case TierClasse.ClasseIntermediaria:
                    if (player.ClasseIntermediariaID == 0)
                    {
                        player.SendMessage(32, "Esse personagem não tem Classe Intermediária");
                        return false;
                    }
                    else
                    {
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

                            player.ClasseIntermediariaID = 0;
                            //player.ClasseAbandonavel = TierClasse.ClasseBasica; //Aqui so serve se quisermos liberar pra poder voltar mais de um tier de classe.
                            player.SendMessage(String.Format("Agora abandonou o caminho da classe {0}. Boa sorte!", this.Nome));
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    break;
                case TierClasse.ClasseAvancada:
                    if (player.ClasseAvancadaID == 0)
                    {
                        player.SendMessage(32, "Esse personagem não tem Classe Avançada");
                        return false;
                    }
                    else
                    {
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

                            player.ClasseAvancadaID = 0;
                            //player.ClasseAbandonavel = TierClasse.ClasseIntermediaria; //Aqui so serve se quisermos liberar pra poder voltar mais de um tier de classe.
                            player.SendMessage(String.Format("Agora abandonou o caminho da classe {0}. Boa sorte!", this.Nome));
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    break;
                case TierClasse.ClasseLendaria:
                    if (player.ClasseLendariaID == 0)
                    {
                        player.SendMessage(32, "Esse personagem não tem Classe Len´dária");
                        return false;
                    }
                    else
                    {
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

                            player.ClasseLendariaID = 0;
                            //player.ClasseAbandonavel = TierClasse.SemClasse; //Aqui so serve se quisermos liberar pra poder voltar mais de um tier de classe.
                            player.SendMessage(String.Format("Agora abandonou o caminho da classe {0}. Boa sorte!", this.Nome));
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    break;
                default:
                    player.SendMessage("Reporte o que você tá tentando fazez com sua classe!");
                    return false;
                    break;
            }
        }
    }
}

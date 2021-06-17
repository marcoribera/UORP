using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Custom.Classes
{
    public static class ClassDef
    {
        private static Dictionary<int, ClassePersonagem> ListaClasses;

        public static void AddClasse(ClassePersonagem classe)
        {
            if (!ListaClasses.ContainsKey(classe.ID))
            {
                //Console.WriteLine(String.Format("Tentou inserir classe {0} com ID {1}", classe.Nome, classe.ID));
                ListaClasses.Add(classe.ID, classe);
            }
            else
            {
                Console.WriteLine(String.Format("Tentou inserir classe {0} no lugar da classe {1}, ambas com ID {2}", classe.Nome, ListaClasses[classe.ID].Nome, classe.ID));
            }
        }

        public static List<ClassePersonagem> GetClasses()
        {
            return ListaClasses.Values.ToList();
        }
        public static List<ClassePersonagem> GetClassesByTier(TierClasse tier)
        {
            List <ClassePersonagem> ClassesByTier = new List<ClassePersonagem>();
            foreach (KeyValuePair<int,ClassePersonagem> objeto in ListaClasses)
            {
                if(objeto.Value.Tier == tier)
                {
                    //Console.WriteLine(String.Format("Inseriu {0}:  {1}", objeto.Key, objeto.Value.Nome));
                    ClassesByTier.Add(objeto.Value);
                }
            }
            return ClassesByTier;
        }

        public static ClassePersonagem GetClasse(int id)
        {
            if (!ListaClasses.ContainsKey(id))
            {
                return null;
            }
            else
            {
                return ListaClasses[id];
            }
        }

        static ClassDef()
        {
            if(ListaClasses == null)
            {
                ListaClasses = new Dictionary<int, ClassePersonagem>();
            }
            if (ListaClasses.Count != 0)
                return;

            AddClasse(new ClassePersonagem(1, "Guerreiro", "Combatente Generalista", TierClasse.ClasseBasica,
                    new Dictionary<SkillName, double>()
                    { //Requisitos dos quais são necessários pelo menos 4 para pegar a classe (Funcionamento para pré-alfa)
                    {SkillName.Cortante, 50},
                    {SkillName.Perfurante, 50},
                    {SkillName.Contusivo, 50},
                    {SkillName.DuasMaos, 50},
                    {SkillName.UmaMao, 50},
                    {SkillName.Atirar, 50},
                    {SkillName.Bloqueio, 50},
                    {SkillName.Briga, 50},
                    {SkillName.PreparoFisico, 50},
                    {SkillName.Sobrevivencia, 50},
                    {SkillName.Bushido, 50}

                    }, //Aumentos de Cap conseguidos
                    new Dictionary<SkillName, double>()
                    {
                    {SkillName.Cortante, 20},
                    {SkillName.Perfurante, 20},
                    {SkillName.Contusivo, 20},
                    {SkillName.DuasMaos, 20},
                    {SkillName.UmaMao, 20},
                    {SkillName.Atirar, 20},
                    {SkillName.Bloqueio, 20},
                    {SkillName.Briga, 20},
                    {SkillName.PreparoFisico, 20},
                    {SkillName.Sobrevivencia, 20},
                    {SkillName.Bushido, 20}
                    }));


            AddClasse(new ClassePersonagem(2, "Ladino", "Treinado nas artes do subterfúgio", TierClasse.ClasseBasica,
                new Dictionary<SkillName, double>()
                { //Requisitos dos quais são necessários pelo menos 4 para pegar a classe (Funcionamento para pré-alfa)
                    {SkillName.Carisma, 50},
                    {SkillName.Furtividade, 50},
                    {SkillName.Mecanica, 50},
                    {SkillName.Percepcao, 50},
                    {SkillName.Prestidigitacao, 50},
                    {SkillName.Tocar, 50},
                    {SkillName.Provocacao, 50},
                    {SkillName.Pacificar, 50},
                    {SkillName.Caos, 50},
                    {SkillName.Perfurante, 50},
                    {SkillName.UmaMao, 50},
                    {SkillName.Anatomia, 50},
                    {SkillName.Envenenamento, 50},
                    {SkillName.Ninjitsu, 50}
                }, //Aumentos de Cap conseguidos
                new Dictionary<SkillName, double>()
                {
                    {SkillName.Carisma, 20},
                    {SkillName.Furtividade, 20},
                    {SkillName.Mecanica, 20},
                    {SkillName.Percepcao, 20},
                    {SkillName.Prestidigitacao, 20},
                    {SkillName.Tocar, 20},
                    {SkillName.Provocacao, 20},
                    {SkillName.Pacificar, 20},
                    {SkillName.Caos, 20},
                    {SkillName.Perfurante, 20},
                    {SkillName.UmaMao, 20},
                    {SkillName.Anatomia, 20},
                    {SkillName.Envenenamento, 20},
                    {SkillName.Ninjitsu, 20}
                }));


            AddClasse(new ClassePersonagem(3, "Mago", "Mago Generalista", TierClasse.ClasseBasica,
                new Dictionary<SkillName, double>()
                { //Requisitos dos quais são necessários pelo menos 4 para pegar a classe (Funcionamento para pré-alfa)
                    {SkillName.Arcanismo, 50},
                    {SkillName.Feiticaria, 50},
                    {SkillName.ImbuirMagica, 50},
                    {SkillName.Misticismo, 50},
                    {SkillName.Ordem, 50},
                    {SkillName.Necromancia, 50},
                    {SkillName.PoderMagico, 50},
                    {SkillName.ResistenciaMagica, 50},
                    {SkillName.Erudicao, 50},
                    {SkillName.PreparoFisico, 50},
                    {SkillName.Contusivo, 50},
                    {SkillName.Alquimia, 50}
                }, //Aumentos de Cap conseguidos
                new Dictionary<SkillName, double>()
                {
                    {SkillName.Arcanismo, 20},
                    {SkillName.Feiticaria, 20},
                    {SkillName.ImbuirMagica, 20},
                    {SkillName.Misticismo, 20},
                    {SkillName.Ordem, 20},
                    {SkillName.Necromancia, 20},
                    {SkillName.PoderMagico, 20},
                    {SkillName.ResistenciaMagica, 20},
                    {SkillName.Erudicao, 20},
                    {SkillName.PreparoFisico, 20},
                    {SkillName.Contusivo, 20},
                    {SkillName.Alquimia, 20}
                }));
            AddClasse(new ClassePersonagem(4, "Trabalhador", "Trabalhador Faz-Tudo", TierClasse.ClasseBasica,
                new Dictionary<SkillName, double>()
                { //Requisitos dos quais são necessários pelo menos 4 para pegar a classe (Funcionamento para pré-alfa)
                    {SkillName.Agricultura, 50},
                    {SkillName.Carpintaria, 50},
                    {SkillName.ConhecimentoArmaduras, 50},
                    {SkillName.ConhecimentoArmas, 50},
                    {SkillName.Costura, 50},
                    {SkillName.Extracao, 50},
                    {SkillName.Ferraria, 50},
                    {SkillName.Medicina, 50},
                    {SkillName.Veterinaria, 50},
                    {SkillName.Carisma, 50},
                    {SkillName.Percepcao, 50}
                }, //Aumentos de Cap conseguidos
                new Dictionary<SkillName, double>()
                {
                    {SkillName.Agricultura, 20},
                    {SkillName.Carpintaria, 20},
                    {SkillName.ConhecimentoArmaduras, 20},
                    {SkillName.ConhecimentoArmas, 20},
                    {SkillName.Costura, 20},
                    {SkillName.Extracao, 20},
                    {SkillName.Ferraria, 20},
                    {SkillName.Medicina, 20},
                    {SkillName.Veterinaria, 20},
                    {SkillName.Carisma, 20},
                    {SkillName.Percepcao, 20}
                }));
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Regions;
using Server.Misc;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Targeting;
using Server.ContextMenus;

namespace Server.Health
{
    public class InfectionUtility
    {
        private static double[][] m_AllScalars = new double[14][];

        public static double[][] AllScalars { get { return m_AllScalars; } set { m_AllScalars = value; } }

        public static bool RESET;

        public static void Initialize()
        {
            bool testTrue = false;
            try 
            {
                if (!RESET)
                {
                    testTrue = m_AllScalars[1][1] > 1; // This test will always return false, or will give an error (expected on load)
                    if (Core.Debug) Console.WriteLine("Test result: {0}.", testTrue);
                }
            }
            catch
            {
                testTrue = true;
            }

            if (RESET || testTrue)
            {
                if (Core.Debug && !RESET) Console.WriteLine("Test failed so we are initializing AllScalars.");

                RESET = false;
                // Adjust these values to increase the likelyhood of particular Viruses in different facets
                // Meningitis, Gastroenteritis, Botulism, Influenza, AvianFlu, Hepatitis, AIDS, Smallpox, Rabies
                m_AllScalars[0] = new double[] { 0.008, 0.0001, 0.006, 0.05, 0.001, 0.004, 0.03, 0.001, 0.01 };  // Felucca
                m_AllScalars[1] = new double[] { 0.008, 0.0001, 0.006, 0.05, 0.001, 0.004, 0.03, 0.001, 0.01 };  // Trammel
                m_AllScalars[2] = new double[] { 0.008, 0.0001, 0.006, 0.05, 0.001, 0.004, 0.03, 0.001, 0.01 };  // Ilshenar
                m_AllScalars[3] = new double[] { 0.008, 0.0001, 0.006, 0.05, 0.001, 0.004, 0.03, 0.001, 0.01 };  // Malas
                m_AllScalars[4] = new double[] { 0.008, 0.0001, 0.006, 0.05, 0.001, 0.004, 0.03, 0.001, 0.01 };  // Tokuno
                m_AllScalars[5] = new double[] { 0.008, 0.0001, 0.006, 0.05, 0.001, 0.004, 0.03, 0.001, 0.01 };  // TerMur

                // Adjust these values to increase the likelyhood of particular Disorders in different races
                // Autism, Bipolarism, Dyslexia, Hyperactivity, Obsessive
                m_AllScalars[6] = new double[] { 0.01, 0.01, 0.01, 0.01, 0.01 };           // Human
                m_AllScalars[7] = new double[] { 0.01, 0.01, 0.01, 0.01, 0.01 };           // Elf

                // Adjust these values to increase the likelyhood of particular Diseases in different races
                // Kidney, Arthritis, Anemia, Dystrophy, Palsy, Hodgkins, Diabetes, Cancer
                m_AllScalars[8] = new double[] { 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01 };           // Human
                m_AllScalars[9] = new double[] { 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01 };           // Elf

                // Adjust these values to increase the likelyhood of particular Syndromes in different races 
                // Albinism, Downs, Marfan, Bifida, Tourette
                m_AllScalars[10] = new double[] { 0.01, 0.01, 0.01, 0.01, 0.01 };           // Human
                m_AllScalars[11] = new double[] { 0.01, 0.01, 0.01, 0.01, 0.01 };           // Elf

                /* Adjust these values to increase the likelyhood of particular Allergies in different races
                 * Nuts, Dairy, Shellfish, Citrus, Wheat, Mushrooms, Feline, Canine, Equine, 
                 * Bovine, Arachnid, Rodent, Oak, Pine, Maple, Hay, Lavender, FruitTree 
                 */
                m_AllScalars[12] = new double[] { 
                0.001, 0.001, 0.01, 0.001, 0.001, 0.001, 0.01, 0.01, 0.01, 
                0.01, 0.01, 0.01, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001 };           // Human
                m_AllScalars[13] = new double[] { 
                0.01, 0.01, 0.001, 0.01, 0.01, 0.01, 0.001, 0.001, 0.001, 
                0.001, 0.001, 0.001, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01 };           // Elf
            }
        }

        public static double RandomDouble()
        {
            return RandomDouble(Int32.MaxValue);
        }

        // I replaced the standard RandomDouble function with this one, which more acurately reflects what I want in a random value.
        // This will return a random value between 0.00...01 and 1.0 depending on the precision input. (Currently 2147483647)
        public static double RandomDouble(int precision)
        {
            double a = (double)(Utility.RandomMinMax(1, precision));
            double b = (double)(Utility.RandomMinMax(1, precision));
            return Math.Min(a, b) / Math.Max(a, b);
        }

        // Adjust these values to change the likelihood of bacterial infection at various light levels.
        // (The theory is that the darker it is, the greater the chance of bacterial growth.)
        public static double BacterialScalar(Mobile from)
        {
            if (LightCycle.ComputeLevelFor(from) > 25) return 0.11;
            else if (LightCycle.ComputeLevelFor(from) > 19) return 0.08;
            else if (LightCycle.ComputeLevelFor(from) > 11) return 0.06;
            else if (LightCycle.ComputeLevelFor(from) > 7) return 0.04;
            else if (LightCycle.ComputeLevelFor(from) > 3) return 0.02;
            return 0.01;
        }

        public static double GetVirusScalar(Map map, VirusType typ) { return m_AllScalars[map.MapID][(int)typ]; }

        public static double GetDisorderScalar(Race race, DisorderType typ) { return m_AllScalars[race.RaceID + 6][(int)typ]; }

        public static double GetDiseaseScalar(Race race, DiseaseType typ) { return m_AllScalars[race.RaceID + 8][(int)typ]; }

        public static double GetSyndromeScalar(Race race, SyndromeType typ) { return m_AllScalars[race.RaceID + 10][(int)typ]; }

        public static double GetAllergyScalar(Race race, AllergyType typ) { return m_AllScalars[race.RaceID + 12][(int)typ]; }

        public static bool IsInfectious(Mobile m) { return (HasVirus(m) || HasBacteria(m)); }

        public static bool HasVirus(Mobile m) { return (MasterSystem.GetCondition(m).Viruses.Contains(true)); }

        public static bool HasBacteria(Mobile m) { return (MasterSystem.GetCondition(m).Bacteria.Contains(true)); }

        public static bool HasVirus(Mobile m, VirusType virus) { return (MasterSystem.GetCondition(m).Viruses[(int)virus]); }

        public static bool HasBacteria(Mobile m, BacteriaType bacteria) { return (MasterSystem.GetCondition(m).Bacteria[(int)bacteria]); }

        public static bool HasFungus(Mobile m, FungusType fungus) { return (MasterSystem.GetCondition(m).Fungi[(int)fungus]); }

        public static bool HasAllergy(Mobile m, AllergyType allergen) { return (MasterSystem.GetCondition(m).Allergies[(int)allergen]); }

        public static bool HasDisorder(Mobile m, DisorderType disorder) { return (MasterSystem.GetCondition(m).Disorders[(int)disorder]); }

        public static bool HasDisease(Mobile m, DiseaseType disease) { return (MasterSystem.GetCondition(m).Diseases[(int)disease]); }

        public static bool HasSyndrome(Mobile m, SyndromeType syndrome) { return (MasterSystem.GetCondition(m).Syndromes[(int)syndrome]); }

        public static bool HasAilment(Mobile m, AilmentType ailment) { return (MasterSystem.GetCondition(m).Ailments[(int)ailment]); }

        public static bool HasSymptom(Mobile m, SymptomType symptom) { return (MasterSystem.GetCondition(m).Symptoms[(int)symptom]); }

        public static bool HasWound(Mobile m, WoundType wound) { return (MasterSystem.GetCondition(m).Wounds[(int)wound]); }

        public static bool IsImmuneTo(Mobile m, ImmunityType immunity) { return (MasterSystem.GetCondition(m).Immunity[(int)immunity]); }

        public static bool IsImmuneTo(Mobile m, VirusType virus)
        {
            if (virus == VirusType.Gastroenteritis) return false;
            return (MasterSystem.GetCondition(m).Immunity[(int)virus]);
        }

        public static bool IsImmuneTo(Mobile m, BacteriaType bacteria)
        {
            if (bacteria == BacteriaType.Chickenpox)
                return (MasterSystem.GetCondition(m).Immunity[(int)ImmunityType.Chickenpox]);
            if (bacteria == BacteriaType.Strep)
                return (MasterSystem.GetCondition(m).Immunity[(int)ImmunityType.Strep]);
            else return false;
        }
    }

    // It is NOT recommended to change the number of elements in these enums, although changing some of the names would probably be OK.
    // A few of the formulas and both gumps rely on the exact number of elements here. Gastroenteritis, Chickenpox and Strep are named above.
    public enum VirusType { Meningitis = 0, Gastroenteritis, Botulism, Influenza, AvianFlu, Hepatitis, AIDS, Smallpox, Rabies }
    public enum BacteriaType { Chickenpox = 0, EColi, Pneumonia, Conjunctivitis, Impetigo, Strep }
    public enum FungusType { Appendicitis = 0, Tinea, Candida, Dermititis }
    public enum AllergyType { Nuts = 0, Dairy, Shellfish, Citrus, Wheat, Mushrooms, Feline, Canine, Equine, Bovine,
        Arachnid, Rodent, Oak, Pine, Maple, Hay, Lavender, FruitTree }
    public enum DisorderType { Autism = 0, Bipolarism, Dyslexia, Hyperactivity, Obsessive }
    public enum DiseaseType { Kidney = 0, Arthritis, Anemia, Dystrophy, Palsy, Hodgkins, Diabetes, Cancer }
    public enum SyndromeType { Albinism = 0, Downs, Marfan, Bifida, Tourette }
    public enum AilmentType { Asthma = 0, Scoliosis, Epilepsy, CysticFibrosis, Blindness, Deafness, Laryngitis, Acne }
    public enum SymptomType { Indigestion = 0, Headache, Cramps, Vomiting, Spots, Rash, MoodSwings, Swearing, LossOfHair, 
        DryMouth, Wheezing, Coughing, Sneezing, Itching, Boils, LossOfSight, LossOfHearing, LossOfMobility, LightSensitivity }
    public enum WoundType { Cut = 0, Burn, Tear, Break, Puncture, Amputation }
    public enum ImmunityType { Meningitis = 0, Appendicitis, Botulism, Influenza, AvianFlu, Hepatitis, AIDS, Smallpox, Rabies, Chickenpox, Strep }
    
    public class Condition
    {
        private Mobile m_Owner;
        private List<bool> m_Viruses;
        private List<bool> m_Bacteria;
        private List<bool> m_Fungi;
        private List<bool> m_Allergies;
        private List<bool> m_Disorders;
        private List<bool> m_Diseases;
        private List<bool> m_Syndromes;
        private List<bool> m_Ailments;
        private List<bool> m_Symptoms;
        private List<bool> m_Wounds;
        private List<bool> m_Immunity;

        public Mobile Owner { get { return m_Owner; } set { m_Owner = value; } }
        public List<bool> Viruses { get { return m_Viruses; } set { m_Viruses = value; } }
        public List<bool> Bacteria { get { return m_Bacteria; } set { m_Bacteria = value; } }
        public List<bool> Fungi { get { return m_Fungi; } set { m_Fungi = value; } }
        public List<bool> Allergies { get { return m_Allergies; } set { m_Allergies = value; } }
        public List<bool> Disorders { get { return m_Disorders; } set { m_Disorders = value; } }
        public List<bool> Diseases { get { return m_Diseases; } set { m_Diseases = value; } }
        public List<bool> Syndromes { get { return m_Syndromes; } set { m_Syndromes = value; } }
        public List<bool> Ailments { get { return m_Ailments; } set { m_Ailments = value; } }
        public List<bool> Symptoms { get { return m_Symptoms; } set { m_Symptoms = value; } }
        public List<bool> Wounds { get { return m_Wounds; } set { m_Wounds = value; } }
        public List<bool> Immunity { get { return m_Immunity; } set { m_Immunity = value; } }

        public Condition(Mobile owner)
        {
            /*When a character is added to the system, we check a random number against the odds of being born with
             * a genetic disease, syndrome, or disorder. Also, all new characters are checked for allergies.
             */
            m_Owner = owner;
            m_Viruses = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(VirusType)).Length; x++) { m_Viruses.Add(false); }
            m_Bacteria = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(BacteriaType)).Length; x++) { m_Bacteria.Add(false); }
            m_Fungi = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(FungusType)).Length; x++) { m_Fungi.Add(false); }
            m_Allergies = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(AllergyType)).Length; x++)
            {
                m_Allergies.Add(InfectionUtility.RandomDouble() < InfectionUtility.GetAllergyScalar(owner.Race, (AllergyType)x));
            }
            m_Disorders = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(DisorderType)).Length; x++)
            {
                m_Disorders.Add(InfectionUtility.RandomDouble() < InfectionUtility.GetDisorderScalar(owner.Race, (DisorderType)x));
            }
            m_Diseases = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(DiseaseType)).Length; x++)
            {
                m_Diseases.Add(InfectionUtility.RandomDouble() < InfectionUtility.GetDiseaseScalar(owner.Race, (DiseaseType)x));
            }
            m_Syndromes = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(SyndromeType)).Length; x++)
            {
                m_Syndromes.Add(InfectionUtility.RandomDouble() < InfectionUtility.GetSyndromeScalar(owner.Race, (SyndromeType)x));
            }
            m_Ailments = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(AilmentType)).Length; x++) { m_Ailments.Add(false); }
            m_Symptoms = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(SymptomType)).Length; x++) { m_Symptoms.Add(false); }
            m_Wounds = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(WoundType)).Length; x++) { m_Wounds.Add(false); }

            // Immunity: Meningitis, Botulism, Influenza, AvianFlu, Hepatitis, Smallpox, Rabies, Chickenpox, Strep, Appendicitis
            m_Immunity = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(ImmunityType)).Length; x++) { m_Immunity.Add(false); }
        }

        public Condition(GenericReader reader)
        {
            m_Owner = reader.ReadMobile();
            m_Viruses = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(VirusType)).Length; x++) { m_Viruses.Add(reader.ReadBool()); }
            m_Bacteria = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(BacteriaType)).Length; x++) { m_Bacteria.Add(reader.ReadBool()); }
            m_Fungi = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(FungusType)).Length; x++) { m_Fungi.Add(reader.ReadBool()); }
            m_Allergies = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(AllergyType)).Length; x++) { m_Allergies.Add(reader.ReadBool()); }
            m_Disorders = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(DisorderType)).Length; x++) { m_Disorders.Add(reader.ReadBool()); }
            m_Diseases = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(DiseaseType)).Length; x++) { m_Diseases.Add(reader.ReadBool()); }
            m_Syndromes = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(SyndromeType)).Length; x++) { m_Syndromes.Add(reader.ReadBool()); }
            m_Ailments = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(AilmentType)).Length; x++) { m_Ailments.Add(reader.ReadBool()); }
            m_Symptoms = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(SymptomType)).Length; x++) { m_Symptoms.Add(reader.ReadBool()); }
            m_Wounds = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(WoundType)).Length; x++) { m_Wounds.Add(reader.ReadBool()); }
            m_Immunity = new List<bool>();
            for (int x = 0; x < Enum.GetValues(typeof(ImmunityType)).Length; x++) { m_Immunity.Add(reader.ReadBool()); }
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write((Mobile)m_Owner);
            for (int x = 0; x < Enum.GetValues(typeof(VirusType)).Length; x++) { writer.Write(m_Viruses[x]); }
            for (int x = 0; x < Enum.GetValues(typeof(BacteriaType)).Length; x++) { writer.Write(m_Bacteria[x]); }
            for (int x = 0; x < Enum.GetValues(typeof(FungusType)).Length; x++) { writer.Write(m_Fungi[x]); }
            for (int x = 0; x < Enum.GetValues(typeof(AllergyType)).Length; x++) { writer.Write(m_Allergies[x]); }
            for (int x = 0; x < Enum.GetValues(typeof(DisorderType)).Length; x++) { writer.Write(m_Disorders[x]); }
            for (int x = 0; x < Enum.GetValues(typeof(DiseaseType)).Length; x++) { writer.Write(m_Diseases[x]); }
            for (int x = 0; x < Enum.GetValues(typeof(SyndromeType)).Length; x++) { writer.Write(m_Syndromes[x]); }
            for (int x = 0; x < Enum.GetValues(typeof(AilmentType)).Length; x++) { writer.Write(m_Ailments[x]); }
            for (int x = 0; x < Enum.GetValues(typeof(SymptomType)).Length; x++) { writer.Write(m_Symptoms[x]); }
            for (int x = 0; x < Enum.GetValues(typeof(WoundType)).Length; x++) { writer.Write(m_Wounds[x]); }
            for (int x = 0; x < Enum.GetValues(typeof(ImmunityType)).Length; x++) { writer.Write(m_Immunity[x]); }
        }
    }

    public class MasterSystem : Item
    {
        private static Dictionary<int, Condition> m_PlayerValues;
        public static Dictionary<int, Condition> PlayerValues { get { return m_PlayerValues; } set { m_PlayerValues = value; } }

        public static void Initialize()
        {
            EventSink.CharacterCreated += new CharacterCreatedEventHandler(EventSink_CharacterCreatedHealthCheck);
            EventSink.HungerChanged += new HungerChangedEventHandler(EventSink_HungerChangedHealthCheck);
            EventSink.PlayerDeath += new PlayerDeathEventHandler(EventSink_PlayerDeathHealthCheck);
            EventSink.AggressiveAction += new AggressiveActionEventHandler(EventSink_AggressiveActionHealthCheck);
            EventSink.Movement += new MovementEventHandler(EventSink_MovementHealthCheck);
            EventSink.Login += new LoginEventHandler(EventSink_LoginHealthCheck);
            EventSink.Speech += new SpeechEventHandler(EventSink_SpeechHealthCheck);
        }

        static void EventSink_SpeechHealthCheck(SpeechEventArgs args)
        {
            Mobile mobile = args.Mobile;
            Condition con = GetCondition(mobile);

            /* When a character speaks, check nearby players as possible targets of contagion.
             * Also, check if player has Syndrome or other condition that would affect Speech.
             * I was thinking Tourette Syndrome might be interesting here...
             */
            if (InfectionUtility.IsInfectious(mobile))
            {
            }
        }

        static void EventSink_LoginHealthCheck(LoginEventArgs args)
        {
            Mobile mobile = args.Mobile;
            Condition con = GetCondition(mobile);
            /* When a character logs in, we make sure they are in the System.
             */
        }

        private static void EventSink_CharacterCreatedHealthCheck(CharacterCreatedEventArgs args)
        {
            Mobile mobile = args.Mobile;
            /* When a character is created, we add them to the system. */
            Condition con = GetCondition(mobile);
        }

        private static void EventSink_HungerChangedHealthCheck(HungerChangedEventArgs args)
        {
            Mobile mobile = args.Mobile;
            Condition con = GetCondition(mobile);
            int oldvalue = args.OldValue;
            if (oldvalue < mobile.Hunger)
            {
                /* If they just ate, we check to see if the food was tainted.
                 * We also need to check against any food allergies they have.
                 * We also check to see if eating certain foods would cure some conditions.
                 * This function is not able to be implemented under the current system
                 * because RunUO does not remember what you ate within the Event args.
                 * ...Possible core hack or future enhancement...
                 */
            }
        }

        private static void EventSink_PlayerDeathHealthCheck(PlayerDeathEventArgs args)
        {
            Mobile mobile = args.Mobile;
            Condition con = GetCondition(mobile);
            /* When a player dies, certain health problems will go away. (Symptoms, Wounds, Fungus)
             * We need to keep viruses and bacteria alive so their corpse can infect people...
             */


        }

        private static void EventSink_AggressiveActionHealthCheck(AggressiveActionEventArgs args)
        {
            Mobile from = args.Aggressor;
            Condition fromCon = GetCondition(from);
            Mobile to = args.Aggressed;
            Condition toCon = GetCondition(to);
            //When a player attacks or is attacked by another Mobile, we check for infectious conditions.

            if (InfectionUtility.IsInfectious(from) || InfectionUtility.IsInfectious(to))
            {
                double scalar;
                for (int x = 0; x < Enum.GetValues(typeof(VirusType)).Length; x++)
                {
                    if (!InfectionUtility.HasVirus(from, (VirusType)x) && InfectionUtility.HasVirus(to, (VirusType)x) &&
                        !InfectionUtility.IsImmuneTo(from, (VirusType)x))
                    {
                        scalar = InfectionUtility.GetVirusScalar(from.Map, (VirusType)x);
                        if (InfectionUtility.RandomDouble() < scalar)
                        {
                            if (Core.Debug && from.Player)
                                from.SendMessage("You have been infected with the {0} virus from {1}.", ((VirusType)x).ToString(), to.Name);
                            fromCon.Viruses[x] = true;
                        }
                    }
                    else if (InfectionUtility.HasVirus(from, (VirusType)x) && !InfectionUtility.HasVirus(to, (VirusType)x) &&
                        !InfectionUtility.IsImmuneTo(to, (VirusType)x))
                    {
                        scalar = InfectionUtility.GetVirusScalar(from.Map, (VirusType)x);
                        if (InfectionUtility.RandomDouble() < scalar)
                        {
                            if (Core.Debug && to.Player)
                                to.SendMessage("You have been infected with the {0} virus from {1}.", ((VirusType)x).ToString(), from.Name);
                            toCon.Viruses[x] = true;
                        }
                    }
                }
                for (int x = 0; x < Enum.GetValues(typeof(BacteriaType)).Length; x++)
                {
                    if (!InfectionUtility.HasBacteria(from, (BacteriaType)x) && InfectionUtility.HasBacteria(to, (BacteriaType)x) &&
                        !InfectionUtility.IsImmuneTo(from, (BacteriaType)x))
                    {
                        scalar = InfectionUtility.BacterialScalar(to);
                        if (InfectionUtility.RandomDouble() < scalar)
                        {
                            if (Core.Debug && from.Player)
                                from.SendMessage("You have been infected with {0} from {1}.", ((BacteriaType)x).ToString(), to.Name);
                            fromCon.Bacteria[x] = true;
                        }
                    }
                    else if (InfectionUtility.HasBacteria(from, (BacteriaType)x) && !InfectionUtility.HasBacteria(to, (BacteriaType)x) &&
                        !InfectionUtility.IsImmuneTo(to, (BacteriaType)x))
                    {
                        scalar = InfectionUtility.BacterialScalar(to);
                        if (InfectionUtility.RandomDouble() < scalar)
                        {
                            if (Core.Debug && to.Player)
                                to.SendMessage("You have been infected with {0} from {1}.", ((BacteriaType)x).ToString(), from.Name);
                            toCon.Bacteria[x] = true;
                        }
                    }
                }
            }
        }

        private static void EventSink_MovementHealthCheck(MovementEventArgs args)
        {
            Mobile mover = args.Mobile;
            Condition con = GetCondition(mover);
            bool opened = false;
            List<Mobile> infectious = new List<Mobile>();
            foreach (object o in mover.GetObjectsInRange(1)) // We check against corpses that we move near
            {
                if (o is Corpse)
                {
                    Corpse c = o as Corpse;
                    if (c.Owner == null || c.Owner.Deleted) continue;

                    if (InfectionUtility.IsInfectious(c.Owner))
                    {
                        if (c.Openers.Contains(mover)) opened = true;  //They opened an infectious corpse...ew!
                        infectious.Add(c.Owner);
                    }
                }
            }

            if (infectious.Count > 0)
            {
                foreach (Mobile toCheck in infectious)
                {
                    for (int x = 0; x < Enum.GetValues(typeof(VirusType)).Length; x++)
                    {
                        if (InfectionUtility.HasVirus(toCheck, (VirusType)x) && !InfectionUtility.HasVirus(mover, (VirusType)x))
                        {
                            double scalar = InfectionUtility.GetVirusScalar(mover.Map, (VirusType)x);
                            if (opened) scalar *= 2;
                            if (InfectionUtility.RandomDouble() < scalar)
                            {
                                if (Core.Debug && mover.Player) mover.SendMessage("You have been infected with the {0} virus from the corpse of {1}.",
                                    ((VirusType)x).ToString(), toCheck.Name);
                                con.Viruses[x] = true;
                            }
                        }
                    }
                    for (int x = 0; x < Enum.GetValues(typeof(BacteriaType)).Length; x++)
                    {
                        if (InfectionUtility.HasBacteria(toCheck, (BacteriaType)x) && !InfectionUtility.HasBacteria(mover, (BacteriaType)x))
                        {
                            double bacScalar = InfectionUtility.BacterialScalar(mover);
                            if (opened) bacScalar *= 2;
                            if (InfectionUtility.RandomDouble() < bacScalar)
                            {
                                if (Core.Debug && mover.Player) mover.SendMessage("You have been infected with {0} from the corpse of {1}.",
                                    ((BacteriaType)x).ToString(), toCheck.Name);
                                con.Bacteria[x] = true;
                            }
                        }
                    }
                }
            }
        }

        [Constructable]
        public MasterSystem()
            : base(0xF91)
        {
            Name = "Health System Master Control Item";
            if (m_PlayerValues == null)
                m_PlayerValues = new Dictionary<int, Condition>();
        }

        public MasterSystem(Serial serial)
            : base(serial)
        {
        }

        public static Condition GetCondition(Mobile m)
        {
            if (m_PlayerValues == null)
            {
                MasterSystem item = new MasterSystem();
                if (m.AccessLevel >= AccessLevel.Administrator && m.Backpack != null) m.Backpack.DropItem(item);
                else
                {
                    item.Visible = false;
                    item.DropToWorld(m, m.Location);
                    Console.WriteLine("Health System Master Control Item was dropped at {0}, map {1}.", m.Location.ToString(), m.Map.ToString());
                }
            }
            if (!PlayerValues.ContainsKey(m.Serial.Value)) PlayerValues.Add(m.Serial.Value, new Condition(m));

            return PlayerValues[m.Serial.Value];
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1); // version

            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < Enum.GetValues(typeof(VirusType)).Length; y++)
                    writer.Write((double)InfectionUtility.AllScalars[x][y]);
            }
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < Enum.GetValues(typeof(DisorderType)).Length; y++)
                    writer.Write((double)InfectionUtility.AllScalars[x + 6][y]);
                for (int y = 0; y < Enum.GetValues(typeof(DiseaseType)).Length; y++)
                    writer.Write((double)InfectionUtility.AllScalars[x + 8][y]);
                for (int y = 0; y < Enum.GetValues(typeof(SyndromeType)).Length; y++)
                    writer.Write((double)InfectionUtility.AllScalars[x + 10][y]);
                for (int y = 0; y < Enum.GetValues(typeof(AllergyType)).Length; y++)
                    writer.Write((double)InfectionUtility.AllScalars[x + 12][y]);
            }

            writer.Write((int)m_PlayerValues.Count);
            if (m_PlayerValues.Count > 0)
            {
                foreach (Condition c in m_PlayerValues.Values)
                {
                    if (c.Owner == null) continue;
                    writer.Write((int)c.Owner.Serial.Value);
                    c.Serialize(writer);
                }
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            InfectionUtility.RESET = false;
            switch (version)
            {
                case 1:
                    {
                        InfectionUtility.Initialize();
                        for (int x = 0; x < 6; x++)
                        {
                            for (int y = 0; y < Enum.GetValues(typeof(VirusType)).Length; y++)
                                InfectionUtility.AllScalars[x][y] = reader.ReadDouble();
                        }
                        for (int x = 0; x < 2; x++)
                        {
                            for (int y = 0; y < Enum.GetValues(typeof(DisorderType)).Length; y++)
                                InfectionUtility.AllScalars[x + 6][y] = reader.ReadDouble();
                            for (int y = 0; y < Enum.GetValues(typeof(DiseaseType)).Length; y++)
                                InfectionUtility.AllScalars[x + 8][y] = reader.ReadDouble();
                            for (int y = 0; y < Enum.GetValues(typeof(SyndromeType)).Length; y++)
                                InfectionUtility.AllScalars[x + 10][y] = reader.ReadDouble();
                            for (int y = 0; y < Enum.GetValues(typeof(AllergyType)).Length; y++)
                                InfectionUtility.AllScalars[x + 12][y] = reader.ReadDouble();
                        }
                        goto case 0;
                    }
                case 0:
                    {
                        if (m_PlayerValues == null)
                            m_PlayerValues = new Dictionary<int, Condition>();
                        int num = reader.ReadInt();
                        if (num > 0)
                        {
                            for (int x = 0; x < num; x++)
                            {
                                try
                                {
                                    m_PlayerValues.Add(reader.ReadInt(), new Condition(reader));
                                }
                                catch { continue; }
                            }
                        }
                        break;
                    }
            }
        }
    }

    public class HealthGump : Gump
    {
        public static void Initialize()
        {
            CommandSystem.Register("Health", AccessLevel.Player, new CommandEventHandler(Health_OnCommand));
            CommandSystem.Register("ViewHealth", AccessLevel.GameMaster, new CommandEventHandler(ViewHealth_OnCommand));
        }

        private const int GreenHue = 0x40;
        private const int RedHue = 0x20;
        private const int BlueHue = 0x777;
        private Mobile m_GM;
        private Mobile m_From;

        [Usage("ViewHealth")]
        [Description("Allows GM to view the HealthGump for any Mobile.")]
        public static void ViewHealth_OnCommand(CommandEventArgs e)
        {
            Mobile caller = e.Mobile;
            caller.Target = new InternalTarget();
        }

        private class InternalTarget : Target
        {
            public InternalTarget() : base(-1, true, TargetFlags.None) { }
            protected override void OnTarget(Mobile from, object o) { if (o is Mobile) from.SendGump(new HealthGump(from, (Mobile)o)); }
        }

        [Usage("Health")]
        [Description("Makes a call to the HealthGump gump.")]
        public static void Health_OnCommand(CommandEventArgs e)
        {
            Mobile caller = e.Mobile;

            if (caller.HasGump(typeof(HealthGump)))
                caller.CloseGump(typeof(HealthGump));
            caller.SendGump(new HealthGump(caller));
        }

        private Condition c;

        public HealthGump(Mobile from) : this(from, from) { }

        public HealthGump(Mobile gm, Mobile from)
            : base(0, 0)
        {
            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            m_GM = gm;
            m_From = from;

            AddPage(0);
            AddBackground(0, 0, 780, 480, 9350);

            c = MasterSystem.GetCondition(from);
            AddLabel(15, 15, BlueHue, "Viruses");
            string[] types = new string[] { "Bacteria", "Fungi", "Allergies", "Disorders", "Diseases", "Syndromes", "Ailments" };
            for (int x = 0; x < 7; x++) AddLabel(117 + (x * 92), 15, BlueHue, types[x]);
            AddLabel(117, 165, BlueHue, "Symptoms");
            AddLabel(485, 215, BlueHue, "Wounds");
            AddLabel(577, 215, BlueHue, "Immunity");
            for (int x = 0; x < c.Viruses.Count; x++) AddLabel(15, 30 + (x * 15), c.Viruses[x] ? GreenHue : RedHue, ((VirusType)x).ToString());
            for (int x = 0; x < c.Bacteria.Count; x++) AddLabel(117, 30 + (x * 15), c.Bacteria[x] ? GreenHue : RedHue, ((BacteriaType)x).ToString());
            for (int x = 0; x < c.Fungi.Count; x++) AddLabel(209, 30 + (x * 15), c.Fungi[x] ? GreenHue : RedHue, ((FungusType)x).ToString());
            for (int x = 0; x < c.Allergies.Count; x++) AddLabel(301, 30 + (x * 15), c.Allergies[x] ? GreenHue : RedHue, ((AllergyType)x).ToString());
            for (int x = 0; x < c.Disorders.Count; x++) AddLabel(393, 30 + (x * 15), c.Disorders[x] ? GreenHue : RedHue, ((DisorderType)x).ToString());
            for (int x = 0; x < c.Diseases.Count; x++) AddLabel(485, 30 + (x * 15), c.Diseases[x] ? GreenHue : RedHue, ((DiseaseType)x).ToString());
            for (int x = 0; x < c.Syndromes.Count; x++) AddLabel(577, 30 + (x * 15), c.Syndromes[x] ? GreenHue : RedHue, ((SyndromeType)x).ToString());
            for (int x = 0; x < c.Ailments.Count; x++) AddLabel(669, 30 + (x * 15), c.Ailments[x] ? GreenHue : RedHue, ((AilmentType)x).ToString());
            for (int x = 0; x < c.Symptoms.Count; x++) AddLabel(117, 180 + (x * 15), c.Symptoms[x] ? GreenHue : RedHue, ((SymptomType)x).ToString());
            for (int x = 0; x < c.Wounds.Count; x++) AddLabel(485, 230 + (x * 15), c.Wounds[x] ? GreenHue : RedHue, ((WoundType)x).ToString());
            for (int x = 0; x < c.Immunity.Count; x++) AddLabel(577, 230 + (x * 15), c.Immunity[x] ? GreenHue : RedHue, ((ImmunityType)x).ToString());
            if (m_GM.AccessLevel >= AccessLevel.GameMaster)
            {
                for (int x = 0; x < c.Viruses.Count; x++) AddButton(0, 32 + (x * 15), 1210, 1210, x + 100, GumpButtonType.Reply, 0);
                for (int x = 0; x < c.Bacteria.Count; x++) AddButton(102, 32 + (x * 15), 1210, 1210, x + 200, GumpButtonType.Reply, 0);
                for (int x = 0; x < c.Fungi.Count; x++) AddButton(194, 32 + (x * 15), 1210, 1210, x + 300, GumpButtonType.Reply, 0);
                for (int x = 0; x < c.Allergies.Count; x++) AddButton(286, 32 + (x * 15), 1210, 1210, x + 400, GumpButtonType.Reply, 0);
                for (int x = 0; x < c.Disorders.Count; x++) AddButton(378, 32 + (x * 15), 1210, 1210, x + 500, GumpButtonType.Reply, 0);
                for (int x = 0; x < c.Diseases.Count; x++) AddButton(470, 32 + (x * 15), 1210, 1210, x + 600, GumpButtonType.Reply, 0);
                for (int x = 0; x < c.Syndromes.Count; x++) AddButton(562, 32 + (x * 15), 1210, 1210, x + 700, GumpButtonType.Reply, 0);
                for (int x = 0; x < c.Ailments.Count; x++) AddButton(654, 32 + (x * 15), 1210, 1210, x + 800, GumpButtonType.Reply, 0);
                for (int x = 0; x < c.Symptoms.Count; x++) AddButton(102, 182 + (x * 15), 1210, 1210, x + 900, GumpButtonType.Reply, 0);
                for (int x = 0; x < c.Wounds.Count; x++) AddButton(470, 232 + (x * 15), 1210, 1210, x + 1000, GumpButtonType.Reply, 0);
                for (int x = 0; x < c.Immunity.Count; x++) AddButton(562, 232 + (x * 15), 1210, 1210, x + 1100, GumpButtonType.Reply, 0);
            }

        }

        public override void OnResponse(Network.NetState sender, RelayInfo info)
        {
            int id = info.ButtonID;

            if (id < 100 || m_GM.AccessLevel < AccessLevel.GameMaster) return;
            else if (id < 200) c.Viruses[id - 100] = !c.Viruses[id - 100];
            else if (id < 300) c.Bacteria[id - 200] = !c.Bacteria[id - 200];
            else if (id < 400) c.Fungi[id - 300] = !c.Fungi[id - 300];
            else if (id < 500) c.Allergies[id - 400] = !c.Allergies[id - 400];
            else if (id < 600) c.Disorders[id - 500] = !c.Disorders[id - 500];
            else if (id < 700) c.Diseases[id - 600] = !c.Diseases[id - 600];
            else if (id < 800) c.Syndromes[id - 700] = !c.Syndromes[id - 700];
            else if (id < 900) c.Ailments[id - 800] = !c.Ailments[id - 800];
            else if (id < 1000) c.Symptoms[id - 900] = !c.Symptoms[id - 900];
            else if (id < 1100) c.Wounds[id - 1000] = !c.Wounds[id - 1000];
            else if (id < 1200) c.Immunity[id - 1100] = !c.Immunity[id - 1100];
            else return;

            m_GM.SendGump(new HealthGump(m_GM, m_From));
        }
    }

    public class HealthScalarGump : Gump
    {
        public static void Initialize()
        {
            CommandSystem.Register("HealthScalar", AccessLevel.Administrator, new CommandEventHandler(HealthScalar_OnCommand));
        }

        private const int GreenHue = 0x40;
        private const int RedHue = 0x20;
        private const int BlueHue = 0x777;

        [Usage("HealthScalar")]
        [Description("Allows an Admin to modify Global Health Scalar values.")]
        public static void HealthScalar_OnCommand(CommandEventArgs e)
        {
            Mobile caller = e.Mobile;

            if (caller.HasGump(typeof(HealthScalarGump)))
                caller.CloseGump(typeof(HealthScalarGump));
            caller.SendGump(new HealthScalarGump(caller));
        }

        private Condition c;

        public HealthScalarGump(Mobile from)
            : base(0, 0)
        {
            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            c = MasterSystem.GetCondition(from);
            AddPage(0);
            AddBackground(0, 0, 780, 480, 9350);

            string[] facetsA = new string[] { "Felucca", "Trammel", "Ilshenar" };
            string[] facetsB = new string[] { "", "", "", "Malas", "Tokuno", "TerMur" };
            string[] races = new string[] { "Human", "Elf" };

            AddPage(1);
            AddLabel(35, 11, BlueHue, "Viruses");
            AddButton(30, 435, 238, 240, 2, GumpButtonType.Reply, 0); //APPLY button
            AddButton(700, 11, 245, 244, 0, GumpButtonType.Page, 3); //DEFAULT button
            AddButton(235, 435, 2471, 2470, 0, GumpButtonType.Page, 2); //Next Page
            
            for (int x = 0; x < facetsA.Length; x++)
            {
                AddLabel(165 + (x * 175), 50, BlueHue, facetsA[x]);
                for (int y = 0; y < c.Viruses.Count; y++) AddLabel(165 + (x * 175), 67 + (y * 17), GreenHue, ((VirusType)y).ToString());
                for (int y = 0; y < c.Viruses.Count; y++)
                    AddTextEntry(112 + (x * 175), 67 + (y * 17), 50, 15, BlueHue, ((x + 1) * 100) + y,
                        InfectionUtility.GetVirusScalar(Map.AllMaps[x], (VirusType)y).ToString());
            }
            for (int x = 3; x < facetsB.Length; x++)
            {
                AddLabel(165 + ((x - 3) * 175), 250, BlueHue, facetsB[x]);
                for (int y = 0; y < c.Viruses.Count; y++) AddLabel(165 + ((x - 3) * 175), 267 + (y * 17), GreenHue, ((VirusType)y).ToString());
                for (int y = 0; y < c.Viruses.Count; y++)
                    AddTextEntry(112 + ((x - 3) * 175), 267 + (y * 17), 50, 15, BlueHue, ((x + 1) * 100) + y,
                        InfectionUtility.GetVirusScalar(Map.AllMaps[x], (VirusType)y).ToString());
            }

            AddPage(2);
            AddLabel(70, 15, BlueHue, "Allergies");
            AddLabel(430, 15, BlueHue, "Disorders");
            AddLabel(430, 145, BlueHue, "Diseases");
            AddLabel(430, 325, BlueHue, "Syndromes");
            AddButton(30, 435, 238, 240, 2, GumpButtonType.Reply, 0); //APPLY button
            AddButton(700, 11, 245, 244, 0, GumpButtonType.Page, 3); //DEFAULT button
            AddButton(235, 435, 2468, 2467, 0, GumpButtonType.Page, 1); //Previous Page
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < c.Disorders.Count; y++) AddLabel(440 + (x * 175), 52 + (y * 17), GreenHue, ((DisorderType)y).ToString());
                for (int y = 0; y < c.Disorders.Count; y++)
                    AddTextEntry(387 + (x * 175), 52 + (y * 17), 50, 15, BlueHue, ((x + 1) * 1000) + y,
                        InfectionUtility.GetDisorderScalar(Race.Races[x], (DisorderType)y).ToString());
                AddLabel(440 + (x * 175), 165, BlueHue, Race.Races[x].ToString());
                for (int y = 0; y < c.Diseases.Count; y++) AddLabel(440 + (x * 175), 182 + (y * 17), GreenHue, ((DiseaseType)y).ToString());
                for (int y = 0; y < c.Diseases.Count; y++)
                    AddTextEntry(387 + (x * 175), 182 + (y * 17), 50, 15, BlueHue, ((x + 3) * 1000) + y,
                        InfectionUtility.GetDiseaseScalar(Race.Races[x], (DiseaseType)y).ToString());
                AddLabel(440 + (x * 175), 345, BlueHue, Race.Races[x].ToString());
                for (int y = 0; y < c.Syndromes.Count; y++) AddLabel(440 + (x * 175), 362 + (y * 17), GreenHue, ((SyndromeType)y).ToString());
                for (int y = 0; y < c.Syndromes.Count; y++)
                    AddTextEntry(387 + (x * 175), 362 + (y * 17), 50, 15, BlueHue, ((x + 5) * 1000) + y,
                        InfectionUtility.GetSyndromeScalar(Race.Races[x], (SyndromeType)y).ToString());
                AddLabel(80 + (x * 175), 35, BlueHue, Race.Races[x].ToString());
                for (int y = 0; y < c.Allergies.Count; y++) AddLabel(80 + (x * 175), 52 + (y * 17), GreenHue, ((AllergyType)y).ToString());
                for (int y = 0; y < c.Allergies.Count; y++)
                    AddTextEntry(27 + (x * 175), 52 + (y * 17), 50, 15, BlueHue, ((x + 7) * 1000) + y,
                        InfectionUtility.GetAllergyScalar(Race.Races[x], (AllergyType)y).ToString());
                AddLabel(440 + (x * 175), 35, BlueHue, Race.Races[x].ToString());
            }

            AddPage(3);
            AddHtml(237, 120, 273, 203, "WARNING! Resetting to Default Values will ERASE any changes you have ever made! Are you sure you wish to RESET ALL VALUES?", true, false);
            AddCheck(244, 331, 2152, 2154, false, 5);
            AddLabel(281, 336, BlueHue, "I understand and wish to proceed.");
            AddButton(320, 370, 12012, 12014, 5, GumpButtonType.Reply, 0);  // Continue Button
        }

        public override void OnResponse(Network.NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            int id = info.ButtonID;
            TextRelay z;

            if (id < 2 || from.AccessLevel < AccessLevel.Administrator) return;
            if (id == 5)
            {
                if (info.IsSwitched(5))
                {
                    InfectionUtility.RESET = true;
                    InfectionUtility.Initialize();
                }
                else
                {
                    from.SendMessage("You must check the box to confirm.");
                }
            }
            else if (id == 2)
            {
                try
                {
                    for (int x = 0; x < 6; x++)
                    {
                        for (int y = 0; y < c.Viruses.Count; y++)
                        {
                            z = info.GetTextEntry(((x + 1) * 100) + y);
                            InfectionUtility.AllScalars[x][y] = Double.Parse(z.Text);
                        }
                    }
                    for (int x = 0; x < 2; x++)
                    {
                        for (int y = 0; y < c.Disorders.Count; y++)
                        {
                            z = info.GetTextEntry(((x + 1) * 1000) + y);
                            InfectionUtility.AllScalars[x + 6][y] = Double.Parse(z.Text);
                        }
                        for (int y = 0; y < c.Diseases.Count; y++)
                        {
                            z = info.GetTextEntry(((x + 3) * 1000) + y);
                            InfectionUtility.AllScalars[x + 8][y] = Double.Parse(z.Text);
                        }
                        for (int y = 0; y < c.Syndromes.Count; y++)
                        {
                            z = info.GetTextEntry(((x + 5) * 1000) + y);
                            InfectionUtility.AllScalars[x + 10][y] = Double.Parse(z.Text);
                        }
                        for (int y = 0; y < c.Allergies.Count; y++)
                        {
                            z = info.GetTextEntry(((x + 7) * 1000) + y);
                            InfectionUtility.AllScalars[x + 12][y] = Double.Parse(z.Text);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    from.SendMessage("Text Entry must be valid Double values.");
                }
            }

            from.SendGump(new HealthScalarGump(from));
        }
    }
}

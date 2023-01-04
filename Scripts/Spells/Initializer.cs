using System;

namespace Server.Spells
{
    public class Initializer
    {
        public static void Initialize()
        {
            // First circle
            Register(00, typeof(First.ClumsySpell));
            Register(01, typeof(First.CreateFoodSpell));
            Register(02, typeof(First.FeeblemindSpell));
            Register(03, typeof(First.HealSpell));
            Register(04, typeof(First.MagicArrowSpell));
            Register(05, typeof(First.NightSightSpell));
            Register(06, typeof(First.ReactiveArmorSpell));
            Register(07, typeof(First.WeakenSpell));

            // Second circle
            Register(08, typeof(Second.AgilitySpell));
            Register(09, typeof(Second.CunningSpell));
            Register(10, typeof(Second.CureSpell));
            Register(11, typeof(Second.HarmSpell));
            Register(12, typeof(Second.MagicTrapSpell));
            Register(13, typeof(Second.RemoveTrapSpell));
            Register(14, typeof(Second.ProtectionSpell));
            Register(15, typeof(Second.StrengthSpell));

            // Third circle
            Register(16, typeof(Third.BlessSpell));
            Register(17, typeof(Third.FireballSpell));
            Register(18, typeof(Third.MagicLockSpell));
            Register(19, typeof(Third.PoisonSpell));
            Register(20, typeof(Third.TelekinesisSpell));
            Register(21, typeof(Third.TeleportSpell));
            Register(22, typeof(Third.UnlockSpell));
            Register(23, typeof(Third.WallOfStoneSpell));

            // Fourth circle
            Register(24, typeof(Fourth.ArchCureSpell));
            Register(25, typeof(Fourth.ArchProtectionSpell));
            Register(26, typeof(Fourth.CurseSpell));
            Register(27, typeof(Fourth.FireFieldSpell));
            Register(28, typeof(Fourth.GreaterHealSpell));
            Register(29, typeof(Fourth.LightningSpell));
            Register(30, typeof(Fourth.ManaDrainSpell));
            Register(31, typeof(Fourth.RecallSpell));

            // Fifth circle
            Register(32, typeof(Fifth.BladeSpiritsSpell));
            Register(33, typeof(Fifth.DispelFieldSpell));
            Register(34, typeof(Fifth.IncognitoSpell));
            Register(35, typeof(Fifth.MagicReflectSpell));
            Register(36, typeof(Fifth.MindBlastSpell));
            Register(37, typeof(Fifth.ParalyzeSpell));
            Register(38, typeof(Fifth.PoisonFieldSpell));
            Register(39, typeof(Fifth.SummonCreatureSpell));

            // Sixth circle
            Register(40, typeof(Sixth.DispelSpell));
            Register(41, typeof(Sixth.EnergyBoltSpell));
            Register(42, typeof(Sixth.ExplosionSpell));
            Register(43, typeof(Sixth.InvisibilitySpell));
            Register(44, typeof(Sixth.MarkSpell));
            Register(45, typeof(Sixth.MassCurseSpell));
            Register(46, typeof(Sixth.ParalyzeFieldSpell));
            Register(47, typeof(Sixth.RevealSpell));

            // Seventh circle
            Register(48, typeof(Seventh.ChainLightningSpell));
            Register(49, typeof(Seventh.EnergyFieldSpell));
            Register(50, typeof(Seventh.FlameStrikeSpell));
            Register(51, typeof(Seventh.GateTravelSpell));
            Register(52, typeof(Seventh.ManaVampireSpell));
            Register(53, typeof(Seventh.MassDispelSpell));
            Register(54, typeof(Seventh.MeteorSwarmSpell));
            Register(55, typeof(Seventh.PolymorphSpell));

            // Eighth circle
            Register(56, typeof(Eighth.EarthquakeSpell));
            Register(57, typeof(Eighth.EnergyVortexSpell));
            Register(58, typeof(Eighth.ResurrectionSpell));
            Register(59, typeof(Eighth.AirElementalSpell));
            Register(60, typeof(Eighth.SummonDaemonSpell));
            Register(61, typeof(Eighth.EarthElementalSpell));
            Register(62, typeof(Eighth.FireElementalSpell));
            Register(63, typeof(Eighth.WaterElementalSpell));

            if (Core.AOS)
            {
                // Necromancy spells
                Register(100, typeof(Necromancy.AnimateDeadSpell));
                Register(101, typeof(Necromancy.BloodOathSpell));
                Register(102, typeof(Necromancy.CorpseSkinSpell));
                Register(103, typeof(Necromancy.CurseWeaponSpell));
                Register(104, typeof(Necromancy.EvilOmenSpell));
                Register(105, typeof(Necromancy.HorrificBeastSpell));
                Register(106, typeof(Necromancy.LichFormSpell));
                Register(107, typeof(Necromancy.MindRotSpell));
                Register(108, typeof(Necromancy.PainSpikeSpell));
                Register(109, typeof(Necromancy.PoisonStrikeSpell));
                Register(110, typeof(Necromancy.StrangleSpell));
                Register(111, typeof(Necromancy.SummonFamiliarSpell));
                Register(112, typeof(Necromancy.VampiricEmbraceSpell));
                Register(113, typeof(Necromancy.VengefulSpiritSpell));
                Register(114, typeof(Necromancy.WitherSpell));
                Register(115, typeof(Necromancy.WraithFormSpell));

                if (Core.SE)
                    Register(116, typeof(Necromancy.ExorcismSpell));

                // Paladin abilities
                Register(200, typeof(Chivalry.CleanseByFireSpell));
                Register(201, typeof(Chivalry.CloseWoundsSpell));
                Register(202, typeof(Chivalry.ConsecrateWeaponSpell));
                Register(203, typeof(Chivalry.DispelEvilSpell));
                Register(204, typeof(Chivalry.DivineFurySpell));
                Register(205, typeof(Chivalry.EnemyOfOneSpell));
                Register(206, typeof(Chivalry.HolyLightSpell));
                Register(207, typeof(Chivalry.NobleSacrificeSpell));
                Register(208, typeof(Chivalry.RemoveCurseSpell));
                Register(209, typeof(Chivalry.SacredJourneySpell));

                if (Core.SE)
                {
                    // Samurai abilities
                    Register(400, typeof(Bushido.HonorableExecution));
                    Register(401, typeof(Bushido.Confidence));
                    Register(402, typeof(Bushido.Evasion));
                    Register(403, typeof(Bushido.CounterAttack));
                    Register(404, typeof(Bushido.LightningStrike));
                    Register(405, typeof(Bushido.MomentumStrike));

                    // Ninja abilities
                    Register(500, typeof(Ninjitsu.FocusAttack));
                    Register(501, typeof(Ninjitsu.DeathStrike));
                    Register(502, typeof(Ninjitsu.AnimalForm));
                    Register(503, typeof(Ninjitsu.KiAttack));
                    Register(504, typeof(Ninjitsu.SurpriseAttack));
                    Register(505, typeof(Ninjitsu.Backstab));
                    Register(506, typeof(Ninjitsu.Shadowjump));
                    Register(507, typeof(Ninjitsu.MirrorImage));
                }

                if (Core.ML)
                {
                    Register(600, typeof(Spellweaving.ArcaneCircleSpell));
                    Register(601, typeof(Spellweaving.GiftOfRenewalSpell));
                    Register(602, typeof(Spellweaving.ImmolatingWeaponSpell));
                    Register(603, typeof(Spellweaving.AttuneWeaponSpell));
                    Register(604, typeof(Spellweaving.ThunderstormSpell));
                    Register(605, typeof(Spellweaving.NatureFurySpell));
                    Register(606, typeof(Spellweaving.SummonFeySpell));
                    Register(607, typeof(Spellweaving.SummonFiendSpell));
                    Register(608, typeof(Spellweaving.ReaperFormSpell));
                    Register( 609, typeof( Spellweaving.WildfireSpell ) );
                    Register(610, typeof(Spellweaving.EssenceOfWindSpell));
                    Register( 611, typeof( Spellweaving.DryadAllureSpell ) );
                    Register(612, typeof(Spellweaving.EtherealVoyageSpell));
                    Register(613, typeof(Spellweaving.WordOfDeathSpell));
                    Register(614, typeof(Spellweaving.GiftOfLifeSpell));
                    Register( 615, typeof( Spellweaving.ArcaneEmpowermentSpell ) );
                }



                if (Core.SA)
                {
                    Register(677, typeof(Mysticism.NetherBoltSpell));
                    Register(678, typeof(Mysticism.HealingStoneSpell));
                    Register(679, typeof(Mysticism.PurgeMagicSpell));
                    Register(680, typeof(Mysticism.EnchantSpell));
                    Register(681, typeof(Mysticism.SleepSpell));
                    Register(682, typeof(Mysticism.EagleStrikeSpell));
                    Register(683, typeof(Mysticism.AnimatedWeaponSpell));
                    Register(684, typeof(Mysticism.StoneFormSpell));
                    Register(685, typeof(Mysticism.SpellTriggerSpell));
                    Register(686, typeof(Mysticism.MassSleepSpell));
                    Register(687, typeof(Mysticism.CleansingWindsSpell));
                    Register(688, typeof(Mysticism.BombardSpell));
                    Register(689, typeof(Mysticism.SpellPlagueSpell));
                    Register(690, typeof(Mysticism.HailStormSpell));
                    Register(691, typeof(Mysticism.NetherCycloneSpell));
                    Register(692, typeof(Mysticism.RisingColossusSpell));

                    Register(700, typeof(SkillMasteries.InspireSpell));
                    Register(701, typeof(SkillMasteries.InvigorateSpell));
                    Register(702, typeof(SkillMasteries.ResilienceSpell));
                    Register(703, typeof(SkillMasteries.PerseveranceSpell));
                    Register(704, typeof(SkillMasteries.TribulationSpell));
                    Register(705, typeof(SkillMasteries.DespairSpell));
                }

                if (Core.TOL)
                {
                    Register(706, typeof(SkillMasteries.DeathRaySpell));
                    Register(707, typeof(SkillMasteries.EtherealBurstSpell));
                    Register(708, typeof(SkillMasteries.NetherBlastSpell));
                    Register(709, typeof(SkillMasteries.MysticWeaponSpell));
                    Register(710, typeof(SkillMasteries.CommandUndeadSpell));
                    Register(711, typeof(SkillMasteries.ConduitSpell));
                    Register(712, typeof(SkillMasteries.ManaShieldSpell));
                    Register(713, typeof(SkillMasteries.SummonReaperSpell));
                    Register(714, typeof(SkillMasteries.PassiveMasterySpell));
                    Register(715, typeof(SkillMasteries.PassiveMasterySpell));
                    Register(716, typeof(SkillMasteries.WarcrySpell));
                    Register(717, typeof(SkillMasteries.PassiveMasterySpell));
                    Register(718, typeof(SkillMasteries.RejuvinateSpell));
                    Register(719, typeof(SkillMasteries.HolyFistSpell));
                    Register(720, typeof(SkillMasteries.ShadowSpell));
                    Register(721, typeof(SkillMasteries.WhiteTigerFormSpell));
                    Register(722, typeof(SkillMasteries.FlamingShotSpell));
                    Register(723, typeof(SkillMasteries.PlayingTheOddsSpell));
                    Register(724, typeof(SkillMasteries.ThrustSpell));
                    Register(725, typeof(SkillMasteries.PierceSpell));
                    Register(726, typeof(SkillMasteries.StaggerSpell));
                    Register(727, typeof(SkillMasteries.ToughnessSpell));
                    Register(728, typeof(SkillMasteries.OnslaughtSpell));
                    Register(729, typeof(SkillMasteries.FocusedEyeSpell));
                    Register(730, typeof(SkillMasteries.ElementalFurySpell));
                    Register(731, typeof(SkillMasteries.CalledShotSpell));
                    Register(732, typeof(SkillMasteries.PassiveMasterySpell));
                    Register(733, typeof(SkillMasteries.ShieldBashSpell));
                    Register(734, typeof(SkillMasteries.BodyGuardSpell));
                    Register(735, typeof(SkillMasteries.HeightenedSensesSpell));
                    Register(736, typeof(SkillMasteries.ToleranceSpell));
                    Register(737, typeof(SkillMasteries.InjectedStrikeSpell));
                    Register(738, typeof(SkillMasteries.PassiveMasterySpell));
                    Register(739, typeof(SkillMasteries.RampageSpell));
                    Register(740, typeof(SkillMasteries.FistsOfFurySpell));
                    Register(741, typeof(SkillMasteries.PassiveMasterySpell));
                    Register(742, typeof(SkillMasteries.WhisperingSpell));
                    Register(743, typeof(SkillMasteries.CombatTrainingSpell));
                }
            }

            // MAGIAS NOVAS
            // Algoz
            Register(70, typeof(Algoz.EmbasbacarSpell));
            Register(71, typeof(Algoz.IntelectoDoAcolitoSpell));
            Register(72, typeof(Algoz.EnfraquecerSpell));
            Register(73, typeof(Algoz.ForcaDoAcolitoSpell));
            Register(74, typeof(Algoz.AtrapalharSpell));
            Register(75, typeof(Algoz.AgilidadeDoAcolitoSpell)); 
            Register(76, typeof(Algoz.ToqueDaDorSpell));
            Register(77, typeof(Algoz.BanimentoProfanoSpell));
            Register(78, typeof(Algoz.ArmaVampiricaSpell));
            Register(79, typeof(Algoz.BanirDemonioSpell));
            Register(80, typeof(Algoz.BencaoProfanaSpell));
            Register(81, typeof(Algoz.DesafioProfanoSpell));
            Register(82, typeof(Algoz.EspiritoMalignoSpell));
            Register(83, typeof(Algoz.FormaVampiricaSpell));
            Register(84, typeof(Algoz.FuriaProfanaSpell));
            Register(85, typeof(Algoz.HaloProfanoSpell));
            Register(86, typeof(Algoz.PeleCadavericaSpell));
            Register(87, typeof(Algoz.SaudeProfanaSpell));

            // MAGIAS DO COSMOS Solar
            Register(750, typeof(CosmosSolar.AceleracaoSpell));
            Register(751, typeof(CosmosSolar.DefletirSpell));
            Register(752, typeof(CosmosSolar.MaoCosmicaSpell));
            Register(753, typeof(CosmosSolar.MiragemSpell));
            Register(754, typeof(CosmosSolar.AuraPsiquicaSpell));
            Register(755, typeof(CosmosSolar.ToqueCalmanteSpell));
            Register(756, typeof(CosmosSolar.CampoDeExtaseSpell));
            Register(757, typeof(CosmosSolar.ArremessoSpell));


            // Magia de Cosmos Lunar
            Register(771, typeof(CosmosLunar.ApertoDaMorteSpell));
            Register(772, typeof(CosmosLunar.CeleridadeSpell));
            Register(773, typeof(CosmosLunar.DrenarVidaSpell));
            Register(774, typeof(CosmosLunar.ExplosaoPsiquicaSpell));
            Register(775, typeof(CosmosLunar.IlusaoSpell));
            Register(776, typeof(CosmosLunar.LancamentoSpell));
            Register(777, typeof(CosmosLunar.RaioSpell));
            Register(778, typeof(CosmosLunar.RedirecionarSpell));



            // MAGIAS DA SKILL FEITICARIA (MagiasFeiticaria)

            // MAGIAS DE PALADINO
            Register(800, typeof(Paladino.ToqueCicatrizanteSpell));
            Register(801, typeof(Paladino.IntelectoDoDevotoSpell));
            Register(802, typeof(Paladino.AgilidadeDoDevotoSpell));
            Register(803, typeof(Paladino.ToqueCurativoSpell));
            Register(804, typeof(Paladino.BanimentoSagradoSpell));
            Register(805, typeof(Paladino.FuriaSagradaSpell));
            Register(806, typeof(Paladino.ForcaDoDevotoSpell));
            Register(807, typeof(Paladino.ArmaSagradaSpell));
            Register(808, typeof(Paladino.BencaoSagradaSpell));
            Register(809, typeof(Paladino.ToqueRegeneradorSpell));
            Register(810, typeof(Paladino.DesafioSagradoSpell));
            Register(811, typeof(Paladino.EmanacaoPuraSpell));
            Register(812, typeof(Paladino.BanimentoCelestialSpell));
            Register(813, typeof(Paladino.SacrificioSantoSpell));
            Register(814, typeof(Paladino.SaudeDivinaSpell));
            Register(815, typeof(Paladino.HaloDivinoSpell));
            Register(816, typeof(Paladino.EspiritoBenignoSpell));


            // CLERIGO DA VIDA
            Register(900, typeof(ClerigoDaVida.AlimentoDaVidaSpell));
            Register(901, typeof(ClerigoDaVida.ElevarAgilidadeSpell));
            Register(902, typeof(ClerigoDaVida.ElevarForcaSpell));
            Register(903, typeof(ClerigoDaVida.ElevarInteligenciaSpell));
            Register(904, typeof(ClerigoDaVida.AscencaoCompletaSpell));
            Register(905, typeof(ClerigoDaVida.AuraPurificadoraSpell));
            Register(906, typeof(ClerigoDaVida.ClarearVistaSpell));
            Register(907, typeof(ClerigoDaVida.CuraLeveSpell));
            Register(908, typeof(ClerigoDaVida.CuraMininaSpell));
            Register(909, typeof(ClerigoDaVida.CuraModeradaSpell));
            Register(910, typeof(ClerigoDaVida.FogoDeVidaSpell));
            Register(911, typeof(ClerigoDaVida.ProtecaoDaLuzSpell));
            Register(912, typeof(ClerigoDaVida.PurificarAreaSpell));
            Register(913, typeof(ClerigoDaVida.PurificarSpell));
            Register(914, typeof(ClerigoDaVida.ReanimarMortoSpell));
            Register(915, typeof(ClerigoDaVida.RecuperacaoBreveSpell));
            Register(916, typeof(ClerigoDaVida.RemoverMaldicaoSpell));
            Register(917, typeof(ClerigoDaVida.ReviverSpell));
            Register(918, typeof(ClerigoDaVida.VidaEternaSpell));

            // CLERIGO DOS MORTOS

            Register(950, typeof(ClerigoDosMortos.AbrirFeridasSpell));
            Register(951, typeof(ClerigoDosMortos.AlimentoDaMorteSpell));
            Register(952, typeof(ClerigoDosMortos.CampoVenenosoSpell));
            Register(953, typeof(ClerigoDosMortos.DrenarAgilidadeSpell));
            Register(954, typeof(ClerigoDosMortos.DrenarEssenciaSpell));
            Register(955, typeof(ClerigoDosMortos.DrenarForcaSpell));
            Register(956, typeof(ClerigoDosMortos.DrenarInteligenciaSpell));
            Register(957, typeof(ClerigoDosMortos.DrenarManaSpell));
            Register(958, typeof(ClerigoDosMortos.DrenarQuintessenciaSpell));
            Register(959, typeof(ClerigoDosMortos.EntorpecerSpell));
            Register(960, typeof(ClerigoDosMortos.EnvenenarMenteSpell));
            Register(961, typeof(ClerigoDosMortos.EnvenenarSpell));
            Register(962, typeof(ClerigoDosMortos.ErguerCadaverSpell));
            Register(963, typeof(ClerigoDosMortos.EscurecerVistaSpell));
            Register(964, typeof(ClerigoDosMortos.FogoDaMorteSpell));
            Register(965, typeof(ClerigoDosMortos.PactoDeSangueSpell));
            Register(966, typeof(ClerigoDosMortos.ProtecaoDasTrevasSpell));
            Register(967, typeof(ClerigoDosMortos.RitualLichSpell));
            Register(968, typeof(ClerigoDosMortos.SonolenciaSpell));


            // ELEMENTARISTA

            Register(620, typeof(Elementarista.AtaqueCongelanteSpell));
            Register(621, typeof(Elementarista.AvalancheSpell));
            Register(622, typeof(Elementarista.BolaDeGeloSpell));
            Register(623, typeof(Elementarista.CampoGelidoSpell));
            Register(624, typeof(Elementarista.TempestadeGelidaSpell));
            Register(625, typeof(Elementarista.CongelarSpell));
            Register(626, typeof(Elementarista.MorteMassivaSpell));
            Register(627, typeof(Elementarista.NuvemGasosaSpell));


            // MONGE
            Register(860, typeof(Monge.AtaqueElementalSpell));
            Register(861, typeof(Monge.CorrerNoVentoSpell));
            Register(862, typeof(Monge.DisciplinaMentalSpell));
            Register(863, typeof(Monge.EsferaDeKiSpell));
            Register(864, typeof(Monge.FerimentoInternoSpell));
            Register(865, typeof(Monge.GolpeAscendenteSpell));
            Register(866, typeof(Monge.GolpeAtordoanteSpell));
            Register(867, typeof(Monge.GolpesFortesSpell));
            Register(868, typeof(Monge.GolpesVelozesSpell));
            Register(869, typeof(Monge.InvestidaAterradoraSpell));
            Register(870, typeof(Monge.MenteVelozSpell));
            Register(871, typeof(Monge.MetabolizarFerimentoSpell));
            Register(872, typeof(Monge.MetabolizarVenenoSpell));
            Register(873, typeof(Monge.PalmaExplosivaSpell));
            Register(874, typeof(Monge.RigidezAprimoradaSpell));
            Register(875, typeof(Monge.SaltoAprimoradoSpell));
            Register(876, typeof(Monge.SocoDoKiSpell));
            Register(877, typeof(Monge.SocoTectonicoSpell));
            Register(878, typeof(Monge.SocoVulcanicoSpell));
            Register(879, typeof(Monge.SuperacaoSpell));


            // First circle
            Register(420, typeof(First.OlhosDaCorujaSpell));
            
            // Second circle
            

            // Third circle
            

            // Fourth circle
            

            // Fifth circle
            

            // Sixth circle
            

            // Seventh circle
            

            // Eighth circle
            

        }

        public static void Register(int spellID, Type type)
        {
            SpellRegistry.Register(spellID, type);
        }
    }
}

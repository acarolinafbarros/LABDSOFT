using GAM.Data;
using GAM.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAM.Models
{
    using GAM.Models.Enums;

    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Apagar & Criar database
            context.Database.EnsureDeleted();
            context.Database.Migrate();

            var _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (context.Users.Any(u => u.UserName == "admin"))
            {
                return;
            }

            // Roles
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("Medico"));
            await _roleManager.CreateAsync(new IdentityRole("Enfermeiro"));
            await _roleManager.CreateAsync(new IdentityRole("EnfermeiroCoordenador"));
            await _roleManager.CreateAsync(new IdentityRole("DiretorGeral"));
            await _roleManager.CreateAsync(new IdentityRole("AssistenteSocial"));
            await _roleManager.CreateAsync(new IdentityRole("Embriologista"));
            await _roleManager.CreateAsync(new IdentityRole("DiretoraLaboratorio"));
            await _roleManager.CreateAsync(new IdentityRole("PMA"));

            // User Admin               
            var admin = new ApplicationUser
            {
                UserName = "admin@gam.com",
                Email = "admin@gam.com"
            };

            string userPWD = "Admin123!";

            var createAdmin = await _userManager.CreateAsync(admin, userPWD);
 
            if (createAdmin.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, "Admin");
            }

            // User Medico
            var medico = new ApplicationUser
            {
                UserName = "medico@gam.com",
                Email = "medico@gam.com"
            };

            string medicoPWD = "Medico123!";

            var createMedico = await _userManager.CreateAsync(medico, medicoPWD);

            if (createMedico.Succeeded)
            {
                await _userManager.AddToRoleAsync(medico, "Medico");
            }

            // User Enfermeiro
            var enfermeiro = new ApplicationUser
            {
                UserName = "enfermeiro@gam.com",
                Email = "enfermeiro@gam.com"
            };

            string enfermeiroPWD = "Enfermeiro123!";

            var createEnfermeiro = await _userManager.CreateAsync(enfermeiro, enfermeiroPWD);

            if (createEnfermeiro.Succeeded)
            {
                await _userManager.AddToRoleAsync(enfermeiro, "Enfermeiro");
            }

            // User EnfermeiroCoordenador
            var enfermeiroCoordenador = new ApplicationUser
            {
                UserName = "enfermeiroCoordenador@gam.com",
                Email = "enfermeiroCoordenador@gam.com"
            };

            string enfermeiroCoordenadorPWD = "EnfermeiroCoordenador123!";

            var createEnfermeiroCoordenador = await _userManager.CreateAsync(enfermeiroCoordenador, enfermeiroCoordenadorPWD);

            if (createEnfermeiroCoordenador.Succeeded)
            {
                await _userManager.AddToRoleAsync(enfermeiroCoordenador, "EnfermeiroCoordenador");
            }

            // User DiretorGeral
            var diretorGeral = new ApplicationUser
            {
                UserName = "diretorGeral@gam.com",
                Email = "diretorGeral@gam.com"
            };

            string diretorGeralPWD = "DiretorGeral123!";

            var createDiretorGeral = await _userManager.CreateAsync(diretorGeral, diretorGeralPWD);

            if (createDiretorGeral.Succeeded)
            {
                await _userManager.AddToRoleAsync(diretorGeral, "DiretorGeral");
            }

            // User AssistenteSocial
            var assistenteSocial = new ApplicationUser
            {
                UserName = "assistenteSocial@gam.com",
                Email = "assistenteSocial@gam.com"
            };

            string assistenteSocialPWD = "AssistenteSocial123!";

            var createAssistenteSocial = await _userManager.CreateAsync(assistenteSocial, assistenteSocialPWD);

            if (createAssistenteSocial.Succeeded)
            {
                await _userManager.AddToRoleAsync(assistenteSocial, "AssistenteSocial");
            }

            // User Embriologista
            var embriologista = new ApplicationUser
            {
                UserName = "embriologista@gam.com",
                Email = "embriologista@gam.com"
            };

            string embriologistaPWD = "Embriologista123!";

            var createEmbriologista = await _userManager.CreateAsync(embriologista, embriologistaPWD);

            if (createEmbriologista.Succeeded)
            {
                await _userManager.AddToRoleAsync(embriologista, "Embriologista");
            }

            // User DiretoraLaboratorio
            var diretoraLaboratorio = new ApplicationUser
            {
                UserName = "diretoraLaboratorio@gam.com",
                Email = "diretoraLaboratorio@gam.com"
            };

            string diretoraLaboratorioPWD = "DiretoraLaboratorio123!";

            var createDiretoraLaboratorio = await _userManager.CreateAsync(diretoraLaboratorio, diretoraLaboratorioPWD);

            if (createDiretoraLaboratorio.Succeeded)
            {
                await _userManager.AddToRoleAsync(diretoraLaboratorio, "DiretoraLaboratorio");
            }

            // User PMA
            var pma = new ApplicationUser
            {
                UserName = "PMA@gam.com",
                Email = "PMA@gam.com"
            };

            string pmaPWD = "PMA123!";

            var createPMA = await _userManager.CreateAsync(pma, pmaPWD);

            if (createPMA.Succeeded)
            {
                await _userManager.AddToRoleAsync(pma, "PMA");
            }


            // ----------------------------------------------------------------------------------------
            // Criar Dador

            var dador = new DadorViewModels.Dador
            {
                Nome = "Marcelo Moreno",
                Morada = "Praça D. Joao II",
                DataNasc = DateTime.UtcNow,
                LocalNasc = "Amarante",
                DocIdentificacao = "123987456",
                Nacionalidade = "Portugal",
                Profissao = "Engenheiro Quimico",
                GrauEscolaridade = Enums.GrauEscolaridadeEnum.Mestrado,
                EstadoCivil = Enums.EstadoCivilEnum.Viuvo,
                NumFilhos = 0,
                Altura = 185,
                Peso = 78,
                CorPele = "Branco",
                CorOlhos = "Verde",
                CorCabelo = "Preto",
                TexturaCabelo = "Liso",
                GrupoSanguineo = Enums.GrupoSanguineoEnum.BNeg,
                Etnia = "Apache",
                IniciaisDador = "MM",
                FaseDador = Enums.FaseDadorEnum.PrimeiraDadiva,
                EstadoDador = Enums.EstadoDadorEnum.PendenteAprovacao,
                DadosDador = Enums.ValidacaoEnum.Aceite,
                NumAbortos = 0,
                TotalGestacoes = 0
            };

            context.Add(dador);
            await context.SaveChangesAsync();

            var dador2 = new DadorViewModels.Dador
            {
                Nome = "Jack Spargato",
                Morada = "Perola Preta",
                DataNasc = DateTime.UtcNow,
                LocalNasc = "Pacifico",
                DocIdentificacao = "123987456",
                Nacionalidade = "Tortugues",
                Profissao = "Pirata",
                GrauEscolaridade = Enums.GrauEscolaridadeEnum.EnsinoBasico,
                EstadoCivil = Enums.EstadoCivilEnum.Solteiro,
                NumFilhos= 1,
                Altura = 185,
                Peso = 78,
                CorPele = "Branco",
                CorOlhos = "Verdes",
                CorCabelo = "Preto",
                TexturaCabelo = "Pirata",
                GrupoSanguineo = Enums.GrupoSanguineoEnum.BNeg,
                Etnia = "Piratuga",
                IniciaisDador = "JS",
                FaseDador = Enums.FaseDadorEnum.PrimeiraDadiva,
                EstadoDador = Enums.EstadoDadorEnum.ProcessoInativo,
                DadosDador = Enums.ValidacaoEnum.Aceite,
                NumAbortos = 0,
                TotalGestacoes = 0
            };

            context.Add(dador2);
            await context.SaveChangesAsync();

            var dador3 = new DadorViewModels.Dador
            {
                Nome = "Josefino Rapachino",
                Morada = "Praia da Juventude Eterna",
                DataNasc = DateTime.UtcNow,
                LocalNasc = "Centro do Mundo",
                DocIdentificacao = "123987456",
                Nacionalidade = "Portugal",
                Profissao = "Ajudante dos Golfinhos",
                GrauEscolaridade = Enums.GrauEscolaridadeEnum.Doutoramento,
                EstadoCivil = Enums.EstadoCivilEnum.Casado,
                NumFilhos=2,
                Altura = 185,
                Peso = 78,
                CorPele = "Preta",
                CorOlhos = "Verde",
                CorCabelo = "Loiro",
                TexturaCabelo = "Rasta",
                GrupoSanguineo = Enums.GrupoSanguineoEnum.BNeg,
                Etnia = "Golfinho",
                IniciaisDador = "MM",
                FaseDador = Enums.FaseDadorEnum.PrimeiraDadiva,
                EstadoDador = Enums.EstadoDadorEnum.ProcessoInativo,
                DadosDador = Enums.ValidacaoEnum.Pendente,
                NumAbortos = 0,
                TotalGestacoes = 0
            };

            context.Add(dador3);
            await context.SaveChangesAsync();

            // ----------------------------------------------------------------------------------------
            // Criar Resultado Analise

            var resAnalise = new Laboratorio.ResultadoAnalise
            {
                Data = DateTime.UtcNow,
                NomeMedico = "Joaquim Pereira",
                NomeEmbriologista = "Vicente Sousa",
                ValidacaoMedico = Enums.ValidacaoEnum.Aceite,
                ValidacaoLaboratorio = Enums.ValidacaoEnum.Aceite
            };

            context.Add(resAnalise);
            await context.SaveChangesAsync();
            
            // ----------------------------------------------------------------------------------------
            // Criar Amostra

            var dadorObj = context.Dador.SingleOrDefaultAsync(d => d.Nome == "Marcelo Moreno");
            var dadorObj2 = context.Dador.SingleOrDefaultAsync(d => d.Nome == "Jack Spargato");
            var dadorObj3 = context.Dador.SingleOrDefaultAsync(d => d.Nome == "Josefino Rapachino");


            var amostra1 = new Laboratorio.Amostra
            {
                DadorId = dadorObj.Result.DadorId,
                EstadoAmostra = Enums.EstadoAmostraEnum.EmAnalise,
                TipoAmostra = Enums.TipoAmostraEnum.Sangue,
                DataRecolha = DateTime.UtcNow,
                Banco = GamEnums.TipoBancoEnum.Indefinido,
                Piso = GamEnums.PisoEnum.Indefinido,
                Cannister = GamEnums.CannisterEnum.Indefinido,
                GlobetCor = GamEnums.GlobetCorEnum.Indefinido,
                GlobetNumero = GamEnums.GlobetNumeroEnum.Indefinido,
                PalhetaCor = GamEnums.PalhetaCorEnum.Indefinido
            };

            var amostra2 = new Laboratorio.Amostra
            {
                DadorId = dadorObj.Result.DadorId,
                EstadoAmostra = Enums.EstadoAmostraEnum.Analisada,
                TipoAmostra = Enums.TipoAmostraEnum.Espermatozoide,
                DataRecolha = DateTime.UtcNow,
                Banco = GamEnums.TipoBancoEnum.Indefinido,
                Piso = GamEnums.PisoEnum.Indefinido,
                Cannister = GamEnums.CannisterEnum.Indefinido,
                GlobetCor = GamEnums.GlobetCorEnum.Indefinido,
                GlobetNumero = GamEnums.GlobetNumeroEnum.Indefinido,
                PalhetaCor = GamEnums.PalhetaCorEnum.Indefinido
            };

            var amostra3 = new Laboratorio.Amostra
            {
                DadorId = dadorObj.Result.DadorId,
                EstadoAmostra = Enums.EstadoAmostraEnum.Analisada,
                TipoAmostra = Enums.TipoAmostraEnum.Sangue,
                DataRecolha = DateTime.UtcNow,
                Banco = GamEnums.TipoBancoEnum.Indefinido,
                Piso = GamEnums.PisoEnum.Indefinido,
                Cannister = GamEnums.CannisterEnum.Indefinido,
                GlobetCor = GamEnums.GlobetCorEnum.Indefinido,
                GlobetNumero = GamEnums.GlobetNumeroEnum.Indefinido,
                PalhetaCor = GamEnums.PalhetaCorEnum.Indefinido
            };

            var amostra4 = new Laboratorio.Amostra
            {
                DadorId = dadorObj2.Result.DadorId,
                EstadoAmostra = Enums.EstadoAmostraEnum.EmAnalise,
                TipoAmostra = Enums.TipoAmostraEnum.Sangue,
                DataRecolha = DateTime.UtcNow,
                Banco = GamEnums.TipoBancoEnum.Indefinido,
                Piso = GamEnums.PisoEnum.Indefinido,
                Cannister = GamEnums.CannisterEnum.Indefinido,
                GlobetCor = GamEnums.GlobetCorEnum.Indefinido,
                GlobetNumero = GamEnums.GlobetNumeroEnum.Indefinido,
                PalhetaCor = GamEnums.PalhetaCorEnum.Indefinido
            };

            var amostra5 = new Laboratorio.Amostra
            {
                DadorId = dadorObj3.Result.DadorId,
                EstadoAmostra = Enums.EstadoAmostraEnum.EmAnalise,
                TipoAmostra = Enums.TipoAmostraEnum.Sangue,
                DataRecolha = DateTime.UtcNow,
                Banco = GamEnums.TipoBancoEnum.Indefinido,
                Piso = GamEnums.PisoEnum.Indefinido,
                Cannister = GamEnums.CannisterEnum.Indefinido,
                GlobetCor = GamEnums.GlobetCorEnum.Indefinido,
                GlobetNumero = GamEnums.GlobetNumeroEnum.Indefinido,
                PalhetaCor = GamEnums.PalhetaCorEnum.Indefinido
            };

            context.Add(amostra1);
            context.Add(amostra2);
            context.Add(amostra3);
            context.Add(amostra4);
            context.Add(amostra5);
            await context.SaveChangesAsync();

            // ----------------------------------------------------------------------------------------
            // Criar Analise
            
            var amostraObj1 = context.Amostra.SingleOrDefaultAsync(a => a.NrAmosta.Equals(112233));
            var resultAnaliseObj = context.ResultadoAnalise.SingleOrDefaultAsync(r => r.NomeMedico == "Joaquim Pereira" && r.NomeEmbriologista == "Vicente Sousa");

            var analise1 = new Laboratorio.Analise
            {
                AmostraId = amostraObj1.Result.AmostraId,
                ResultadoAnaliseId = resultAnaliseObj.Result.ResultadoAnaliseId,
                Nome = "HBsAg",
                Data = DateTime.UtcNow
            };

            var analise2 = new Laboratorio.Analise
            {
                AmostraId = amostraObj1.Result.AmostraId,
                ResultadoAnaliseId = resultAnaliseObj.Result.ResultadoAnaliseId,
                Nome = "Ac HCV",
                Data = DateTime.UtcNow
            };

            context.Add(analise1);
            context.Add(analise2);
            await context.SaveChangesAsync();
            

            // ----------------------------------------------------------------------------------------
            // Criar Espermograma

            var amostraObj2 = context.Amostra.SingleOrDefaultAsync(a => a.NrAmosta.Equals(112234));

            var espermograma = new Laboratorio.Espermograma
            {
                AmostraId = amostraObj2.Result.AmostraId,
                DataEspermograma = DateTime.UtcNow,
                Volume = (float)1.8,
                Cor = "Esbranquiçada",
                Viscosidade = "Normal",
                Liquefacao = "30 minutos",
                Ph = (float)8.0,
                Observacoes = "Sem nada a registar",
                ConcentracaoEspermatozoides = 70000000,
                GrauA = 07,
                GrauB = 57,
                GrauC = 04,
                GrauD = 32,
                Leucocitos = 4000000,
                Vitalidade = 69,
                ObservacoesConcentracao = "Sem nada a registar"
            };

            context.Add(espermograma);
            await context.SaveChangesAsync();

            // ----------------------------------------------------------------------------------------
            // Criar Material

            var espermogramaObj1 = context.Espermograma.FirstOrDefault();

            var material1 = new RegistoMaterial.Material
            {
                EspermogramaId = espermogramaObj1.EspermogramaId,
                Nome = "Tubo de Ensaio",
                Lote = "B12",
                QuantidadeUtilizada = 2,
                Categoria = Enums.CategoriaEnum.Laboratorial
            };

            var material2 = new RegistoMaterial.Material
            {
                EspermogramaId = espermogramaObj1.EspermogramaId,
                Nome = "Proveta",
                Lote = "C6",
                QuantidadeUtilizada = 3,
                Categoria = Enums.CategoriaEnum.Laboratorial
            };

            context.Add(material1);
            context.Add(material2);
            await context.SaveChangesAsync();

            // ----------------------------------------------------------------------------------------
            // Criar Metodo
            
            var analiseObj1 = context.Analise.SingleOrDefaultAsync(a => a.Nome == "HBsAg");
            var analiseObj2 = context.Analise.SingleOrDefaultAsync(a => a.Nome == "Ac HCV");

            var metodo1 = new Laboratorio.Metodo
            {
                AnaliseId = analiseObj1.Result.AnaliseId,
                Nome = "CMIA", 
                InterpretacaoNeg = "Não Reativo",
                InterpretacaoPos = "Reativo",
                ValorReferenciaNeg = (float)1.00,
                ValorReferenciaPos =  (float)1.00,
                ResultadoNumerico = (float)(1.5), 
                Resultado = "Reativo"
            };

            var metodo2 = new Laboratorio.Metodo
            {
                AnaliseId = analiseObj1.Result.AnaliseId,
                Nome = "MEIA",
                InterpretacaoNeg = "Negativo",
                InterpretacaoPos = "Positivo",
                ValorReferenciaNeg = (float)2.00,
                ValorReferenciaPos = (float)2.00,
                ResultadoNumerico = (float)2.2,
                Resultado = "Positivo"
            };

            var metodo3 = new Laboratorio.Metodo
            {
                AnaliseId = analiseObj2.Result.AnaliseId,
                Nome = "CMIA",
                InterpretacaoNeg = "Não Reativo",
                InterpretacaoPos = "Reativo",
                ValorReferenciaNeg = (float)0.80,
                ValorReferenciaPos = (float)1.00,
                ResultadoNumerico = (float)(1.1),
                Resultado = "Reativo"
            };

            var metodo4 = new Laboratorio.Metodo
            {
                AnaliseId = analiseObj2.Result.AnaliseId,
                Nome = "ELISA",
                InterpretacaoNeg = "Não Reativo",
                InterpretacaoPos = "Reativo",
                ValorReferenciaNeg = (float)1.00,
                ValorReferenciaPos = (float)1.00,
                ResultadoNumerico = (float)(0.9),
                Resultado = "Não Reativo"
            };

            context.Add(metodo1);
            context.Add(metodo2);
            context.Add(metodo3);
            context.Add(metodo4);
            await context.SaveChangesAsync();
            
        }
    }
}

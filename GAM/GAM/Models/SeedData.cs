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
            await _roleManager.CreateAsync(new IdentityRole("DiretorGeral"));
            await _roleManager.CreateAsync(new IdentityRole("AssistenteSocial"));
            await _roleManager.CreateAsync(new IdentityRole("Embriologista"));
            await _roleManager.CreateAsync(new IdentityRole("DiretoraLaboratorio"));
            await _roleManager.CreateAsync(new IdentityRole("PMA"));

            // Admin User                
            var user = new ApplicationUser
            {
                UserName = "admin@gam.com",
                Email = "admin@gam.com"
            };

            string userPWD = "Admin123!";

            var createUser = await _userManager.CreateAsync(user, userPWD);

            // Admin User - Role  
            if (createUser.Succeeded)
            {
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                var result = await _userManager.AddToRoleAsync(user, "Admin");
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
                EstadoDador = Enums.EstadoDadorEnum.ProcessoAtivo,
                NumAbortos = 0,
                TotalGestacoes = 0
            };

            context.Add(dador);
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

            var amostra1 = new Laboratorio.Amostra
            {
                DadorId = dadorObj.Result.DadorId,
                EstadoAmostra = Enums.EstadoAmostraEnum.Analisada,
                TipoAmostra = Enums.TipoAmostraEnum.Sangue,
                DataRecolha = DateTime.UtcNow,
                Localizacao = "Fila 3 - Posicao 4",
                NrAmosta = 112233
            };

            var amostra2 = new Laboratorio.Amostra
            {
                DadorId = dadorObj.Result.DadorId,
                EstadoAmostra = Enums.EstadoAmostraEnum.Analisada,
                TipoAmostra = Enums.TipoAmostraEnum.Espermatozoide,
                DataRecolha = DateTime.UtcNow,
                Localizacao = "Fila 1 - Posicao 10",
                NrAmosta = 112234
            };

            context.Add(amostra1);
            context.Add(amostra2);
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

            // ----------------------------------------------------------------------------------------
            // Criar Material

            //var espermogramaObj1 = context.Espermograma.FirstOrDefault();

            //var material1 = new RegistoMaterial.Material
            //{
            //    EspermogramaId = espermogramaObj1.EspermogramaId,
            //    Nome = "Tubo de Ensaio",
            //    Lote = "B12",
            //    QuantidadeUtilizada = 2,
            //    Categoria = Enums.CategoriaEnum.Laboratorial
            //};

            //var material2 = new RegistoMaterial.Material
            //{
            //    EspermogramaId = espermogramaObj1.EspermogramaId,
            //    Nome = "Proveta",
            //    Lote = "C6",
            //    QuantidadeUtilizada = 3,
            //    Categoria = Enums.CategoriaEnum.Laboratorial
            //};

            //context.Add(material1);
            //context.Add(material2);
            //await context.SaveChangesAsync();
        }
    }
}

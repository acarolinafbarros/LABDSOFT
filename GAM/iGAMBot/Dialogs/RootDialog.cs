using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.Luis;

namespace iGAMBot.Dialogs
{
    using iGAMBot.Controllers;
    using iGAMBot.Model;
    using System.Collections.Generic;

    [LuisModel("a25794fc-f24d-4f3d-803f-77a8323c9a3c", "e2f6649b61d840d7b1b219b6918f31b9")]
    [Serializable]
    public class RootDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        private async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Não entendi o que quiseste dizer. Eis algumas tarefas em que te posso ajudar:\n" +
                                    "- Marcar consulta.\n" +
                                    "- Cancelar consulta.\n" +
                                    "- Consultar resultados espermograma.\n" +
                                    "- Esclarecimento de dúvidas.");
        }

        // ---------------------------------------------------------------------------------------------------------------------------

        [LuisIntent("MarcarConsulta")]
        private async Task MarcarConsulta(IDialogContext context, LuisResult result)
        {
            PromptDialog.Text(context, MostrarMarcarConsulta, "Para marcares uma consulta deves estar registado no sistema \n" + 
                                                              "Por favor insere o teu número de identificação.");
        }

        public async Task MostrarMarcarConsulta(IDialogContext context, IAwaitable<string> result)
        {
            var docIdentificacao = await result;

            BotToGamController BotToGamController = new BotToGamController();
            List<ModelDadorMarcarConsultToBot> dadorAlvo = BotToGamController.CheckIfDadorForMarcarConsulta(docIdentificacao);

            if (dadorAlvo == null) // Ou seja, o dador não existe
            {
                PromptDialog.Text(context, MostrarOpcoesParaDadorNaoRegistado, "Reparei que não és um dador registado no sistema");
            }
            else
            {
                // 1 - Mostrar os slots disponiveis

                await context.PostAsync("Eis algumas das datas disponiveis para marcar uma consulta:");
                foreach (var item in dadorAlvo)
                {
                    await context.PostAsync("Slot: " + item.SlotId + " | Data: " + item.DataConsultaDisponivel);
                }

                // 2 - Guardar dados do dador 
                var dadorAtual = new DadorAtual
                {
                    Nome = dadorAlvo.Find(d => d.DocIdentificacao == docIdentificacao).Nome,
                    DocIdentificacao = dadorAlvo.Find(d => d.DocIdentificacao == docIdentificacao).DocIdentificacao,
                    DadorId = dadorAlvo.Find(d => d.DocIdentificacao == docIdentificacao).DadorId
                };

                // 3 - Dar a escolher um dos slots
                PromptDialog.Text(context, LerOpcaoSlot, "Indica-me pf o slot que preferes");
            }
        }

        private async Task LerOpcaoSlot(IDialogContext context, IAwaitable<string> result)
        {
            string opEscolhida = await result;
            int op = Int32.Parse(opEscolhida);

            BotToGamController BotToGamController = new BotToGamController();

            // 3 - Guardar as alteracoes - call MarcarConsulta

            // TODO: dadorId is hardcoded. Arranjar maneira de passar o campo a partir do metodo MostrarMarcarConsulta. Talvez usar o objeto DadorAtual
            if (!BotToGamController.MarcarConsulta(44, op))
            {
                await context.PostAsync("Aconteceu um erro ao teu agendar a tua consulta. Por favor tenta de novo mais tarde");
            }
            else
            {
                await context.PostAsync("A tua consulta foi agendada com sucesso!");
            }
        }

        // ---------------------------------------------------------------------------------------------------------------------------

        [LuisIntent("CancelarConsulta")]
        private async Task CancelarConsulta(IDialogContext context, LuisResult result)
        {
            PromptDialog.Text(context, MostrarCancelarConsulta, "Para cancelares uma consulta deves estar registado no sistema \n" +
                                                                "Por favor insere o teu número de identificação.");
        }

        public async Task MostrarCancelarConsulta(IDialogContext context, IAwaitable<string> result)
        {
            var docIdentificacao = await result;

            BotToGamController BotToGamController = new BotToGamController();
            ModelDadorCancelarConsultToBot dadorAlvo = BotToGamController.CheckIfDadorForCancelarConsulta(docIdentificacao);

            if (dadorAlvo == null) // Ou seja, o dador não existe
            {
                PromptDialog.Text(context, MostrarOpcoesParaDadorNaoRegistado, "Reparei que não és um dador registado no sistema ou que não tens uma consulta agendada");
            }
            else
            {
                if (!BotToGamController.CancelarConsulta(dadorAlvo.DadorId, dadorAlvo.ConsultaId))
                {
                    await context.PostAsync("Aconteceu um erro ao teu cancelar a tua consulta. Por favor tenta de novo mais tarde");
                }
                else
                {
                    await context.PostAsync("A tua consulta do dia " + dadorAlvo.DataConsulta + " foi cancelada com sucesso!");
                }
            }
        }

        // ---------------------------------------------------------------------------------------------------------------------------

        [LuisIntent("EsclarecerDuvidas")]
        private async Task EsclarecerDuvidas(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Acho que te posso ajudar a esclarecer algumas dúvidas, tais como: \n" +
                                    "- Que idade precisas de ter para ser dador \n" + 
                                    "- Quanto dinheiro podes receber se fores dador");
        }

        [LuisIntent("EsclarecerDuvidasDinheiro")]
        private async Task EsclarecerDuvidasDinheiro(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Entendi que querias esclarecer dúvidas relacionadas com o valor que podes receber caso sejas dador. \n" +
                                    "A verdade é que não te vou pagar um tusto!");
        }

        [LuisIntent("EsclarecerDuvidasIdade")]
        private async Task EsclarecerDuvidasIdade(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Entendi que querias esclarecer dúvidas relacionadas com a idade necessária para ser dador. \n" +
                                    "A verdade é que necessitas de ter uma idade compreendida entre os 18 e os 40 anos");
        }

        // ---------------------------------------------------------------------------------------------------------------------------

        [LuisIntent("ConsultarResultadosEspermograma")]
        private async Task ConsultarResultadosEspermograma(IDialogContext context, LuisResult result)
        {
            PromptDialog.Text(context, MostrarEspermograma, "Para consultares os resultados do espermograma deves estar registado no sistema \n" +
                                                            "Por favor insere o teu número de identificação.");
        }

        public async Task MostrarEspermograma(IDialogContext context, IAwaitable<string> result)
        {
            var docIdentificacao = await result;

            BotToGamController BotToGamController = new BotToGamController();
            ModelDadorResEspermToBot dadorAlvo = BotToGamController.CheckIfDadorForResEsperm(docIdentificacao);

            if (dadorAlvo == null) // Ou seja, o dador não existe
            {
                PromptDialog.Text(context, MostrarOpcoesParaDadorNaoRegistado, "Reparei que não és um dador registado no sistema");
            }
            else
            {
                await context.PostAsync("Eis alguns dados que consegui obter para te mostrar: " +
                                        "Identificador Amostra : " + dadorAlvo.AmostraId + " | " +
                                        "Grau A : " + dadorAlvo.GrauA + " | " +
                                        "Grau B : " + dadorAlvo.GrauB + " | " +
                                        "Grau C : " + dadorAlvo.GrauC + " | " +
                                        "Grau D : " + dadorAlvo.GrauD);
            }
        }

        // ---------------------------------------------------------------------------------------------------------------------------

        private async Task MostrarOpcoesParaDadorNaoRegistado (IDialogContext context, IAwaitable<string> result)
        {
            await context.PostAsync("Eis algumas tarefas em que te posso ajudar:\n" +
                                    "- Esclarecimento de dúvidas.");
        }

        private async Task MostrarOpcoesParaDadorRegistado(IDialogContext context, IAwaitable<string> result)
        {
            await context.PostAsync("Eis algumas tarefas em que te posso ajudar:\n" +
                                    "- Marcar consulta.\n" +
                                    "- Cancelar consulta.\n" +
                                    "- Consultar resultados espermograma.\n" +
                                    "- Esclarecimento de dúvidas.");
        }
    }
}

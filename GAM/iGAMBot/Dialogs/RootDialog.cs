using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.Luis;

namespace iGAMBot.Dialogs
{
    using iGAMBot.Controllers;
    using iGAMBot.Model;

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

            var dadorAlvo = ""; // call controller iGAM para ir buscar os dados do dador

            if (dadorAlvo == null || dadorAlvo == "") // Ou seja, o dador não existe
            {
                PromptDialog.Text(context, MostrarOpcoesParaDadorNaoRegistado, "Reparei que não és um dador registado no sistema");
            }

            // Listar as datas possiveis para marcar uma consulta
        }

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
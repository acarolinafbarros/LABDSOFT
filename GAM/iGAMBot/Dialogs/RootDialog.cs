using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.Luis;

namespace iGAMBot.Dialogs
{
    [LuisModel("a25794fc-f24d-4f3d-803f-77a8323c9a3c", "e2f6649b61d840d7b1b219b6918f31b9")]
    [Serializable]
    public class RootDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        private async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Não entendi o que quis dizer. Eis algumas tarefas em que lhe posso ajudar:\n" +
                        "- Marcar consulta.\n" +
                        "- Cancelar consulta.\n" +
                        "- Consultar Resultados Espermograma.\n" +
                        "- Esclarecimento de Dúvidas.");
        }

        [LuisIntent("MarcarConsulta")]
        private async Task MarcarConsulta(IDialogContext context, LuisResult result)
        {
            PromptDialog.Text(context, VerifyIfRegistered, "Para marcar consulta deve estar registado no sistema\nPor favor insira o seu número de identificação.");
        }

        public async Task VerifyIfRegistered(IDialogContext context, IAwaitable<string> result)
        {
            var numeroIdentificacao = await result;

            /* Chamada a BD para ver se o dador existe
              * Se existir: Instanciar os dados do dador num objeto para ser manipulado
              * Se não existir: Redirecionar para o metodo AtuarParaDadorNaoRegistado
            */
            var resultadoBD = ""; // Substituir "" pela query a BD

            if (resultadoBD != null) // O dador esta registado
            {
                await context.PostAsync("Dador registado");
            }
            else // O dador afinal nao esta registado
            {
                await context.PostAsync("Dador nao registado");
            }
        }

        [LuisIntent("CancelarConsulta")]
        private async Task CancelarConsulta(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Cancelar Consulta");

        }

        [LuisIntent("EsclarecerDuvidas")]
        private async Task EsclarecerDuvidas(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Esclarecer Duvidas");

        }

        [LuisIntent("EsclarecerDuvidasDinheiro")]
        private async Task EsclarecerDuvidasDinheiro(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Esclarecer Duvidas Dinheiro");

        }

        [LuisIntent("EsclarecerDuvidasIdade")]
        private async Task EsclarecerDuvidasIdade(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Esclarecer Duvidas Idade");

        }

        [LuisIntent("ConsultarResultadosEspermograma")]
        private async Task ConsultarResultadosEspermograma(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Consultar Resultados Espermograma");

        }











    //    public Task StartAsync(IDialogContext context)
    //    {
    //        context.Wait(MessageReceivedAsync);

    //        return Task.CompletedTask;
    //    }


    //    private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
    //    {
    //        string response = "Olá. Eis algumas tarefas em que lhe posso ajudar:\n" +
    //                    "- Marcar consulta.\n" +
    //                    "- Cancelar consulta.\n" +
    //                    "- Consultar Resultados Espermograma.\n" +
    //                    "- Esclarecimento de Dúvidas.";

    //        var activity = await result as Activity;

    //        luis = new LUISService();
    //        string luisResponse = await luis.MakeRequest(activity.Text);

    //        if (DoAction(luisResponse))
    //        {
    //            response = "Operação concluída com sucesso.";
    //        }

    //        // return our reply to the user
    //        //await context.PostAsync($"You sent {activity.Text} which was {length} characters");
    //        await context.PostAsync(response);

    //        context.Wait(MessageReceivedAsync);
    //    }

    //    public async Task DarBoasVindas(IDialogContext context, IAwaitable<object> result)
    //    {
    //        string helloMessage = "Olá. Eis algumas tarefas em que lhe posso ajudar:\n" +
    //                    "- Marcar consulta.\n" +
    //                    "- Cancelar consulta.\n" +
    //                    "- Consultar Resultados Espermograma.\n" +
    //                    "- Esclarecimento de Dúvidas.";

    //        PromptDialog.Text(context, AnswerResponse, helloMessage);
    //    }

    //    public async Task AnswerResponse(IDialogContext context, IAwaitable<string> result)
    //    {
            
    //    }

        

    //    private bool DoAction(string action)
    //    {
    //        bool actionFound = false;

    //        switch (action)
    //        {
    //            case "MarcarConsulta":

    //                actionFound = true;
    //                break;
    //            case "EsclarecerDuvidasIdade":

    //                actionFound = true;
    //                break;
    //            case "EsclarecerDuvidasDinheiro":

    //                actionFound = true;
    //                break;
    //            case "EsclarecerDuvidas":

    //                actionFound = true;
    //                break;
    //            case "ConsultarResultadosEspermograma":

    //                actionFound = true;
    //                break;
    //            case "CancelarConsulta":

    //                actionFound = true;
    //                break;
    //            default:

    //                break;
    //        }

    //        return actionFound;
    //    }
    }
}
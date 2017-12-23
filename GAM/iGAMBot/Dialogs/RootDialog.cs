using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
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

        // ---------------------------------------------------------------------------------------------------------------------------

        [LuisIntent("MarcarConsulta")]
        private async Task MarcarConsulta(IDialogContext context, LuisResult result)
        {
            PromptDialog.Text(context, VerifyIfRegistered, "Para marcar consulta deves estar registado no sistema\nPor favor insere o teu número de identificação.");
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
            await context.PostAsync("Consultar Resultados Espermograma");

        }

    }
}
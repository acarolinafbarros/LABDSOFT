using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;

namespace iGAMChatBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            context.Wait(MessageReceivedAsync);
        }

        /* Workflow:
            * 1 - Bot: 
            *          Olá,sou o seu Assistente iGAM e estou aqui para o ajudar.
            *          Pode-me indicar por favor se já se registou anteriormente? 
            *          (Mostrar opções: Sim/Não)
            * 
            * 2 - Bot: Se for um dador registado : 
            *                                      Pode-me indicar por favor o número do seu documento de identificação?
            *                                      2.1 - Se estiver registado:
            *                                              2.1.1 - Bem-vindo <nome_dador>,
            *                                                      Eis algumas tarefas em que lhe posso ajudar:
            *                                                      (Mostrar opções: 1 - Marcar consulta | 2 - Cancelar Consulta | 3 - Consultar Resultados Espermograma)
            *                                      2.2 - Se não estiver registado:
            *                                              2.2.1 - Reparei que afinal ainda não se encontra registado.
            *                                                      Eis algumas tarefas em que lhe posso ajudar:
            *                                                      (Mostrar opções: 1 - Registar | 2 - Esclarecimento de Dúvidas)
            *          Se não for um dador registado:     
            *                                      Eis algumas tarefas em que lhe posso ajudar:
            *                                      (Mostrar opções: 1 - Registar | 2 - Esclarecimento de Dúvidas)
         */

        private enum Enum_YesOrNo { Sim, Nao }
        private enum Enum_DadorRegistado { MarcarConsulta, CancelarConsulta, ConsultarResultadosEspermograma }
        private enum Enum_DadorNaoRegistado { Registar, EsclarecimentoDuvidas }

        public async Task DarBoasVindas(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Olá, sou o seu Assistente iGAM e estou aqui para o ajudar.");

            var options = new Enum_YesOrNo[] { Enum_YesOrNo.Sim, Enum_YesOrNo.Nao };
            var descriptions = new string[] { "Sim", "Não" };

            PromptDialog.Choice<Enum_YesOrNo>(context, DizerSeDadorRegistado, options,
                                                   "Já se registou anteriormente?", descriptions: descriptions);
        }

        private async Task DizerSeDadorRegistado(IDialogContext context, IAwaitable<Enum_YesOrNo> result)
        {
            var opEscolhida = await result;
            if (opEscolhida == Enum_YesOrNo.Sim)
            {
                PromptDialog.Text(context, AtuarParaDadorRegistado, "Pode-me indicar por favor o número do seu documento de identificação?");
            }
            else
            {
                PromptDialog.Text(context, AtuarParaDadorNaoRegistado, "Reparei que você não é um dador registado");
            }
        }

        // --------------------------------------------------------------------------------------------------------------------------------------

        public async Task VerificarSeDadorRegistado(IDialogContext context, IAwaitable<string> result)
        {           
            var numeroIdentificacao = await result;

            /* Chamada a BD para ver se o dador existe
              * Se existir: Instanciar os dados do dador num objeto para ser manipulado
              * Se não existir: Redirecionar para o metodo AtuarParaDadorNaoRegistado
            */
            var resultadoBD = ""; // Substituir "" pela query a BD

            if (resultadoBD != null) // O dador esta registado
            {
                PromptDialog.Text(context, AtuarParaDadorRegistado, "Bem-vindo <Utente>!"); // Substituir <Utente> por + objDador.Nome
            }
            else // O dador afinal nao esta registado
            {
                PromptDialog.Text(context, AtuarParaDadorNaoRegistado, "Reparei que você não é um dador registado");
            }
        }

        // --------------------------------------------------------------------------------------------------------------------------------------

        public async Task AtuarParaDadorRegistado(IDialogContext context, IAwaitable<object> result)
        {
            var options = new Enum_DadorRegistado[] { Enum_DadorRegistado.MarcarConsulta, Enum_DadorRegistado.CancelarConsulta, Enum_DadorRegistado.ConsultarResultadosEspermograma };
            var descriptions = new string[] { "Marcar Consulta", "Cancelar Consulta", "Consultar Resultados Espermograma" };

            PromptDialog.Choice<Enum_DadorRegistado>(context, ReencaminharOpsDadorRegistado, options,
                                       "Eis algumas tarefas em que lhe posso ajudar:", descriptions: descriptions);
        }

        private async Task ReencaminharOpsDadorRegistado(IDialogContext context, IAwaitable<Enum_DadorRegistado> result)
        {
            var opEscolhida = await result;
            if (opEscolhida == Enum_DadorRegistado.MarcarConsulta)
            {
                // Chamar o controller de marcar uma consulta
            }
            else if (opEscolhida == Enum_DadorRegistado.CancelarConsulta)
            {
                // Chamar o controller de cancelar uma consulta
            }
            else if (opEscolhida == Enum_DadorRegistado.ConsultarResultadosEspermograma)
            {
                // Chamar o controller de consultar os resultados do espermograma
            }
        }

        // --------------------------------------------------------------------------------------------------------------------------------------

        public async Task AtuarParaDadorNaoRegistado(IDialogContext context, IAwaitable<object> result)
        {
            var options = new Enum_DadorNaoRegistado[] { Enum_DadorNaoRegistado.Registar, Enum_DadorNaoRegistado.EsclarecimentoDuvidas };
            var descriptions = new string[] { "Registar", "Esclarecimento de Dúvidas" };

            PromptDialog.Choice<Enum_DadorNaoRegistado>(context, ReencaminharOpsDadorNaoRegistado, options,
                                       "Eis algumas tarefas em que lhe posso ajudar:", descriptions: descriptions);
        }

        private async Task ReencaminharOpsDadorNaoRegistado(IDialogContext context, IAwaitable<Enum_DadorNaoRegistado> result)
        {
            var opEscolhida = await result;
            if (opEscolhida == Enum_DadorNaoRegistado.Registar)
            {
                // Chamar página de registo de dador
            }
            else if (opEscolhida == Enum_DadorNaoRegistado.EsclarecimentoDuvidas)
            {
                // Chamar o controller que respode as duvidas
            }
        }
    }
}


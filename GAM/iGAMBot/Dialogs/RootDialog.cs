using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using iGAMBot.Controllers;

namespace iGAMBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private static LUISService luis; 

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


            luis = new LUISService();
            string luisResponse = await luis.MakeRequest(activity.Text);


            // return our reply to the user
            //await context.PostAsync($"You sent {activity.Text} which was {length} characters");
            await context.PostAsync($"{luisResponse}");

            context.Wait(MessageReceivedAsync);
        }
    }
}
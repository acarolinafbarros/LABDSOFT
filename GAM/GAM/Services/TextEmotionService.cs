using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.ProjectOxford.Common.Contract;
using Microsoft.ProjectOxford.Emotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAM.Services
{
    public class TextEmotionService
    {
        ITextAnalyticsAPI client; 
        
        const string APIKEY = "a5ca77fe9cf24cbcb1316964aeb24636";

        public TextEmotionService()
        {
            client = new TextAnalyticsAPI
            {
                AzureRegion = AzureRegions.Westcentralus,
                SubscriptionKey = APIKEY
            };
        }

        public List<double?> AnalyzeEmotion(List<string> texts)
        {
            List<double?> sentimentScores = new List<double?>();
            List<MultiLanguageInput> inputList = new List<MultiLanguageInput>();

            for (int i = 0; i < texts.Count; i++)
            {
                inputList.Add(new MultiLanguageInput("pt", i.ToString(), texts[i]));
            }

            SentimentBatchResult result = client.Sentiment(new MultiLanguageBatchInput(inputList));

            // Storing the sentiment results
            foreach (var document in result.Documents)
            {
                sentimentScores.Add(document.Score);
            }

            return sentimentScores;
        }
    }
}

namespace Twitter_Sentiment.Business
{
    using System;
    using Azure;
    using Azure.AI.TextAnalytics;
    using Twitter_Sentiment.Business.Interfaces;
    using Twitter_Sentiment.Core.Models.Common;
    using Twitter_Sentiment.Core.Models.RequestModels;
    using Twitter_Sentiment.Core.Models.ResponseModels;

    /// <summary>
    /// TweetLanguageDetection class.
    /// </summary>
    public class TweetLanguageDetection : ITweetLanguageDetection
    {
        /// <summary>
        /// AzureKeyCredential object.
        /// </summary>
        private static AzureKeyCredential? _credentials;

        /// <summary>
        /// Uri object.
        /// </summary>
        private static Uri? _endpoint;

        /// <summary>
        /// Function to process request model to detect tweet language.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="key"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task<List<TweetLanguageDetectionResponseModel>> DetectTweetLanguage(IEnumerable<TweetLanguageDetectionRequestModel> requestModel, string key, string endpoint)
        {
            _credentials = new AzureKeyCredential(key);
            _endpoint = new Uri(endpoint);

            List<TweetLanguageDetectionResponseModel> responseModel = new();
            var client = new TextAnalyticsClient(_endpoint, _credentials);


            foreach (TweetLanguageDetectionRequestModel tweet in requestModel)
            {
                bool result = await LanguageDetection(client, tweet.Tweet_text);
                var response = new TweetLanguageDetectionResponseModel(tweet.Tweet_text, result);
                responseModel.Add(response);
            }

            return responseModel;
        }

        /// <summary>
        /// Function to process request model to detect tweet sentiment.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="key"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task<List<TweetSentimentDetectionResponseModel>> DetectTweetSentiment(IEnumerable<TweetSentimentDetectionRequestModel> requestModel, string key, string endpoint)
        {
            _credentials = new AzureKeyCredential(key);
            _endpoint = new Uri(endpoint);

            var client = new TextAnalyticsClient(_endpoint, _credentials);
            List<TweetSentimentDetectionResponseModel> responseModel = new();


            foreach (TweetSentimentDetectionRequestModel tweet in requestModel)
            {
                DocumentSentiment? result = await SentimentDetection(client, tweet.Tweet_text);
                if (result == null) continue;

                SentimentScoreModel sentimentScores = new()
                {
                    Positive = result.ConfidenceScores.Positive,
                    Negative = result.ConfidenceScores.Negative,
                    Neutral = result.ConfidenceScores.Neutral
                };
                var mood = GetMood(sentimentScores);
                var response = new TweetSentimentDetectionResponseModel(tweet.Tweet_text, mood, sentimentScores);

                responseModel.Add(response);
            }

            return responseModel;
        }

        /// <summary>
        /// Helper function to detect language of text.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="tweet"></param>
        /// <returns></returns>
        private async static Task<bool> LanguageDetection(TextAnalyticsClient client, string? tweet)
        {
            if (tweet == string.Empty) return false;


            DetectedLanguage detectedLanguage = await client.DetectLanguageAsync(tweet);
            return detectedLanguage.Name == "English";
        }

        /// <summary>
        /// Helper function to detect sentiment of text.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="tweet"></param>
        /// <returns></returns>
        private async static Task<DocumentSentiment?> SentimentDetection(TextAnalyticsClient client, string? tweet)
        {
            if (tweet == string.Empty) return null;

            DocumentSentiment sentiments = await client.AnalyzeSentimentAsync(tweet);
            return sentiments;
        }

        /// <summary>
        /// Helper function to get the detected mood.
        /// </summary>
        /// <param name="sentiments"></param>
        /// <returns></returns>
        private static string GetMood(SentimentScoreModel sentiments)
        {
            if (sentiments.Positive > sentiments.Negative && sentiments.Positive > sentiments.Neutral) return "POSITIVE";

            else if (sentiments.Negative > sentiments.Positive && sentiments.Negative > sentiments.Neutral) return "NEGATIVE";

            else return "NEUTRAL";
        }
    }
}
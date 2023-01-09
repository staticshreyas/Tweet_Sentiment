namespace Twitter_Sentiment.Business
{
    using System;
    using Azure;
    using Azure.AI.TextAnalytics;
    using Twitter_Sentiment.Business.Interfaces;
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
        public List<TweetLanguageDetectionResponseModel> DetectTweetLanguage(TweetLanguageDetectionRequestModel[] requestModel, string key, string endpoint)
        {
            _credentials = new AzureKeyCredential(key);
            _endpoint = new Uri(endpoint);

            var client = new TextAnalyticsClient(_endpoint, _credentials);
            List<TweetLanguageDetectionResponseModel> responseModel = new();

            foreach (TweetLanguageDetectionRequestModel tweet in requestModel)
            {
                bool result = LanguageDetectionExample(client, tweet.Tweet_text);
                var response = new TweetLanguageDetectionResponseModel(tweet.Tweet_text, result);
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
        private static bool LanguageDetectionExample(TextAnalyticsClient client, string? tweet)
        {
            DetectedLanguage detectedLanguage = client.DetectLanguage(tweet);
            return detectedLanguage.Name == "English";
        }
    }
}
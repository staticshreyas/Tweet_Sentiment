namespace Twitter_Sentiment.Business.Interfaces
{
    using Twitter_Sentiment.Core.Models.RequestModels;
    using Twitter_Sentiment.Core.Models.ResponseModels;

    /// <summary>
    /// TweetLanguageDetection Interface.
    /// </summary>
    public interface ITweetLanguageDetection
    {
        /// <summary>
        /// Function to process request model to detect tweet language.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="key"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public Task<List<TweetLanguageDetectionResponseModel>> DetectTweetLanguage(IEnumerable<TweetLanguageDetectionRequestModel> requestModel, string key, string endpoint);

        /// <summary>
        /// Function to process request model to detect tweet sentiment.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="key"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public Task<List<TweetSentimentDetectionResponseModel>> DetectTweetSentiment(IEnumerable<TweetSentimentDetectionRequestModel> requestModel, string key, string endpoint);
    }
}
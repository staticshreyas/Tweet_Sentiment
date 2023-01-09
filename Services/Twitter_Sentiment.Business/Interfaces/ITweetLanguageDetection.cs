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
        public List<TweetLanguageDetectionResponseModel> DetectTweetLanguage(TweetLanguageDetectionRequestModel[] requestModel, string key, string endpoint);
    }
}
namespace Twitter_Sentiment.Core.Models.ResponseModels
{
    using Twitter_Sentiment.Core.Models.Common;

    /// <summary>
    /// TweetLanguageDetection Response Model.
    /// </summary>
    public class TweetLanguageDetectionResponseModel : Tweet
    {
        /// <summary>
        /// TweetLanguageDetectionResponseModel Constructor.
        /// </summary>
        /// <param name="tweet_Text"></param>
        /// <param name="result"></param>
        public TweetLanguageDetectionResponseModel(string? tweet_Text, bool isEnglish)
        {
            Tweet_text = tweet_Text;
            Is_english = isEnglish;
        }

        /// <summary>
        /// Is english.
        /// </summary>
        /// <value></value>
        public bool Is_english { get; set; }
    }
}
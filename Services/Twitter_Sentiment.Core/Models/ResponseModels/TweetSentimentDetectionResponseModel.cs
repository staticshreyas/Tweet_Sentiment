namespace Twitter_Sentiment.Core.Models.ResponseModels
{
    using Twitter_Sentiment.Core.Models.Common;

    /// <summary>
    /// TweetSentimentDetection Response Model.
    /// </summary>
    public class TweetSentimentDetectionResponseModel : Tweet
    {
        public TweetSentimentDetectionResponseModel(string? tweet_Text, string mood, SentimentScoreModel score)
        {
            Tweet_text = tweet_Text;
            Detected_mood = mood;
            Sentiment_score = score;
        }

        /// <summary>
        /// Sentiment Score.
        /// </summary>
        public SentimentScoreModel Sentiment_score { get; set; }

        /// <summary>
        /// Detected Mood.
        /// </summary>
        /// <value></value>
        public string Detected_mood { get; set; }
    }
}
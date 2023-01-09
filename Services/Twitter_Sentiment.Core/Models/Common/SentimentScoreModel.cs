namespace Twitter_Sentiment.Core.Models.Common
{
    /// <summary>
    /// Sentiment Score Model.
    /// </summary>
    public class SentimentScoreModel
    {
        /// <summary>
        /// Positive probability.
        /// </summary>
        public double Positive { get; set; }

        /// <summary>
        /// Neutral probability.
        /// </summary>
        public double Neutral { get; set; }

        /// <summary>
        /// Negative probability.
        /// </summary>
        public double Negative { get; set; }
    }
}
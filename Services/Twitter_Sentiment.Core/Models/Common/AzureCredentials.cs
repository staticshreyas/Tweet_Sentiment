namespace Twitter_Sentiment.Core.Models.Common
{
    /// <summary>
    /// AzureCredentials class.
    /// </summary>
    public class AzureCredentials
    {
        /// <summary>
        /// Key.
        /// </summary>
        /// <value></value>
        public string AZURE_LANGUAGE_DETECTION_KEY { get; set; } = "";

        /// <summary>
        /// Endpoint.
        /// </summary>
        /// <value></value>
        public string AZURE_LANGUAGE_DETECTION_ENDPOINT { get; set; } = "";
    }
}

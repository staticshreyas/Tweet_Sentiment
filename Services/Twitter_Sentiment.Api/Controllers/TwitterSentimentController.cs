namespace Twitter_Sentiment.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Twitter_Sentiment.Business.Interfaces;
    using Twitter_Sentiment.Core.Models.Common;
    using Twitter_Sentiment.Core.Models.RequestModels;
    using Twitter_Sentiment.Core.Models.ResponseModels;

    /// <summary>
    /// Twitter Sentiment Controller.
    /// </summary>
    [ApiController]
    [Route("api")]
    public class TwitterSentimentController : ControllerBase
    {
        /// <summary>
        /// Logger Object.
        /// </summary>
        private readonly ILogger<TwitterSentimentController> _logger;

        /// <summary>
        /// TweetLanguageDetection Object.
        /// </summary>
        private readonly ITweetLanguageDetection _tweetLanguageDetection;

        /// <summary>
        /// Options Object.
        /// </summary>
        private readonly IOptions<AzureCredentials> _azureCredentials;

        /// <summary>
        /// Controller Constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="tweetLanguageDetection"></param>
        /// <param name="azureCredentials"></param>
        public TwitterSentimentController(ILogger<TwitterSentimentController> logger,
            ITweetLanguageDetection tweetLanguageDetection,
            IOptions<AzureCredentials> azureCredentials
        )
        {
            this._logger = logger;
            this._tweetLanguageDetection = tweetLanguageDetection;
            this._azureCredentials = azureCredentials;
        }

        /// <summary>
        /// Controller for Language Detection API.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost("language-detection")]
        public async Task<ActionResult<TweetLanguageDetectionResponseModel>> PostLanguageDetection([FromBody] IEnumerable<TweetLanguageDetectionRequestModel> requestModel)
        {
            this._logger.LogInformation("API START", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff"));
            if (requestModel == null)
            {
                return BadRequest();
            }

            var response = await this._tweetLanguageDetection.DetectTweetLanguage(requestModel, this._azureCredentials.Value.AZURE_LANGUAGE_DETECTION_KEY, this._azureCredentials.Value.AZURE_LANGUAGE_DETECTION_ENDPOINT);

            if (response == null)
            {
                return NotFound();
            }
            this._logger.LogInformation("API SUCCESS END", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff"));
            return Ok(response);
        }

        /// <summary>
        /// Controller for Language Detection API.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost("sentiment-score")]
        public async Task<ActionResult<TweetSentimentDetectionResponseModel>> PostTweetSentiment([FromBody] IEnumerable<TweetSentimentDetectionRequestModel> requestModel)
        {
            this._logger.LogInformation("API START", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff"));
            if (requestModel == null)
            {
                return BadRequest();
            }

            var response = await this._tweetLanguageDetection.DetectTweetSentiment(requestModel, this._azureCredentials.Value.AZURE_LANGUAGE_DETECTION_KEY, this._azureCredentials.Value.AZURE_LANGUAGE_DETECTION_ENDPOINT);

            if (response == null)
            {
                return NotFound();
            }
            this._logger.LogInformation("API SUCCESS END", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff"));
            return Ok(response);
        }
    }
}
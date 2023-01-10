namespace Twitter_Sentiment.Api
{
    using Twitter_Sentiment.Business;
    using Twitter_Sentiment.Business.Interfaces;
    using Twitter_Sentiment.Core.Models.Common;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.Configure<AzureCredentials>(Configuration.GetSection("AzureCredentials"));

            services.AddScoped<ITweetLanguageDetection, TweetLanguageDetection>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                policy =>
                {
                    policy.WithOrigins("https://twitter.com");
                });
            });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TweetSentimentAPI v1");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using LineOpenApi.MessagingApi.Api;
using LineOpenApi.MessagingApi.Client;
using LineOpenApi.MessagingApi.Model;
using LineOpenApi.Webhook.Model;
using Newtonsoft.Json;

namespace LineBotFunctions;

public class ReplyFunction
{
    private readonly ILogger _logger;

    public ReplyFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ReplyFunction>();
    }

    [Function("ReplyFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        try
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var callbackRequest = JsonConvert.DeserializeObject<CallbackRequest>(body);

            var api = new MessagingApiApi(new Configuration
            {
                AccessToken = Environment.GetEnvironmentVariable("ChannelAccessToken"),
                BasePath = "https://api.line.me"
            });

            foreach (var ev in callbackRequest.Events)
            {
                // echo when receive text message
                if (ev is MessageEvent messageEvent && messageEvent.Message is TextMessageContent textMessageContent)
                {
                    var replyMessageRequest = new ReplyMessageRequest(messageEvent.ReplyToken, new List<Message>
                    {
                        new TextMessage(textMessageContent.Text)
                        {
                            Type = "text"
                        }
                    });
                    await api.ReplyMessageAsync(replyMessageRequest);
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError($"Error: {e.Message}");
            _logger.LogError($"Error: {e.StackTrace}");
        }
        
        return req.CreateResponse(HttpStatusCode.OK);
    }
}
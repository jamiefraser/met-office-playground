using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace metoffice_playground
{
    public static class Subscribe
    {
        [FunctionName("subscribe")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get",  Route = null)] HttpRequest req,
            ILogger log)
        {
            string mode = req.Query["hub.mode"];
            string challenge = req.Query["hub.challenge"];
            string verify_token = req.Query["hub.verify_token"];
            log.LogInformation($"Hub Mode is: {mode}");
            log.LogInformation($"Hub Challenge is: {challenge}");
            log.LogInformation($"Hub verify token is: {verify_token}");
            return challenge != null
                ? (ActionResult)new OkObjectResult($"{verify_token}")
                : new BadRequestObjectResult("Please pass a challenge on the query string or in the request body");
        }
    }
}

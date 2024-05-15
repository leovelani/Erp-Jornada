using Erp_Jornada.Exceptions.HttpErrors;
using Erp_Jornada.Tools;
using Erp_Jornada.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Erp_Jornada.Middlewares
{
    public class ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
    {
        private readonly RequestDelegate next = next;
        private readonly ILogger<ErrorMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);

                await UnAuthorized(context);

            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, $"Erro ocorrido com TraceId: {context.TraceIdentifier}");

            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.Path,
                Type = "https://learn.microsoft.com/pt-br/troubleshoot/developer/webapps/iis/www-administration-management/http-status-code",
                Extensions =
                        {
                            { "traceId", context.TraceIdentifier },
                            { "Logref", Guid.NewGuid().ToString() },
                            { "Message", "Messagem padrão que não é especifica do erro"}
                        }
            };

            int statusCode;
            ErrorResponseVm errorResponseVm;

            switch (ex)
            {
                //HTTP 400
                case Microsoft.IdentityModel.Tokens.SecurityTokenMalformedException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponseVm = new ErrorResponseVm("ErroID401", "Não autorizado.");
                    problemDetails.Extensions["Messagem Específica do erro"] = "Erro 401....explicando o erro melhor..";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://www.hostinger.com.br/tutoriais/erro-401#:~:text=O%20Erro%20401%20indica%20um,exigem%20um%20login%20para%20acesso.";
                    break;
                case ArgumentException _:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    errorResponseVm = new ErrorResponseVm("ErroID400", "Requisição inválida.");
                    problemDetails.Extensions["Messagem Específica do erro"] = "Erro 400....explicando..";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://learn.microsoft.com/pt-br/iis/troubleshoot/diagnosing-http-errors/troubleshooting-http-400-errors-in-iis";
                    break;

                //HTTP 401
                case UnauthorizedAccessException _:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponseVm = new ErrorResponseVm("ErroID401", "Não autorizado.");
                    problemDetails.Extensions["Messagem Específica do erro"] = "Erro 401....explicando o erro melhor..";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://www.hostinger.com.br/tutoriais/erro-401#:~:text=O%20Erro%20401%20indica%20um,exigem%20um%20login%20para%20acesso.";
                    break;

                //HTTP 402
                case PaymentRequiredException _:
                    statusCode = (int)HttpStatusCode.PaymentRequired;
                    errorResponseVm = new ErrorResponseVm("ErroID402", "Pagamento necessário.");
                    problemDetails.Extensions["Messagem Específica do erro"] = "Erro 402....explicando o erro melhor.. O pagamento....";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://kinsta.com/pt/base-de-conhecimento/http-402/#:~:text=O%20código%20HTTP%20402%20ou,“experimental”%20ou%20em%20desenvolvimento.";
                    break;

                //HTTP 403
                case ForbiddenException _:
                    statusCode = (int)HttpStatusCode.Forbidden;
                    errorResponseVm = new ErrorResponseVm("ErroID403", "Acesso proibido.");
                    problemDetails.Extensions["Messagem Específica do erro"] = "Erro 403....explicando o erro melhor.. Acesso foi proibido...";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://www.hostinger.com.br/tutoriais/o-que-significa-erro-403";
                    break;

                //HTTP 404  
                case NotFoundException _:
                    statusCode = (int)HttpStatusCode.NotFound;
                    errorResponseVm = new ErrorResponseVm("ErroID404", "Recurso não encontrado.");
                    problemDetails.Extensions["Message"] = "Erro 404....explicando o erro melhor.. O erro não foi encontrado porque falta inserir um id válido";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://www.hostinger.com.br/tutoriais/erro-404";
                    break;

                //HTTP 405
                case MethodNotAllowedException _:
                    statusCode = (int)HttpStatusCode.MethodNotAllowed;
                    errorResponseVm = new ErrorResponseVm("ERROID405", "Método não permitido na requisição.");
                    problemDetails.Extensions["Message"] = "Erro 405....O método especificado na requisição não é permitido para o recurso identificado.";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://kinsta.com/pt/blog/erro-405-method-not-allowed/";
                    break;

                //HTTP 408
                case RequestTimeoutException _:
                    statusCode = (int)HttpStatusCode.RequestTimeout;
                    errorResponseVm = new ErrorResponseVm("ERROID408", "O servidor encerrou a conexão.");
                    problemDetails.Extensions["Message"] = "Erro 408....O servidor encerrou a conexão porque a requisição levou muito tempo.";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://kinsta.com/pt/base-de-conhecimento/http-408/";
                    break;

                //HTTP 409
                case ConflictException _:
                    statusCode = (int)HttpStatusCode.Conflict;
                    errorResponseVm = new ErrorResponseVm("ErroID409", "Conflito");
                    problemDetails.Extensions["Message"] = "Erro 409, Deu um conflito em algo..";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://cloud.ibm.com/docs/vpc?topic=vpc-troubleshoot-lb-409&locale=pt-BR#:~:text=O%20código%20de%20status%20HTTP,devido%20a%20conflito%20na%20solicitação..";
                    break;

                //HTTP 414
                case URITooLongException _:
                    statusCode = (int)HttpStatusCode.RequestUriTooLong;
                    errorResponseVm = new ErrorResponseVm("ErroID414", "URI Solicidade é muito longa.");
                    problemDetails.Extensions["Message"] = "Erro 414, A URI solicitada é muito longa para o servidor processar.";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://kinsta.com/pt/base-de-conhecimento/414-request-uri-too-large/";
                    break;


                //HTTP 500
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponseVm = new ErrorResponseVm("ErroID500", "Ocorreu um erro interno no servidor.");
                    break;
            }

            problemDetails.Status = statusCode;
            problemDetails.Title = Tool.GetErrorTitle(statusCode);
            problemDetails.Detail = errorResponseVm.Errors.FirstOrDefault()?.Message;

            await ChangeContext(context, problemDetails, statusCode);
        }

        private static async Task UnAuthorized(HttpContext context)
        {
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                await ChangeContext(context,
                    new ResultModel<dynamic>(HttpStatusCode.Unauthorized,
                    Tool.GetErrorTitle((int)HttpStatusCode.Unauthorized))
                    , (int)HttpStatusCode.Unauthorized);

            else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                await ChangeContext(context,
                    new ResultModel<dynamic>(HttpStatusCode.Forbidden,
                    Tool.GetErrorTitle((int)HttpStatusCode.Forbidden))
                    , (int)HttpStatusCode.Forbidden);
        }

        public static async Task ChangeContext<T>(HttpContext context, T problemDetails, int status)
        {
            context.Response.StatusCode = status;
            var result = JsonConvert.SerializeObject(problemDetails);
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsync(result);
        }
    }
}

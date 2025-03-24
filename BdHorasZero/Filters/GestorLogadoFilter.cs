using BdHorasZero.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BdHorasZero.Filters
{
    public class GestorLogadoFilter: IAsyncActionFilter
    {
        
        private readonly GestoresService _gestoresService;

        public GestorLogadoFilter(GestoresService gestoresService)
        {
            _gestoresService = gestoresService;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // carrega as informações do Gestor logado na aplicação:
            await _gestoresService.CarregarGestorAsync();

            // obtém o Gestor da sessão:
            var gestor = _gestoresService.ObterGestorDaSessao();

            // se não houver gestor na sessão, redireciona para a página de login:
            if (gestor == null)
            {
                context.Result = new RedirectResult("~/Identity/Account/Login");
                return;
            }

            // se o controller for do tipo 'Controller', armazena os dados do Gestor no ViewData:

            if (context.Controller is Controller controller)
            {
                controller.ViewData["Gestor"] = gestor;
            }

            // continua a execução da action do controller:
            await next();
        }
    }
}

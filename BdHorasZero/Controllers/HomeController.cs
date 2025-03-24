using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BdHorasZero.Models;
using BdHorasZero.Services;
using Microsoft.AspNetCore.Authorization;
using BdHorasZero.Filters;

namespace BdHorasZero.Controllers;

// filtro para obter as informações do Gestor logado
[ServiceFilter(typeof(GestorLogadoFilter))]

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly GestoresService _gestoresService;

    public HomeController(ILogger<HomeController> logger, GestoresService gestoresService)
    {
        _logger = logger;
        _gestoresService = gestoresService;
    }


    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }



    //[Authorize]
    //public async Task<IActionResult> Index()
    //{
        //await _gestoresService.CarregarGestorAsync();

        //var gestor = _gestoresService.ObterGestorDaSessao();
        //if (gestor == null)
        //{
        //    return Redirect("~/Identity/Account/Login");
        //    //return RedirectToAction("Login", "Account", new { area = "Identity" });
        //    //return RedirectToAction("Login", "Account");
        //}
        //ViewData["Gestor"] = gestor;
    //    return View();
    //}
}

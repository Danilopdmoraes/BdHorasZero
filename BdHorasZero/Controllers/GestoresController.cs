﻿using BdHorasZero.Data;
using BdHorasZero.Filters;
using BdHorasZero.Models;
using BdHorasZero.Repository;
using BdHorasZero.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BdHorasZero.Controllers
{
    [ServiceFilter(typeof(GestorLogadoFilter))]

    public class GestoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IGestoresRepository _gestoresRepository;
        private readonly GestoresService _gestoresService;

        public GestoresController (
            ApplicationDbContext context,
            IGestoresRepository gestoresRepository,
            GestoresService gestoresService
            )
        {
            _context = context;
            _gestoresRepository = gestoresRepository;
            _gestoresService = gestoresService;
        }


        [Authorize]
        public async Task<IActionResult> CadastrarGrupoGestores()
        {
            return View();
        }


        [HttpPost]
        public async Task <IActionResult> AlterarTbGestores(GestoresModel novoNomeGrupo)
        {
            _gestoresRepository.AtualizarTbGestores(novoNomeGrupo);
            
            return Redirect("~/Vinculos/MontarGrupo");
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _gestoresService.ObterGrupoDoGestorLogado();
            return View(viewModel);
        }


        public IActionResult RenomearGrupo()
        {
            return View();
        }



    }
}

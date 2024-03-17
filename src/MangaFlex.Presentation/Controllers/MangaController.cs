using MangaFlex.Core.Data.Mangas.Models;
using MangaFlex.Core.Data.Mangas.Services;
using MangaFlex.Core.Data.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MangaFlex.Presentation.Controllers
{
    [Route("[controller]/[action]")]
    public class MangaController : Controller
    {
        private readonly ISender sender;
        private readonly IMangaService mangaService;

        public MangaController(ISender sender, IMangaService mangaService)
        {
            this.sender = sender;
            this.mangaService = mangaService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/mangas")]
        public async Task<IActionResult> Mangas()
        {
            //await mangaService.ReadAsync("fc0a7b86-992e-4126-b30f-ca04811979bf");
            IEnumerable<Manga> mangas = Enumerable.Empty<Manga>();

            try
            {
                var command = new FindMangasCommand(null!);
                mangas = await sender.Send(command);
            }
            catch (AggregateException ex)
            {
                foreach (ArgumentException error in ex.Flatten().InnerExceptions)
                {
                    base.ModelState.AddModelError(error.ParamName!, error.Message);
                }
                return BadRequest("While searching for this request an error happened");
            }

            return View(model: mangas);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> About(string id)
        {
            try
            {
                var manga = await mangaService.GetByIdAsync(id);
                return View(manga);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                return BadRequest("An unexpected error occurred.");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Read(string id)
        {
            var mangaPages = await mangaService.ReadAsync(id);
            return View(mangaPages);
        }
    }
}
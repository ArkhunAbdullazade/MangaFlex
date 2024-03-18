using MangaFlex.Core.Data.Mangas.Models;
using MangaFlex.Core.Data.Mangas.Services;
using MangaFlex.Core.Data.Mangas.ViewModels;
using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Route("/Mangas")]
        public async Task<IActionResult> Mangas(int page = 1, string? search = null)
        {
            MangasViewModel mangasViewModel = new();

            try
            {
                var command = new FindMangasCommand(search!, page);
                mangasViewModel = await sender.Send(command);
            }
            catch (AggregateException ex)
            {
                foreach (ArgumentException error in ex.Flatten().InnerExceptions)
                {
                    System.Console.WriteLine(error.Message);
                    base.ModelState.AddModelError(error.ParamName!, error.Message);
                }
                return BadRequest("While searching for this request an error happened");
            }

            return View(model: mangasViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> About(string id)
        {
            try
            {
                var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                var manga = await mangaService.GetByIdAsync(id);
                var IsInLastWatch = new IsMangaInLastWatchesCommand(userid, id);
                var result = await sender.Send(IsInLastWatch);
                if (result == false)
                {
                    var command = new AddLastWatchCommand(userid, id);
                    await sender.Send(command);
                }
                else
                {
                    var DeleteCommand = new DeleteLastWatchCommand(userid, id);
                    await sender.Send(DeleteCommand);
                    var command = new AddLastWatchCommand(userid, id);
                    await sender.Send(command);
                }
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
        public async Task<IActionResult> Read(string id, int chapter = 1)
        {
            var mangaChapterViewModel = await mangaService.ReadAsync(id, chapter);
            return View(mangaChapterViewModel);
        }
    }
}
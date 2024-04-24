using MangaFlex.Core.Data.LastWatches.Command;
using MangaFlex.Core.Data.Mangas.Services;
using MangaFlex.Core.Data.Mangas.ViewModels;
using MangaFlex.Core.Data.UserMangas.Commands;
using MangaFlex.Core.Data.UserMangas.Models;
using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Presentation.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MangaFlex.Presentation.Controllers
{
    public class MangaController : Controller
    {
        private readonly ISender sender;
        private readonly IMangaService mangaService;
        private readonly UserManager<User> userManager;

        public MangaController(ISender sender, IMangaService mangaService, UserManager<User> userManager)
        {
            this.sender = sender;
            this.userManager = userManager;
            this.mangaService = mangaService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/Mangas")]
        public async Task<IActionResult> Mangas(int page = 1, string? search = null)
        {
            MangasViewModel mangasViewModel;
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
        public async Task<IActionResult> AvailableLanguages(string id)
        {
            ViewBag.MangaId = id; 
            var availableLanguages = await mangaService.GetAvailableLanguages(id);
            return View(availableLanguages);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> About(string id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                var manga = await mangaService.GetByIdAsync(id);
                var IsInLastWatch = new IsMangaInLastWatchesCommand(userId, id);
                var result = await sender.Send(IsInLastWatch);

                if (result == false)
                {
                    var command = new AddLastWatchCommand(userId, id);
                    await sender.Send(command);
                }
                else
                {
                    var DeleteCommand = new DeleteLastWatchCommand(userId, id);
                    await sender.Send(DeleteCommand);
                    var command = new AddLastWatchCommand(userId, id);
                    await sender.Send(command);
                }

                var isMangaReadByUserCommand = new IsMangaReadByUserCommand(userId, manga.Id);
                ViewData["IsMangaLogged"] = await sender.Send(isMangaReadByUserCommand);

                if(ViewData["IsMangaLogged"] as bool? ?? false) {
                    var UserMangaCommand = new GetByUserAndMangaIdCommand(userId, manga.Id);
                    ViewData["MangaStatus"] = (await sender.Send(UserMangaCommand))?.Status;
                }

                return View(manga);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                return BadRequest("An unexpected error occurred.");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddUserManga([FromBody] UserMangaDto userMangaDto)
        {

            var newUserManga = new UserManga {
                UserId = this.userManager.GetUserId(User!),
                MangaId = userMangaDto.MangaId,
                Status = userMangaDto.Status,
            };
            
            try
            {
                var userMangaCommand = new AddUserMangaCommand(newUserManga);
                await sender.Send(userMangaCommand);
            }
            catch (Exception)
            {
                return BadRequest("There is No Such Manga To Add");
            }

            return RedirectToAction("About", new { id = userMangaDto.MangaId });
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUserManga([FromBody] UserMangaDto userMangaDto)
        {
            var newUserManga = new UserManga {
                UserId = this.userManager.GetUserId(User!),
                MangaId = userMangaDto.MangaId,
                Status = userMangaDto.Status,
            };
            
            try
            {
                var userMangaCommand = new UpdateUserMangaCommand(newUserManga);
                await sender.Send(userMangaCommand);
            }
            catch (Exception)
            {
                return BadRequest("There is No Such Manga To Update");
            }

            return RedirectToAction("About", new { id = userMangaDto.MangaId });
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUserManga(string id)
        {
            try
            {
                var userMangaCommand = new DeleteUserMangaCommand(this.userManager.GetUserId(User!), id);
                await sender.Send(userMangaCommand);
            }
            catch (Exception)
            {
                return BadRequest("There is No Such Manga To Update");
            }

            return RedirectToAction("About", new { id });
        }

        public IActionResult SetLanguage(string language, string mangaId)
        {
            Response.Cookies.Append("Language", language);
            string? redirectUrl = Url.Action("Read", "Manga", new { id = mangaId });
            return Redirect(redirectUrl!);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Read(string id, int chapter = 1)
        {
            string language = HttpContext.Request.Cookies["Language"] ?? "en";
            var mangaChapterViewModel = await mangaService.ReadAsync(id, chapter, language);
            return View(mangaChapterViewModel);
        }
    }
}
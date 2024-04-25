using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MangaFlex.Core.Data.Users.Models;
using Microsoft.AspNetCore.Identity;
using MangaFlex.Core.Data.Mangas.Services;
using MangaFlex.Core.Data.Mangas.ViewModels;
using MangaFlex.Core.Data.Users.Commands;
using System.Security.Claims;
using MangaFlex.Core.Data.LastWatches.Command;
using MangaFlex.Core.Data.UserMangas.Commands;
using MangaFlex.Core.Data.UserMangas.Models;
using MangaFlex.Api.Dtos;
using MangaFlex.Core.Data.Mangas.Models;

namespace MangaFlex.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
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

    /// <summary>
    /// Finds mangas.
    /// </summary>
    /// <returns>A list of mangas.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /Mangas
    ///     {
    ///        "query": "One",
    ///        "page": 1
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Returns a list of mangas</response>
    /// <response code="400">If request is incorrect</response>
    [HttpGet]
    [AllowAnonymous]
    [Route("/api/Mangas")]
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
            }
            return BadRequest("While searching for this request an error happened");
        }
        return Ok(mangasViewModel);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> AvailableLanguages(string id)
    {
        var command = new GetAvailableLanguagesCommand(id);
        var availableLanguages = await sender.Send(command);
        return Ok(availableLanguages);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> About(string id)
    {
        Manga manga;
        
        try
        {
            var mangaCommand = new GetMangaByIdCommand(id);
            manga = await sender.Send(mangaCommand);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var IsInLastWatch = new IsMangaInLastWatchesCommand(userId, id);
            var result = await sender.Send(IsInLastWatch);

            if (result == false)
            {
                var lasWatchCommand = new AddLastWatchCommand(userId, id);
                await sender.Send(lasWatchCommand);
            }
            else
            {
                var deleteCommand = new DeleteLastWatchCommand(userId, id);
                await sender.Send(deleteCommand);
                var command = new AddLastWatchCommand(userId, id);
                await sender.Send(command);
            }

        }
        catch
        {
            return BadRequest("An unexpected error occurred.");
        }

        return Ok(manga);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> IsMangaLogged(string id)
    {
        Manga manga;
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        manga = await mangaService.GetByIdAsync(id);
        var isMangaReadByUserCommand = new IsMangaReadByUserCommand(userId, manga.Id);
        var result = await sender.Send(isMangaReadByUserCommand);
        return Ok(result);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> UserMangaStatus(string id)
    {
        Manga manga;
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        manga = await mangaService.GetByIdAsync(id);
        var UserMangaCommand = new GetByUserAndMangaIdCommand(userId, manga.Id);
        var result = (await sender.Send(UserMangaCommand))?.Status;
        return Ok(result);
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
        return CreatedAtAction("About", new { id = userMangaDto.MangaId }, newUserManga);
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

        return Ok(newUserManga.Id);
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
        return Ok(id);
    }

    [HttpGet]
    [Authorize]
    public IActionResult SetLanguage(string language, string mangaId)
    {
        return Ok(new SetLanguageVM()
        {
            MangaId = mangaId,
            Language = language,
        });
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Read(string id, int chapter = 1)
    {
        string language = HttpContext.Request.Headers["language"];     
        Console.WriteLine(language);
        var readMangaCommand = new ReadMangaCommand(id, chapter, language);
        var mangaChapterViewModel = await sender.Send(readMangaCommand);

        return Ok(mangaChapterViewModel);
    }
}
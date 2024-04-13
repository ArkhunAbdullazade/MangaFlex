using WebApiMangaFlex.Mangas.Data.Mangas.Services;
using WebApiMangaFlex.Mangas.Data.Mangas.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMangaFlex.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MangaController : Controller
    {
        private readonly IMangaService mangaService;

        public MangaController(IMangaService mangaService)
        {
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
        [Route("/Mangas")]
        public async Task<IActionResult> Mangas([FromQuery] string? query = null, [FromQuery] int page = 1)
        {
            MangasViewModel mangasViewModel;
            try
            {
                var result = await mangaService.FindMangasAsync(query, page);
                return Ok(result);
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

        }

        [HttpGet]
        // [Authorize]
        public async Task<IActionResult> AvailableLanguages(string id)
        {
            ViewBag.MangaId = id;
            var availableLanguages = await mangaService.GetAvailableLanguages(id);
            return Ok(availableLanguages);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Read(string id, int chapter = 1)
        {
            string language = HttpContext.Request.Cookies["Language"] ?? "en";
            var mangaChapterViewModel = await mangaService.ReadAsync(id, chapter, language);
            return Ok(mangaChapterViewModel);
        }
    }
}
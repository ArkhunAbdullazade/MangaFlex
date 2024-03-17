using MangaFlex.Core.Data.Mangas.Models;
using MangaFlex.Core.Data.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MangaFlex.Presentation.Controllers
{
    [Route("[controller]")]
    public class MangaController : Controller
    {
        private readonly ISender sender;

        public MangaController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpGet]
        [AllowAnonymous]
        [ActionName("Mangas")]
        [Route("/Mangas")]
        public async Task<IActionResult> FindMangas()
        {
            IEnumerable<Manga> mangas = Enumerable.Empty<Manga>();

            try
            {
                var command = new FindMangasCommand(null!);
                mangas = await sender.Send(command);
            }
            catch(AggregateException ex)
            {
                foreach (ArgumentException error in ex.Flatten().InnerExceptions)
                {
                    base.ModelState.AddModelError(error.ParamName!, error.Message);
                }
                return BadRequest("While searching for this request an error happened");
            }
        
            return View(model: mangas);
        }
    }
}
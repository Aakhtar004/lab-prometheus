using Microsoft.AspNetCore.Mvc;
using ClienteAPI.Data;
using ClienteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClienteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposDocumentosController : ControllerBase
    {
        private readonly BdClientesContext _context;
        public TiposDocumentosController(BdClientesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDocumento>>> Get() =>
            await _context.TiposDocumentos.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoDocumento>> Get(int id)
        {
            var td = await _context.TiposDocumentos.FindAsync(id);
            if (td == null) return NotFound();
            return td;
        }
    }
}

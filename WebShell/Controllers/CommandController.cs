using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShell.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace WebShell.Controllers
{
    [Route("api/Command")]
    [ApiController]
    public class CommandController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CommandController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Command.Select(s => s.Value).ToListAsync() });
        }
    }
}

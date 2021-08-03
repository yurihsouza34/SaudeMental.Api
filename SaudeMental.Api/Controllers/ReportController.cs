using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaudeMental.Api.Context;
using SaudeMental.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SaudeMental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly AppDbContext _context;

        public ReportController(IReportService reportService, AppDbContext context)
        {
            _reportService = reportService;
            _context = context;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            var formsInfosCount = await _context.FormInfos.Where(f => f.UserId == userId).CountAsync();

            if (formsInfosCount == 0)
                return NotFound();

            var userInfo = await _context.UserInfos.Where(u => u.userId == userId).FirstAsync();

            if (userInfo == null)
                return NotFound("Configuracoes do usuario nao encontradas");

            var formsInfos = await _context.FormInfos.Where(u => u.UserId == userId).Skip(Math.Max(0, formsInfosCount - 30)).ToListAsync();

            if (formsInfos.Count == 0 || formsInfos == null)
                return NotFound();

            var pdfFile = _reportService.GeneratePdfReport(formsInfos, userInfo);
            return File(pdfFile,
            "application/octet-stream", "saude_mental.pdf");
        }
    }
}

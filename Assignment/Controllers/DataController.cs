using Assignment.DTOs;
using Assignment.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Assignment.Controllers
{
    public class DataController : Controller
    {
        private readonly IContentDataService contentDataService;
        public DataController(IContentDataService _contentDataService)
        {
            contentDataService = _contentDataService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await contentDataService.GetAllAsync();
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult DataDetails()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewData(ModifyContentDataDTO contentDataDTO)
        {
            if(!ModelState.IsValid) return View("DataDetails", contentDataDTO);
            var result = await contentDataService.InsertContentData(contentDataDTO);
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ServerMessage = result.Message;
            return View("DataDetails", contentDataDTO);
        }
        [HttpGet]
        public async Task<IActionResult> GenerateReportById(int ContentId)
        {
            var host = $"{Request.Scheme}://{Request.Host}";
            var result = await contentDataService.GenerateReportById(ContentId, host);
            if (!result.IsSuccess)
            {
                ViewBag.ServerMessage = result.Message;
                return View("Index");
            }
            return File(result.Data, "application/pdf", $"{Guid.NewGuid()}.pdf");
        }
        [HttpGet]
        public async Task<IActionResult> GenerateReportAll()
        {
            var host = $"{Request.Scheme}://{Request.Host}";
            var result = await contentDataService.GenerateReportAll(host);
            if (!result.IsSuccess)
            {
                ViewBag.ServerMessage = result.Message;
                return View("Index");
            }
            return File(result.Data, "application/pdf", $"{Guid.NewGuid()}.pdf");
        }
    }
}

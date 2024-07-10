
using Microsoft.AspNetCore.Mvc;
using MKTDotNetCore.MVCChartApp.Models;

namespace MKTDotNetCore.MVCChartApp.Controllers
{
    public class CanvasJsController : Controller
    {
        public IActionResult LineChart()
        {
            var lst = new List<CanvasLineChartModel>()
            {
                new CanvasLineChartModel
                {
                    y=450
                },
                new CanvasLineChartModel
                {
                    y=414
                },
                new CanvasLineChartModel
                {
                    y=520
                },
                new CanvasLineChartModel
                {
                    y=460
                },
                new CanvasLineChartModel
                {
                    y=450
                },
                new CanvasLineChartModel
                {
                    y=500
                },
                new CanvasLineChartModel
                {
                    y=480
                },
                new CanvasLineChartModel
                {
                    y=480
                },
                new CanvasLineChartModel
                {
                    y=410
                },
                new CanvasLineChartModel
                {
                    y=500
                },
                new CanvasLineChartModel
                {
                    y=480
                },
                new CanvasLineChartModel
                {
                    y=510
                }
            };
            return View(lst);
        }
    }
}


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MKTDotNetCore.RestApiWithNLayer.Feature.MyanmarMonth
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyanmarMonthController : ControllerBase
    {
        private async Task<MyanmarMonth> GetDataAsync()
        {
            string jsonStr =await System.IO.File.ReadAllTextAsync("data2.json");
            var model=JsonConvert.DeserializeObject<MyanmarMonth>(jsonStr);
            return model!;
        }

        [HttpGet("Month")]
        public async Task<IActionResult> Month()
        {
            var model= await GetDataAsync();
            return Ok(model.Tbl_Months.Select(x => x.MonthMm));
        }

        [HttpGet("Detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var model= await GetDataAsync();
            var item = model.Tbl_Months.Where(x => x.Id == id).Select(x => new 
            {
               Description = x.Description,
               Detail=x.Detail
            }).FirstOrDefault();
            return Ok(item);
        }
    }

    public class MyanmarMonth
    {
        public Tbl_Months[] Tbl_Months { get; set; }
    }

    public class Tbl_Months
    {
        public int Id { get; set; }
        public string MonthMm { get; set; }
        public string MonthEn { get; set; }
        public string FestivalMm { get; set; }
        public string FestivalEn { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
    }

}

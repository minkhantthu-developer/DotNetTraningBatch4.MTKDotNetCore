
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MKTDotNetCore.RestApiWithNLayer.Feature.MyanmarProverb
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyanmarProverbController : ControllerBase
    {
        private async Task<Tbl_MyanmarProverb> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("data3.json");
            var model = JsonConvert.DeserializeObject<Tbl_MyanmarProverb>(jsonStr);
            return model;
        }

        [HttpGet]
        public async Task <IActionResult> GetTitles()
        {
            var model=await GetDataAsync();
            return Ok(model.Tbl_MMProverbsTitle);
        }

        [HttpGet("{titlename}")]
        public async Task<IActionResult> GetTitle(string titlename)
        {
            var model= await GetDataAsync();
            var item = model.Tbl_MMProverbsTitle.FirstOrDefault(x=>x.TitleName==titlename);
            if (item is null) return NotFound();
            var lst = model.Tbl_MMProverbs.Where(x => x.TitleId == item.TitleId).Select(x=>new Tbl_MmproverbName
            {
                TitleId=x.TitleId,
                ProverbId=x.ProverbId,
                ProverbName=x.ProverbName
            });
            return Ok(lst);
        }
    }

    public class Tbl_MyanmarProverb
    {
        public Tbl_Mmproverbstitle[] Tbl_MMProverbsTitle { get; set; }
        public Tbl_Mmproverbs[] Tbl_MMProverbs { get; set; }
    }

    public class Tbl_Mmproverbstitle
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
    }

    public class Tbl_Mmproverbs
    {
        public int TitleId { get; set; }
        public int ProverbId { get; set; }
        public string ProverbName { get; set; }
        public string ProverbDesp { get; set; }
    }

    public class Tbl_MmproverbName
    {
        public int TitleId { get; set; }
        public int ProverbId { get; set; }
        public string ProverbName { get; set; }
    }



}

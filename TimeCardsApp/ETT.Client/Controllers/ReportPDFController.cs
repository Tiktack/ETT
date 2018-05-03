using ETT.Web.Models.Record;
using ETT.Web.Models.Report;
using ETT.Web.Util.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ETT.Web.Controllers
{
    public class ReportPDFController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public ReportPDFController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public RedirectResult PdfRecordWriter(IEnumerable<RecordViewModel> records)
        {
            ToPDF pdfwriter = new ToPDF();
            pdfwriter.RecordsToPDF(records);
            return RedirectPermanent("/Record/UserList");
        }
        
        [HttpPost]
        public IActionResult GetExcel(IEnumerable<ReportRecordsViewModel> records)
        {
            string fileName = "Report_" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";
            ToPDF pdfwriter = new ToPDF();
            var excelInst = pdfwriter.GenerateExcel(records);
            return File(excelInst.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        [HttpPost]
        public IActionResult GetPdf(IEnumerable<ReportRecordsViewModel> records)
        {
            string fileName = "Report_" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".pdf";
            ToPDF pdfwriter = new ToPDF();
            var bytes = pdfwriter.GeneratePdf(records).ToArray();
            return File(bytes, "application/pdf", fileName);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using usue_online_tests.Data;
using usue_online_tests.Migrations;

namespace usue_online_tests.Report
{
    public abstract class ReportMaker
    {
        public IReportDataProvider DataProvider { get; }

        protected ReportMaker(IReportDataProvider dataProvider)
        {
            DataProvider = dataProvider;
        }

        public abstract IActionResult CreateReport(int examId);
    }
}

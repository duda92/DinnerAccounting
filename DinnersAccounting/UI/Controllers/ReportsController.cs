using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using DA.Dinners.Domain.Concrete;
using Syncfusion.XlsIO;
using UI.Mailers;
using UI.Models;

namespace UI.Controllers
{
    public class ReportsController : Controller
    {
        //
        // GET: /Reports/

        private List<ReportModel> lst;
        private DateTime startdate, enddate;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GenerateExcel(FormCollection col)
        {
            var contoller = new UI.Controllers.Api.OrdersController(new OrderRepository(), new PersonRepository(), new ProductRepository(), new ContinuousPropositionRepository(), new DayPropositionRepository(), new UserMailer());
            SetPeriod(col["GetReportType"], col);
            lst = contoller.GetOrders(startdate, enddate, User.Identity.Name);
            string filename = string.Format("Report_for_{0}_{1}.xls", User.Identity.Name, col[0]).Replace('\\', '_');
            CreateExcel(lst, filename);
            return File(string.Format("C:\\temp\\{0}", filename), "application/msexcel", filename);
        }

        private void SetPeriod(string filter, FormCollection collection)
        {
            if (filter == GetReportType.LastWeek.ToString())
            {
                DateTime dt = DateTime.Now;
                startdate = dt.AddDays(1 - Convert.ToDouble(dt.DayOfWeek));
                enddate = dt.AddDays(7 - Convert.ToDouble(dt.DayOfWeek));
            }
            else if (filter == GetReportType.LastMonth.ToString())
            {
                startdate = FirstDayOfMonthFromDateTime(DateTime.Now);
                enddate = LastDayOfMonthFromDateTime(DateTime.Now);
            }
            else if (filter == GetReportType.LastYear.ToString())
            {
                startdate = new DateTime(DateTime.Now.Year, 1, 1);
                enddate = new DateTime(DateTime.Now.Year, 12, 31);
            }
            else if (filter == GetReportType.CustomPeriod.ToString())
            {
                startdate = DateTime.Parse(collection["StartDate"]);
                enddate = DateTime.Parse(collection["EndDate"]);
            }
        }

        public DateTime FirstDayOfMonthFromDateTime(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public DateTime LastDayOfMonthFromDateTime(DateTime dateTime)
        {
            var firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }

        private void CreateExcel(List<ReportModel> data, string filename)
        {
            var dt = new DataTable();
            decimal totalamount = 0, totalexpences = 0;
            var engine = new ExcelEngine { ThrowNotSavedOnDestroy = false };
            IApplication eApplicaiton = engine.Excel;
            IWorkbook eWorkbook = eApplicaiton.Workbooks.Create(1);
            IWorksheet eWorksheet = eWorkbook.Worksheets[0];
            IStyle headerStyle = eWorkbook.Styles.Add("HeaderStyle");
            headerStyle.Font.Bold = true;
            if (data.Count > 0)
            {
                dt.Columns.Add("User Name");
                dt.Columns.Add("Date");
                dt.Columns.Add("Order Details");
                dt.Columns.Add("Price");
                dt.Columns.Add("Balance");
                eWorksheet.Range[1, 1].Value = string.Format("Report for {0} in period from {1} to {2}", User.Identity.Name, startdate.Date.ToShortDateString(), enddate.Date.ToShortDateString());
                eWorksheet.Range[1, 1].ColumnWidth = 130;
                eWorksheet.Range[1, 2].ColumnWidth = 10;
                eWorksheet.Range[1, 3].ColumnWidth = 25;
                foreach (ReportModel t in data)
                {
                    dt.Rows.Add(t.UserName, t.DateOfDinner, t.OrderDetails, t.Price, t.UsersAmountOfMoney);
                    totalamount += t.UsersAmountOfMoney;
                    totalexpences += t.Price;
                }

                eWorksheet.Range[dt.Rows.Count + 4, 1].CellStyle.Font.Bold = true;
                eWorksheet.Range[dt.Rows.Count + 4, 3].CellStyle.Font.Bold = true;
                eWorksheet.Range[dt.Rows.Count + 5, 1].CellStyle.Font.Bold = true;
                eWorksheet.Range[dt.Rows.Count + 5, 3].CellStyle.Font.Bold = true;
                eWorksheet.Range[dt.Rows.Count + 4, 1].Value = "Total user Amount of Money:";
                eWorksheet.Range[dt.Rows.Count + 4, 3].Value = totalamount.ToString();
                eWorksheet.Range[dt.Rows.Count + 5, 1].Value = "Total complex expences:";
                eWorksheet.Range[dt.Rows.Count + 5, 3].Value = totalexpences.ToString();
                eWorksheet.ImportDataTable(dt, true, 3, 1);
                eWorksheet.InsertRow(dt.Rows.Count + 4, 1);
                eWorksheet.Columns[0].ColumnWidth = 15;
                eWorksheet.Rows[2].CellStyleName = "HeaderStyle";
                eWorksheet.Rows[0].CellStyleName = "HeaderStyle";
            }
            else
            {
                eWorksheet.Range[1, 1].Value = "нет данных за выбраный период";
            }

            try
            {
                eWorkbook.SaveAs("C:\\temp\\" + filename);
            }
            catch (Exception)
            {
            }
        }
    }
}
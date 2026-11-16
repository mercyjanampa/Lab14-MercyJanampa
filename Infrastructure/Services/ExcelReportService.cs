using ClosedXML.Excel;
using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;

namespace Lab11_MercyJanampa.Infrastructure.Services;

public class ExcelReportService : IExcelReportService
{
    public byte[] GenerateTicketsReport(IEnumerable<Ticket> tickets)
    {
        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("Tickets");

        worksheet.Cell(1, 1).Value = "ID";
        worksheet.Cell(1, 2).Value = "Usuario";
        worksheet.Cell(1, 3).Value = "Título";
        worksheet.Cell(1, 4).Value = "Descripción";
        worksheet.Cell(1, 5).Value = "Estado";
        worksheet.Cell(1, 6).Value = "Fecha creación";
        worksheet.Cell(1, 7).Value = "Fecha cierre";

        var row = 2;

        foreach (var ticket in tickets)
        {
            worksheet.Cell(row, 1).Value = ticket.TicketId.ToString();
            worksheet.Cell(row, 2).Value = ticket.UserId.ToString();
            worksheet.Cell(row, 3).Value = ticket.Title;
            worksheet.Cell(row, 4).Value = ticket.Description ?? "";
            worksheet.Cell(row, 5).Value = ticket.Status;
            worksheet.Cell(row, 6).Value = ticket.CreatedAt;

            if (ticket.ClosedAt.HasValue)
            {
                worksheet.Cell(row, 7).Value = ticket.ClosedAt.Value;
            }

            row++;
        }

        var header = worksheet.Range("A1:G1");

        header.Style.Font.Bold = true;
        header.Style.Fill.BackgroundColor = XLColor.Cyan;
        header.Style.Alignment.Horizontal =
            XLAlignmentHorizontalValues.Center;

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();

        workbook.SaveAs(stream);

        return stream.ToArray();
    }

    public byte[] GenerateResponsesReport(
        IEnumerable<Response> responses)
    {
        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("Respuestas");

        worksheet.Cell(1, 1).Value = "ID";
        worksheet.Cell(1, 2).Value = "Ticket";
        worksheet.Cell(1, 3).Value = "Respondido por";
        worksheet.Cell(1, 4).Value = "Mensaje";
        worksheet.Cell(1, 5).Value = "Fecha";

        var row = 2;

        foreach (var response in responses)
        {
            worksheet.Cell(row, 1).Value =
                response.ResponseId.ToString();

            worksheet.Cell(row, 2).Value =
                response.TicketId.ToString();

            worksheet.Cell(row, 3).Value =
                response.ResponderId.ToString();

            worksheet.Cell(row, 4).Value =
                response.Message;

            worksheet.Cell(row, 5).Value =
                response.CreatedAt;

            row++;
        }

        var header = worksheet.Range("A1:E1");

        header.Style.Font.Bold = true;
        header.Style.Fill.BackgroundColor = XLColor.Cyan;
        header.Style.Alignment.Horizontal =
            XLAlignmentHorizontalValues.Center;

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();

        workbook.SaveAs(stream);

        return stream.ToArray();
    }
}
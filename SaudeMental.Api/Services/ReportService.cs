using iTextSharp.text;
using iTextSharp.text.pdf;
using SaudeMental.Api.Model;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaudeMental.Api.Services
{
    public class ReportService : IReportService
    {
        public ReportService()
        {
        }
        public byte[] GeneratePdfReport(List<FormInfo> formInfos, UserInfo userInfo)
        {
            var html = $@"
            <!DOCTYPE html>
            <html lang=""en"">
                <head>
                    Aplicativo Saude Mental. Emitido em: {DateTime.Now.Day}/${DateTime.Now.Month}/${DateTime.Now.Year}.,
                </head>
                <body>
                    <table style='width: 100%;'>
                        <tr>
                            <th>Data De Criação</th>
                            <th>Depressão</th>
                            <th>Ansiedade</th>
                            <th>Questão 01</th>
                            <th>Questão 02</th>
                            <th>Questão 03</th>
                            <th>Questão 04</th>
                            <th>Questão 05</th>
                            <th>Questão 06</th>
                            <th>Questão 07</th>
                            <th>Questão 08</th>
                            <th>Questão 09</th>
                            <th>Questão 10</th>
                            <th>Questão 11</th>
                            <th>Questão 12</th>
                            <th>Questão 13</th>
                            <th>Questão 14</th>
                        </tr>";
            foreach (var item in formInfos)
            {
                html += $@"
                        <tr>
                            <td>{item.DataDeCriacao.Day.ToString("00")}/{item.DataDeCriacao.Month.ToString("00")}/{item.DataDeCriacao.Year.ToString("00")}</td>
                            <td>{item.DepressionScore}</td>
                            <td>{item.AnxietyScore}</td>
                            <td>{item.Question01}</td>
                            <td>{item.Question02}</td>
                            <td>{item.Question03}</td>
                            <td>{item.Question04}</td>
                            <td>{item.Question05}</td>
                            <td>{item.Question06}</td>
                            <td>{item.Question07}</td>
                            <td>{item.Question08}</td>
                            <td>{item.Question09}</td>
                            <td>{item.Question10}</td>
                            <td>{item.Question11}</td>
                            <td>{item.Question12}</td>
                            <td>{item.Question13}</td>
                            <td>{item.Question14}</td>
                        </tr>
            ";
            }
            html += $@"
                    </table>
                </body>
            </html>";

            byte[] result;

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                var pdfDoc = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 10f);
                PdfWriter.GetInstance(pdfDoc, ms);
                pdfDoc.Open();

                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font = new Font(bf, 9.5f, Font.NORMAL);

                var info = new Paragraph($"Usuário: {formInfos.FirstOrDefault().UserId}  |  Data: {DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}", font);
                var clinicInfo = new Paragraph($"Faz tratamento: { (userInfo.DoTreatment ? "Sim" : "Não") }  |  Diagnósticado com Depressão: {(userInfo.DepressionDiagnosis ? "Sim" : "Não")}  |  Diagnósticado com Ansiedade: {(userInfo.AnxietyDiagnosis ? "Sim" : "Não")}", font);

                pdfDoc.Add(info);
                pdfDoc.Add(clinicInfo);
                pdfDoc.Add(new Paragraph(" "));

                var table = new PdfPTable(17)
                {
                    WidthPercentage = 100f,
                    HeaderRows = 1
                };

                table.AddCell(new Phrase("Data De Criação", font));
                table.AddCell(new Phrase("Depressão", font));
                table.AddCell(new Phrase("Ansiedade", font));
                table.AddCell(new Phrase("Questão 01", font));
                table.AddCell(new Phrase("Questão 02", font));
                table.AddCell(new Phrase("Questão 03", font));
                table.AddCell(new Phrase("Questão 04", font));
                table.AddCell(new Phrase("Questão 05", font));
                table.AddCell(new Phrase("Questão 06", font));
                table.AddCell(new Phrase("Questão 07", font));
                table.AddCell(new Phrase("Questão 08", font));
                table.AddCell(new Phrase("Questão 09", font));
                table.AddCell(new Phrase("Questão 10", font));
                table.AddCell(new Phrase("Questão 11", font));
                table.AddCell(new Phrase("Questão 12", font));
                table.AddCell(new Phrase("Questão 13", font));
                table.AddCell(new Phrase("Questão 14", font));

                foreach (var item in formInfos)
                {
                    table.AddCell(new Phrase($"{item.DataDeCriacao.Day}/{item.DataDeCriacao.Month}/{item.DataDeCriacao.Year}", font));
                    table.AddCell(new Phrase(item.DepressionScore.ToString(), font));
                    table.AddCell(new Phrase(item.AnxietyScore.ToString(), font));
                    table.AddCell(new Phrase(item.Question01.ToString(), font));
                    table.AddCell(new Phrase(item.Question02.ToString(), font));
                    table.AddCell(new Phrase(item.Question03.ToString(), font));
                    table.AddCell(new Phrase(item.Question04.ToString(), font));
                    table.AddCell(new Phrase(item.Question05.ToString(), font));
                    table.AddCell(new Phrase(item.Question06.ToString(), font));
                    table.AddCell(new Phrase(item.Question07.ToString(), font));
                    table.AddCell(new Phrase(item.Question08.ToString(), font));
                    table.AddCell(new Phrase(item.Question09.ToString(), font));
                    table.AddCell(new Phrase(item.Question10.ToString(), font));
                    table.AddCell(new Phrase(item.Question11.ToString(), font));
                    table.AddCell(new Phrase(item.Question12.ToString(), font));
                    table.AddCell(new Phrase(item.Question13.ToString(), font));
                    table.AddCell(new Phrase(item.Question14.ToString(), font));
                };

                pdfDoc.Add(table);
                pdfDoc.Close();
                result = ms.ToArray();
            }
                return result;
        }
    }
}

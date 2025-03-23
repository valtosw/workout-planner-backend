using iTextSharp.text.pdf;
using iTextSharp.text;
using WorkoutPlanner.Models;

namespace WorkoutPlanner.Services
{
    public class PdfService
    {
        public byte[] GenerateWorkoutPlanPdf(WorkoutPlan workoutPlan)
        {
            using MemoryStream stream = new();
            Document document = new(PageSize.A4, 50, 50, 50, 50);
            PdfWriter.GetInstance(document, stream);
            document.Open();

            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 22, BaseColor.Black);
            Font sectionTitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, new BaseColor(44, 62, 80));
            Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.DarkGray);
            Font tableHeaderFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.White);
            BaseColor tableHeaderColor = BaseColor.Black;

            string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "Icons", "icon.svg");

            if (!string.IsNullOrEmpty(logoPath) && File.Exists(logoPath))
            {
                Image logo = Image.GetInstance(logoPath);
                logo.ScaleToFit(120, 120);
                logo.Alignment = Element.ALIGN_CENTER;
                document.Add(logo);
            }

            Paragraph title = new(workoutPlan.Name.ToUpper(), titleFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20
            };
            document.Add(title);

            document.Add(new Paragraph($"Created by: {workoutPlan.CreatedBy.FirstName} {workoutPlan.CreatedBy.LastName}", normalFont));
            if (workoutPlan.AssignedTo is not null)
            {
                document.Add(new Paragraph($"Assigned to: {workoutPlan.AssignedTo.FirstName} {workoutPlan.AssignedTo.LastName}", normalFont));
            }

            document.Add(new Paragraph("\n"));

            Paragraph sectionTitle = new("Workout Plan Details", sectionTitleFont)
            {
                SpacingBefore = 10,
                SpacingAfter = 10
            };
            document.Add(sectionTitle);

            PdfPTable table = new(4) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 3, 3, 2, 2 });

            string[] headers = { "Exercise", "Muscle Group", "Sets x Reps", "Weight (kg)" };
            foreach (string header in headers)
            {
                PdfPCell cell = new(new Phrase(header, tableHeaderFont))
                {
                    BackgroundColor = tableHeaderColor,
                    Padding = 8,
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                table.AddCell(cell);
            }

            foreach (var entry in workoutPlan.WorkoutPlanEntries)
            {
                table.AddCell(new PdfPCell(new Phrase(entry.Exercise.Name, normalFont)) { Padding = 6 });
                table.AddCell(new PdfPCell(new Phrase(entry.Exercise.MuscleGroup.Name, normalFont)) { Padding = 6 });
                table.AddCell(new PdfPCell(new Phrase($"{entry.Sets} x {entry.Reps}", normalFont)) { Padding = 6, HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase($"{entry.Weight} kg", normalFont)) { Padding = 6, HorizontalAlignment = Element.ALIGN_CENTER });
            }

            document.Add(table);

            document.Add(new Paragraph("\n\nStay consistent and keep pushing your limits!", sectionTitleFont) { Alignment = Element.ALIGN_CENTER });

            document.Close();
            return stream.ToArray();
        }
    }
}

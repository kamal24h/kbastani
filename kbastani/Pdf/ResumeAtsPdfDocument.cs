using DataAccess.Vms;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace WebApp.Pdf
{
    
    public class ResumeAtsPdfDocument : IDocument
    {
        private readonly ResumeViewModel _model;

        public ResumeAtsPdfDocument(ResumeViewModel model)
        {
            _model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Size(PageSizes.A4);
                page.PageColor("white");

                page.Content().Column(col =>
                {
                    ComposeHeader(col);
                    ComposeSummary(col);
                    ComposeSkills(col);
                    ComposeExperience(col);
                    ComposeEducation(col);
                    ComposeProjects(col);
                });
            });
        }

        void ComposeHeader(ColumnDescriptor col)
        {
            col.Item().Text(_model.FullNameEn)
                .FontSize(22).SemiBold();

            col.Item().Text(_model.JobTitleEn)
                .FontSize(12).FontColor("#444444");

            col.Item().Text($"{_model.Email} | {_model.Phone} | {_model.Location}")
                .FontSize(10).FontColor("#555555");
        }

        void ComposeSummary(ColumnDescriptor col)
        {
            col.Item().PaddingTop(15).Text("Professional Summary")
                .FontSize(14).SemiBold();

            col.Item().Text(_model.BioEn)
                .FontSize(10).LineHeight(1.4f);
        }

        void ComposeSkills(ColumnDescriptor col)
        {
            col.Item().PaddingTop(15).Text("Skills")
                .FontSize(14).SemiBold();

            var skills = string.Join(", ", _model.Skills.Select(s => s.NameEn));

            col.Item().Text(skills)
                .FontSize(10).LineHeight(1.4f);
        }

        void ComposeExperience(ColumnDescriptor col)
        {
            col.Item().PaddingTop(15).Text("Experience")
                .FontSize(14).SemiBold();

            foreach (var e in _model.Experiences.OrderByDescending(x => x.StartDate))
            {
                col.Item().Text($"{e.JobTitleEn} — {e.CompanyEn}")
                    .FontSize(11).SemiBold();

                col.Item().Text($"{e.StartDate:yyyy} - {e.EndDate?.ToString("yyyy") ?? "Present"}")
                    .FontSize(10).FontColor("#555555");

                col.Item().Text(e.DescriptionEn)
                    .FontSize(10).LineHeight(1.4f);

                col.Item().PaddingBottom(10);
            }
        }

        void ComposeEducation(ColumnDescriptor col)
        {
            col.Item().PaddingTop(15).Text("Education")
                .FontSize(14).SemiBold();

            foreach (var e in _model.Educations.OrderByDescending(x => x.StartDate))
            {
                col.Item().Text($"{e.DegreeEn} — {e.UniversityEn}")
                    .FontSize(11).SemiBold();

                col.Item().Text($"{e.StartDate:yyyy} - {e.EndDate:yyyy}")
                    .FontSize(10).FontColor("#555555");

                col.Item().Text(e.DescriptionEn)
                    .FontSize(10).LineHeight(1.4f);

                col.Item().PaddingBottom(10);
            }
        }

        void ComposeProjects(ColumnDescriptor col)
        {
            col.Item().PaddingTop(15).Text("Projects")
                .FontSize(14).SemiBold();

            foreach (var p in _model.Projects.OrderByDescending(x => x.CreatedAt))
            {
                col.Item().Text(p.TitleEn)
                    .FontSize(11).SemiBold();

                col.Item().Text(p.DescriptionEn)
                    .FontSize(10).LineHeight(1.4f);

                if (!string.IsNullOrEmpty(p.GithubUrl))
                    col.Item().Text($"GitHub: {p.GithubUrl}").FontSize(10);

                if (!string.IsNullOrEmpty(p.ProjectUrl))
                    col.Item().Text($"Demo: {p.ProjectUrl}").FontSize(10);

                col.Item().PaddingBottom(10);
            }
        }
    }

}

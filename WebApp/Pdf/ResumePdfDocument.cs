using DataAccess.Vms;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace WebApp.Pdf
{
    public class ResumePdfDocument : IDocument
    {
        private readonly ResumeViewModel _model;
        private readonly string _culture;

        public ResumePdfDocument(ResumeViewModel model, string culture)
        {
            _model = model;
            _culture = culture;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
            });
        }

        void ComposeHeader(IContainer container)
        {
            var name = _culture == "fa" ? _model.FullNameFa : _model.FullNameEn;
            var title = _culture == "fa" ? _model.JobTitleFa : _model.JobTitleEn;
            var bio = _culture == "fa" ? _model.BioFa : _model.BioEn;

            container.Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text(name).FontSize(26).SemiBold();
                    col.Item().Text(title).FontSize(14).FontColor(Colors.Blue.Medium);
                    col.Item().Text(bio).FontSize(10).FontColor(Colors.Grey.Darken2).LineHeight(1.4f);
                });
            });
        }

        void ComposeContent(IContainer container)
        {
            container.Column(col =>
            {
                // Skills
                col.Item().PaddingTop(15).Element(c =>
                {
                    c.Text(_culture == "fa" ? "مهارت‌ها" : "Skills")
                     .FontSize(16).SemiBold();
                });

                col.Item().Element(ComposeSkills);

                // Experience
                col.Item().PaddingTop(15).Element(c =>
                {
                    c.Text(_culture == "fa" ? "سوابق کاری" : "Experience")
                     .FontSize(16).SemiBold();
                });

                col.Item().Element(ComposeExperience);

                // Education
                col.Item().PaddingTop(15).Element(c =>
                {
                    c.Text(_culture == "fa" ? "سوابق تحصیلی" : "Education")
                     .FontSize(16).SemiBold();
                });

                col.Item().Element(ComposeEducation);

                // Projects (خلاصه)
                col.Item().PaddingTop(15).Element(c =>
                {
                    c.Text(_culture == "fa" ? "پروژه‌ها" : "Projects")
                     .FontSize(16).SemiBold();
                });

                col.Item().Element(ComposeProjects);
            });
        }

        void ComposeSkills(IContainer container)
        {
            container.Column(col =>
            {
                foreach (var s in _model.Skills.OrderByDescending(x => x.Level))
                {
                    var name = _culture == "fa" ? s.NameFa : s.NameEn;
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Text($"{name}").FontSize(10);
                        row.ConstantItem(50)
                           .AlignRight()
                           .Text($"{s.Level}%").FontSize(10).FontColor(Colors.Grey.Darken2);
                    });
                }
            });
        }

        void ComposeExperience(IContainer container)
        {
            container.Column(col =>
            {
                foreach (var e in _model.Experiences.OrderByDescending(x => x.StartDate))
                {
                    var jobTitle = _culture == "fa" ? e.JobTitleFa : e.JobTitleEn;
                    var company = _culture == "fa" ? e.CompanyFa : e.CompanyEn;
                    var desc = _culture == "fa" ? e.DescriptionFa : e.DescriptionEn;
                    var period = e.StartDate.Year + " - " +
                                 (e.EndDate?.Year.ToString() ?? (_culture == "fa" ? "اکنون" : "Present"));

                    col.Item().PaddingBottom(5).Element(c =>
                    {
                        c.Text($"{jobTitle} - {company}").FontSize(11).SemiBold();
                    });
                    col.Item().Text(period).FontSize(9).FontColor(Colors.Grey.Darken2);
                    col.Item().Text(desc).FontSize(9).FontColor(Colors.Grey.Darken3).LineHeight(1.3f);
                    col.Item().PaddingBottom(5);
                }
            });
        }

        void ComposeEducation(IContainer container)
        {
            container.Column(col =>
            {
                foreach (var e in _model.Educations.OrderByDescending(x => x.StartDate))
                {
                    var degree = _culture == "fa" ? e.DegreeFa : e.DegreeEn;
                    var uni = _culture == "fa" ? e.UniversityFa : e.UniversityEn;
                    var desc = _culture == "fa" ? e.DescriptionFa : e.DescriptionEn;
                    var period = e.StartDate.Year + " - " + e.EndDate?.Year;

                    col.Item().PaddingBottom(5).Element(c =>
                    {
                        c.Text($"{degree} - {uni}").FontSize(11).SemiBold();
                    });
                    col.Item().Text(period).FontSize(9).FontColor(Colors.Grey.Darken2);
                    col.Item().Text(desc).FontSize(9).FontColor(Colors.Grey.Darken3).LineHeight(1.3f);
                    col.Item().PaddingBottom(5);
                }
            });
        }

        void ComposeProjects(IContainer container)
        {
            container.Column(col =>
            {
                foreach (var p in _model.Projects.OrderByDescending(x => x.CreatedAt).Take(5))
                {
                    var title = _culture == "fa" ? p.TitleFa : p.TitleEn;
                    var desc = _culture == "fa" ? p.DescriptionFa : p.DescriptionEn;

                    col.Item().Text(title).FontSize(11).SemiBold();
                    col.Item().Text(desc).FontSize(9).FontColor(Colors.Grey.Darken3).LineHeight(1.3f);
                    col.Item().PaddingBottom(5);
                }
            });
        }
    }

}

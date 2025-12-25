/YourProject
│
├── AppDbContext.cs
├── Program.cs
├── appsettings.json
│
├── Models
│   ├── BlogPost.cs
│   ├── BlogCategory.cs
│   ├── Tag.cs
│   ├── BlogPostTag.cs
│   ├── Comment.cs
│   ├── Skill.cs
│   ├── Project.cs
│   ├── Experience.cs
│   ├── Education.cs
│   ├── About.cs
│   └── ContactMessage.cs
│
├── ViewModels
│   ├── BlogPostViewModel.cs
│   ├── ProjectViewModel.cs
│   ├── AboutViewModel.cs
│   ├── ContactFormViewModel.cs
│   └── ResumeViewModel.cs
│
├── Services
│   └── EmailService.cs
│
├── Pdf
│   ├── ResumePdfDocument.cs
│   └── ResumeAtsPdfDocument.cs
│
├── Resources
│   ├── Admin.fa.resx
│   ├── Admin.en.resx
│   ├── Shared.fa.resx
│   └── Shared.en.resx
│
├── Controllers
│   ├── BlogController.cs
│   ├── ResumeController.cs
│   ├── AboutController.cs
│   └── ContactController.cs
│
├── Areas
│   └── Admin
│       ├── Controllers
│       │   ├── DashboardController.cs
│       │   ├── BlogController.cs
│       │   ├── CategoryController.cs
│       │   ├── TagController.cs
│       │   ├── CommentController.cs
│       │   ├── SkillController.cs
│       │   ├── ProjectController.cs
│       │   ├── ExperienceController.cs
│       │   ├── EducationController.cs
│       │   └── AboutController.cs
│       │
│       └── Views
│           ├── Shared
│           │   └── _AdminLayout.cshtml
│           │
│           ├── Dashboard
│           │   └── Index.cshtml
│           │
│           ├── Blog
│           │   ├── Index.cshtml
│           │   ├── Create.cshtml
│           │   └── Edit.cshtml
│           │
│           ├── Category
│           │   ├── Index.cshtml
│           │   ├── Create.cshtml
│           │   └── Edit.cshtml
│           │
│           ├── Tag
│           │   ├── Index.cshtml
│           │   ├── Create.cshtml
│           │   └── Edit.cshtml
│           │
│           ├── Comment
│           │   └── Index.cshtml
│           │
│           ├── Skill
│           │   ├── Index.cshtml
│           │   ├── Create.cshtml
│           │   └── Edit.cshtml
│           │
│           ├── Project
│           │   ├── Index.cshtml
│           │   ├── Create.cshtml
│           │   └── Edit.cshtml
│           │
│           ├── Experience
│           │   ├── Index.cshtml
│           │   ├── Create.cshtml
│           │   └── Edit.cshtml
│           │
│           ├── Education
│           │   ├── Index.cshtml
│           │   ├── Create.cshtml
│           │   └── Edit.cshtml
│           │
│           └── About
│               ├── Index.cshtml
│               └── Edit.cshtml
│
├── Views
│   ├── Shared
│   │   └── _Layout.cshtml
│   │
│   ├── Blog
│   │   ├── Index.cshtml
│   │   ├── Details.cshtml
│   │   └── Search.cshtml
│   │
│   ├── Resume
│   │   └── Index.cshtml
│   │
│   ├── About
│   │   └── Index.cshtml
│   │
│   └── Contact
│       └── Index.cshtml
│
└── wwwroot
    ├── css
    ├── js
    ├── uploads
    │   ├── blog
    │   ├── projects
    │   └── profile
    └── images

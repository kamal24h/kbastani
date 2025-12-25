using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Vms
{
    public class HomeViewModel
    {
        public About? About { get; set; }
        public List<BlogPost> LatestPosts { get; set; } = new();
        public List<Project> LatestProjects { get; set; } = new();
    }

}

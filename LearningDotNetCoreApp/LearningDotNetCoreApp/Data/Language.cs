using System.Collections.Generic;

namespace LearningDotNetCoreApp.Data
{
    public class Language
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

       public ICollection<Books> books { get; set; }
    }
}

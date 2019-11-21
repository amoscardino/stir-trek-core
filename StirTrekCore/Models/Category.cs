using System.Collections.Generic;

namespace StirTrekCore.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public List<CategoryItem> Items { get; set; }
        public long Sort { get; set; }
    }
}

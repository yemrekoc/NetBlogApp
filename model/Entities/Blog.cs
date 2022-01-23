using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Blog : BaseModel
    {
        public string Title { get; set; }
        public string BlogText { get; set; }
        public string comImage { get; set; }
        public string comSentence { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}

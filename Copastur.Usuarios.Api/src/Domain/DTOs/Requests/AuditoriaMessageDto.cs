using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class AuditoriaMessageDto
    {
        public string Content { get; set; }
        public string Method { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}

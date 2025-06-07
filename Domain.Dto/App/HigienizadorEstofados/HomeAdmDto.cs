using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.App.HigienizadorEstofados
{
    public class HomeAdmDto
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string ServiceType { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
    }
}

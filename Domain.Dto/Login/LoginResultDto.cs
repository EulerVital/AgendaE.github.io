using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.Login
{
    public class LoginResultDto
    {
        public string Token { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.Common.Enum.Cadastro
{
    public enum UsuarioStatusEnum
    {
        [Description("Validar Celular")]
        ValidarCelular = 0,

        [Description("Validar E-mail")]
        ValidarEmail = 1,

        [Description("Ativo")]
        Ativo = 2,

        [Description("Inativo")]
        Inativo = 3,

        [Description("Pendência de Pagemento")]
        PendenciaPagamento = 4
    }
}

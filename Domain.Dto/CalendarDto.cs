using AgendaE.Common.Enum.App.Salao;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class CalendarDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [MaxLength(200, ErrorMessage = "Campo nome aceita até 200 caracteres")]        
        public string Nome { get; set; }

        [Required(ErrorMessage = "Celular é obrigatório")]
        [MaxLength(20, ErrorMessage = "Campo celular aceita até 20 caracteres")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Data/Hora do agendamento é obrigatória")]
        public DateTime DataHoraAgendamento { get; set; }

        public static implicit operator CalendarModel(CalendarDto calendarDto)
        {
            return new CalendarModel
            {
                Celular = calendarDto.Celular,
                Nome = calendarDto.Nome,
                Data = calendarDto.DataHoraAgendamento,
                DataCriacao = DateTime.Now,
                Status = StatusAgendaSalaoEnum.SolicitacaoAgendamentoNegadaData.GetHashCode()
            };
        }
    }
}

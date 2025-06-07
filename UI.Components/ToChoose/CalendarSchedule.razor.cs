using Microsoft.AspNetCore.Components;
using UI.Components.Helper;

namespace UI.Components.ToChoose
{
    public class CalendarScheduleBase : BaseComponent
    {
        public DateTime? Data { get; set; }
        public string? Celular { get; set; }
        public string? Nome { get; set; }

        protected override void OnInitialized()
        {
            Data = DateTime.Now.AddDays(1);
        }

        public void Avancar()
        {
            Navigation.NavigateTo("escolher-itens");
        }
    }
}

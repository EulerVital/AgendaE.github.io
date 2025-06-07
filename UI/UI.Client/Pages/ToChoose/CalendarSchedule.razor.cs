using Microsoft.AspNetCore.Components;
using UI.Client.Helper;

namespace UI.Client.Pages.ToChoose
{
    public class CalendarScheduleBase : BaseComponent
    {
        public DateTime? SelectedDate { get; set; }

        protected override void OnInitialized()
        {
            SelectedDate = DateTime.Now.AddDays(1);
        }

        public void Avancar()
        {
            Navigation.NavigateTo("escolher-itens");
        }
    }
}

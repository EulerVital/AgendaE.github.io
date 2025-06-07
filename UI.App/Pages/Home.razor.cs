using Microsoft.AspNetCore.Components;
using UI.Components.Helper;

namespace UI.App.Pages
{
    public class HomeBase : BaseComponent
    {
        public void Agendar()
        {
            Navigation.NavigateTo("agendar");
        }
    }
}

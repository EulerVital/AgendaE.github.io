using Microsoft.AspNetCore.Components;

namespace UI.Client.Helper
{
    public abstract class BaseComponent : ComponentBase
    {
        [Inject]
        public NavigationManager Navigation { get; set; }

        public virtual void Voltar(string url)
        {
            Navigation.NavigateTo(url, true);
        }
    }
}

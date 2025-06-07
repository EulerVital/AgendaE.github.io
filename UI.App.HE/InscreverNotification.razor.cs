using UI.Components.Helper;

namespace UI.App.HE 
{ 

    public class InscreverNotificationBase : BaseComponent
    {
        public bool IsMostrarBotao { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var isToken = await LocalStorage.ContainsKeyAsync("key-cloud");

                IsMostrarBotao = !isToken;
                await InvokeAsync(StateHasChanged);
            }
        }

        public async Task Subscribe()
        {
            var token = await _js.InvokeAsync<string>("registerForPush", ["BNZshPZGuwzWXpoGNGUlffirYgGqYLd_LoZv0RXCEf92SKiVN3noXr75g6AVTasXVSGgOORLZAK2lmSoN-5jO_k"]);
            await LocalStorage.SetItemAsync("key-cloud", token);
            await InvokeAsync(StateHasChanged);
        }
    }
}

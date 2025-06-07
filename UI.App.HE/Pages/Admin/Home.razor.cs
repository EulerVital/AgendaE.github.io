using AgendaE.Common.Enum.App.HigienizadorEstofados;
using AgendaE.CoreClient.Base;
using AgendaE.CoreClient.Contract.HigienizadorEstofados;
using Domain.Dto.App.HigienizadorEstofados;
using Domain.Dto.Login;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using UI.Components.Helper;

namespace UI.App.HE.Pages.Admin
{
    public class HomeBase : BaseComponent
    {
        [Inject]
        public CustomAuthStateProviderClient Auth { get; set; }

        [Inject]
        public IHomeAdmCoreClient CoreService { get; set; }

        public List<SolicitacaoOrcamentoHEDto>? LatestRequests { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var token = await LocalStorage.GetItemAsync<LoginResultDto>("authToken");

                if (token == null)
                    return;

                LatestRequests = await CoreService.GetLatestAsync(5, token.Id);
                await InvokeAsync(StateHasChanged);
            }
        }

        public void GoToNewRequests()
        {
            Navigation.NavigateTo("/admin/requests?filter=new");
        }

        public void ViewRequest(int id)
        {
            Navigation.NavigateTo($"/admin/requests/{id}");
        }

        public Color GetStatusColor(StatusOrcamentoHEEnum status) => status switch
        {
            StatusOrcamentoHEEnum.Pendente => Color.Warning,
            StatusOrcamentoHEEnum.Aprovado => Color.Success,
            StatusOrcamentoHEEnum.Cancelado => Color.Error,
            StatusOrcamentoHEEnum.Recusado => Color.Error,
            StatusOrcamentoHEEnum.EmAnalise => Color.Info,
            _ => Color.Default
        };
    }
}

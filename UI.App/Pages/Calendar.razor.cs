using MudBlazor;
using UI.Components.Helper;

namespace UI.App.Pages
{
    public class CalendarBase : BaseComponent
    {
        public DateTime? Data { get; set; }
        public string? Celular { get; set; }
        public string? TextoOpcional { get; set; }
        public string? Nome { get; set; }
        public string? HoraSelecionada { get; set; }
        public int ControlaTela { get; set; } = 1;

        private bool disabledBotaoAvancar;
        public bool DisabledBotaoAvancar { get { return disabledBotaoAvancar = string.IsNullOrEmpty(Nome) && string.IsNullOrEmpty(Celular); } set { disabledBotaoAvancar = value; } }

        public void AvancarEscolhaDataHora()
        {
            ControlaTela = 2;
        }

        public void FinalizarAgendamento()
        {
            Navigation.NavigateTo("escolher-itens");
        }

        public void Voltar()
        {
            ControlaTela = 1;
            Nome = string.Empty;
            Celular = string.Empty;
        }

        public void EscolheHorario(string horaSelecionada)
        {
            HoraSelecionada = horaSelecionada;
        }

        public Color CorHorario()
        {
            return Color.Success;
        }

        public async Task OnChangeData(DateTime? dateTime)
        {
            Data = dateTime;
            HoraSelecionada = string.Empty;

            await Task.Delay(1);
        }
    }
}

using AgendaE.Common.Enum.App.HigienizadorEstofados;
using Domain.Dto.App.HigienizadorEstofados;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using AgendaE.Common.Extension;
using Microsoft.AspNetCore.Components.Forms;
using Domain.Dto;
using UI.Components.Helper;
using Domain.Entity.App.HigienizadorEstofados;
using Domain.Dto.Usuario;


namespace UI.App.HE.Pages
{
    public class SolicitarOrcamentoBase : BaseComponent
    {
        [Parameter]
        public string codid { get; set; }

        private const string KEY_LOCAL_STORAGE = "HigienizadorEstofados";

        public SolicitacaoOrcamentoHEInsertDto? Orcamento { get; set; }
        public ItemHigienizacaoHEInsertDto Itens { get; set; } = new ItemHigienizacaoHEInsertDto();
        public Dictionary<Guid, string>? SelectedFile { get; set; }
        public UsuarioDto? Usuario { get; set; }

        public int TipoItem { get; set; }
        public int Tamanho { get; set; }

        public Dictionary<int, string> TiposItens { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> TamanhoItens { get; set; } = new Dictionary<int, string>();

        #region Atrbutos Tela controles

        public int PanelIndex { get; set; } = 0;
        public bool ProgressUploadImagens { get; set; }

        public bool DisabledPanel02 { get; set; } = true;
        public bool DisabledPanel03 { get; set; } = true;

        public bool DisabledCep { get; set; } = true;
        public bool DisabledCampoCep { get; set; } = false;

        public bool BtnVisibleFinaizarImagens { get { return !AtivaBotaoEnviarImagens() && Orcamento != null && Orcamento.Imagens != null && Orcamento.Imagens.Count > 0; } }
        public bool BtnVisibleUploadImagem { get { return !AtivaBotaoEnviarImagens(); } }
        public bool BtnVisibleEnviarImagem { get { return AtivaBotaoEnviarImagens(); } }

        #endregion

        public string BaseEndPoint { get; set; } = "higienizadorestofados/orcamento";

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                TiposItens = EnumDescriptonExension.ToDictionary<TipoItemHEEnum>();
                TamanhoItens = EnumDescriptonExension.ToDictionary<TamanhoItemHEEnum>();

                await CarregarDados();
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            Usuario = await CarregarUsuarioResponsavel(codid);
            if (Usuario == null)
                Navigation.NavigateTo("not-found");
        }

        public async Task AdicionarItem()
        {
            if (Orcamento == null)
                return;

            if (Orcamento.ItensHigianizacao == null)
                Orcamento.ItensHigianizacao = new List<ItemHigienizacaoHEInsertDto>();

            Itens.Tamanho = Enum.Parse<TamanhoItemHEEnum>(Tamanho.ToString());
            Itens.TipoItem = Enum.Parse<TipoItemHEEnum>(TipoItem.ToString());

            Tamanho = 0;
            TipoItem = 0;

            Orcamento.ItensHigianizacao.Add(Itens);

            await LocalStorage.SetItemAsync(KEY_LOCAL_STORAGE, Orcamento);

            Itens = new ItemHigienizacaoHEInsertDto();
        }

        public async Task RemoverItem(ItemHigienizacaoHEInsertDto item)
        {
            if (Orcamento == null)
                return;

            if (Orcamento.ItensHigianizacao == null)
                return;

            Orcamento.ItensHigianizacao.Remove(item);

            await LocalStorage.SetItemAsync(KEY_LOCAL_STORAGE, Orcamento);
        }

        public async Task RemoverImagem(Guid key)
        {
            try
            {
                ProgressUploadImagens = true;

                if (Orcamento == null)
                    return;

                if (Orcamento.Imagens == null)
                    return;

                if (SelectedFile != null)
                {
                    var image = SelectedFile.FirstOrDefault(w => w.Key == key);

                    if (Orcamento.Imagens.Exists(e => e.CaminhoFoto == image.Value))
                        Orcamento.Imagens.RemoveAll(e => e.CaminhoFoto == image.Value);

                    if (image.Value.Contains("https://"))
                    {
                        var response = await Http.PostAsJsonAsync("UploadImagem/Delete", image.Value);
                    }

                    SelectedFile.Remove(key);

                    await LocalStorage.SetItemAsync(KEY_LOCAL_STORAGE, Orcamento);

                    Snackbar.Add("Imagem excluida !!", Severity.Success);
                }
            }
            catch
            {
                Snackbar.Add("Falha ao excluir Imagem !!", Severity.Error);
                throw;
            }
            finally
            {
                ProgressUploadImagens = false;
            }
        }

        public async Task AvancarPasso2()
        {
            try
            {
                await LocalStorage.SetItemAsync(KEY_LOCAL_STORAGE, Orcamento);
                Snackbar.Add("Dados de contatos gravados!", Severity.Success);
                AtivarPainel(1);
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Erro: {ex.Message}", Severity.Error);
            }
        }

        public async Task AvancarPasso3()
        {
            try
            {
                await LocalStorage.SetItemAsync(KEY_LOCAL_STORAGE, Orcamento);
                Snackbar.Add("Itens de higienização gravados!", Severity.Success);
                AtivarPainel(2);
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Erro: {ex.Message}", Severity.Error);
            }
        }

        public async Task EnviarOrcamento()
        {
            try
            {
                var response = await Http.PostAsJsonAsync(BaseEndPoint, Orcamento);

                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Orçamento solicitado com sucesso!", Severity.Success);
                }
                else
                {
                    Snackbar.Add("Erro ao enviar orçamento", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Erro: {ex.Message}", Severity.Error);
            }
        }

        public async Task EnviarImagens()
        {
            try
            {
                if (SelectedFile == null)
                {
                    Snackbar.Add("Você precisa anexar ao menos uma imagem !", Severity.Info);
                    return;
                }

                ProgressUploadImagens = true;

                foreach (var item in SelectedFile)
                {
                    if (item.Value.Contains("https://"))
                        continue;

                    var response = await Http.PostAsJsonAsync("UploadImagem", item.Value);

                    if (!response.IsSuccessStatusCode)
                    {
                        Snackbar.Add("Falha ao transmitir imagem", Severity.Success);
                    }

                    var result = await response.Content.ReadFromJsonAsync<UploadResult>();
                    
                    if(result != null && Orcamento != null)
                    {
                        Orcamento.Imagens = Orcamento.Imagens ?? new List<FotosOrcamentoHEInsertDto>();
                        Orcamento.Imagens.Add(new FotosOrcamentoHEInsertDto()
                        {
                            CaminhoFoto = result.Url
                        });
                    }
                }

                ProgressUploadImagens = false;
                Snackbar.Add("Imagen(s) enviadas com sucesso!", Severity.Success);
                LerImagensToConvert();
                await LocalStorage.SetItemAsync(KEY_LOCAL_STORAGE, Orcamento);
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Erro: {ex.Message}", Severity.Error);
            }
        }

        public async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            var list = e.GetMultipleFiles();
            SelectedFile = SelectedFile ?? new Dictionary<Guid, string>();

            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    string imageBase = await LoadImagem(item);
                    if (!string.IsNullOrEmpty(imageBase))
                        SelectedFile.Add(Guid.NewGuid(), imageBase);
                }
            }
        }

        public async Task BuscarCEP()
        {
            // Remove caracteres não numéricos
            var cep = new string(Orcamento.CEP.Where(char.IsDigit).ToArray());

            // Verifica se o CEP está completo (8 dígitos)
            if (cep.Length == 8)
            {
                DisabledCampoCep = true;

                try
                {
                    // Faz a requisição para a API ViaCEP
                    var httpClient = new HttpClient();
                    var response = await httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

                    if (response.IsSuccessStatusCode)
                    {
                        var conteudo = await response.Content.ReadFromJsonAsync<ViaCEPResponseDto>();

                        if (!conteudo.erro)
                        {
                            // Preenche os campos com os dados retornados
                            Orcamento.Logradouro = conteudo.logradouro;
                            Orcamento.Bairro = conteudo.bairro;
                            Orcamento.Cidade = conteudo.localidade;
                            Orcamento.Estado = conteudo.uf;

                            // Desbloqueia os campos
                            DisabledCep = false;
                        }
                        else
                        {
                            // CEP não encontrado
                            await DialogService.ShowMessageBox("Atenção", "CEP não encontrado. Por favor, verifique o número digitado.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao buscar CEP: {ex.Message}");
                    await DialogService.ShowMessageBox("Erro", "Não foi possível consultar o CEP. Por favor, tente novamente mais tarde.");
                }
                finally
                {
                    DisabledCampoCep = false;
                    StateHasChanged();
                }
            }
        }

        #region Metodos privados

        private void AtivarPainel(int panel)
        {
            if (panel == 1)
                DisabledPanel02 = false;
            else if(panel == 2)
                DisabledPanel03 = false;

            PanelIndex = panel;
            StateHasChanged();
        }

        private void LerImagensToConvert()
        {
            if (!(Orcamento != null && Orcamento.Imagens != null && Orcamento.Imagens.Count > 0))
                return;

            SelectedFile = new Dictionary<Guid, string>();

            foreach (var item in Orcamento.Imagens)
                SelectedFile.Add(Guid.NewGuid(), item.CaminhoFoto);
        }

        private bool AtivaBotaoEnviarImagens()
        {
            if (SelectedFile == null)
                return false;

            return SelectedFile.Where(w=>w.Value.Contains("base64")).Any();
        }

        private async Task CarregarDados()
        {
            Orcamento = await LocalStorage.GetItemAsync<SolicitacaoOrcamentoHEInsertDto>(KEY_LOCAL_STORAGE) ?? new SolicitacaoOrcamentoHEInsertDto();
            DisabledPanel02 = !(Orcamento.ItensHigianizacao != null && Orcamento.ItensHigianizacao.Count > 0);
            DisabledPanel03 = !(Orcamento.Imagens != null && Orcamento.Imagens.Count > 0);

            DisabledCep = !(!string.IsNullOrEmpty(Orcamento.CEP) && !string.IsNullOrEmpty(Orcamento.Logradouro));

            LerImagensToConvert();
            AtivarPainel(0);

            await InvokeAsync(StateHasChanged);
        }

        public async Task FinalizarSolicitacaoOrcamento()
        {
            ProgressUploadImagens = true;

            try
            {
                if (Usuario == null || Orcamento == null)
                    return;

                Orcamento.UsuarioId = Usuario.Id;

                var response = await Http.PostAsJsonAsync("HigienizadorEstofados/Orcamento", Orcamento);

                if (response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    var objSolicitacao = await response.Content.ReadFromJsonAsync<SolicitacaoOrcamentoHE>();
                    if (objSolicitacao == null)
                        throw new Exception("Erro na requisição de solitação e orçamento");

                    Snackbar.Add("Solicitação enviada com sucesso, entraremos em contato o mai breve possível", Severity.Success);
                    await LocalStorage.RemoveItemAsync(KEY_LOCAL_STORAGE);
                    await CarregarDados();
                }   
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ProgressUploadImagens = false;

            }
        }

        #endregion
    }
}

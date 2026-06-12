using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FinScan.App.Services;

namespace FinScan.App.ViewModels
{
    /// <summary>
    /// Responsabilidade única: orquestrar a tela de Custo de Oportunidade.
    /// Não contém lógica financeira — delega para ISimulacaoInvestimentoService.
    /// </summary>
    public class CustoOportunidadeViewModel : INotifyPropertyChanged
    {
        private readonly ISimulacaoInvestimentoService _simulacaoService;

        // ── Backing fields ──────────────────────────────────────────────────

        private string _valorInicialText  = string.Empty;
        private string _aporteMensalText  = string.Empty;
        private int    _tempoMeses        = 12;
        private bool   _isLoading;
        private bool   _resultadoVisivel;
        private bool   _temErro;
        private string _mensagemErro      = string.Empty;

        private decimal _valorTotalInvestido;
        private decimal _rendimentoBruto;
        private decimal _valorTotalFinal;
        private decimal _taxaSelicUtilizada;

        // ── Construtor ──────────────────────────────────────────────────────

        public CustoOportunidadeViewModel(ISimulacaoInvestimentoService simulacaoService)
        {
            _simulacaoService = simulacaoService;

            // CanExecute impede disparo duplo durante o carregamento
            SimularCommand = new Command(
                execute:    async () => await ExecutarSimulacaoAsync(),
                canExecute: ()    => !IsLoading
            );
        }

        // ── Propriedades de Entrada ─────────────────────────────────────────

        public string ValorInicialText
        {
            get => _valorInicialText;
            set => SetProperty(ref _valorInicialText, value);
        }

        public string AporteMensalText
        {
            get => _aporteMensalText;
            set => SetProperty(ref _aporteMensalText, value);
        }

        public int TempoMeses
        {
            get => _tempoMeses;
            set => SetProperty(ref _tempoMeses, value);
        }

        // ── Propriedades de Estado de UI ────────────────────────────────────

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                SetProperty(ref _isLoading, value);
                // Notifica o Command para reavaliação do CanExecute
                (SimularCommand as Command)?.ChangeCanExecute();
            }
        }

        public bool ResultadoVisivel
        {
            get => _resultadoVisivel;
            set => SetProperty(ref _resultadoVisivel, value);
        }

        public bool TemErro
        {
            get => _temErro;
            set => SetProperty(ref _temErro, value);
        }

        public string MensagemErro
        {
            get => _mensagemErro;
            set => SetProperty(ref _mensagemErro, value);
        }

        // ── Propriedades de Resultado ───────────────────────────────────────

        public decimal ValorTotalInvestido
        {
            get => _valorTotalInvestido;
            set => SetProperty(ref _valorTotalInvestido, value);
        }

        public decimal RendimentoBruto
        {
            get => _rendimentoBruto;
            set => SetProperty(ref _rendimentoBruto, value);
        }

        public decimal ValorTotalFinal
        {
            get => _valorTotalFinal;
            set => SetProperty(ref _valorTotalFinal, value);
        }

        public decimal TaxaSelicUtilizada
        {
            get => _taxaSelicUtilizada;
            set => SetProperty(ref _taxaSelicUtilizada, value);
        }

        // ── Command ─────────────────────────────────────────────────────────

        public ICommand SimularCommand { get; }

        // ── Métodos Privados ─────────────────────────────────────────────────

        private async Task ExecutarSimulacaoAsync()
        {
            if (!TentarParsearEntradas(out var request))
                return;

            IsLoading        = true;
            TemErro          = false;
            ResultadoVisivel = false;

            try
            {
                var resultado = await _simulacaoService.CalcularAsync(request);
                AplicarResultado(resultado);
            }
            catch (Exception)
            {
                ExibirErro("Não foi possível realizar a simulação. Tente novamente.");
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// Valida e converte as entradas de texto em um SimulacaoRequest.
        /// Separado do Execute para manter o método de simulação limpo (SRP interno).
        /// </summary>
        private bool TentarParsearEntradas(out SimulacaoRequest request)
        {
            request = null!;
            var cultura = System.Globalization.CultureInfo.GetCultureInfo("pt-BR");
            var estilos = System.Globalization.NumberStyles.Any;

            if (!decimal.TryParse(ValorInicialText, estilos, cultura, out var valorInicial) 
                || valorInicial < 0)
            {
                ExibirErro("Informe um valor inicial válido.");
                return false;
            }

            if (!decimal.TryParse(AporteMensalText, estilos, cultura, out var aporteMensal) 
                || aporteMensal < 0)
            {
                ExibirErro("Informe um aporte mensal válido.");
                return false;
            }

            request = new SimulacaoRequest
            {
                ValorInicial  = valorInicial,
                AporteMensal  = aporteMensal,
                TempoMeses    = TempoMeses
            };

            return true;
        }

        /// <summary>
        /// Projeta o DTO de resposta nas propriedades bindáveis — não há lógica aqui.
        /// </summary>
        private void AplicarResultado(SimulacaoResponse resultado)
        {
            ValorTotalInvestido = resultado.ValorTotalInvestido;
            RendimentoBruto     = resultado.RendimentoBruto;
            ValorTotalFinal     = resultado.ValorTotalFinal;
            TaxaSelicUtilizada  = resultado.TaxaSelicUtilizada;
            ResultadoVisivel    = true;
        }

        private void ExibirErro(string mensagem)
        {
            MensagemErro = mensagem;
            TemErro      = true;
        }

        // ── INotifyPropertyChanged ───────────────────────────────────────────

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void SetProperty<T>(ref T campo, T valor, 
            [CallerMemberName] string propriedade = "")
        {
            if (EqualityComparer<T>.Default.Equals(campo, valor)) return;
            campo = valor;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propriedade));
        }
    }
}
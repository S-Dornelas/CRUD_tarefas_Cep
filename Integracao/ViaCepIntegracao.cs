using AppCRUD.Integracao.Interfaces;
using AppCRUD.Integracao.Refit;
using AppCRUD.Integracao.Response;

namespace AppCRUD.Integracao
{
    public class ViaCepIntegracao : IViaCepIntegracao
    {
        private readonly IViaCepIntegracaoRefit _viaCepIntegracao;
        public ViaCepIntegracao(IViaCepIntegracaoRefit viaCepIntegracaoRefit) 
        {
            _viaCepIntegracao = viaCepIntegracaoRefit;        
        }

        public async Task<ViaCepResponse> ObterDadosViaCep(string cep)
        {
           var responseData = await _viaCepIntegracao.ObterDadosViaCep(cep);

            if (responseData != null && responseData.IsSuccessStatusCode)
            {
                return responseData.Content;
            }

            return null;
        }
    }
}

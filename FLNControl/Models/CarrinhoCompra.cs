using System.Collections.Generic;
using Newtonsoft.Json;

namespace FLNControl.Models
{
    public class CarrinhoCompra
    {
        [JsonProperty("cpf")]
        public string Cpf; 

        [JsonProperty("nomeCli")]
        public string NomeCli; 

        [JsonProperty("endereco")]
        public string Endereco; 

        [JsonProperty("numero")]
        public string Numero; 

        [JsonProperty("bairro")]
        public string Bairro; 

        [JsonProperty("cidade")]
        public string Cidade; 

        [JsonProperty("uf")]
        public string Uf; 

        [JsonProperty("formaPagamento")]
        public string FormaPagamento; 

        [JsonProperty("parcelas")]
        public int Parcelas; 

        [JsonProperty("vencimento")]
        public int Vencimento; 

        [JsonProperty("listaProdutos")]
        public List<List<object>> ListaProdutos; 
    }
}

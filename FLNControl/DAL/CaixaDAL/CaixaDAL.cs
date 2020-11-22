using engenharia.Models.Caixa;
using engenharia.Models.CaixaMovimentacao;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace engenharia.DAL.CaixaDAL
{
    public class CaixaDAL
    {
        MySqlPersistence bd = new MySqlPersistence();

        public Caixa Abrir(Caixa caixa)
        {
            return caixa;
        }
        public void Fechar(Caixa caixa)
        {

        }

        public void AtualizarCaixa(CaixaMovimentacao movimentacao)
        {

        }

    }
}
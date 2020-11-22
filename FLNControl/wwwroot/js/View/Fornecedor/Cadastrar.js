let CadastrarConfig = {
    CarregarNome: function () {
        var TituloPagina = document.querySelector("#TituloPagina");
        var CaminhoPagina = document.querySelector("#CaminhoPagina");

        TituloPagina.innerHTML = 'Novo Fornecedor';
        CaminhoPagina.innerHTML = `
            <li class="breadcrumb-item"><a href="/Fornecedor">Fornecedor</a></li>
            <li class="breadcrumb-item">Cadastro</a></li>
        `;

    }
}
CadastrarConfig.CarregarNome();
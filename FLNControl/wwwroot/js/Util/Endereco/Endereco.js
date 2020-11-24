var Endereco = {
    CarregarComboboxEstados: function () { },
    CarregarComboboxCidades: function () { },
    BuscarTabelaEnderecos: function () {
    HTTPClient.get('/Endereco/ListarTodosEnderecosCompletos')
        .then(
            function (retornoServidor) {
                return retornoServidor.json();
            }
        ).then(
            function (obj) {
                console.log(obj);
                Endereco.CarregarTabelaEnderecos(obj);
            }
        ).catch(
            function () {
                alert('Erro no servidor');
            }
        ).finally(
            function () { }
        );

    },
    CarregarTabelaEnderecos: function (listaDeEnderecos) {
        var divTabelaEndereco = document.querySelector('#tableBody');
        var linhas = '';
        if (typeof (listaDeEnderecos) == "undefined") {
            ElementosEndereco.CriarTabela();
        }
        else {
            listaDeEnderecos.forEach((endereco) => {
                linhas += ` <tr>
                                <td>${endereco.codigo}</td>
                                <td>${endereco.rua}</td>
                                <td>${endereco.bairro}</td>
                                <td>${endereco.cep}</td>
                                <td>${endereco.descricao}</td>
                                <td>
                                    <button class="btn btn-outline-warning" value="${endereco.codigo}">
                                        Editar
                                    </button>
                                    <button class="btn btn-outline-danger"  value="${endereco.codigo}">
                                        Excluir
                                    </button>
                                </td>
                            </tr>`;

            });
        }
        divTabelaEndereco.innerHTML = linhas;
    },
    CarregarFormulario: function (endereco) {
        var divcadastrarEndereco = document.querySelector('#formularioEndereco');
        divcadastrarEndereco.innerHTML = `
            <div class="col-12">
                <div class="row ">
                    <div class="col-2">
                        <div class="form-group" hidden>
                            <label for="inputCodigoEndereco">Código</label>
                            <input type="number" class="form-control" id="inputCodigoEndereco" placeholder="" value="${typeof (endereco) != "undefined" ? endereco.codigo : '12'}">
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label for="inputRuaEndereco">Rua</label>
                            <input type="text" class="form-control" id="inputRuaEndereco" placeholder="" value="${typeof (endereco) != "undefined" ? endereco.rua : 'rua'}">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label for="inputBairroEndereco">Bairro</label>
                            <input type="text" class="form-control" id="inputBairroEndereco" placeholder="" value="${typeof (endereco) != "undefined" ? endereco.bairro : 'bairro'}">
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label for="inputTipoEndereco">Tipo</label>
                            <input type="text" class="form-control" id="inputTipoEndereco" placeholder="" value="${typeof (endereco) != "undefined" ? endereco.tipo : 'tipo'}">
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label for="inputCepEndereco">CEP</label>
                            <input type="text" class="form-control" id="inputCepEndereco" placeholder="" value="${typeof (endereco) != "undefined" ? endereco.cep : 'cep'}">
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label for="inputDescricaoEndereco">Descrição</label>
                            <input type="text" class="form-control" id="inputDescricaoEndereco" placeholder="" value="${typeof (endereco) != "undefined" ? endereco.descricao : 'descricao'}">
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label for="adicionarEndereco">Adicionar Novo Endereço </label>
                            <div id="BtnAdicionarEndereco" class="btn btn-outline-primary w-100">
                                Gravar
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `


    }
    
}
var ElementosEndereco = {
    CriarElemento: function () {
        this.CriarCard();
        this.CriarTabela();
        var divcadastrarEndereco = document.querySelector('#formularioEndereco');
        divcadastrarEndereco.innerHTML = `<div class="btn btn-outline-success" onclick="ElementosEndereco.CriarFormulario()">Cadastrar Novo Endereço</div>`;
    },
    CriarCard: function () {
        var divcadastrarEndereco = document.querySelector('#cadastrarEndereco');
        divcadastrarEndereco.innerHTML = `
        <div class="card card-primary">
            <div class="card-header">
                <h3 class="card-title">Cadastro completo de Endereço</h3>
            </div>
            <div class="card-body">
                <div id="formularioEndereco" class="row">
                </div>
                <hr/>
                <div id="tabelaEndereco" class="row">
                </div>
            </div>                
        </div>
        `
    },
    CriarFormulario: function () {
        var divcadastrarEndereco = document.querySelector('#formularioEndereco');
        divcadastrarEndereco.innerHTML = `
            <div class="col-12">
                <div class="row ">
                    <div class="col-2">
                        <div class="form-group" hidden>
                            <label for="inputCodigoEndereco">Código</label>
                            <input type="number" class="form-control" id="inputCodigoEndereco" placeholder="" >
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label for="inputRuaEndereco">Rua</label>
                            <input type="text" class="form-control" id="inputRuaEndereco" placeholder="" >
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label for="inputBairroEndereco">Bairro</label>
                            <input type="text" class="form-control" id="inputBairroEndereco" placeholder="" >
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label for="inputTipoEndereco">Tipo</label>
                            <input type="text" class="form-control" id="inputTipoEndereco" placeholder="">
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label for="inputCepEndereco">CEP</label>
                            <input type="text" class="form-control" id="inputCepEndereco" placeholder="">
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label for="inputDescricaoEndereco">Descrição</label>
                            <input type="text" class="form-control" id="inputDescricaoEndereco" placeholder="">
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label for="adicionarEndereco">Adicionar Novo Endereço </label>
                            <div id="BtnAdicionarEndereco" class="btn btn-outline-primary w-100">
                                Gravar
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `


    },
    CriarTabela: function (listaDeEnderecos) {
        var divTabelaEndereco = document.querySelector('#tabelaEndereco');
        var linhas = '';
        divTabelaEndereco.innerHTML = `   
                            <h4>Endereços Cadastrados</h4>
                            <div class="card-body table-responsive p-0" style="height: 300px;">
                                <table id="tableAll" class="table table-head-fixed text-nowrap">
                                    <thead id="tableHead">
                                        <tr>
                                            <th>Código</th>
                                            <th>Rua</th>
                                            <th>Bairro</th>
                                            <th>Tipo</th>
                                            <th>CEP</th>
                                            <th>Ações</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tableBody">
                                        <tr>
                                            <td colspan="6">
                                                <h3>
                                                    Nenhum endereço cadastrado...
                                                </h3>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>`;    
        
    }
}

/*Forma de carregar todos os elementos do card*/
ElementosEndereco.CriarElemento();
Endereco.BuscarTabelaEnderecos();
/*Fim */

/*

CarregarTabela: function (dados) {
    let tabela = document.querySelector("#tableBody");

    tabela.innerHTML = "";

    let linhas = "";

    dados.forEach((dado) => {

        linhas +=
            `<tr>
                    <td>${dado.codigo}</td >
                    <td>${dado.nome}</td>
                    <td>${dado.cpf}</td>
                    <td>${dado.email}</td>
                    <td>${dado.telefone}</td>
                    <td>${(dado.fiado == '1' ? "Sim" : "Não")}</td>
                    <td>
                        <button class="btn btn-outline-warning" value="${dado.codigo}">
                            Editar
                                </button>
                        <button class="btn btn-outline-danger"  value="${dado.codigo}">
                            Excluir
                                </button>
                        <button class="btn btn-outline-info"  value="${dado.codigo}">
                            Ver +
                                </button>
                    </td>
                </tr>`
    }
    )
    tabela.innerHTML = linhas;
}
*/
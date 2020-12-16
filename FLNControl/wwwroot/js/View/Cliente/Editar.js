let Editar = {
    CarregarTabela: function (dados) {
        var inputNome = document.querySelector('#inputNome');
        var inputCpf = document.querySelector('#inputCpf');
        var inputTelefone = document.querySelector('#inputTelefone');
        var inputEmail = document.querySelector('#inputEmail');
        var inputDataNascimento = document.querySelector('#inputDataNascimento');
        var checkFiado = document.querySelector('#checorpoformulariockFiado');
        var inputValorLimiteFiado = document.querySelector('#inputValorLimiteFiado');
        var inputParcelasFiado = document.querySelector('#inputParcelasFiado');
        var inputDiasVencimento = document.querySelector('#inputDiasVencimento');

        var dados = {
            nome: inputNome.value,
            cpf: inputCpf.value,
            telefone: inputTelefone.value,
            email: inputEmail.value,
            dataNascimento: inputDataNascimento.value,
            fiado: checkFiado.value,
            valorLimiteFiado: inputValorLimiteFiado.value == "" ? 0 : inputValorLimiteFiado.value,
            dataVencimento: inputDiasVencimento.options[inputDiasVencimento.selectedIndex].value,
            parcelasLimiteFiado: inputParcelasFiado.options[inputParcelasFiado.selectedIndex].value
        };

        inputNome.value = dados.nome;
        inputCpf.value = dados.cpf;
        inputTelefone.value = dados.telefone;
        inputEmail.value = dados.email;
        inputDataNascimento.value = dados.dataNascimento;
        checkFiado.value = dados.fiado;
        inputValorLimiteFiado.value = dados.valorLimiteFiado;
    },
    BuscarCliente: function () {
        HTTPClient.get("/Cliente/PesquisarClientesPorCodigo?codigo=" + IndexInterface.codigo)
            .then(
                function (retornoServidor) {
                    return retornoServidor.json()
                }
            )
            .then(
                function (obj) {

                    Index.CarregarTabela(obj);
                }
            )
            .catch(
                function () {
                    alert("Deu erro no HTTPClient");
                }
            )
            .finally(
                function () {
                }
            )

    }
    
}
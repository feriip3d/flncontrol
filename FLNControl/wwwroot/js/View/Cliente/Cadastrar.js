
let Cadastrar = {
    GravarCliente: function () {
        var { dados, retorno } = this.ValidarDados();
        if (retorno) {
            HTTPClient.post('/Cliente/CadastrarClienteCompleto', dados)
                .then(function (retornoServidor) {
                    return retornoServidor.json();
                })
                .then(function (dados) {
                    console.log(dados);
                    alert('Cadastrado com sucesso!');
                })
                .catch(function (erroServidor) {
                    alert('erro servidor');
                })
                .finally(function () { });
        }
        else
            alert("erro");
    },
    ValidarDados: function () {
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
            dataNascimento: inputDataNascimento.value ,
            fiado: checkFiado.value,
            valorLimiteFiado: inputValorLimiteFiado.value == "" ? 0 : inputValorLimiteFiado.value,
            dataVencimento: inputDiasVencimento.options[inputDiasVencimento.selectedIndex].value,
            parcelasLimiteFiado: inputParcelasFiado.options[inputParcelasFiado.selectedIndex].value
        };


        var erros = [];

        if (inputNome.value == '') {
            erros.push({
                campo: 'inputNome',
                mensagem: 'vazio',
                elemento: inputNome
            });
        }
        if (inputCpf.value == '') {
            erros.push({
                campo: 'inputCpf',
                mensagem: 'vazio',
                elemento: inputCpf
            });
        }
        if (inputTelefone.value == '') {
            erros.push({
                campo: 'inputTelefone',
                mensagem: 'vazio',
                elemento: inputTelefone
            });
        }
        if (inputEmail.value == '') {
            erros.push({
                campo: 'inputEmail',
                mensagem: 'vazio',
                elemento: inputEmail
            });
        }
        if (inputDataNascimento.value == '') {
            erros.push({
                campo: 'inputDataNascimento',
                mensagem: 'vazio',
                elemento: inputDataNascimento
            });
        }
        if (checkFiado.checked) {

            if (inputValorLimiteFiado.value == '') {
                erros.push({
                    campo: 'inputValorLimiteFiado',
                    mensagem: 'vazio',
                    elemento: inputValorLimiteFiado
                });
            }
            if (inputParcelasFiado.options[inputParcelasFiado.selectedIndex].value == '') {
                erros.push({
                    campo: 'inputParcelasFiado',
                    mensagem: 'vazio',
                    elemento: inputParcelasFiado
                });
            }
            if (inputDiasVencimento.options[inputDiasVencimento.selectedIndex].value == '') {
                erros.push({
                    campo: 'inputDiasVencimento',
                    mensagem: 'vazio',
                    elemento: inputDiasVencimento
                });
            }
        }
        CadastrarConfig.CamposComErros(erros);

        var retorno = erros.length == 0;
        return { dados, retorno };
    }
}


let CadastrarConfig = {
    CarregarNome: function () {
        var TituloPagina = document.querySelector("#TituloPagina");
        var CaminhoPagina = document.querySelector("#CaminhoPagina");

        TituloPagina.innerHTML = 'Novo Cliente';
        CaminhoPagina.innerHTML = `
            <li class="breadcrumb-item"><a href="/Cliente">Cliente</a></li>
            <li class="breadcrumb-item">Cadastro</a></li>
        `;

    },
    AtivaInformaçõesFiado: function (element) {
        var corpo = document.querySelector('#corpoformulario ');
        var elementDias = document.querySelector('#elementDiasFiado ');
        var elementValor = document.querySelector('#elementValorFiado ');
        var elementParcelas = document.querySelector('#elementParcelasFiado ');
        element.checked = element.checked == true;

        if (element.checked) {
            element.value = 1;
            elementDias.hidden = false;
            elementValor.hidden = false;
            elementParcelas.hidden = false;
        }
        else {
            lement.value = 0;
            elementDias.hidden = true;
            elementValor.hidden = true;
            elementParcelas.hidden = true;
        }
    },
    CamposComErros: function (erros) {
        /* erros :{
         *  campo: 'id',
         *  mensagem: 'mensagem do erro'
         * }
         *
        var elem = document.querySelect('#inputEmail');
        elem.List

         */
         
        erros.map(element => {
            var elem = document.querySelector('#' + element.campo);
            switch (elem.type) {
                case 'text': case 'number': case 'email': case 'date': 
                    elem.classList.add('is-invalid');
                    break;
            }
        });

    }

}


CadastrarConfig.CarregarNome();
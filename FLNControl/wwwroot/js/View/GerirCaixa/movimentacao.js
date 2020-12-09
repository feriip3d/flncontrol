var validateState = false;

var validacaoMessage = "Por favor, responda o campo de preenchimento obrigatório (*)!";


document.addEventListener('keydown', function (event) {
    document.querySelector("#dinheiro").value = document.querySelector("#dinheiro").value.replace(/[^\d,]/g, '');

    if (/[0-9]/.test(event.key)) {
        var i = document.querySelector("#dinheiro").value.length;
        if (i >= 2) {
            if (i == 2) var dist = 1; else dist = 2;
            document.querySelector("#dinheiro").value = document
                .querySelector("#dinheiro")
                .value
                .replace(',', '')
                .substring(0, document.querySelector("#dinheiro").value.length - dist)
                + ',' + document
                .querySelector("#dinheiro")
                    .value
                    .replace(',', '')
                    .replace(/[^\d]/, '')
                .substring(document.querySelector("#dinheiro").value.length - dist);
        }
    }
});

function realizarMovimentacao(data_abertura, valor_final) {
    
    var operacao = $("#operacaoSelected").val();
    var data = $("#inputDataAbert").val();
    var valor = $("#inputValorAtual").val();
    var motivo = $("#inputMotivo").val();
    var dinheiro = $("#dinheiro").val();

    let aux = 0;

    if (operacao == "" || operacao == "default" && $(operacao).hasClass('required')) {
        $(operacao).addClass('invalid');
        $(operacao).focus();
        $(operacao).addClass('border');
        aux++;
    }

    if (data == "" || data == "default" && $(data).hasClass('required')) {
        $(data).addClass('invalid');
        $(data).focus();
        $(data).addClass('border');
        aux++;
    }

    if (motivo == "" || motivo == "default" && $(motivo).hasClass('required')) {
        $(motivo).addClass('invalid');
        $(motivo).focus();
        $(motivo).addClass('border');
        aux++;
    }

    if (dinheiro == "" || dinheiro == "default" && $(dinheiro).hasClass('required')) {
        $(dinheiro).addClass('invalid');
        $(dinheiro).focus();
        $(dinheiro).addClass('border');
        aux++;
    }

    if (aux > 0) {
        alert(validacaoMessage);
    }
    else {
        var abertura = new Date(data_abertura);

        var movimentacao = new Date(data);
        movimentacao = new Date(movimentacao.getUTCFullYear(), movimentacao.getUTCMonth(), movimentacao.getUTCDate());
        
        if (abertura.getTime() > movimentacao.getTime()) {
            console.log(abertura.getTime(), movimentacao.getTime());
            alert("Data anterior a data de abertura.");
            return;
        }

        if (dinheiro <= 0 ) {
            alert('O valor informado não está correto, tente novamente');
            $("#operacaoSelected").val("");
            $("#inputMotivo").val("");
            $("#dinheiro").val("");
            return;
        }

        if (operacao == 'Sangria' && parseFloat(dinheiro) > parseFloat(valor_final)) {
            alert("O valor informado não está disponível para a operação.");
            $("#operacaoSelected").val("");
            $("#inputMotivo").val("");
            $("#dinheiro").val("");
            return;
        }

        $.ajax({
            url: '/GerirCaixa/MovimentacaoCaixa',
            dataType: 'html',
            type: 'POST',
            data: { operacao: operacao, data: data, valor: valor, motivo: motivo, dinheiro: dinheiro },
            beforeSend: function () {
                console.log('Carregando...');
            }
        }).done(function (msg) {
            msg = jQuery.parseJSON(msg);
            if (msg.retorno) {
                location.href = "/Acesso"
            }
            else {
                alert("Erro ao tentar realizar Movimentação, por favor tente novamente!");
                $("#operacaoSelected").val("");
                $("#inputMotivo").val("");
                $("#dinheiro").val("");
            }
        }).fail(function (jqXHR, textstatus, msg) {
            alert(msg);
            $("#operacaoSelected").val("");
            $("#inputMotivo").val("");
            $("#dinheiro").val("");
        });
    }
}

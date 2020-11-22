var validateState = false;

var validacaoMessage = "Por favor, responda o campo de preenchimento obrigatório (*)!";

function realizarMovimentacao() {
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
                $("#inputDataAbert").val("");
                $("#inputValorAtual").val("");
                $("#inputMotivo").val("");
                $("#dinheiro").val("");
            }
        }).fail(function (jqXHR, textstatus, msg) {
            alert(msg);
            $("#operacaoSelected").val("");
            $("#inputDataAbert").val("");
            $("#inputValorAtual").val("");
            $("#inputMotivo").val("");
            $("#dinheiro").val("");
        });
    }
}

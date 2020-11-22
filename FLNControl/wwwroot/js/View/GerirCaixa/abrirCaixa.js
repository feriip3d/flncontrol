var validateState = false;

var validacaoMessage = "Por favor, responda o campo de preenchimento obrigatório (*)!";

function abrirCaixa() {
    var data = $("#inputDataAbertura").val();
    var valor = $("#inputValorDisp").val();

    let aux = 0;

    if (data == "" || data == "default" && $(data).hasClass('required')) {
        $(data).addClass('invalid');
        $(data).focus();
        $(data).addClass('border');
        aux++;
    }

    if (valor == "" || valor == "default" && $(valor).hasClass('required')) {
        $(valor).addClass('invalid');
        $(valor).focus();
        $(valor).addClass('border');
        aux++;
    }

    if (aux > 0) {
        alert(validacaoMessage);
    }
    else {
        $.ajax({
            url: '/GerirCaixa/AbrirCaixa',
            dataType: 'html',
            type: 'POST',
            data: { data: data, valor: valor },
            beforeSend: function () {
                console.log('Carregando...');
            }
        }).done(function (msg) {
            msg = jQuery.parseJSON(msg);
            if (msg.retorno) {
                location.href = "/Acesso"
            }
            else {
                alert("Erro ao tentar logar, por favor tente novamente!");
                $("#inputDataAbertura").val("");
                $("#inputValorDisp").val("");
            }
        }).fail(function (jqXHR, textstatus, msg) {
            alert(msg);
            $("#inputDataAbertura").val("");
            $("#inputValorDisp").val("");
        });
    }
}


function fecharCaixa() {
    var data = $("#inputDataAberturaFechar").val();
    var valor = $("#inputValorDispFechar").val();

    let aux = 0;

    if (data == "" || data == "default" && $(data).hasClass('required')) {
        $(data).addClass('invalid');
        $(data).focus();
        $(data).addClass('border');
        aux++;
    }

    if (valor == "" || valor == "default" && $(valor).hasClass('required')) {
        $(valor).addClass('invalid');
        $(valor).focus();
        $(valor).addClass('border');
        aux++;
    }

    if (aux > 0) {
        alert(validacaoMessage);
    }
    else {
        $.ajax({
            url: '/GerirCaixa/FecharCaixaAberto',
            dataType: 'html',
            type: 'POST',
            data: { data: data, valor: valor },
            beforeSend: function () {
                console.log('Carregando...');
            }
        }).done(function (msg) {
            msg = jQuery.parseJSON(msg);
            if (msg.retorno) {
                location.href = "/Acesso"
            }
            else {
                alert("Erro ao tentar logar, por favor tente novamente!");
                $("#inputDataAbertura").val("");
                $("#inputValorDisp").val("");
            }
        }).fail(function (jqXHR, textstatus, msg) {
            alert(msg);
            $("#inputDataAbertura").val("");
            $("#inputValorDisp").val("");
        });
    }
}

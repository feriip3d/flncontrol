var validateState = false;

var validacaoMessage = "Por favor, responda o campo de preenchimento obrigatório (*)!";

document.addEventListener('keydown', function (event) {
    document.querySelector("#inputValorDisp").value = document.querySelector("#inputValorDisp").value.replace(/[^\d,]/g, '');

    if (/[0-9]/.test(event.key)) {
        var i = document.querySelector("#inputValorDisp").value.length;
        if (i >= 2) {
            if (i == 2) var dist = 1; else dist = 2;
            document.querySelector("#inputValorDisp").value = document
                .querySelector("#inputValorDisp")
                .value
                .replace(',', '')
                .substring(0, document.querySelector("#inputValorDisp").value.length - dist)
                + ',' + document
                    .querySelector("#inputValorDisp")
                    .value
                    .replace(',', '')
                    .replace(/[^\d]/, '')
                    .substring(document.querySelector("#inputValorDisp").value.length - dist);
        }
    }
});

function abrirCaixa(valor_anterior, status_anterior) {
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
        data_abertura = new Date(data);
        data_abertura = new Date(data_abertura.getUTCFullYear(), data_abertura.getUTCMonth(), data_abertura.getUTCDate());

        data_atual = new Date();
        data_atual = new Date(data_atual.getUTCFullYear(), data_atual.getUTCMonth(), data_atual.getUTCDate());

        if (data_abertura < data_atual) {
            alert("A data de abertura deve ser igual ou posterior a atual.");
            return;
        }
        console.log(valor_anterior);
        
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


function fecharCaixa(data_abertura, valor_final) {
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
        abertura = new Date(data_abertura);
        fechamento = new Date(data);
        fechamento = new Date(fechamento.getUTCFullYear(), fechamento.getUTCMonth(), fechamento.getUTCDate());
        if (fechamento < abertura) {
            alert("A data de fechamento deve ser igual ou posterior a de abertura!");
            return;
        }
        if (fechamento > Date.now()) {
            alert("A data de fechamento deve estar entre a data de abertura e a atual.");
            return;
        }
        if (parseFloat(valor_final) != parseFloat(valor)) {
            alert("O valor informado é diferente do registrado em caixa, verifique.");
            return;
        }
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

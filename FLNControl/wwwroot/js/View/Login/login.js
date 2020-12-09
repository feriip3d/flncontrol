

function entrarSistema() {
    var login = $("#login").val();
    var senha = $("#senha").val();
    $.ajax({
        url: '/Colaborador/Autenticacao',
        dataType: 'html',
        type: 'POST',
        data: { login: login, senha: senha },
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
            $("#login").val("");
            $("#senha").val("");
        }
    }).fail(function (jqXHR, textstatus, msg) {
        alert(msg);
        $("#login").val("");
        $("#senha").val("");
    });
}
var validateState = false;

var validacaoMessage = "Por favor, responda o campo de preenchimento obrigatório (*)!";

function gravarNovoAcesso() {
    var nome = $("#inputName").val();
    var login = $("#inputLogin").val();
    var senha = $("#inputSenha").val();
    var cpf = $("#inputCPF").val();
    var rg = $("#inputRG").val();
    var data_nasc = $("#inputDataNasc").val();
    var telefone = $("#inputTelefone").val();
    var cargo = $("#cargoSelected").val();
    var status = $("#statusSelected").val();
    var nivel = $("#nivelSelected").val();
    var email = $("#inputEmail").val();

    let aux = 0;
    if (nome == "" || nome == "default" && $(nome).hasClass('required')) {
        $(nome).addClass('invalid');
        $(nome).focus();
        $(nome).addClass('border');
        aux++;
    }

    if (login == "" || login == "default" && $(login).hasClass('required')) {
        $(login).addClass('invalid');
        $(login).focus();
        $(login).addClass('border');
        aux++;
    }
    if (senha == "" || senha == "default" && $(senha).hasClass('required')) {
        $(senha).addClass('invalid');
        $(senha).focus();
        $(senha).addClass('border');
        aux++;
    }
    if (cpf == "" || cpf == "default" && $(cpf).hasClass('required')) {
        $(cpf).addClass('invalid');
        $(cpf).focus();
        $(cpf).addClass('border');
        aux++;
    }
    if (rg == "" || rg == "default" && $(rg).hasClass('required')) {
        $(rg).addClass('invalid');
        $(rg).focus();
        $(rg).addClass('border');
        aux++;
    }
    if (data_nasc == "" || data_nasc == "default" && $(data).hasClass('required')) {
        $(data_nasc).addClass('invalid');
        $(data_nasc).focus();
        $(data_nasc).addClass('border');
        aux++;
    }
    if (telefone == "" || telefone == "default" && $(telefone).hasClass('required')) {
        $(telefone).addClass('invalid');
        $(telefone).focus();
        $(telefone).addClass('border');
        aux++;
    }

    if (aux > 0) {
        alert(validacaoMessage);
    }
    else {
        validateState = true;

        $.ajax({
            url: '/Colaborador/CadastrarAcesso',
            dataType: 'html',
            type: 'POST',
            data: { nome: nome, login: login, senha: senha, cpf: cpf, rg: rg, data_nasc: data_nasc, telefone: telefone, email: email, cargo: cargo, status: status, nivel: nivel },
        }).done(function (msg) {
            msg = jQuery.parseJSON(msg);
            if (msg.retorno) {
                location.href = "/Acesso"
            }
            else {
                alert("Erro ao tentar cadastrar, por favor tente novamente!");
                $("#nome").val("");
                $("#login").val("");
                $("#senha").val("");
                $("#cpf").val("");
                $("#rg").val("");
                $("#data_nasc").val("");
                $("#telefone").val("");
                $("#cargo").val("");
                $("#status").val("");
                $("#nivel").val("");
            }
        }).fail(function (jqXHR, textstatus, msg) {
            alert(msg);
            $("#nome").val("");
            $("#login").val("");
            $("#senha").val("");
            $("#cpf").val("");
            $("#rg").val("");
            $("#data_nasc").val("");
            $("#telefone").val("");
            $("#cargo").val("");
            $("#status").val("");
            $("#nivel").val("");
        });

    }
}

function gravarPrimeiroAcesso() {
    var nome = $("#inputName").val();
    var login = $("#inputLogin").val();
    var senha = $("#inputSenha").val();
    var cpf = $("#inputCPF").val();
    var rg = $("#inputRG").val();
    var data_nasc = $("#inputDataNasc").val();
    var telefone = $("#inputTelefone").val();
    var cargo = $("#cargoSelected").val();
    var status = $("#statusSelected").val();
    var nivel = $("#nivelSelected").val();
    var email = $("#inputEmail").val();

    let aux = 0;
    if (nome == "" || nome == "default" && $(nome).hasClass('required')) {
        $(nome).addClass('invalid');
        $(nome).focus();
        $(nome).addClass('border');
        aux++;
    }

    if (login == "" || login == "default" && $(login).hasClass('required')) {
        $(login).addClass('invalid');
        $(login).focus();
        $(login).addClass('border');
        aux++;
    }
    if (senha == "" || senha == "default" && $(senha).hasClass('required')) {
        $(senha).addClass('invalid');
        $(senha).focus();
        $(senha).addClass('border');
        aux++;
    }
    if (cpf == "" || cpf == "default" && $(cpf).hasClass('required')) {
        $(cpf).addClass('invalid');
        $(cpf).focus();
        $(cpf).addClass('border');
        aux++;
    }
    if (rg == "" || rg == "default" && $(rg).hasClass('required')) {
        $(rg).addClass('invalid');
        $(rg).focus();
        $(rg).addClass('border');
        aux++;
    }
    if (data_nasc == "" || data_nasc == "default" && $(data).hasClass('required')) {
        $(data_nasc).addClass('invalid');
        $(data_nasc).focus();
        $(data_nasc).addClass('border');
        aux++;
    }
    if (telefone == "" || telefone == "default" && $(telefone).hasClass('required')) {
        $(telefone).addClass('invalid');
        $(telefone).focus();
        $(telefone).addClass('border');
        aux++;
    }

    if (aux > 0) {
        alert(validacaoMessage);
    }
    else {
        validateState = true;

        $.ajax({
            url: '/Colaborador/CadastrarAcesso',
            dataType: 'html',
            type: 'POST',
            data: { nome: nome, login: login, senha: senha, cpf: cpf, rg: rg, data_nasc: data_nasc, telefone: telefone, email: email, cargo: cargo, status: status, nivel: nivel },
        }).done(function (msg) {
            msg = jQuery.parseJSON(msg);
            if (msg.retorno) {
                location.href = "/Colaborador"
            }
            else {
                alert("Erro ao tentar cadastrar, por favor tente novamente!");
                $("#nome").val("");
                $("#login").val("");
                $("#senha").val("");
                $("#cpf").val("");
                $("#rg").val("");
                $("#data_nasc").val("");
                $("#telefone").val("");
                $("#cargo").val("");
                $("#status").val("");
                $("#nivel").val("");
            }
        }).fail(function (jqXHR, textstatus, msg) {
            alert(msg);
            $("#nome").val("");
            $("#login").val("");
            $("#senha").val("");
            $("#cpf").val("");
            $("#rg").val("");
            $("#data_nasc").val("");
            $("#telefone").val("");
            $("#cargo").val("");
            $("#status").val("");
            $("#nivel").val("");
        });

    }
}

function redirectGravar() {
    location.href = "/Colaborador/Gravar"
}

function redirectEdit() {
    location.href = "/Colaborador/EditarUsuario"
}

function editarAcesso() {
    var nome = $("#inputName").val();
    var login = $("#inputLogin").val();
    var senha = $("#inputSenha").val();
    var cpf = $("#inputCPF").val();
    var rg = $("#inputRG").val();
    var data_nasc = $("#inputDataNasc").val();
    var telefone = $("#inputTelefone").val();
    var cargo = $("#cargoSelected").val();
    var status = $("#statusSelected").val();
    var nivel = $("#nivelSelected").val();
    var email = $("#inputEmail").val();
    var id = $("#id").val();

    let aux = 0;
    if (nome == "" || nome == "default" && $(nome).hasClass('required')) {
        $(nome).addClass('invalid');
        $(nome).focus();
        $(nome).addClass('border');
        aux++;
    }

    if (login == "" || login == "default" && $(login).hasClass('required')) {
        $(login).addClass('invalid');
        $(login).focus();
        $(login).addClass('border');
        aux++;
    }
    if (senha == "" || senha == "default" && $(senha).hasClass('required')) {
        $(senha).addClass('invalid');
        $(senha).focus();
        $(senha).addClass('border');
        aux++;
    }
    if (cpf == "" || cpf == "default" && $(cpf).hasClass('required')) {
        $(cpf).addClass('invalid');
        $(cpf).focus();
        $(cpf).addClass('border');
        aux++;
    }
    if (rg == "" || rg == "default" && $(rg).hasClass('required')) {
        $(rg).addClass('invalid');
        $(rg).focus();
        $(rg).addClass('border');
        aux++;
    }
    if (data_nasc == "" || data_nasc == "default" && $(data).hasClass('required')) {
        $(data_nasc).addClass('invalid');
        $(data_nasc).focus();
        $(data_nasc).addClass('border');
        aux++;
    }
    if (telefone == "" || telefone == "default" && $(telefone).hasClass('required')) {
        $(telefone).addClass('invalid');
        $(telefone).focus();
        $(telefone).addClass('border');
        aux++;
    }

    if (aux > 0) {
        alert(validacaoMessage);
    }
    else {
        validateState = true;

        $.ajax({
            url: '/Colaborador/EditarUsuarioExistente',
            dataType: 'html',
            type: 'POST',
            data: { nome: nome, login: login, senha: senha, cpf: cpf, rg: rg, data_nasc: data_nasc, telefone: telefone, email: email, cargo: cargo, status: status, nivel: nivel, id: id },
        }).done(function (msg) {
            msg = jQuery.parseJSON(msg);
            if (msg.retorno) {
                location.href = "/Acesso"
            }
            else {
                alert("Erro ao tentar editar, por favor tente novamente!");
                $("#nome").val("");
                $("#login").val("");
                $("#senha").val("");
                $("#cpf").val("");
                $("#rg").val("");
                $("#data_nasc").val("");
                $("#telefone").val("");
                $("#cargo").val("");
                $("#status").val("");
                $("#nivel").val("");
            }
        }).fail(function (jqXHR, textstatus, msg) {
            alert(msg);
            $("#nome").val("");
            $("#login").val("");
            $("#senha").val("");
            $("#cpf").val("");
            $("#rg").val("");
            $("#data_nasc").val("");
            $("#telefone").val("");
            $("#cargo").val("");
            $("#status").val("");
            $("#nivel").val("");
        });

    }
}

function pegarDadosEdit(nome, login, senha, cpf, rg, data_nasc, telefone, email, cargo, status, nivel) {
    var nomeSend = nome;
    var loginSend = login;
    var senhaSend = senha;
    var cpfSend = cpf;
    var rgSend = rg;
    var data_nascSend = data_nasc;
    var telefoneSend = telefone;
    var emailSend = email;
    var cargoSend = cargo;
    var statusSend = status;
    var nivelSend = nivel;

    $.ajax({
        url: '/Colaborador/Editar',
        dataType: 'html',
        type: 'POST',
        data: { nome: nome, login: login, senha: senha, cpf: cpf, rg: rg, data_nasc: data_nasc, telefone: telefone, email: email, cargo: cargo, status: status, nivel: nivel },
    }).done(function (msg) {
        location.href = "/Acesso"
    });
}

function mascaraDeTelefone() {
    function mascaraDeTelefone(telefone) {
        const textoAtual = telefone.value;
    }
}

function mascaraDeData() {
    function mascaraDeData(data) {
        const textoAtual = data.value;
    }
}

function mascaraDeCPF() {
    function mascaraDeCPF(CPF) {
        const textoAtual = cpf.value;
    }
}

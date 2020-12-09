import { HTTPRequest } from "./lib/HTTPRequest.js";
import { FLNUtil } from "./lib/FLNUtil.js";

try {
    document.querySelector('#f-parametros')
        .addEventListener("submit", (e) => {
            e.preventDefault();
            FLNProduto.consultar();
        });
} catch (e) {
}

try {
    document.addEventListener('keydown', function (event) {
        document.querySelector("#i-tel").value = document.querySelector("#i-tel").value.replace(/\(\d{2,}\) \d{4,}\-\d{5}/g, '');

        if (/[0-9]/.test(event.key)) {
            var i = document.querySelector("#i-tel").value.length;
            if (i == 1)
                document.querySelector("#i-tel").value = "(" + document
                    .querySelector("#i-tel")
                    .value;

            if (i == 3)
                document.querySelector("#i-tel").value = document
                    .querySelector("#i-tel")
                    .value + ")";

            if (i == 8)
                document.querySelector("#i-tel").value = document
                    .querySelector("#i-tel")
                    .value + "-";
        }
    });
} catch (e) {
}


try {
    document.addEventListener('keydown', function (event) {
        document.querySelector("#i-ie").value = document.querySelector("#i-ie").value.replace(/\d{3,}\.\d{3,}\.\d{3,}\.\d{3,}\.}/g, '');

        if (/[0-9]/.test(event.key)) {
            var i = document.querySelector("#i-ie").value.length;
            if (i == 3 || i == 7 || i == 11)
                document.querySelector("#i-ie").value = document
                    .querySelector("#i-ie")
                    .value + '.';
        }
    });
} catch (e) {
}

window.FLNParametro = {
    atualizar: () => {
        let razaosc = document.querySelector("#i-razaosc").value;
        let nomef = document.querySelector("#i-nomef").value;
        let cnpj = document.querySelector("#i-cnpj").value;
        let ie = document.querySelector("#i-ie").value;
        let telefone = document.querySelector("#i-tel").value;
        let email = document.querySelector("#i-email").value;
        let site = document.querySelector("#i-site").value;
        let logoGrande = document.querySelector("#i-logo-g").value;
        let logoPequeno = document.querySelector("#i-logo-p").value;
        let endereco = document.querySelector("#i-end").value;
        let cidade = document.querySelector("#i-cidade").value;
        let uf = document.querySelector("#i-uf").value;

        if (FLNUtil.isEmpty(razaosc)) {
            toastr.error('O campo <b>Razão Social</b> é obrigatório!');
            return;
        }

        if (FLNUtil.isEmpty(nomef)) {
            toastr.error('O campo <b>Nome Fantasia</b> é obrigatório!');
            return;
        }

        if (FLNUtil.isEmpty(cnpj)) {
            toastr.error('O campo <b>CNPJ</b> é obrigatório!');
            return;
        }

        if (FLNUtil.isEmpty(ie)) {
            toastr.error('O campo <b>Inscrição Estadual</b> é obrigatório!');
            return;
        }

        if (FLNUtil.isEmpty(site)) {
            toastr.error('O campo <b>Site</b> é obrigatório!');
            return;
        }

        if (FLNUtil.isEmpty(email)) {
            toastr.error('O campo <b>Email</b> é obrigatório!');
            return;
        }
        if (FLNUtil.isEmpty(logoGrande)) {
            toastr.error('O campo <b>Logo Grande (Caminho)</b> é obrigatório!');
            return;
        }
        if (FLNUtil.isEmpty(logoPequeno)) {
            toastr.error('O campo <b>Logo Pequeno (Caminho)</b> é obrigatório!');
            return;
        }
        if (FLNUtil.isEmpty(telefone)) {
            toastr.error('O campo <b>Telefone</b> é obrigatório!');
            return;
        }

        if (FLNUtil.isEmpty(endereco)) {
            toastr.error('O campo <b>Endereço</b> é obrigatório!');
            return;
        }

        if (FLNUtil.isEmpty(cidade)) {
            toastr.error('O campo <b>Cidade</b> é obrigatório!');
            return;
        }

        if (FLNUtil.isEmpty(uf)) {
            toastr.error('O campo <b>UF</b> é obrigatório!');
            return;
        }

        let requestData = {
            razaosc: razaosc,
            nomef: nomef,
            cnpj: cnpj,
            ie: ie,
            telefone: telefone,
            email: email,
            site: site, 
            logoGrande: logoGrande,
            logoPequeno: logoPequeno,
            endereco: endereco,
            cidade: cidade,
            uf: uf,
        }

        HTTPRequest.post('/Parametro/Atualizar', requestData)
            .then((requestResponse) => {
                return requestResponse.json();
            })
            .then((requestResponse) => {
                console.log(requestResponse);
                if (requestResponse.success) {
                    window.location.reload(true);
                } else {
                    toastr.warning('Ocorreu um erro ao atualizar os parametros do sistema. Verifique os campos e tente novamente.');
                }
            })
            .catch((requestResponse) => {
                console.log(requestResponse);
                toastr.error('Ocorreu um erro inesperado ao atualizar os parametros do sistema. Tente novamente mais tarde.');
            }).finally(() => {
            });
    },

    reverter: (form) => {
        document.querySelector("#" + form).reset();
    }
}
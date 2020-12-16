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
        document.querySelector("#i-cnpj").value = document.querySelector("#i-cnpj").value.replace(/\d{2,}\.\d{3,}\.\d{3,}\/\d{4,}\-\d{2,}\./g, '');

        if (/[0-9]/.test(event.key)) {
            var i = document.querySelector("#i-cnpj").value.length;
            if (i == 2 || i == 6)
                document.querySelector("#i-cnpj").value = document
                    .querySelector("#i-cnpj")
                    .value + '.';

            if (i == 10)
                document.querySelector("#i-cnpj").value = document
                    .querySelector("#i-cnpj")
                    .value + '/';

            if (i == 15)
                document.querySelector("#i-cnpj").value = document
                    .querySelector("#i-cnpj")
                    .value + '-';
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

        if (!FLNParametro.validarCnpj(cnpj)) {
            toastr.error('O <b>CNPJ</b> informado é inválido!');
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
    },

    validarCnpj: (val) => {
        if (val.length == 14) {
            var cpf = val.trim();

            cpf = cpf.replace(/\./g, '');
            cpf = cpf.replace('-', '');
            cpf = cpf.split('');

            var v1 = 0;
            var v2 = 0;
            var aux = false;

            for (var i = 1; cpf.length > i; i++) {
                if (cpf[i - 1] != cpf[i]) {
                    aux = true;
                }
            }

            if (aux == false) {
                return false;
            }

            for (var i = 0, p = 10; (cpf.length - 2) > i; i++, p--) {
                v1 += cpf[i] * p;
            }

            v1 = ((v1 * 10) % 11);

            if (v1 == 10) {
                v1 = 0;
            }

            if (v1 != cpf[9]) {
                return false;
            }

            for (var i = 0, p = 11; (cpf.length - 1) > i; i++, p--) {
                v2 += cpf[i] * p;
            }

            v2 = ((v2 * 10) % 11);

            if (v2 == 10) {
                v2 = 0;
            }

            if (v2 != cpf[10]) {
                return false;
            } else {
                return true;
            }
        } else if (val.length == 18) {
            var cnpj = val.trim();

            cnpj = cnpj.replace(/\./g, '');
            cnpj = cnpj.replace('-', '');
            cnpj = cnpj.replace('/', '');
            cnpj = cnpj.split('');

            var v1 = 0;
            var v2 = 0;
            var aux = false;

            for (var i = 1; cnpj.length > i; i++) {
                if (cnpj[i - 1] != cnpj[i]) {
                    aux = true;
                }
            }

            if (aux == false) {
                return false;
            }

            for (var i = 0, p1 = 5, p2 = 13; (cnpj.length - 2) > i; i++, p1--, p2--) {
                if (p1 >= 2) {
                    v1 += cnpj[i] * p1;
                } else {
                    v1 += cnpj[i] * p2;
                }
            }

            v1 = (v1 % 11);

            if (v1 < 2) {
                v1 = 0;
            } else {
                v1 = (11 - v1);
            }

            if (v1 != cnpj[12]) {
                return false;
            }

            for (var i = 0, p1 = 6, p2 = 14; (cnpj.length - 1) > i; i++, p1--, p2--) {
                if (p1 >= 2) {
                    v2 += cnpj[i] * p1;
                } else {
                    v2 += cnpj[i] * p2;
                }
            }

            v2 = (v2 % 11);

            if (v2 < 2) {
                v2 = 0;
            } else {
                v2 = (11 - v2);
            }

            if (v2 != cnpj[13]) {
                return false;
            } else {
                return true;
            }
        } else {
            return false;
        }
    }
}
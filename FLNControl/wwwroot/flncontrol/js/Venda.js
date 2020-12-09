import { HTTPRequest } from "./lib/HTTPRequest.js";
import { FLNUtil } from "./lib/FLNUtil.js";

let itensPedido = [];
let clienteId;

window.FLNVenda = {
    listarProdutos: () => {
        let locationUrl = window.location.protocol + "//" + window.location.host;
        let requestUrl = new URL(locationUrl + "/Produto/Listar");

        HTTPRequest.get(requestUrl)
            .then((requestResponse) => {
                return requestResponse.json();
            })

            .then((requestResponse) => {
                if (!requestResponse.erro) {
                    let listaProdutos = document.querySelector("#s-prod");
                    requestResponse.produtos.forEach((item, index) => {
                        let produtoOpt = document.createElement('option');
                        produtoOpt.value = item.id;
                        produtoOpt.innerHTML += item.descricao + " (" + "R$ " + parseFloat(item.valorVenda).toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + ")";

                        listaProdutos.append(produtoOpt);
                    });
                } else {
                    toastr.warning('Nenhum produto foi encontrado. Cadastre um produto para adicionar ao pedido.');
                }
            })
            .catch(() => {
                toastr.error('Ocorreu um erro inesperado ao realizar de produtos. Tente novamente mais tarde.');
            }).finally(() => {
            });
    },

    adicionarProduto: async () => {
        let id = document.querySelector("#s-prod").value;
        let qtde = document.querySelector("#i-qtde-prod").value;
        if (!(parseInt(id) > 0)) {
            toastr.error('É necessário ao selecionar o produto para adicioná-lo ao carrinho.');
            return;
        }

        if (parseInt(qtde) > 0) {

            let locationUrl = window.location.protocol + "//" + window.location.host;
            let requestUrl = new URL(locationUrl + "/Produto/Consultar");
            let requestData = {
                termo: id,
                tipo: "codigo",
            };

            await HTTPRequest.post(requestUrl, requestData)
                .then((requestResponse) => {
                    return requestResponse.json();
                })
                .then((requestResponse) => {
                    if (!requestResponse.erro) {
                        var encontrado = false;
                        itensPedido.forEach((item) => {
                            if (item[0].id == requestResponse.produtos[0].id) {
                                item[1] = parseInt(item[1]) + parseInt(qtde);

                                encontrado = true;
                            }
                        });

                        if (!encontrado) {
                            itensPedido.push([requestResponse.produtos[0], qtde]);
                        }
                    } else {
                        toastr.warning('Produto não encontrado');
                    }
                })
                .catch(() => {
                    toastr.error('Ocorreu um erro inesperado ao adicionar o produto. Tente novamente mais tarde.');
                }).finally(() => {
                });

            let total = 0;
            let tabelaProdutos = document.querySelector("#tb-lista-produtos");
            let produtosRows = document.createElement("tbody");
            $("#tb-lista-produtos tbody tr").remove();

            itensPedido.forEach((item, index) => {
                let row = produtosRows.insertRow();
                let cellNome = row.insertCell(0);
                let cellQtde = row.insertCell(1);
                let cellPreco = row.insertCell(2);
                let cellPrecoTotal = row.insertCell(3);
                let cellAcoes = row.insertCell(4);

                cellNome.appendChild(document.createTextNode(item[0].descricao));
                cellQtde.appendChild(document.createTextNode(item[1]));
                cellPreco.appendChild(document.createTextNode("R$ " + parseFloat(item[0].valorVenda).toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })));
                cellPrecoTotal.appendChild(document.createTextNode("R$ " + parseFloat(item[0].valorVenda * parseInt(item[1])).toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })));

                let remover = document.createElement('input');
                remover.type = "button";
                remover.value = "Remover";
                remover.className = "btn btn-danger";
                remover.setAttribute("onClick", "FLNVenda.removerItem(" + item[0].id + ")");
                cellAcoes.appendChild(remover);

                total += (parseFloat(item[0].valorVenda) * parseInt(item[1]));
                tabelaProdutos.replaceChild(produtosRows, document.querySelector("#tb-lista-produtos").tBodies[0]);
            });


            document.querySelector("#sp-total").innerHTML = parseFloat(total).toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
        } else {
            toastr.error('É necessário ao menos uma unidade do produto para adicioná-lo ao carrinho.');
        }
    },

    removerItem: (id) => {
        if (itensPedido.length > 1) {
            for (let i = 0; i < itensPedido.length; i++) {
                if (itensPedido[i][0].id == id) {
                    itensPedido.splice(i, 1);
                }
            }
        }
        else {
            itensPedido.pop();
        }

        let total = 0;
        let tabelaProdutos = document.querySelector("#tb-lista-produtos");
        let produtosRows = document.createElement("tbody");
        $("#tb-lista-produtos tbody tr").remove();

        itensPedido.forEach((item, index) => {
            let row = produtosRows.insertRow();
            let cellNome = row.insertCell(0);
            let cellQtde = row.insertCell(1);
            let cellPreco = row.insertCell(2);
            let cellPrecoTotal = row.insertCell(3);
            let cellAcoes = row.insertCell(4);

            cellNome.appendChild(document.createTextNode(item[0].descricao));
            cellQtde.appendChild(document.createTextNode(item[1]));
            cellPreco.appendChild(document.createTextNode("R$ " + parseFloat(item[0].valorVenda).toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })));
            cellPrecoTotal.appendChild(document.createTextNode("R$ " + parseFloat(item[0].valorVenda * parseInt(item[1])).toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })));

            let remover = document.createElement('input');
            remover.type = "button";
            remover.value = "Remover";
            remover.className = "btn btn-danger";
            remover.setAttribute("onClick", "FLNVenda.removerItem(" + item[1] + ")");
            cellAcoes.appendChild(remover);

            total += (parseFloat(item[0].valorVenda) * parseInt(item[1]));
            tabelaProdutos.replaceChild(produtosRows, document.querySelector("#tb-lista-produtos").tBodies[0]);
        });


        document.querySelector("#sp-total").innerHTML = parseFloat(total).toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    },

    efetuarVenda: () => {
        let locationUrl = window.location.protocol + "//" + window.location.host;
        let requestUrl = new URL(locationUrl + "/Venda/Efetuar");

        let cpf = document.querySelector("#i-cpf").value;
        let nomeCli = document.querySelector("#i-nomecli").value;
        let endereco = document.querySelector("#i-endereco").value;
        let numero = document.querySelector("#i-numero").value;
        let bairro = document.querySelector("#i-bairro").value;
        let cidade = document.querySelector("#i-cidade").value;
        let uf = document.querySelector("#i-uf").value;
        let formaPagamento = document.querySelector("#s-forma-pag").value;
        let parcelas;
        if (formaPagamento == 1) {
            parcelas = 1;
        } else {
            parcelas = document.querySelector("#s-parcelas").value;
        }


        if (FLNUtil.isEmpty(cpf) || !FLNVenda.validaCpf(cpf)) {
            toastr.error('O CPF informado é inválido.');
            return;
        }

        if (FLNUtil.isEmpty(nomeCli)) {
            toastr.error('O nome do cliente deve ser preenchido.');
            return;
        }

        if (FLNUtil.isEmpty(endereco)) {
            toastr.error('O endereço do cliente deve ser preenchido.');
            return;
        }


        if (FLNUtil.isEmpty(numero)) {
            toastr.error('O numero do endereço do cliente deve ser preenchido.');
            return;
        }

        if (FLNUtil.isEmpty(bairro)) {
            toastr.error('O bairro do endereço do cliente deve ser preenchido.');
            return;
        }

        if (FLNUtil.isEmpty(cidade)) {
            toastr.error('A cidade do endereço do cliente deve ser preenchida.');
            return;
        }

        if (FLNUtil.isEmpty(uf)) {
            toastr.error('A UF do endereço do cliente deve ser preenchida.');
            return;
        }

        if (!(Array.isArray(itensPedido) && itensPedido.length)) {
            toastr.error('A lista de produtos deve conter ao menos um produto.');
            return;
        }

        let itens = [];
        itensPedido.forEach((item) => {
            itens.push([item[0].id, item[1]]);
        });

        let requestData = {
            cpf: cpf,
            nomeCli: nomeCli,
            endereco: endereco,
            numero: numero,
            bairro: bairro,
            cidade: cidade,
            uf: uf,
            formaPagamento: formaPagamento,
            parcelas: parcelas,
            listaProdutos: itens,
        };

        console.log(requestData);
        HTTPRequest.post(requestUrl, requestData)
            .then((requestResponse) => {
                return requestResponse.json();
            })

            .then((requestResponse) => {
                if (!requestResponse.erro) {
                    let listaProdutos = document.querySelector("#s-prod");
                    requestResponse.produtos.forEach((item, index) => {
                        let produtoOpt = document.createElement('option');
                        produtoOpt.value = item.id;
                        produtoOpt.innerHTML += item.descricao + " (" + "R$ " + parseFloat(item.valorVenda).toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + ")";

                        listaProdutos.append(produtoOpt);
                    });
                } else {
                    toastr.warning('Nenhum produto foi encontrado. Cadastre um produto para adicionar ao pedido.');
                }
            })
            .catch(() => {
                toastr.error('Ocorreu um erro inesperado ao realizar de produtos. Tente novamente mais tarde.');
            }).finally(() => {
            });
    },

    validaCpf: (cpf) => {
        cpf = cpf.replace(/\D/g, '');
        if (cpf.toString().length != 11 || /^(\d)\1{10}$/.test(cpf)) return false;
        var result = true;
        [9, 10].forEach(function (j) {
            var soma = 0, r;
            cpf.split(/(?=)/).splice(0, j).forEach(function (e, i) {
                soma += parseInt(e) * ((j + 2) - (i + 1));
            });
            r = soma % 11;
            r = (r < 2) ? 0 : 11 - r;
            if (r != cpf.substring(j, j + 1)) result = false;
        });
        return result;
    },
}
import { HTTPRequest } from "./lib/HTTPRequest.js";
import { FLNUtil } from "./lib/FLNUtil.js";

let itensPedido = [];
let clienteId;

window.FLNVenda = {
    consultar: () => {
        let termo = document.querySelector("#i-termo-busca").value;
        let tipo = document.querySelector("input[name=rd-tipo-termo]:checked").value;

            if (FLNUtil.isEmpty(termo)) {
                toastr.error('O Campo <b>Termo de Pesquisa</b> precisa ser preenchido.');
            }
            else if (termo.trim().length < 3) {
                toastr.error('O Campo <b>Termo de Pesquisa</b> precisa ter no mínimo <b>3</b> caracteres.');
            }
            else {
                if (FLNUtil.isEmpty(tipo)) {
                    toastr.error('O Campo <b>Tipo</b> precisa ser selecionado!');
                } else {
                    document.querySelector("#rowResultado").style.display = "none";
                    let requestData = {
                        termo: termo,
                        tipo: tipo,
                    }

                    HTTPRequest.post('/Venda/Consultar', requestData)
                        .then((requestResponse) => {
                            return requestResponse.json();
                        })

                        .then((requestResponse) => {
                            if (requestResponse.total > 0) {
                                document.querySelector("#rowResultado").style.display = "block";
                                $('#resultadoBusca').DataTable().destroy();
                                $('#resultadoBusca').DataTable({
                                    "data": requestResponse[0],
                                    "destroy": true,
                                    "retrieve": true,
                                    "columns": [
                                        { data: data[0] },
                                        { data: data[2] },
                                        { data: data[1] },
                                    ],
                                    "language": {
                                        "sEmptyTable": "Nenhum registro encontrado",
                                        "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                                        "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                                        "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                                        "sInfoPostFix": "",
                                        "sInfoThousands": ".",
                                        "sLengthMenu": "_MENU_ resultados por página",
                                        "sLoadingRecords": "Carregando...",
                                        "sProcessing": "Processando...",
                                        "sZeroRecords": "Nenhum registro encontrado",
                                        "sSearch": "Pesquisar",
                                        "oPaginate": {
                                            "sNext": "Próximo",
                                            "sPrevious": "Anterior",
                                            "sFirst": "Primeiro",
                                            "sLast": "Último"
                                        },
                                        "oAria": {
                                            "sSortAscending": ": Ordenar colunas de forma ascendente",
                                            "sSortDescending": ": Ordenar colunas de forma descendente"
                                        },
                                        "select": {
                                            "rows": {
                                                "_": "Selecionado %d linhas",
                                                "0": "Nenhuma linha selecionada",
                                                "1": "Selecionado 1 linha"
                                            }
                                        },
                                        "buttons": {
                                            "copy": "Copiar para a área de transferência",
                                            "copyTitle": "Cópia bem sucedida",
                                            "copySuccess": {
                                                "1": "Uma linha copiada com sucesso",
                                                "_": "%d linhas copiadas com sucesso"
                                            }
                                        }
                                    }
                                });
                                $('#resultadoBusca tbody tr').css("cursor", "pointer");
                            } else {
                                toastr.warning('Nenhum dado encontrado')
                            }
                        })
                        .catch((requestResponse) => {
                            console.log(requestResponse);
                            toastr.error('Ocorreu um erro inesperado ao realizar a busca. Tente novamente mais tarde.');
                        }).finally(() => {

                        });
                }
            }
    },

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

        let diaVencimento = document.querySelector("#s-vencimento").value;
        let vencimento;
        if (formaPagamento == 1) {
            vencimento = 0;
        } else {
            vencimento = diaVencimento;
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
            vencimento: vencimento,
            listaProdutos: itens,
        };

        HTTPRequest.post(requestUrl, requestData)
            .then((requestResponse) => {
                return requestResponse.json();
            })

            .then((requestResponse) => {
                console.log(requestResponse);
                if (requestResponse.id > 0) {
                    window.location.href = "/Venda/Visualizar/" + requestResponse.id;
                } else {
                    toastr.warning('Nenhum produto foi encontrado. Cadastre um produto para adicionar ao pedido.');
                }
            })
            .catch(() => {
                toastr.error('Ocorreu um erro inesperado ao realizar a venda de produtos. Tente novamente mais tarde.');
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
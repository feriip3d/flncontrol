import { HTTPRequest } from "./lib/HTTPRequest.js";
import { FLNUtil } from "./lib/FLNUtil.js";

try {
document.querySelector('#f-consultar')
    .addEventListener("submit", (e) => {
        e.preventDefault();
        FLNProduto.consultar();
    });
} catch (e) {
    
}

try {
    document.querySelector('#f-cadastrar')
        .addEventListener("submit", (e) => {
            e.preventDefault();
            FLNProduto.cadastrar();
        });
} catch (e) {

}

document.addEventListener('keyup', function(event) {
    if (event.target.id == 'i-valor-compra')
        var target = "#i-valor-compra";
    else if (event.target.id == 'i-valor-venda')
        var target = "#i-valor-venda";
    else
        return;

    document.querySelector(target).value = document.querySelector(target).value.replace(/[^\d,]/g, '');
});

document.addEventListener('keydown', function(event) {
    if(event.target.id == 'i-valor-compra')
        var target = "#i-valor-compra";
    else if(event.target.id == 'i-valor-venda')
        var target = "#i-valor-venda";
    else
        return;

    document.querySelector(target).value = document.querySelector(target).value.replace(/[^\d,]/g, '');


    if(/[0-9]/.test(event.key)){
        var i = document.querySelector(target).value.length;
        if(i >= 2) {
            if (i == 2) var dist = 1; else dist = 2;
            document.querySelector(target).value = document
                    .querySelector(target)
                    .value
                    .replace(',', '')
                    .substring(0, document.querySelector(target).value.length - dist)
                + ',' + document
                    .querySelector(target)
                    .value
                    .replace(',', '')
                    .replace(/[^\d]/,'')
                    .substring(document.querySelector(target).value.length - dist);
        }
    }
});

window.FLNProduto = {
    consultar: () => {
        let termo = document.querySelector("#i-termo-busca").value;
        let tipo = document.querySelector("input[name=rd-tipo-termo]:checked").value;

        if (tipo == 'all') {
            HTTPRequest.get('/Produto/ListarTodos')
                .then((requestResponse) => {
                    return requestResponse.json();
                })

                .then((requestResponse) => {
                    if (requestResponse.total > 0) {
                        document.querySelector("#rowResultado").style.display = "block";
                        $('#resultadoBusca').DataTable().destroy();
                        $('#resultadoBusca').DataTable({
                            "data": requestResponse.produtos,
                            "destroy": true,
                            "retrieve": true,
                            "columns": [
                                { data: "id" },
                                { data: "categoria" },
                                { data: "descricao" },
                                { data: "marca" },
                                {
                                    "data": "valorVenda",
                                    "render": (data, type, row, meta) => {
                                        return "R$ " + parseFloat(data).toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })
                                    }
                                },
                                {
                                    "data": "valorCompra",
                                    "render": (data, type, row, meta) => {
                                        return "R$ " + parseFloat(data).toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })
                                    }
                                },
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
        else {
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

                    HTTPRequest.post('/Produto/Consultar', requestData)
                        .then((requestResponse) => {
                            return requestResponse.json();
                        })

                        .then((requestResponse) => {
                            if (requestResponse.total > 0) {
                                document.querySelector("#rowResultado").style.display = "block";
                                $('#resultadoBusca').DataTable().destroy();
                                $('#resultadoBusca').DataTable({
                                    "data": requestResponse.produtos,
                                    "destroy": true,
                                    "retrieve": true,
                                    "columns": [
                                        { data: "id" },
                                        { data: "categoria" },
                                        { data: "descricao" },
                                        { data: "marca" },
                                        {
                                            "data": "valorVenda",
                                            "render": (data, type, row, meta) => {
                                                return "R$ " + parseFloat(data).toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })
                                            }
                                        },
                                        {
                                            "data": "valorCompra",
                                            "render": (data, type, row, meta) => {
                                                return "R$ " + parseFloat(data).toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })
                                            }
                                        },
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
        }
    },

    cadastrar: () => {
        let desc = document.querySelector("#i-desc").value;
        let categ = document.querySelector("#i-categ").value;
        let brand = document.querySelector("#i-brand").value;
        let valorVenda = document.querySelector("#i-valor-venda").value;
        let valorCompra = document.querySelector("#i-valor-compra").value;
        
        if(FLNUtil.isEmpty(desc)) {
            toastr.error('O campo <b>Descrição</b> é obrigatório!');
            return;
        }

        if(FLNUtil.isEmpty(categ)) {
            toastr.error('O campo <b>Categoria</b> é obrigatório!');
            return;
        }

        if(FLNUtil.isEmpty(brand)) {
            toastr.error('O campo <b>Marca</b> é obrigatório!');
            return;
        }

        if(FLNUtil.isEmpty(valorCompra) || parseInt(valorCompra) <= 0) {
            toastr.error('O campo <b>Valor de Compra</b> é obrigatório!');
            return;
        }

        if(FLNUtil.isEmpty(valorVenda) || parseInt(valorVenda) <= 0) {
            toastr.error('O campo <b>Valor de Venda</b> é obrigatório!');
            return;
        }
        let requestData = {
            desc: desc,
            categ: categ,
            marca: brand,
            precoVenda: valorVenda,
            precoCompra: valorCompra,
        }

        HTTPRequest.post('/Produto/Novo', requestData)
            .then((requestResponse) => {
                return requestResponse.json();
            })
            .then((requestResponse) => {
                if(requestResponse.success) {
                    toastr.success('Cadastro de produto efetudado. <a href="/Produto/Visualizar/'+requestResponse.id+'"><b>Clique aqui</b></a> para visualizar o produto')
                } else {
                    toastr.warning('Ocorreu um erro ao cadastrar o produto. Verifique os campos e tente novamente.');
                }
            })
            .catch((requestResponse) => {
                console.log(requestResponse);
                toastr.error('Ocorreu um erro inesperado ao cadastrar o produto. Tente novamente mais tarde.');
            }).finally(() => {
                FLNProduto.limpar('f-cadastrar');
            });
    },

    alterar: () => {
        let id = document.querySelector("#i-prod-codigo").value;
        let desc = document.querySelector("#i-desc").value;
        let categ = document.querySelector("#i-categ").value;
        let brand = document.querySelector("#i-brand").value;
        let valorVenda = document.querySelector("#i-valor-venda").value;
        let valorCompra = document.querySelector("#i-valor-compra").value;

        if (FLNUtil.isEmpty(desc)) {
            toastr.error('O campo <b>Descrição</b> é obrigatório!');
            return;
        }

        if (FLNUtil.isEmpty(categ)) {
            toastr.error('O campo <b>Categoria</b> é obrigatório!');
            return;
        }

        if (FLNUtil.isEmpty(brand)) {
            toastr.error('O campo <b>Marca</b> é obrigatório!');
            return;
        }

        if (FLNUtil.isEmpty(valorCompra) || parseInt(valorCompra) <= 0) {
            toastr.error('O campo <b>Valor de Compra</b> é obrigatório!');
            return;
        }

        if (FLNUtil.isEmpty(valorVenda) || parseInt(valorVenda) <= 0) {
            toastr.error('O campo <b>Valor de Venda</b> é obrigatório!');
            return;
        }
        let requestData = {
            id: id,
            desc: desc,
            categ: categ,
            marca: brand,
            precoVenda: valorVenda,
            precoCompra: valorCompra,
        }

        HTTPRequest.post('/Produto/Alterar/Gravar', requestData)
            .then((requestResponse) => {
                return requestResponse.json();
            })
            .then((requestResponse) => {
                if (requestResponse.success) {
                    window.location.href = "/Produto/Visualizar/" + id;
                } else {
                    toastr.warning('Ocorreu um erro ao cadastrar o produto. Verifique os campos e tente novamente.');
                }
            })
            .catch((requestResponse) => {
                console.log(requestResponse);
                toastr.error('Ocorreu um erro inesperado ao cadastrar o produto. Tente novamente mais tarde.');
            }).finally(() => {
            });
    },
    
    inicio: () => {
        window.location.href = "/";
    },

    telaConsultar: () => {
        window.location.href = "/Produto";
    },

    telaAlterar: (id) => {
        window.location.href = "/Produto/Alterar/"+id;
    },

    excluir: (id) => {
        HTTPRequest.get('/Produto/Excluir/'+ id)
            .then((requestResponse) => {
                return requestResponse.json();
            })
            .then((requestResponse) => {
                if(requestResponse.success) {
                    toastr.success('O produto foi deletado!');
                    window.location.href="/Produto"
                } else {
                    toastr.warning('Ocorreu um erro ao excluir o produto.');
                }
            })
            .catch((requestResponse) => {
                console.log(requestResponse);
                toastr.error('Ocorreu um erro inesperado ao excluir o produto. Tente novamente mais tarde.');
            }).finally(() => {
        });
    },
    
    limpar: (form) => {
        document.querySelector("#" + form).reset();
    }
}
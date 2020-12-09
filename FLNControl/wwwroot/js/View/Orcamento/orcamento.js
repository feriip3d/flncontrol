function AtivaInformaçõesFiado (element) {
    var corpo = document.querySelector('#corpoformulario ');
    var elementDias = document.querySelector('#elementDiasFiado ');
    var elementValor = document.querySelector('#elementValorFiado ');
    var elementParcelas = document.querySelector('#elementParcelasFiado ');
    element.checked = element.checked == true;

    if (element.checked) {
        elementDias.hidden = false;
        elementValor.hidden = false;
        elementParcelas.hidden = false;
    }
    else {
        elementDias.hidden = true;
        elementValor.hidden = true;
        elementParcelas.hidden = true;
    }
}

function gravarNovoOrcamento() {
    $("div").each(function (i) {
        $("#quantidadeProdutos").val();
    })
   
}

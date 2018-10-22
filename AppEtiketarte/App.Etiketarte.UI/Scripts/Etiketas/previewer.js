var config = {
    text: '',
    font: '',
    maxLength: 10,
    fontColor: '#fff',
    imageId: '',
    imageRoute: '',
    formId: '',
    qty: 1,
    price: 2500,
    orderCost: 0,
};

$(document).ready(function () {
    initPage();
});

$('#control-formas').click(function () {
    showOptions('control-formas', 'opciones__formas');
});

$('#control-disenos').click(function () {
    showOptions('control-disenos', 'opciones__disenos');
});

$('#control-texto').click(function () {
    showOptions('control-texto', 'opciones__texto');
});

$(document).on('click', '.forma', function () {
    //setFormId($(this).attr('id'));
    //decorateFormSelected($(this));
    printImagePreview();
    validateSteps();
});

$(document).on('click', '.arte', function () {
    //setImage(this.id);
    //decorateImageSelected($(this));
    //printImagePreview();
    //validateSteps();
});

$('#input-demo').keyup(function () {
    setDemoText(this);
    printTextPreview();
});

$('#input-demo').change(function () {
    validateSteps();
});

$('#fuentes').change(function () {
    setTextFont();
});

$('.show-color').click(function () {
    setTextColor(this.id);
});

//$('#cantidad-etiquetas').change(function () {
//    setQty($(this).val());
//    setOrderCost();
//    printOrderCost();
//});

function initPage() {
    $('.opciones__disenos').hide();
    $('.opciones__texto').hide();
    $('.continuar-diseno').hide();
    $('.pedido').hide();
    $('.alerta-texto').hide();
    //$('#cantidad-etiquetas').val(config.qty);
    setOrderCost();
}

function showOptions(controlId, optionClass) {
    // Resetea los elementos
    $('.opciones').hide();
    $('.controles__elemento').removeClass('control-activo');

    // Muestra las opciones y resalta el control activo
    $('.' + optionClass).show();
    $('#' + controlId).addClass('control-activo');
}

function setFormId(id) {
    config.formId = id;
}

function setImage(id) {
    config.imageId = id;
    config.imageRoute = id;
}

function setDemoText(element) {
    config.text = $(element).val();
}

function decorateFormSelected(element) {
    $('.forma').removeClass('forma-selected');
    element.addClass('forma-selected');
}

function decorateImageSelected(element) {
    $('.arte').removeClass('diseno-selected');
    element.addClass('diseno-selected');
}

function showImagePreview() {
    $('.etiqueta-prev').attr('src', config.imageRoute);
}

function printImagePreview() {
    if (config.formId != '' && config.imageId != '') {
        $('.prevista__titulo').hide();
        showImagePreview();
    }
}

function showTextPreview() {
    $('#texto-demo').text(config.text);
    $('#texto-demo').show();
}

function hideTextPreview() {
    $('#texto-demo').hide();
}

function printTextPreview() {
    if (!exceedMaxLength()) {
        if (config.formId != '' && config.imageId != '') {
            showTextPreview();
        }
        $('.alerta-texto').hide();
    } else {
        hideTextPreview();
        $('.alerta-texto').show();
    }
}

function setTextFont() {
    config.font = $('#fuentes').val();
}

function setTextColor(id) {
    var color = $('#' + id).css('backgroundColor');
    $('#texto-demo').css('color', color);
}

function exceedMaxLength() {
    return (config.text.length > config.maxLength);  
}

function setQty(qty) {
    config.qty = qty;
}

function setOrderCost() {
    config.orderCost = config.qty * config.price;
}

function printOrderCost() {
    $('.pedido__costo').text(config.orderCost);
}

function validateSteps() {
    if (config.formId != '' && config.imageId != '') {
        if (config.text != '') {
            $('.pedido').show();
            printOrderCost();
        } else {
            $('.pedido').hide();
        }
    }
}

var data = {
    LineCharacters: 0,
    NumberOfLines: 1,
    fontSizeMin: '',
    fontSizeMax: '',
    fontSize: '',
    sizesCollection: new Array(),
}

$(document).ready(function () {
    init();
});

$('#imgInp').change(function () {
    showImageActions();
});

$('#maxCharPerLine').change(function () {
    data.LineCharacters = $(this).val();
    fillDraggable();
    showImageActions();
})

$('#lineNumbers').change(function () {
    data.NumberOfLines = $(this).val();
    fillDraggable();
});

$('#minSizeFont').change(function () {
    data.fontSizeMin = $(this).val();
    setSizesCollection();
});

$('#maxSizeFont').change(function () {
    data.fontSizeMax = $(this).val();
    setSizesCollection();
});

$('#fontSizes').change(function () {
    data.fontSize = $(this).val();
    setFontSize();
});

function init() {
    $('#draggable').hide();
    $('.text-actions').hide();
    $('#lineNumbers').val(data.NumberOfLines);
    data.fontSizeMin = $('#minSizeFont').val();
    data.fontSizeMax = $('#maxSizeFont').val();
    setSizesCollection();
}

function fillDraggable() {
    var el = document.getElementById('draggableLength');

    el.innerText = "";

    if (data.NumberOfLines > 1) {
        for (var i = 0; i < data.NumberOfLines; i++) {

            for (var j = 0; j < data.LineCharacters; j++) {
                el.innerText += 'M';
            }

            el.innerHTML += '<br>';
        }
    }
    else {
        for (var i = 0; i < data.LineCharacters; i++) {
            el.innerText += 'M';
        }
    }
}

function showImageActions() {

    var src = $('#img-upload').attr('src');

    if (typeof src !== typeof undefined && src !== false) {
        if (data.LineCharacters > 0) {
            $('.arte-preview-alert').hide();
            $('#draggable').show();
            $('.text-actions').show();
        }
    }
}

function setSizesCollection() {
    $('.font-size-option').remove();

    if (data.fontSizeMin != '' && data.fontSizeMax != '') {
        data.sizesCollection = fillSizesCollection();
        $.each(data.sizesCollection, function (index, value) {
            $('#fontSizes').append('<option class="font-size-option">' + value + 'px' + '</option>');
        });
    }
}

function fillSizesCollection() {
    var sizes = new Array();

    if (data.fontSizeMin != '' && data.fontSizeMax != '') {
        for (var i = parseInt(data.fontSizeMin); i <= parseInt(data.fontSizeMax); i++) {
            sizes.push(i);
        }
    }

    return sizes;
}

function setFontSize() {
    $('#demoText, #draggableLength').css('font-size', data.fontSize);
}

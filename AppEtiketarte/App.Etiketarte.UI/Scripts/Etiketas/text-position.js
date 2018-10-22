'use strict';

var position = $('#draggable').position();
var align = 'left';

$('#draggable').draggable({
    containment: 'parent',
    stop: function (event, ui) {
        position = $(this).position();
    }
});

$('#setPosition').click(function () {
    setPosition();
});

$('#btnAlignLeft').click(function () {
    $('.text').css('text-align', 'left');
    align = 'left';
    setAlignText();
});

$('#btnAlignCenter').click(function () {
    $('.text').css('text-align', 'center');
    align = 'center';
    setAlignText();
});

$('#btnAlignRight').click(function () {
    $('.text').css('text-align', 'right');
    align = 'right';
    setAlignText();
});

function setPosition() {
    $('.text-container').css({
        position: 'absolute',
        top: position.top + 'px',
        left: position.left + 'px'
    });

    $('#textTop').val(position.top);
    $('#textLeft').val(position.left);
    setAlignText();
    $('#containerWidth').val($('#draggable').width());

    setWidth();
    fillDemoText($('#maxCharPerLine').val() * $('#lineNumbers').val());
}

function setAlignText() {
    $('#textAlign').val(align);
}

function showPosition(tTop, tLeft, tAlign) {
    $('#draggable').css({ 'top': parseInt(tTop), 'left': parseInt(tLeft) });

    $('.text-container').css({
        position: 'absolute',
        top: tTop + 'px',
        left: tLeft + 'px'
    });

    $('.text').css('text-align', tAlign);

    setWidth();
    fillDemoText($('#maxCharPerLine').val() * $('#lineNumbers').val());
}

function setWidth() {
    $('.text-container').css('width', $('.length').width());
};

function fillDemoText(length) {
    var randomText = "PRUEBA".split('');
    var stringDemo = [];
    var limit = 0;

    for (var x = 0; x < length ; x++) {
        if (limit >= randomText.length) {
            limit = 0;
        }

        stringDemo.push(randomText[limit]);
        limit++;
    }

    $('#demoText').text(stringDemo.join(''));
}
var steps = {
    reviewIsDone: false,
    userIsLogin: false,
    addressIsSet: false,
    shippingMethodIsSet: false
}

$(document).ready(function () {
    init();
});

$('.step-item').click(function () {
    showCurrentStep(this.id, $(this).attr('data-target'));
});

$('.step-submit').click(function () {
    stepSubmit(this.id);
    disabledSteps();
});

function init() {
    showCurrentStep('step-review', $('#step-review').attr('data-target'));
    disabledSteps();
}

function showCurrentStep(stepId, stepContent) {
    $('.step-item').removeClass('current-step');
    $('#' + stepId).addClass('current-step');
    $('.cart-content').hide();
    $('#' + stepContent).show();
}

function stepSubmit(id) {
    switch (id) {
        case 'review-submit':
            steps.reviewIsDone = true;
            showCurrentStep('step-login', 'inicio-sesion');
            break;
        case 'login-submit':
            steps.userIsLogin = true;
            showCurrentStep('step-address', 'direccion');
            break;
        case 'address-submit':
            steps.addressIsSet = true;
            showCurrentStep('step-shipping', 'envio');
            break;
        case 'shipping-submit':
            steps.shippingMethodIsSet = true;
            showCurrentStep('step-checkout', 'finalizar');
            break;
    }
}

function disabledSteps() {
    $('#step-login, #step-address, #step-shipping, #step-checkout').css('pointer-events', 'none');

    if (steps.reviewIsDone) {
        $('#step-login').css('pointer-events', 'auto');
    } 

    if (steps.userIsLogin) {
        $('#step-address').css('pointer-events', 'auto');
    }

    if (steps.addressIsSet) {
        $('#step-shipping').css('pointer-events', 'auto');
    }

    if (steps.shippingMethodIsSet) {
        $('#step-checkout').css('pointer-events', 'auto');
    }
}



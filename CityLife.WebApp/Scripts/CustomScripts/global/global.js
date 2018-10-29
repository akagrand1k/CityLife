/*TOASTS NOTIFY MESSAGES*/
function SuccessTost(heading, text) {
    $.toast().reset('all');
    $("body").removeAttr('class');
    $.toast({
        heading: heading,
        text: text,
        position: 'top-right',
        loaderBg: '#e69a2a',
        icon: 'success',
        hideAfter: 3500,
        stack: 6
    });
    return false;
}

function FailureTost(heading, text) {
    $.toast().reset('all');
    $("body").removeAttr('class');
    $.toast({
        heading: heading,
        text: text,
        position: 'top-right',
        loaderBg: '#e69a2a',
        icon: 'error',
        hideAfter: 3500,
        stack: 6
    });
    return false;
}



function SomethingWrongTost() {
    $.toast().reset('all');
    $("body").removeAttr('class');
    $.toast({
        heading: 'Упс!Что то пошло не так!',
        text: '',
        position: 'top-right',
        loaderBg: '#e69a2a',
        icon: 'error',
        hideAfter: 3500,
        stack: 6
    });
    return false;
}
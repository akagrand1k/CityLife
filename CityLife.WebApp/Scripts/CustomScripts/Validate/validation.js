$(function () {

    //Form validation

    /*Validate rules*/

    $.validator.addMethod('EmailValidate', function (value, element) {
        return this.optional(element) || /^[0-9]{3}\-[0-9]{3}$/gi.test(value);
    }, 'Введите корректное значение');

    var addStepRules = function (step, selector, rules) {
        $(selector).rules('add', rules);
    };

    //Tooltipster initialize
    $('form input').tooltipster({
        trigger: 'custom',
        position: 'right',
        onlyOne: false,
    });



    //Profile password change form
    $("#profile-passchange").validate({
        errorPlacement: function (error, element) {
            var ele = $(element),
                err = $(error),
                msg = err.text();
            if (msg != null && msg !== '') {
                ele.tooltipster('content', msg);
                ele.tooltipster('open');
            }
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass(errorClass).addClass(validClass).tooltipster('close');
        },
        rules: {
            'new-pass': {
                minlength:8
            },
            'confirm-pass': {
                minlength: 8,
                equalTo: '#new-pass'
            }
        },
        messages: {
            'confirm-pass': {
                minlength: 'Пароль должен быть не меньше 8 символов!',
                equalTo: 'Пароли не совпадают.'
            },
            'new-pass': {
                minlength: 'Пароль должен быть не меньше 8 символов!',
            },
        },
        submitHandler: function (form) {
            alert('BUTTON CLICK!');
            var oldpass = $('#old-pass').val();
            var newpass = $('#new-pass').val();
            $.ajax({
                url: "/User/Profile/PasswordChange",
                type: "POST",
                data: {
                    'currentPassword': oldpass,
                    'newPassword': newpass,
                },
                success: function (data) {
                    if (data == 'success') {
                        SuccessTost('Смена пароля',
                            'Ваш пароль успешно изменен!');
                    }
                    else {
                        FailureTost('При смене пароля произошла ошибка!', 'Проверьте текущий пароль!');
                    }
                },
                error: function (er) {
                    SomethingWrongTost();
                }
            });
            return false;
        },
    });

    //addStepRules(1, '#old-pass', {
    //    required: true,
    //    minlength: 8,
    //    messages: {
    //        required: 'Введите корректное значение',
    //        minlength: 'Пароль должен быть не меньше 8 символов!'
    //    }
    //});


    //Profile change email validate form
    $("#profile-emailchange").validate({
        errorPlacement: function (error, element) {
            var ele = $(element),
                err = $(error),
                msg = err.text();
            if (msg != null && msg !== '') {
                ele.tooltipster('content', msg);
                ele.tooltipster('open');
            }
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass(errorClass).addClass(validClass).tooltipster('close');
        },
        rules: {
            'user-email': {
                required:true,
                email:true,
            },
        },
        messages: {
            'user-email': {
                required:'Введите корректное значение',
                email: 'Введите корректный email!',
            },
        },
        submitHandler: function (form) {
            console.log('was clicked');
            var emailVal = $('#user-email').val();
            $.ajax({
                url: "/User/Profile/EmailChange",
                type: "POST",
                data: {
                    'email': emailVal,
                },
                success: function (data) {
                    if (data == 'success') {
                        SuccessTost('На ваш email было отправлено письмо!',
                            'Через 5 секунд, вы будете перемещены на страницу авторизации.');
                        window.setTimeout(window.location.href = '/account/login',5000);
                    }
                    else {
                        FailureTost('Произошла ошибка!', 'Повторите позже!Если ошибка повториться, обратитесь к администратору!');
                    }
                },
                error: function (er) {
                    SomethingWrongTost();
                }

            });
            return false;
        },
    });



});
$("#uploadAvatar").change(function () {
    var data = new FormData();
    var files = $("#uploadAvatar").get(0).files;
    if (files.length > 0) {
        data.append("Avatar", files[0]);
    }
    $.ajax({
        url: "/User/Profile/UploadAvatar",
        type: "POST",
        processData: false,
        contentType: false,
        data: data,
        success: function (data) {
            $('#userAvatar').attr('src', data);
            $('#userAvatarSmall').attr('src', data);
        },
        error: function (er) {
            alert(er);
        }

    });
});

//Edit Profile
$('#editProfile').click(function () {

    GetProfile();

    function GetProfile() {
        $.ajax({
            url: '/User/Profile/EditProfile',
            type: 'GET',
            cache: false,
            success: function (profile) {
                //var miliBrithDate = profile.BrithDate.replace(/\/Date\((-?\d+)\)\//, '$1');
                //var parseDate = new Date(parseInt(miliBrithDate));

                $('#GivenName').val(profile.GivenName);
                $('#FamilyName').val(profile.FamilyName);

                $('#BrithDate').val(profile.BrithDate);
                $('#FirstPhone').val(profile.FirstPhone);
                $('#SecondPhone').val(profile.SecondPhone);
            }
        });
    }

});


//Save profile ajax
$('#saveProfile').click(function () {

    var GivenName =  $('#GivenName').val();
    var FamilyName = $('#FamilyName').val();
    var BrithDAte = $('#BrithDate').val();
    var FirstPhone = $('#FirstPhone').val();
    var SecondPhone = $('#SecondPhone').val();

    $.ajax({
        type: "POST",
        url: '/User/Profile/EditProfile',
        data: {
            'GivenName': GivenName,
            'FamilyName': FamilyName,
            'BrithDate': BrithDAte,
            'FirstPhone': FirstPhone,
            'SecondPhone': SecondPhone,
        },
        error: function (xhr, status, error) {
            //...
        },
        success: function (result) {
            // result.FirstName
        }

    });
})


//birthday datepicker
CallDateTimePicker();
function CallDateTimePicker() {
    $('#birthdayDatePicker').datetimepicker({
        format: 'DD.MM.YYYY',
        inline: false,
        sideBySide: true,
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar",
            up: "fa fa-arrow-up",
            down: "fa fa-arrow-down"
        },
    })
    var localeString = 'ru';
    $('#birthdayDatePicker').data("DateTimePicker").locale(localeString);
}


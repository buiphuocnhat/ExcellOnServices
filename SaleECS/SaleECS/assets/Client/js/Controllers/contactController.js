var contact = {
    init: function () {
        contact.registerEvents();
    },
    registerEvents: function () {
        $('#btnSubmit').off('click').on('click', function () {
            var fullname = $('#fullname').val();
            var service = $('#service').val();
            var email = $('#email').val();
            var content = $('#content').val();
                                                        
            $.ajax({
                url: '/Contact/Submit',
                type: 'POST',
                dataType: 'json',
                data: {
                    fullname: fullname,
                    service: service,
                    email: email,
                    content: content
                },
                success: function (res) {
                    if (res.status == true) {
                        window.alert("Submit OK!");
                    }
                }
            });                 
        });
    }
}
contact.init();
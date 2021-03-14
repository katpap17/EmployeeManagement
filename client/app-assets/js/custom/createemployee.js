
$(document).ready(function(){

    $("#btn_create").on("click", function(){
        let data = {
            "name": $("#name").val(),
            "lastname": $("#lastname").val()
         };
        $.ajax({
            type: 'POST',
            url: 'https://localhost:44337/api/Employees',
            contentType: 'application/json',
            data: JSON.stringify(data),
        }).done(function () {
            window.location = '/Employees.html';
        }).fail(function (msg) {
            alert('Something went wrong.');
        });

    });

    

});

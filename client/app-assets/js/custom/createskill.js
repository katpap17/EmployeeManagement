
$(document).ready(function(){

    $("#btn_create").on("click", function(){
        let data = {
            "name": $("#name").val(),
            "description": $("#description").val()
         };
        $.ajax({
            type: 'POST',
            url: 'https://localhost:44337/api/Skills',
            contentType: 'application/json',
            data: JSON.stringify(data),
        }).done(function () {
            window.location = '/Skills.html';
        }).fail(function (msg) {
            alert('Something went wrong.');
        });

    });

});

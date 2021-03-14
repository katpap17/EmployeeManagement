$(document).ready(function(){
	
    var id = window.location.href.split('=')[1];
    $.get('https://localhost:44337/api/Skills/'+id, function(data){
        if(data != '') {
			$('#skill').append(
                '<tr>'+'\n'+
                '	<td class="center-align">Id</td>'+'\n'+
                '	<td class="users-view-username">'+data.id+'</td>'+'\n'+
                '</tr>'+
                '\n' +
                '<tr>'+'\n'+
                '	<td class="center-align">Name</td>'+'\n'+
                '	<td class="users-view-username">'+data.name+'</td>'+'\n'+
                '</tr>'+'\n' +
                '<tr>'+'\n'+
                '	<td class="center-align">Date Created:</td>'+'\n'+
                '	<td class="users-view-username">'+data.dateCreated+'</td>'+'\n'+
                '</tr>'+'\n' +
                '<tr>'+'\n'+
                '	<td class="center-align">Description:</td>'+'\n'+
                '	<td class="users-view-username">'+data.description+'</td>'+'\n'+
                '</tr>'+'\n' 
            );
		}else{
			alert('Skill not fount!')
		}
	},"json").fail(function(error){
		alert(error.responseText);
	});

    $("#update").on("click", function(){
        let data = {
            "id": id,
            "name": $("#name").val(),
            "description": $("#description").val()
         };
        $.ajax({
            type: 'PUT',
            url: 'https://localhost:44337/api/Skills/'+id,
            contentType: 'application/json',
            data: JSON.stringify(data),
        }).done(function () {
            window.location = '/Skill.html?id='+id;
        }).fail(function (msg) {
            alert('Something went wrong.');
        });

    });

    $("#delete").on("click", function(){
        let data = {
            "id": id,
         };
        $.ajax({
            type: 'DELETE',
            url: 'https://localhost:44337/api/Skills/'+id,
            contentType: 'application/json',
            data: JSON.stringify(data), 
        }).done(function () {
            window.location = '/Skills.html';
        }).fail(function (msg) {
            alert('Something went wrong.');
        });

    });
});

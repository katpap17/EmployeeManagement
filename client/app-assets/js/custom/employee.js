$(document).ready(function(){
	
    // Get employee data
    var id = window.location.href.split('=')[1];
    $.get('https://localhost:44337/api/Employees/'+id, function(data){
        if(data != '') {
			$('#employee').append(
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
                '	<td class="center-align">LastName:</td>'+'\n'+
                '	<td class="users-view-username">'+data.lastname+'</td>'+'\n'+
                '</tr>'+'\n' +
                '<tr>'+'\n'+
                '	<td class="center-align">Hired at:</td>'+'\n'+
                '	<td class="users-view-username">'+data.hireDate+'</td>'+'\n'+
                '</tr>'+'\n'  
            );
		}else{
			alert('Skill not fount!');
		}
	},"json").fail(function(error){
		alert(error.responseText);
	});

    // Get employee skills
    $.get('https://localhost:44337/api/Employees/'+id+'/getskills', function(data){
        if(data != '') {
            for(var i=0; i<data.length; i++){
                $('#skillset').append(
                    '<tr>'+'\n'+
                    '	<td class="users-view-username">'+data[i].name+'</td>'+'\n'+
                    '</tr>'
                );
            }
		}else{
			alert('No skills fount!');
		}
	},"json").fail(function(error){
		alert(error.responseText);
	});

    // Get all skills
    $.get('https://localhost:44337/api/Skills', function(data){
            if(data != '') {
                var select = '<div class="input-field col s12"><select name="skills" id="skills">';
                for(var i=0; i<data.length; i++){
                    select += '<option value="'+data[i].id+'">'+data[i].name+'</option>';
                }
                select+='</select> </div>';
                $('#add_skill').html(select);
                console.log(select);
            }
        },"json").fail(function(error){
            alert(error.responseText);
        });	

    $("#update").on("click", function(){
        let data = {
            "id": id,
            "name": $("#name").val(),
            "lastname": $("#lastname").val()
         };
        $.ajax({
            type: 'PUT',
            url: 'https://localhost:44337/api/Employees/'+id,
            contentType: 'application/json',
            data: JSON.stringify(data),
        }).done(function () {
            window.location = '/Employee.html?id='+id;
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
            url: 'https://localhost:44337/api/Employees/'+id,
            contentType: 'application/json',
            data: JSON.stringify(data), 
        }).done(function () {
            window.location = '/Employees.html';
        }).fail(function (msg) {
            alert('Something went wrong.');
        });

    });

    $("#btn_add_skill").on("click", function(){
        let data = {
            "id": $("#skills").val()
         };
        $.ajax({
            type: 'POST',
            url: 'https://localhost:44337/api/Employees/'+id+'/addskills',
            contentType: 'application/json',
            data: JSON.stringify(data),
        }).done(function () {
            window.location = '/Employee.html?id='+id;
        }).fail(function (msg) {
            alert('Something went wrong.');
        });

    });
});

$(document).ready(function(){

    var table = $('#logs_table').DataTable();


    $.get('https://localhost:44337/api/Logs', function(data){
        if(data != '') {
            for(var i=0; i<data.length; i++){
                table.row.add([
                    data[i].id,
                    data[i].skill.name,
                    data[i].employee.name,
                    data[i].employee.lastname,
                    data[i].employee.hireDate,
                    data[i].action,
                    data[i].logDate
                ]).draw();
            }
        }
    },"json").fail(function(error){
        alert(error.responseText);
    });	

});
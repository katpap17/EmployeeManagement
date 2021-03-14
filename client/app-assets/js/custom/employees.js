$(document).ready(function(){

    var table = $('#employee_table').DataTable();


    $.get('https://localhost:44337/api/Employees', function(data){
        if(data != '') {
            for(var i=0; i<data.length; i++){
                table.row.add([
                    data[i].id,
                    data[i].lastname,
                    data[i].name,
                    data[i].hireDate
                ]).draw();
            }
        }
    },"json").fail(function(error){
        alert(error.responseText);
    });	
    
    
    $('#employee_table tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        id=data[0];
        window.location = '/Employee.html?id='+id;
    });

    $("#create_employee").on('click', function(){
        window.location = '/CreateEmployee.html';
    });


});
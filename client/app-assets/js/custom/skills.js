$(document).ready(function(){
        $.get('https://localhost:44337/api/Skills', function(data){
            if(data != '') {
                for(var i=0; i<data.length; i++){
                    $('#skill_tbody').append(
                        '<tr class="clickable-row" data-href="Skill.html?id='+data[i].id+'">'+'\n'+
                        '	<td class="center-align">'+data[i].id+'</td>'+'\n'+
                        '	<td class="center-align">'+data[i].name+'</td>'+'\n'+
                        '	<td class="center-align">'+data[i].description+'</td>'+'\n'+
                        '</tr>'+
                        '\n'
                    );
                }
            }
        },"json").fail(function(error){
            alert(error.responseText);
        });	

        $("#create_skill").on('click', function(){
            window.location = '/CreateSkill.html';
        });
    
        $('#skill_table').delegate('.clickable-row', 'click', function(){
            window.location = $(this).data("href");
        });
    
    });
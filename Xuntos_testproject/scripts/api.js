const url="/api/programminglanguages";
let data = {};

if($(location).attr('pathname') == "/api")
{
    fillTable();
}

function fillTable(){
    $.getJSON(url, function(receivedData) {
        data = receivedData
        var html = '';
        for(var i = 0; i < data.length; i++)
                    html += '<tr><td>' + data[i].Name + '</td><td>' + data[i].Experience + '</td><td><a href="javascript:void(0);" class="button fit" id=' + i + '>Remove</a></td></tr>';
        $('#apitable').append(html);
    });
}

function postTableEntry(name,experience){
    const dataToSend = JSON.stringify({"Name": name.value, "Experience": experience.value});
    $.ajax({
        type: 'POST',
        url: '/api/create/programminglanguage',
        data: dataToSend, 
        contentType: "application/json",
        dataType: 'json',
        complete: function (data) {
          refreshPage(); 
        }
    });  
}

function removeTableEntry(arrElement){
    const dataToSend = JSON.stringify({"Id": data[arrElement].Id , "Name": data[arrElement].Name, "Experience": data[arrElement].Experience});
    console.log(dataToSend);
    $.ajax({
        url: 'api/remove/programminglanguage',
        type: 'DELETE',
        data: dataToSend,
        contentType:'application/json',  
        dataType: 'json',                
        complete: function (data) {
           var row = document.getElementById(arrElement);
           row.parentNode.parentNode.remove();
        }
    });
}

function refreshPage(){
    location.reload();
}

$(document).ready(function(){
    $(document).on("click", "a.button" , function() {
        removeTableEntry($(this).attr('id'));
    });
});
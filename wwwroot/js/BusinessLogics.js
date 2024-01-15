function getRandomNumber() {
    debugger;
    $.ajax({
        cache: false,
        url: "/Teams/GetRandomNumber",
        type: "POST",
        //data: x,
        dataType: 'json',
        async: false,
        contentType: "application/json",
        success: function (result) {
            debugger;
            if (result) {
                document.getElementById("jsTeamID").value = result;
                //debugger;
                //alert('Generate Random Number Successfully.');
                //window.location.reload();
            } else {
                alert('Error!!!');
            }
        }

    });
}

function createNewTeam() {
    debugger;
    var id = $("#jsTeamID").val();
    var name = $("#jsTeamName").val();
    $.ajax({
        cache: false,
        url: "/Teams/Create",
        type: "POST",
        data: { teamId: id, teamName: name },
        //dataType: 'json',
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            console.log(data)
            if (result) {
                alert(result);
            } else {
                alert('Error!!!');
            }
        }
        
    });
}
document.addEventListener("DOMContentLoaded",function(event){


});

$(function(){

    var signalrConn = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();
    signalrConn.start()
        .then(() => console.log("Successful Connection"))
        .catch(error => console.log(err));

    //signalrConn.on("WelcomeAlert",function(message){
    //    alert(message)
    //});

    /* document.addEventListener("click", function (event) {
        var shadeDiv = $('#shadeDiv');
        var x = event.offsetX;
        var y = event.offsetY;

        signalrConn.invoke("SendCoordinates",x,y)
        .catch(error=>console.log(error));

        signalrConn.on("getCoordinates",function(x,y) {
            
            shadeDiv.attr({ style: `position:fixed;left:${x}px;top:${y}px` });
        });


    }); */
  
   
    $('#shadeDiv').draggable({
        axis:"x y",
        drag:function(event,ui){
            
                var x=ui.position.left;
                var y=ui.position.top;
             signalrConn.invoke("SendCoordinates", x, y)
                .catch(error => console.log(error));

            signalrConn.on("GetCoordinates", function (x, y) {

                $('#shadeDiv').attr({ style: `position:relative;left:${x}px;top:${y}px` });
        })
    }
    });


});





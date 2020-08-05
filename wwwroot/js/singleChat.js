$(function(){

    var signalrConn = new signalR.HubConnectionBuilder().withUrl("/dmChatHub").build();
    
    //////////////////////////////////////////////////////////////////////////
        signalrConn.on("WelcomeAlert",data=>{
            console.log(data);
        });

    signalrConn.start()
        .then(() => console.log("Successful Connection"))
        .catch(error => console.log(error));
        //////////////////////////////////////////////////////////////////////////
    let usernameHead = $('#usernameHead').text();
    var chatPane = $('#mCSB_1_container');
    var messageArea = $('#messageArea');
    messageArea.focus();
    var sendButton = $('#sendButton');
    var chatId = $('#chatId').val();
    sessionStorage.setItem('chatId',chatId);
   $('#chatId').remove();
    //////////////////////////////////////////////////////////////////////////
    sendButton.click(event=>{
        var message=messageArea.val();
        if(message.length > 0){

            chatPane.append(appendCurrentUsersMessage(message));
            signalrConn.invoke("SendRealTimeMessage",usernameHead,chatId,message);
            messageArea.val("");
            jQuery(".scrollbar").mCustomScrollbar("scrollTo", "last");
        }
    });

    //////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////
    messageArea.keypress(event=>{
        var keycode=(event.keyCode ? event.keyCode : event.which);
        if(keycode == '13'){
            var message = messageArea.val();
            if (message.length > 0) {

                chatPane.append(appendCurrentUsersMessage(message));
                signalrConn.invoke("SendRealTimeMessage", usernameHead, chatId, message);
                messageArea.val("");
                jQuery(".scrollbar").mCustomScrollbar("scrollTo", "last");
            }
        }
    });
    //////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////

  // window.scrollTo(0,document.body.scrollHeight);
   // jQuery(".scrollbar").mCustomScrollbar("scrollTo","last");
 //////////////////////////////////////////////////////////////////////////

 $('#scrollDownBtn').click(event=>{
     jQuery(".scrollbar").mCustomScrollbar("scrollTo", "last");
 })
//////////////////////////////////////////////////////////////////////////

    signalrConn.on("ReceiveMessage",(message)=>{
        chatPane.append(appendReceivedMessages(message));
        jQuery(".scrollbar").mCustomScrollbar("scrollTo", "last");
    });

    //////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////
    window.addEventListener('beforeunload',event=>{
        signalrConn.stop().then(e=>console.log('')).catch(error=>console.log(error));
    });
});



function appendCurrentUsersMessage(messageTyped){
    if(window.location.pathname == "/discover"){
        return;
    }
    return `
            <div class="chat chat-left justify-content-end">
             <div class="chat-msg">
                  <div class="chat-msg-content">
                   <p>${messageTyped}</p>
                  </div>
             </div>
            </div>
            
            `
}

function appendReceivedMessages(messageReceived) {
    if (window.location.pathname == "/discover") {
        return;
    }
    return `
             <div class="chat">
                <div class="chat-msg">
                    <div class="chat-msg-content">
                         <p>${messageReceived}</p>
                    </div>
               </div>
             </div>
    `
}
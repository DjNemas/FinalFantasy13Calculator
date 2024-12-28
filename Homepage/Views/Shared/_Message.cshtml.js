"use strict"
function clearMessage() {
    let messageBox = document.getElementById("message-box");
    let message = document.getElementById("message");
    messageBox.classList.add("hide");
    message.innerHTML = "";
}
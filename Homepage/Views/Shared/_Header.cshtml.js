"use strict"

function openSide()
{
    let sidepanel = document.getElementById("side-panel");
    sidepanel.classList.add("show");
}

function closeSide() {
    let sidepanel = document.getElementById("side-panel");
    sidepanel.classList.remove("show");
}

function showRegister() {
    let register = document.getElementById("register-side-content");
    register.classList.remove("hide");

    let login = document.getElementById("login-side-content");
    login.classList.add("hide");
}

function showLogin() {
    let register = document.getElementById("register-side-content");
    register.classList.add("hide");

    let login = document.getElementById("login-side-content");
    login.classList.remove("hide");
}
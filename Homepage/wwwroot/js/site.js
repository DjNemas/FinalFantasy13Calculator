// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.getElementById('openSide').addEventListener('click', function () {
    var sidePanel = document.getElementById('sidePanel');
    sidePanel.classList.add('show');
    this.style.display = 'none';
});

document.getElementById('closeIcon').addEventListener('click', function () {
    var sidePanel = document.getElementById('sidePanel');
    sidePanel.classList.remove('show');
    document.getElementById('openSide').style.display = 'block';
});
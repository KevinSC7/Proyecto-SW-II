// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function onlyOne(checkbox) {
    var checkboxes = document.getElementsByName('check')
    checkboxes.forEach((item) => {
        if (item !== checkbox) item.checked = false
    })
}

function OneOrMore(checkbox) {

    var checkboxes = document.getElementsByName('check');
    var ninguno = true;
    checkboxes.forEach((item) => {
        if (item !== checkbox) {
            if (item.checked == true) ninguno = false;
        }
    })
    if (ninguno == true) checkbox.checked = true;

}

function alwaysWithCheck2(checkbox) {

    var check1 = document.getElementsByName('check1')[0];
    var check2 = document.getElementsByName('check2')[0];

    if (!(check1.checked == true || check2.checked == true)) {
        checkbox.checked = true;
    }

}
// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//table script

var p1 = document.getElementById("p1");
var tableSize = window.prompt("podaj wielkość tablicy: ");

if (tableSize > 20 || tableSize < 5 || !Number.isInteger(Number(tableSize))) {
    tableSize = Math.floor(Math.random() * 16) + 5;
    p1.innerHTML = "podane dane były niewłaściwe, więc przyjęto n=" + tableSize;
}
else {
    p1.innerHTML = "n=" + tableSize;
}
var rows = new Array(tableSize);
var columns = new Array(tableSize);

var table = document.getElementById("table");
var thead = table.createTHead();
var firstRow = thead.insertRow();
firstRow.insertCell(0).innerText = "x";

for (let i = 0; i < tableSize; i++) {
    let newValue = Math.floor(Math.random() * 100);
    columns[i] = newValue;
    let newCell = firstRow.insertCell();
    newCell.innerText = newValue;
}


for (let i = 0; i < tableSize; i++) {
    let newValue = Math.floor(Math.random() * 100);
    rows[i] = newValue;

    let newRow = table.insertRow();
    let newCell = newRow.insertCell(0);
    newCell.innerText = newValue;

    for (let j = 0; j < tableSize; j++) {
        newValue = rows[i] * columns[j];
        newCell = newRow.insertCell(j + 1);
        newCell.innerText = newValue;
        if (newValue % 3 == 0)
            newCell.classList.add("threeZero");
        else if (newValue % 3 == 1)
            newCell.classList.add("threeOne");
        else
            newCell.classList.add("threeTwo");
    }

}

//canvas script

const canvasList = document.getElementsByClassName("canvas");

for (let i = 0; i < canvasList.length; i++) {
    let ctx = canvasList[i].getContext("2d");

    canvasList[i].addEventListener("mousemove", drawLine);
    canvasList[i].addEventListener("mouseenter", drawLine);
    canvasList[i].addEventListener("mouseleave", clearLines);

    function drawLine(e) {
        canvasList[i].width = canvasList[i].offsetWidth;
        canvasList[i].height = canvasList[i].offsetHeight;
        let w = canvasList[i].width;
        let h = canvasList[i].height;


        ctx.clearRect(0, 0, w, h);
        ctx.beginPath();
        ctx.moveTo(0, 0);
        ctx.lineTo(e.offsetX, e.offsetY);
        ctx.moveTo(w, 0);
        ctx.lineTo(e.offsetX, e.offsetY);
        ctx.moveTo(0, h);
        ctx.lineTo(e.offsetX, e.offsetY);
        ctx.moveTo(w, h);
        ctx.lineTo(e.offsetX, e.offsetY);
        ctx.stroke();
    }

    function clearLines(e) {
        canvasList[i].width = canvasList[i].offsetWidth;
        canvasList[i].height = canvasList[i].offsetHeight;
        let w = canvasList[i].width;
        let h = canvasList[i].height;


        ctx.clearRect(0, 0, w, h);
    }
}




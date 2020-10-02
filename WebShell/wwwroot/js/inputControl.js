var i = 0;
var arr = [];

$(document).ready(function () {

    myMethod();
})

var myMethod = function () {
    $.ajax({
        url: "/api/command",
        dataType: "json",
        success: function (data) {
            for (k in data.data) {
                arr.push(data.data[k]);
            }
            
        }
    });
}

//https://stackoverflow.com/a/26945342/11321634
//Автор Paul S.
function nextItem() {
    i = i + 1; // increase i by one
    i = i % arr.length; // if we've gone too high, start from `0` again
    return arr[i]; // give us back the item of where we are now
}

function prevItem() {
    if (i === 0) { // i would become 0
        i = arr.length; // so put it at the other end of the array
    }
    i = i - 1; // decrease by one
    return arr[i]; // give us back the item of where we are now
}

let flag = 0;
window.addEventListener('load', function () {

    document.getElementById('command_input').addEventListener('keydown', function (e) {
        // we want to listen for a keypress

        if (flag === 1) {
            switch (e.key) {
                case 'ArrowUp':
                    document.getElementById('command_input').value = prevItem();
                    break;
                case 'ArrowDown':
                    document.getElementById('command_input').value = nextItem();
                    break;

            }
        }
        else {
            if ((e.key == 'ArrowUp') || (e.key == 'ArrowDown')) {
                document.getElementById('command_input').value = arr[arr.length - 1]; // initial value
                flag = 1;
            }
        }
    }
    );
});
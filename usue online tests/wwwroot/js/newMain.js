// todo : 
// ============================ ÂÛÁÐÀÒÜ ÖÂÅÒ

const colorPickerButton = document.querySelector('.color-picker');
const colorListContainer = document.getElementById('color-list');
const colorsItemList = document.querySelectorAll('.color-item');
const colorPickerInput = document.getElementById('color-picker-input');
const trailer = document.getElementById("me");

const myId = makeid(20);

colorPickerButton.addEventListener('click', (e) => {
    e.stopPropagation();
    const visible = colorListContainer.style.display === 'grid';
    if (visible) {
        colorListContainer.style.display = 'none';
    } else {
        colorListContainer.style.display = 'grid';
    }
});

for (let colorItem of colorsItemList) {
    colorItem.addEventListener('click', (e) => {
        const clickedColor = e.target.style.backgroundColor;
        colorPickerButton.style.backgroundColor = clickedColor;
        changeColor(clickedColor);
        colorListContainer.style.display = 'none';
    });
}

colorPickerInput.addEventListener('input', (e) => {
    const newColor = '#' + e.target.value;
    if (newColor.length > 1) {
        colorPickerButton.style.backgroundColor = newColor;
        changeColor(newColor)
    }
});

//  ===================================== ÒÎËÙÈÍÀ


const lineWidthInput = document.getElementById("input-width")
lineWidthInput.addEventListener('change', (e) => {
    const newVal = lineWidthInput.value;
    e.stopPropagation();
    if (newVal > lineWidthInput.max) return;
    ctx.lineWidth = newVal
    trailer.style.width = newVal
    trailer.style.height = newVal
})

//  ====================================== CANVAS

const canvas = document.getElementById("canvas");
canvas.height = window.innerHeight;
canvas.width = window.innerWidth;
const ctx = canvas.getContext("2d");
ctx.lineWidth = colorPickerInput.value;

let prevX = null;
let prevY = null;

// ================================ SOCKET

let socket = null;
let new_uri = null;
const loc = window.location 

if (loc.protocol === "https:") {
    new_uri = "wss:";
} else {
    new_uri = "ws:";
}

new_uri += "/" + loc.host;
new_uri += loc.pathname + "/ws";

if (typeof (WebSocket) !== 'undefined') {
    socket = new WebSocket(new_uri);
} else {
    socket = new MozWebSocket(new_uri);
}

socket.onmessage = function (msg) {
    try {
        const data = msg.data.split(':');
        const command = data[0]

        if (command === 'move') {
            const lineWidth = ctx.lineWidth;
            const strokeStyle = ctx.strokeStyle;
            
            canvas.getContext("2d");
            ctx.beginPath();
            ctx.moveTo(data[1], data[2]);
            ctx.lineTo(data[3], data[4]);
            ctx.lineWidth = data[5];
            ctx.strokeStyle = data[6];
            ctx.stroke();

            ctx.strokeStyle = strokeStyle;
            ctx.lineWidth = lineWidth;
        }

        if (command === 'clear') {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
        }

        if (command === 'cur') {
            const userId = data[1];
            let t;

            t = document.getElementById(userId);

            if (t === null) {
                t = document.createElement('div');
                t.id = userId;
                t.className = "trailer";
                document.body.appendChild(t);
                return;
            }

            const keyFrames = {
                transform: `translate(${data[2]}px, ${data[3]}px)`
            };

            t.animate(keyFrames, {
                fill: "forwards"
            });
        }

        if (command === 'disconnect') {
            let list = document.getElementsByClassName("trailer");

            if (list.length === 0) return;

            for (var i = 0; i < list.length; i++) {
                if (list[i].id !== 'me') {
                    document.body.removeChild(list[i]);
                    i--;
                }
            }

            // const newList = [...list].filter(item => item.id !== "me");
            // newList.forEach(item => {document.removeChild(item)});
        }

        if (data[0] === 'line') {
            roughCanvas.curve(JSON.parse(data[3]), {
                stroke: data[1], strokeWidth: data[2]
            }); }

    } catch (e) {
        console.log(e);
    }

};

socket.onclose = function (event) {
    console.log('lost connection');

    if (typeof (WebSocket) !== 'undefined') {
        socket = new WebSocket(new_uri);
    } else {
        socket = new MozWebSocket(new_uri);
    }
};


// =============================== CANVAS ïðîäîëæàòü

let draw = false;
let resize = false;
let clrs = document.querySelectorAll(".clr");
clrs = Array.from(clrs);
clrs.forEach(clr => {
    clr.addEventListener("click", () => {
        changeColor(clr.dataset.clr);
    });
});

// ================================== CLEAR 

let clearBtn = document.getElementById("clear-btn");
clearBtn.addEventListener("click", () => {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    socket.send('clear:');
});

// ================================== SAVE

let saveBtn = document.getElementById("save-btn");
saveBtn.addEventListener("click", () => {
    let data = canvas.toDataURL("imag/png");
    let a = document.createElement("a");
    a.href = data;
    a.download = "sketch.png";
    a.click();
});

// ================================== MOUSE event

let dx, dy;
let step = 1;
let counter = 0;
let line = [];

window.addEventListener("mousedown", (e) => {
    e.stopPropagation();
    if (e.which === 2) {
        resize = true;
        dx = e.clientX;
        dy = e.clientY;
        return;
    }

    counter = 0;
    line = [];

    if (e.button !== 0) return;
    if (e.target.id !== "canvas") return;
    draw = true;
});

window.addEventListener("mouseup", (e) => {
    if (resize) {
        resize = false;

        // resizeWindow(e.clientX, e.clientY);
    }
    draw = false;

    socket.send('line:' + ctx.strokeStyle + ':' + ctx.lineWidth + ':' + JSON.stringify(line) + ':');
});

window.addEventListener("mousemove", (e) => {

    if (e.button !== 0) return;

    const trailerX = e.clientX - trailer.offsetWidth / 2;
    const trailerY = e.clientY - trailer.offsetHeight / 2;

    const keyFrames = {
        transform: `translate(${trailerX}px, ${trailerY}px)`
    };

    trailer.animate(keyFrames, {
        fill: "forwards"
    });

    socket.send('cur:' + myId + ':' + trailerX + ':' + trailerY + ':');


    if (prevX == null || prevY == null || !draw) {
        prevX = e.clientX;
        prevY = e.clientY;
        return;
    }


    let currentX = e.clientX;
    let currentY = e.clientY;

    counter++;
    if (counter % step === 0) {
        line.push([currentX, currentY]);

        roughCanvas.curve(line, {
            stroke: ctx.strokeStyle, strokeWidth: ctx.lineWidth
        });
    }

    //ctx.beginPath();
    //ctx.moveTo(prevX, prevY);
    //ctx.lineTo(currentX, currentY);
    //ctx.stroke();

    //socket.send('move:' +
    //    prevX +
    //    ':' +
    //    prevY +
    //    ':' +
    //    currentX +
    //    ':' +
    //    currentY +
    //    ':' +
    //    ctx.lineWidth +
    //    ':' +
    //    ctx.strokeStyle +
    //    ':');

    prevX = currentX;
    prevY = currentY;

});

//  ============================ UTILS



function makeid(length) {
    let result = '';
    const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
    const charactersLength = characters.length;
    for (let i = 0; i < length; i++) {
        result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
}

function changeColor(color) {
    ctx.strokeStyle = color;
    trailer.style.backgroundColor = color;

    const list = document.querySelectorAll('.trailer');

    for (let t of list) {
        t.style.backgroundColor = color;
    }

}

function resizeWindow(dxNew, dyNew) {
    let oldimg = ctx.getImageData(0, 0, ctx.canvas.width, ctx.canvas.height);

    canvas.width = canvas.width;
    canvas.height = canvas.height;

    //resize canvas
    ctx.putImageData(oldimg, dxNew - dx, dyNew - dy, 0, 0, ctx.canvas.width, ctx.canvas.height);
}


//-------------------RoughCanvas---------------------

let roughCanvas = rough.canvas(document.getElementById('canvas'));


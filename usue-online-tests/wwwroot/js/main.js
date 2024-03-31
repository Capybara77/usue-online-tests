// trailer
const trailer = document.querySelector("#me");

const canvas = document.getElementById("canvas");
canvas.height = window.innerHeight;
canvas.width = window.innerWidth;
const ctx = canvas.getContext("2d");
let prevX = null;
let prevY = null;


document.getElementById("change").addEventListener("click", () => {
    let oldimg = ctx.getImageData(0, 0, ctx.canvas.width, ctx.canvas.height);

    //resize canvas
    canvas.height = 600;
    canvas.width = 600;
    ctx.putImageData(oldimg, 100, 100, 0, 0, ctx.canvas.width, ctx.canvas.height);
});

const customLineWidth = document.getElementById("lineValue");
ctx.lineWidth = customLineWidth.value;

const myId = makeid(20);

function makeid(length) {
    let result = '';
    const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
    const charactersLength = characters.length;
    for (let i = 0; i < length; i++) {
        result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
}

var socket;

var loc = window.location, new_uri;
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

//command:data1:data2:

socket.onmessage = function (msg) {
    try {
        var data = msg.data.split(':');

        if (data[0] === 'move') {
            var d = ctx.lineWidth;
            var c = ctx.strokeStyle;
            
            canvas.getContext("2d");
            ctx.beginPath();
            ctx.moveTo(data[1], data[2]);
            ctx.lineTo(data[3], data[4]);
            ctx.lineWidth = data[5];
            ctx.strokeStyle = data[6];
            ctx.stroke();

            ctx.strokeStyle = c;
            ctx.lineWidth = d;
        }

        if (data[0] === 'clear') {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
        }

        if (data[0] === 'cur') {
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

        if (data[0] === 'disconnect') {
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


    } catch (e) {
        console.log(e);
    }


};

function changeColor(color) {
    ctx.strokeStyle = color;
    trailer.style.backgroundColor = color;

    const list = document.querySelectorAll('.trailer');

    for (let t of list) {
        t.style.backgroundColor = color;
    }

}

socket.onclose = function (event) {
    console.log('lost connection');

    if (typeof (WebSocket) !== 'undefined') {
        socket = new WebSocket(new_uri);
    } else {
        socket = new MozWebSocket(new_uri);
    }
};



customLineWidth.addEventListener("change", (e) => {
    if (customLineWidth.value < 1 || customLineWidth.value > 20) {
        alert("че ебанулся");
        ctx.lineWidth = 10;
        return;
    }
    ctx.lineWidth = customLineWidth.value;
});


let draw = false;
let resize = false;
let clrs = document.querySelectorAll(".clr");
clrs = Array.from(clrs);
clrs.forEach(clr => {
    clr.addEventListener("click", () => {
        changeColor(clr.dataset.clr);
    });
});

let clearBtn = document.querySelector(".clear");
clearBtn.addEventListener("click", () => {
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    socket.send('clear:');
});

// Saving drawing as image
let saveBtn = document.querySelector(".save");
saveBtn.addEventListener("click", () => {
    let data = canvas.toDataURL("imag/png");
    let a = document.createElement("a");
    a.href = data;
    // what ever name you specify here
    // the image will be saved as that name
    a.download = "sketch.png";
    a.click();
});

let dx, dy;

window.addEventListener("mousedown", (e) => {
    if (e.which === 2) {
        resize = true;
        dx = e.clientX;
        dy = e.clientY;
        return;
    }

    if (e.button !== 0) return;
    draw = true;
});

window.addEventListener("mouseup", (e) => {
    if (resize) {
        resize = false;

        resizeWindow(e.clientX, e.clientY);
    }
    draw = false;
});


function resizeWindow(dxNew, dyNew) {
    let oldimg = ctx.getImageData(0, 0, ctx.canvas.width, ctx.canvas.height);

    canvas.width = canvas.width;
    canvas.height = canvas.height;

    //resize canvas
    ctx.putImageData(oldimg, dxNew - dx, dyNew - dy, 0, 0, ctx.canvas.width, ctx.canvas.height);
}

// window.addEventListener("blur",
//     (e) => {

//     });

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
    ctx.beginPath();
    ctx.moveTo(prevX, prevY);
    ctx.lineTo(currentX, currentY);
    ctx.stroke();

    socket.send('move:' +
        prevX +
        ':' +
        prevY +
        ':' +
        currentX +
        ':' +
        currentY +
        ':' +
        ctx.lineWidth +
        ':' +
        ctx.strokeStyle +
        ':');

    prevX = currentX;
    prevY = currentY;

});


//-------------------RoughCanvas---------------------

let roughCanvas = rough.canvas(document.getElementById('canvas'));

//roughCanvas.line(60, 60, 190, 60);
//roughCanvas.line(60, 60, 190, 60, { strokeWidth: 5 });

//roughCanvas.rectangle(10, 10, 100, 100);
//roughCanvas.rectangle(140, 10, 100, 100, { fill: 'red' });

//roughCanvas.ellipse(350, 50, 150, 80);
//roughCanvas.ellipse(610, 50, 150, 80, { fill: 'blue', stroke: 'red' });

//roughCanvas.linearPath([[690, 10], [790, 20], [750, 120], [690, 100]]);

//roughCanvas.polygon([[690, 130], [790, 140], [750, 240], [690, 220]]);


roughCanvas.arc(350, 300, 200, 180, Math.PI, Math.PI * 1.6, true);
roughCanvas.arc(350, 300, 200, 180, 0, Math.PI / 2, true, {
    stroke: 'red', strokeWidth: 4,
    fill: 'rgba(255,255,0,0.4)', fillStyle: 'solid'
});
roughCanvas.arc(350, 300, 200, 180, Math.PI / 2, Math.PI, true, {
    stroke: 'blue', strokeWidth: 2,
    fill: 'rgba(255,0,255,0.4)'
});


// draw sine curve
let points = [];
for (let i = 0; i < 10; i++) {
    let x = (400 / 20) * i + 10;
    let xdeg = (Math.PI / 100) * x;
    let y = Math.round(Math.sin(xdeg) * 90) + 500;
    points.push([x, y]);
}
roughCanvas.curve(points, {
    stroke: 'red', strokeWidth: 3
});


//roughCanvas.path('M37,17v15H14V17z M50,0H0v50h50z');
//roughCanvas.path('M80 80 A 45 45, 0, 0, 0, 125 125 L 125 80 Z', { fill: 'green' });

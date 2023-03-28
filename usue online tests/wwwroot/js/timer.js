document.addEventListener("DOMContentLoaded", ready);

function ready() {
    let timer = setInterval(function () {
        let timerInput = document.getElementById("time");

        timeSec = timerInput.innerHTML;

        if (isNaN(timeSec)) {
            timerInput.innerHTML = 60;
            return;
        }

        if (timeSec <= 0) {
            clearInterval(timer);
            let form = document.getElementById("answers-form");
            form.submit();
        } else {
            --timeSec;
            timerInput.innerHTML = timeSec;
        }
    },
        1000);
}
document.addEventListener("DOMContentLoaded", ready, 1000);

function ready() {
    timer = setInterval(function () {
            let timerInput = document.getElementById("time");

            timeSec = timerInput.innerHTML;
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
//const videoElement = document.getElementById('camera');
//const captureButton = document.getElementById('capture');

//async function setupCamera() {
//    try {
//        const stream = await navigator.mediaDevices.getUserMedia({ video: true });
//        videoElement.srcObject = stream;
//    } catch (error) {
//        console.error('Ошибка доступа к камере:', error);
//    }
//}

//async function captureAndSend() {
//    const canvas = document.createElement('canvas');
//    canvas.width = videoElement.videoWidth;
//    canvas.height = videoElement.videoHeight;
//    const ctx = canvas.getContext('2d');
//    ctx.drawImage(videoElement, 0, 0, canvas.width, canvas.height);

//    const imageBlob = await new Promise((resolve) => {
//        canvas.toBlob((blob) => resolve(blob), 'image/jpeg');
//    });

//    // Отправка изображения на сервер
//    const formData = new FormData();
//    formData.append('image', imageBlob);

//    const response = await fetch('/camera/sendimage', {
//        method: 'POST',
//        body: formData,
//    });

//    if (response.ok) {
//        console.log('Изображение успешно отправлено на сервер');
//    } else {
//        console.error('Ошибка при отправке изображения:', response.status);
//    }
//}

//setupCamera();

//captureButton.addEventListener('click', () => {
//    captureAndSend();
//});

//// Запуск снимка и отправки каждые 5 секунд
//setInterval(captureAndSend, 5000);



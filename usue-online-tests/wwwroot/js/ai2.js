import vision from 'https://cdn.jsdelivr.net/npm/@mediapipe/tasks-vision@0.10.3';
const { FaceLandmarker, FilesetResolver } = vision;

// --- Элементы DOM ---
const demosSection = document.getElementById('demos');
const videoBlendShapes = document.getElementById('video-blend-shapes');
const video = document.getElementById('webcam');
const loading = document.getElementById('loading');
const examIdInput = document.querySelector('input[name="examId"]');
const userIdInput = document.querySelector('input[name="userId"]');
const box = document.getElementById('box');

// --- Настройки ---
const videoWidth = 80;
const sendInterval = 1000;

// --- Глобальные переменные ---
let faceLandmarker;
let lastSendTime = 0;

// --- Инициализация ---
async function init() {
    await createFaceLandmarker();
    if (hasGetUserMedia()) {
        await enableCam();
    } else {
        console.warn('getUserMedia() не поддерживается вашим браузером');
    }
}

// --- Создание FaceLandmarker ---
async function createFaceLandmarker() {
    const filesetResolver = await FilesetResolver.forVisionTasks(
        'https://cdn.jsdelivr.net/npm/@mediapipe/tasks-vision@0.10.3/wasm'
    );
    faceLandmarker = await FaceLandmarker.createFromOptions(filesetResolver, {
        baseOptions: {
            modelAssetPath: `https://storage.googleapis.com/mediapipe-models/face_landmarker/face_landmarker/float16/1/face_landmarker.task`,
            delegate: 'GPU',
        },
        outputFaceBlendshapes: true,
        runningMode: 'IMAGE', // Начинаем с режима IMAGE
        numFaces: 1,
    });
    demosSection.classList.remove('invisible');
}

// --- Проверка getUserMedia ---
function hasGetUserMedia() {
    return !!(navigator.mediaDevices && navigator.mediaDevices.getUserMedia);
}

// --- Запуск веб-камеры ---
async function enableCam() {
    const constraints = { video: true };
    try {
        const stream = await navigator.mediaDevices.getUserMedia(constraints);
        video.srcObject = stream;
        video.addEventListener('loadeddata', startPrediction); // Используем другую функцию
    } catch (error) {
        console.error('Ошибка доступа к веб-камере:', error);
    }
    loading.style.visibility = 'hidden';
}

// --- Запуск обработки видео ---
async function startPrediction() {
    const radio = video.videoHeight / video.videoWidth;
    video.style.width = videoWidth + 'px';
    video.style.height = videoWidth * radio + 'px';

    // Переключение в режим VIDEO после запуска
    if (faceLandmarker.runningMode !== 'VIDEO') {
        await faceLandmarker.setOptions({ runningMode: 'VIDEO' });
    }
    predictWebcam(); // Запуск цикла обработки
}

// --- Обработка видеопотока ---
async function predictWebcam() {
    const now = Date.now();

    // Отправка данных каждую секунду
    if (now - lastSendTime > sendInterval) {
        const results = faceLandmarker.detectForVideo(video, now);
        if (results.faceBlendshapes?.length) {
            await sendData(results.faceBlendshapes[0]);
        }
        lastSendTime = now;
    }
    window.requestAnimationFrame(predictWebcam);
}

// --- Отправка данных на сервер ---
async function sendData(blendShapes) {
    try {
        const dataToSend = {
            blendShapes: blendShapes,
            examId: examIdInput.value,
            userId: userIdInput.value
        };

        const response = await fetch('/camera/senddata/', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(dataToSend),
        });

        if (!response.ok) {
            throw new Error('Ошибка сервера: ' + response.status);
        }

        const data = await response.text();
        box.style.borderColor = data === 'true' ? 'green' : 'red';

    } catch (error) {
        console.error('Ошибка при отправке данных:', error);
    }
}

// --- Запуск инициализации ---
init();
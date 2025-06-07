import { initializeApp } from "https://www.gstatic.com/firebasejs/10.4.0/firebase-app.js";
import { getMessaging, getToken, onMessage } from "https://www.gstatic.com/firebasejs/10.4.0/firebase-messaging.js";


const firebaseConfig = {
    apiKey: "AIzaSyC6oS52FI7yGTrZhIG9TRpbr0ZP4syWKQY",
    authDomain: "eul-agenda-e.firebaseapp.com",
    databaseURL: "https://eul-agenda-e-default-rtdb.firebaseio.com",
    projectId: "eul-agenda-e",
    storageBucket: "eul-agenda-e.firebasestorage.app",
    messagingSenderId: "77141547556",
    appId: "1:77141547556:web:79c53cb0bbba8bd47b16b1"
};

window.registerForPush = async function (vapidKey) {
    // Solicita permissão para notificações
    const permission = await Notification.requestPermission();
    if (permission !== 'granted') {
        throw new Error('Permissão para notificações não concedida');
    }

    // Registra o service worker
    const registration = await navigator.serviceWorker.register('service-worker.js');

    firebase.initializeApp(firebaseConfig);

    // Inicializa o Firebase (certifique-se de que firebase.initializeApp já foi chamado anteriormente)
    const messaging = firebase.messaging();

    // Obtém o token do FCM
    const token = await messaging.getToken({
        vapidKey: vapidKey,
        serviceWorkerRegistration: registration
    });

    return token;
}
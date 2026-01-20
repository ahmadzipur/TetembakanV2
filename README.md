🎮 Tetembakan – Virtual Reality FPS Game (Unity)

Tetembakan adalah game First Person Shooter (FPS) berbasis Virtual Reality (VR) yang dikembangkan menggunakan Unity. Game ini menghadirkan pengalaman menembak imersif dengan beberapa scene pertempuran, variasi senjata, serta sistem skor dan waktu.

Game ini terus dikembangkan dengan penambahan fitur gameplay, scene baru, dan integrasi perangkat keras eksternal untuk mendukung kontrol berbasis gyro.

🚀 Fitur Utama

🎯 Gameplay FPS VR (First Person View)

🌍 Multi Scene dengan Portal

- Main Scene
- House Defense
- Warzone
- Desert

🔫 Beragam Senjata
- Senjata api (pistol & senapan)
- Serangan jarak dekat (pisau)
- Lemparan granat dengan damage area

⏱️ Sistem Countdown Timer

⭐ Sistem Skor

Skor bertambah saat target terkena peluru atau ledakan

🔄 Perpindahan Scene Interaktif melalui Portal

🧠 Respon Target Dinamis

Jatuh, meledak, atau menghilang saat terkena serangan

🕶️ Sistem Virtual Reality & Kontrol

Karena tidak menggunakan perangkat VR dengan sensor gyro bawaan, game ini menggunakan solusi alternatif:

- 📦 VR Box
- 📡 Sensor Gyro eksternal
- 🔌 ESP8266
- 🧪 Arduino IDE untuk pemrograman sensor
- 🎮 Data gyro dikirim ke Unity untuk mengontrol rotasi kamera pemain

Pendekatan ini memungkinkan simulasi VR tetap berjalan meskipun tanpa headset VR premium.

🛠️ Tools & Teknologi
- Unity Engine (versi 6000.x – stable)
- Bahasa Pemrograman: C#
- Arduino IDE (ESP8266 & Gyro Sensor)
- Asset Pihak Ketiga (Unity Asset Store)
- Git & GitHub

🧪 Pengujian

Game diuji menggunakan Play Mode Unity, dengan hasil:
- Gameplay berjalan stabil tanpa error besar
- Portal antar scene berfungsi dengan baik
- Sensor gyro ESP8266 berhasil mengontrol kamera
- Sistem skor dan timer berjalan sesuai logika

📹 Demo Video

🎥 YouTube:
https://youtu.be/gSNVhGHpSSs?si=gEidwcMNbgCG8SE7

📂 Source Code

🔗 GitHub Repository:
https://github.com/ahmadzipur/TetembakanV2.git


🔗 GitHub Repository Tetembakan versi pertama:
https://github.com/ahmadzipur/Tetembakan.git

🎓 Latar Belakang

Project ini dibuat sebagai Tugas Ujian Akhir Semester Mata Kuliah Augmented & Virtual Reality
Program Studi Teknik Informatika Universitas Teknologi Bandung

🔮 Pengembangan Selanjutnya
- AI musuh yang lebih cerdas
- Sistem level & progres pemain
- Dukungan penuh VR Headset & Controller
- Mode multiplayer
- Optimalisasi performa untuk perangkat VR
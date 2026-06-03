# ASP.NET Core & MinIO S3 Integration

![NET 8.0](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)
![MinIO](https://img.shields.io/badge/MinIO-S3--Compatible-00599C?style=flat-square&logo=minio)
![Docker](https://img.shields.io/badge/Docker-Container-2496ED?style=flat-square&logo=docker)

Данный проект представляет собой практическую работу по организации хранения и обработки файлов с использованием S3-совместимого объектного хранилища **MinIO** и **ASP.NET Core Web API**.

Работа является логическим продолжением цепочки сервисной интеграции (ETL, Kafka, RabbitMQ, Elasticsearch, Apache NiFi) и демонстрирует важнейший паттерн современной разработки — **отделение хранения файлов от прикладной логики приложения (Stateless Architecture)**.

---

## 🏗️ Архитектура решения

Взаимодействие между компонентами системы построено по классической схеме работы с объектными хранилищами:
[ HTTP Client / Swagger ]
│ (multipart/form-data / GET)
▼
[ ASP.NET Core Web API ]
│ (Minio SDK / S3 API)
▼
[ MinIO Server ] ──────► [ Persistent Volume (Docker) ]
│
▼
[ Bucket / Objects ]

---

## 🚀 Функционал

- [x] Развертывание MinIO в Docker с постоянным хранением данных (`volumes`).
- [x] Интеграция ASP.NET Core с MinIO через официальный S3 SDK.
- [x] Эндпоинт для загрузки файлов через `multipart/form-data`.
- [x] Эндпоинт для скачивания файлов по имени объекта.
- [x] **Дополнительное задание (+15 баллов):** - [x] Эндпоинт для получения списка всех файлов в бакете.
  - [x] Генерация временных безопасных ссылок (**Presigned URLs**) для скачивания.

---

## 🛠️ Технологический стек

* **Backend:** ASP.NET Core Web API (.NET 8)
* **S3 Client:** `Minio` NuGet Package
* **Infrastructure:** Docker / Docker Compose
* **API Documentation:** Swagger UI

---

## 📑 Описание API Эндпоинтов

| Метод | Эндпоинт | Описание | Формат данных |
| --- | --- | --- | --- |
| POST | /api/files/upload | Загрузка файла в бакет | MinIOmultipart/form-data |
| GET | /api/files/download/{fileName} | Скачивание файла напрямую через API | FileStreamResult |
| GET | /api/files/list | Получение списка всех объектов в бакете | application/json |
| GET | /api/files/presigned-url/{fileName} | Генерация временной ссылки на файл | text/plain (URL) |

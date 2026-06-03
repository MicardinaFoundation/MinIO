# ASP.NET Core & MinIO S3 Integration

![NET 8.0](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)
![MinIO](https://img.shields.io/badge/MinIO-S3--Compatible-00599C?style=flat-square&logo=minio)
![Docker](https://img.shields.io/badge/Docker-Container-2496ED?style=flat-square&logo=docker)

Данный проект представляет собой практическую работу по организации хранения и обработки файлов с использованием S3-совместимого объектного хранилища **MinIO** и **ASP.NET Core Web API**.

Работа является логическим продолжением цепочки сервисной интеграции (ETL, Kafka, RabbitMQ, Elasticsearch, Apache NiFi) и демонстрирует важнейший паттерн современной разработки — **отделение хранения файлов от прикладной логики приложения (Stateless Architecture)**.

---

## 🏗️ Архитектура решения

Взаимодействие между компонентами системы построено по классической схеме работы с объектными хранилищами:

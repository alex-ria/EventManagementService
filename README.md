# EventManagementService

Сервис управления мероприятиями на ASP.NET Core (C#) с REST API.

## Описание

Проект предоставляет функциональность для создания, обновления, удаления и получения информации о мероприятиях и связанных сущностях.

Основное:
- Модель события, расписание, участники
- CRUD-операции через REST API
- Конфигурация через `appsettings.json`
- Запуск в среде ASP.NET Core (Kestrel)

## Структура проекта

`EmsApi/` — основной проект API:
- `Program.cs` — точка входа и конфигурация WEB-приложения
- `appsettings.json` — базовая конфигурация
- `appsettings.Development.json` — конфигурация для среды разработки

## Требования

- .NET SDK 9.0 (или более поздняя версия)
- Windows / Linux / macOS
- Git (рекомендуется)

## Быстрый старт

1. Клонировать репозиторий:

```bash
git clone https://github.com/alex-ria/EventManagementService.git
cd EventManagementService
```

2. Восстановить зависимости:

```bash
dotnet restore EmsApi/EmsApi.csproj
```

3. Запустить приложение:

```bash
dotnet run --project EmsApi/EmsApi.csproj
```

4. Открыть в браузере:

- `https://localhost:5001` (HTTPS)
- `http://localhost:5000` (HTTP)




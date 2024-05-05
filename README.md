# Logs Monitor

Для работы приложению нужна СУБД PostgreSQL.
Для запуска приложения и СУБД в Docker-контейнерах настроен Docker Compose.

## Переменные окружения

Чтобы запустить проект, нужно создать два файла в корне проекта с переменными окружения для сервисов.

**db.env**
```
POSTGRES_PASSWORD={Пароль postgres пользователя}
```

**api.env**
```
ConnectionStrings__DefaultConnectionString=Host=db;Port=5432;Database=LogsMonitor;Username=postgres;Password={Пароль postgres пользователя}
ASPNETCORE_ENVIRONMENT={Конфигурация приложения (Development | Production)}
```

## Запуск

Чтобы запустить приложение, необходимо иметь установленный Docker на вашем устройстве. 
Используйте следующую команду в корне проекта для запуска:
```
docker-compose up --build
```
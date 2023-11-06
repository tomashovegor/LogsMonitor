# Общая инструкция для работы с миграциями

> #### Замечание к выполнению команд
> - Все команды из документа проверялись и выполнялись в терминале VS
> - Открыть терминал:  В VS правой кнопкой мыши на проект 'LogsMonitor.DataAccess.MSSQL', кликаем на
   пункт 'Открыть в терминале'

#### Установка ряда переменных сессии, определяющих контекст выполнения команд
- **Имя переменной окружения**  
  `$env:ASPNETCORE_ENVIRONMENT="Development"`
- **Путь к запускаемому проекту**  
  `$project="../LogsMonitor"`
- **Каталог с файлами миграций**  
  `$migration_dir="Migrations/"`
- **Имя контекста EF**  
  `$ctx="DBContext"`

#### Примеры команд, составленных из набора переменных выше
1. Добавление новой миграции для контекста  
   `dotnet ef migrations add "Initial" -o $migration_dir -c $ctx -s $project`
2. Удаление последней миграции  
   `dotnet ef migrations remove -c $ctx -s $project`
3. Применение миграций к БД  
   `dotnet ef database update -s $project -c $ctx`
4. Удаление всех данных контекста из БД
   `dotnet ef database update -s $project -c $ctx 0`
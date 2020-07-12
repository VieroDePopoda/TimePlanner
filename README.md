# TimePlanner
Ежедневник
Написано на ASP.NET Core MVC (Core 3.1) MS SQL(EF Core).

Примененные технологии/фреймворки:
  - EF Core
  - SignalR
  - JQuery

Программа содержит:
  - Добавление/Редактирование/Удаление.
  - Помечать записи выполненными.

Все действия выполняются через SignalR(в реальном времени).

Процесс запуска: 
  1)Клонировать репозиторий 
  2)БД TimePlannerDB поместить в C:/Users/имяпользователя 
  3)Пересобрать проект и запустить.

В случае если БД отказывается работать: 
  1)Удалить все файлы из папки Migrations(Remove-Migration) 
  2)Добавить новую миграцию(Add-Migration initial) 
  3)Проверить еще раз.

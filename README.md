Распределитель запросов между работниками с GUI.
Распределяет приходящие запросы между штатом персонала по следующим критериям:

Operator - должен брать любой запрос\
Manager - должен брать запрос, время ожидания которого > Tмен (когда нет свободных операторов)\
Director - должен брать запрос, время ожидания которого > Tдир (когда нет свободных операторов и менеджеров)

Tмен и Тдир - конфигурируемые значения (по умолчанию 20 и 40 сек)

Время испольнения запроса сотрудником имитируется рандомным значением в диапазоне от 10 до 50 сек (также конфигурируемые значения)

В GUI представлены две таблицы - запросы и сотрудники. Сотрудника можно добавить/изменить/уволить. Настройки также можно сконфигурировать.
Запросы добавляются только через API

При отправке по API запроса на создание новой заявки (POST api/calls), таблицы обновляются в реальном времени (веб-сокет). Также любые модификационные действия распределителя запросов (реализация IHostedService) вызывает обновление таблиц.
<img width="1440" alt="image" src="https://user-images.githubusercontent.com/14348827/196545452-94fbe3ac-21cd-4c76-b5e0-3715e2662dc8.png">

Это не продуктивное и не промышленное решение. 
Это мини-приложение с охватом интересных фич в .NET, где-то с намеренно синтетическим подходом к решению задач, с поправкой на маленький объем данных и локальное расположение базы данных (SQLite).
Стоит понимать, что в реальной промышленной среде, подобная задача намного сложнее и требует более детального подхода к архитектуре решения.

Приложение частично покрыто модульными тестами, интеграционные в планах. 

Полный стек:

.NET 5\
DAL - EF, SQLite\
Mapping - AutoMapper\
Документация - Swagger\
Тесты - xUnit, Moq\
Front-end - React JS\
Веб-сокеты - Signal R\
Логирование - Serilog

Методы API можно поссмотреть в swagger:
/swagger/index.html


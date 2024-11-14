# Wi-Fi Network Scanner and Saver

Этот проект представляет собой приложение для сканирования доступных Wi-Fi сетей, отображения их в интерфейсе и сохранения в базе данных. Используется технология WPF и MVVM.

## Описание

Приложение выполняет следующие функции:
- Сканирует доступные Wi-Fi сети с использованием команды `netsh wlan show networks`.
- Отображает список найденных сетей, включая название (SSID) и уровень сигнала.
- Сохраняет информацию о найденных сетях в базу данных (SQL Server).
- Отображает "лучшую" сеть, то есть сеть с наибольшим уровнем сигнала.

## Структура проекта

Проект состоит из следующих основных компонентов:

- **WifiScannerHelper** — класс для выполнения команды сканирования Wi-Fi сетей и парсинга результатов.
- **WifiDatabaseHelper** — класс для работы с базой данных, включает сохранение и обновление информации о сетях.
- **ViewModel (WifiViewModel)** — класс для обработки логики приложения (MVVM), включая команды для сканирования и сохранения сетей.
- **Model (WifiDatabase)** — модель для представления данных о Wi-Fi сетях (SSID и уровень сигнала).

## Установка

1. Клонируйте репозиторий:

   ```bash
   git clone https://github.com/yourusername/your-repository-name.git
2. Редактируете файл ```Db_Connect.config``` под свою БД:

   ```bash
   connectionString="Server=(server)\SQLEXPRESS;Database=X_Tech_db;Integrated Security=True;"
3. Создайте БД и таблицу:
   ```bash
   create database X_Tech_db
   use X_Tech_db
   go
   CREATE TABLE WifiNetworks (
    Id INT PRIMARY KEY IDENTITY,
    SSID NVARCHAR(100) NOT NULL,
    SignalStrength INT NOT NULL
   );



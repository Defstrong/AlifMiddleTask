# Электронный Кошелек API

Этот проект представляет собой REST API для финансового учреждения, предоставляющего услуги электронного кошелька. API поддерживает два типа учетных записей: идентифицированные и неидентифицированные, и предоставляет методы для взаимодействия с электронными кошельками.

# Технологии:
- **ASP.NET CORE WEB API**
- **Entity Framework core**
- **PostgreSQL**

## Аутентификация

Клиенты должны быть аутентифицированы через заголовки HTTP:
- **X-UserId**: Идентификатор пользователя.
- **X-Digest**: HMAC-SHA1 хэш-сумма тела запроса.

## Методы API

### 1. Проверить существует ли аккаунт электронного кошелька

- **Метод:** `POST` - Согласно требованию задания
- **Путь:** `/api/wallet/check/{wallet id}`
- **Аутентификация:** `Заголовки X-UserId и X-Digest`
### 2. Пополнение электронного кошелька
- **Метод:** `POST` - Согласно требованию задания
- **Путь:** `/api/wallet/toup`
- **Тело запроса:**
```json
{
  "walletId": "wallet id",
  "quantity": 500
}
```
### 3. Получить общее количество и суммы операций пополнения за текущий месяц
- **Метод:** `POST` - Согласно требованию задания
- **Путь:** `/api/wallet/sum-of-transactions/{wallet id}`
### 4. Получить баланс электронного кошелька
- **Метод:** `POST` - Согласно требованию задания
- **Путь:** `/api/wallet/balance/{wallet id}`
- **Ограничения по балансу**
- **Максимальный баланс для неидентифицированных счетов:** `10,000 сомони`
- **Максимальный баланс для идентифицированных счетов:** `100,000 сомони`
### Хранение
Для хранения данных было использованно PostgreSQL

### Инструкции по развертыванию
#### 1.Клонируйте репозиторий:
```bash
git clone https://github.com/Defstrong/AlifMiddleTask.git
```

#### 2.Перейдите в директорию проекта:

```bash
cd AlifMiddleTask
```
#### 3.Обновите конфигурацию API в соответствии с вашими
> Но для того чтоб логирование работало нормально поместить это в файл конфигурации API:
```json
  "Serilog": {
    "Using":  [ "Serilog.Sinks.Console", "Serilog.Sinks.File" ],
    "MinimumLevel": "Debug",
    "WriteTo": [
      {
        "Name": "Console"
      },
      { "Name": "File",
        "Args":
        {
          "path": "logs/log.txt",
          "formatter": "Serilog.Formatting.Compact.CompactJsonFormatter, Serilog.Formatting.Compact"
        }
      }
    ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ]
  },
```
#### 4. Соберите проэкт:
``` bash
dotnet build
```
#### 5.Перейдите по директории для запуска API:
``` bash
cd src/Presentation
```

#### 6.Запустите приложение:
```bash
dotnet run
```


#### 7. Используйте API!

### Лицензия
Этот проект лицензирован в соответствии с MIT. Подробности смотрите в файле LICENSE.

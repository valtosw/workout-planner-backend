# Звіт про статичний аналізатор коду

## Інформація про репозиторій

| Атрибут        | Значення                                                                                                |
| --------------- | ---------------------------------------------------------------------------------------------------- |
| Repository      | [workout-planner-backend](https://github.com/valtosw/workout-planner-backend?utm_source=chatgpt.com) |
| Language        | C#                                                                                                   |
| Framework       | ASP.NET Core                                                                                         |
| Static Analyzer | [SonarQube Cloud](https://sonarcloud.io?utm_source=chatgpt.com)                                      |
| Analysis Type   | Static Code Analysis                                                                                 |

---

# 1. Вступ

Програмний проєкт реалізовано за допомогою SonarQube Cloud.

---

# 2. Якість програмного забезпечення

| Категорія        | Проблеми |
| --------------- | ------ |
| Security        | 0      |
| Reliability     | 4      |
| Maintainability | 104    |

Більшість виявлених проблем пов'язані з maintainability, а не з критичними проблемами виконання чи безпеки.

---

# 3. Розподіл ступеня серйозності

| Серйозність | Кількість |
| -------- | ----- |
| Blocker  | 0     |
| High     | 2     |
| Medium   | 100   |
| Low      | 6     |

Більшість проблем є проблемами підтримки середнього ступеня серйозності, які негативно впливають на читабельність, підтримку та довгострокову масштабованість.

---

# 4. Розподіл типів проблем

| Тип            | Кількість |
| --------------- | ----- |
| Bugs            | 2     |
| Vulnerabilities | 0     |
| Code Smells     | 106   |

Проєкт містить дуже мало фактичних помилок і жодних вразливостей, але було виявлено велику кількість code smells.

---

# 5. Розподіл атрибутів коду

| Атрибут      | Кількість |
| -------------- | ----- |
| Consistency    | 97    |
| Intentionality | 7     |
| Adaptability   | 2     |
| Responsibility | 2     |

Більшість проблем пов'язані з проблемами узгодженості, такими як дублювання, обробка посилань, що допускають значення null, та неузгоджені практики написання коду.

---

# 6. Основні метрики коду

| Метрика                 | Значення    | Опис                                                                  |
| ---------------------- | -------- | ---------------------------------------------------------------------------- |
| Vulnerabilities        | 0        | Жодних вразливостей безпеки не виявлено.                                   |
| Security Rating        | A        | Проєкт демонструє гарну загальну якість безпеки.                     |
| Security Hotspots      | 4        | Потенційно вразливі розділи коду, що потребують ручної перевірки.                 |
| Bugs                   | 2        | Невелика кількість проблем, пов'язаних з надійністю.                                  |
| Reliability Rating     | C        | Деякі проблеми, пов'язані з виконанням, можуть впливати на стабільність програми.                |
| Code Smells            | 106      | Велика кількість проблем, пов'язаних з maintainability.                           |
| Maintainability Rating | A        | Загальна maintainability все ще вважається хорошою, незважаючи на недоліки коду.        |
| Technical Debt         | 1h 26min | Орієнтовний час, необхідний для виправлення проблем з maintainability.                       |
| Debt Ratio             | 0.1%     | Технічний борг порівняно з розміром проекту є відносно низьким.                   |
| Coverage               | 0.0%     | Наразі кодову базу не охоплюють жодні автоматизовані тести.                      |
| Lines to Cover         | 932      | Кількість виконуваних рядків, що потребують тестів.                                  |
| Uncovered Lines        | 932      | Усі виконувані рядки залишаються непротестованими.                                        |
| Duplication Density    | 7.3%     | Помірно велика кількість дублікованого коду.                                   |
| Duplicated Lines       | 229      | У проєкті присутня повторювана логіка.                                   |
| Duplicated Blocks      | 10       | Було виявлено кілька дублікатів фрагментів коду.                           |
| Lines of Code          | 2665     | Загальний обсяг фактичного вихідного коду.                                          |
| Files                  | 55       | Кількість проаналізованих вихідних файлів.                                             |
| Classes                | 53       | Загальна кількість класів у проєкті.                                     |
| Functions              | 421      | Загальна кількість методів/функцій.                                           |
| Cyclomatic Complexity  | 525      | Вказує на велику кількість незалежних шляхів виконання та логіки розгалуження. |
| Cognitive Complexity   | 118      | Вказує на області коду, які важко зрозуміти та підтримувати.       |
| Comment Density        | 2.3%     | Кодова база містить дуже мало документації/коментарів.                    |

---

# 7. Аналіз складності

## Cyclomatic Complexity

Цикломатична складність вимірює кількість незалежних шляхів виконання всередині програми. Проєкт має загальну цикломатичну складність 525, що вказує на:

* велику логіку розгалуження
* багато умовних операторів
* складний потік керування
* потенційно складне тестування та дебагінг

Висока цикломатична складність часто є результатом:

* вкладених операторів `if`
* кількох циклів
* складної логіки перевірки
* великих методів контролера

---

## Cognitive Complexity

Когнітивна складність вимірює, наскільки складним є розуміння коду з точки зору розробника. Значення когнітивної складності проєкту становить 118.

Це вказує на:

* складну читабельність деяких методів
* глибоко вкладену логіку
* складні умовні структури
* методи, які слід спростити або розкласти

На відміну від цикломатичної складності, когнітивна складність зосереджена на розумінні та зручності підтримки людиною.

---

# 8. Правила, виявлені під час аналізу

У проєкті було виявлено такі правила аналізатора SonarQube та Roslyn:

| Правило                                                          | Опис                                                                  |
| -------------------------------------------------------------- | ---------------------------------------------------------------------------- |
| CS8618                                                         | Non-nullable property must contain a non-null value when exiting constructor |
| CS8601                                                         | Possible null reference assignment                                           |
| CS8602                                                         | Dereference of a possibly null reference                                     |
| CS8604                                                         | Possible null reference argument                                             |
| CS8625                                                         | Cannot convert null literal to non-nullable reference type                   |
| CA1861                                                         | Prefer `static readonly` fields over constant array arguments                |
| CA1854                                                         | Prefer `TryGetValue()` instead of `ContainsKey()` + indexer                  |
| CA1816                                                         | Dispose methods should call `GC.SuppressFinalize()`                          |
| ASP0015                                                        | ASP.NET routing/performance issue                                            |
| CA1050                                                         | Types should be defined in named namespaces                                  |
| CS9113                                                         | Parameter is unread                                                          |
| SYSLIB1045                                                     | Use generated regular expressions for better performance                     |
| Cognitive Complexity of methods should not be too high         | Методи занадто складні для розуміння та підтримки                         |
| Boolean literals should not be redundant                       | Непотрібні логічні вирази знижують читабельність                           |
| Always set the `DateTimeKind` when creating `DateTime` objects | Запобігає проблемам із часовими поясами та серіалізацією                                   |
| Utility classes should not have public constructors            | Допоміжні/утилітарні класи не слід створювати через конструктори                            |
| Conditionally executed code should be reachable                | Виявляє недосяжні гілки коду                                            |
| Types should be defined in named namespaces                    | Покращує організацію та зручність обслуговування проєкту                            |

---

# 9. Пояснення правила

## Обране правило: Cognitive Complexity of Methods Should Not Be Too High

Це правило виявляє методи, які занадто складні для розуміння через надмірне розгалуження, вкладені умови, цикли або складні логічні структури.

Приклад проблемного коду:

```csharp
[HttpGet("WeightLifted/{id}/{exerciseName}/{period}")]
public async Task<IEnumerable<LogDto>> GetWeightLiftedByExercise(string id, string exerciseName, string period = "Month")
{
    var logs = await context.ProgressLogs
        .Where(log => log.CustomerId == id && log.Exercise.Name == exerciseName)
        .OrderBy(log => log.LogDate)
        .ToListAsync();
            
    if (logs is null || logs.Count == 0)
    {
        return [];
    }
            
    if (period.Equals("Month", StringComparison.OrdinalIgnoreCase))
    {
        var currentDate = DateTime.UtcNow;
        int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
        var logsByDay = logs
            .Where(log => log.LogDate.Year == currentDate.Year && log.LogDate.Month == currentDate.Month)
            .ToDictionary(log => log.LogDate.Day, log => log);
        var result = new List<LogDto>();
        
        for (int day = 1; day <= daysInMonth; day++)
        {    
            if (logsByDay.TryGetValue(day, out var log))
            {
                result.Add(new LogDto { LogDate = log.LogDate, Weight = log.Weight });
            }
            else
            {
                result.Add(new LogDto { LogDate = new DateTime(currentDate.Year, currentDate.Month, day), Weight = 0 });
            }
        }
        return result;
    }
    else if (period.Equals("Year", StringComparison.OrdinalIgnoreCase))
    {
        var currentYear = DateTime.UtcNow.Year;
        var groupedLogs = logs
            .Where(log => log.LogDate.Year == currentYear)
            .GroupBy(log => log.LogDate.Month)
            .ToDictionary(g => g.Key, g => g.Max(log => log.Weight));
        var result = new List<LogDto>();
        
        for (int month = 1; month <= 12; month++)
        {
            result.Add(new LogDto
            {
                LogDate = new DateTime(currentYear, month, 1),
                Weight = groupedLogs.ContainsKey(month) ? groupedLogs[month] : 0
            });
        }

        return result;
    }
}
```

Проблеми, спричинені високою когнітивною складністю:

* складний дебагінг
* складніше обслуговування
* підвищена ймовірність помилок
* погана читабельність
* складніше тестування

Рекомендований рефакторинг:

```csharp
[HttpGet("WeightLifted/{id}/{exerciseName}/{period}")]
public async Task<IEnumerable<LogDto>> GetWeightLiftedByExercise(
    string id,
    string exerciseName,
    string period = "Month")
{
    var logs = await GetExerciseLogs(id, exerciseName);

    if (logs.Count == 0)
    {
        return [];
    }

    return period.ToLowerInvariant() switch
    {
        "month" => BuildMonthlyLogs(logs),
        "year" => BuildYearlyLogs(logs),
        _ => []
    };
}

private async Task<List<ProgressLog>> GetExerciseLogs(string customerId, string exerciseName)
{
    return await context.ProgressLogs
        .Where(log =>
            log.CustomerId == customerId &&
            log.Exercise.Name == exerciseName)
        .OrderBy(log => log.LogDate)
        .ToListAsync();
}

private static IEnumerable<LogDto> BuildMonthlyLogs(List<ProgressLog> logs)
{
    var currentDate = DateTime.UtcNow;
    int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);

    var logsByDay = logs
        .Where(log =>
            log.LogDate.Year == currentDate.Year &&
            log.LogDate.Month == currentDate.Month)
        .ToDictionary(log => log.LogDate.Day, log => log);

    var result = new List<LogDto>();

    for (int day = 1; day <= daysInMonth; day++)
    {
        if (logsByDay.TryGetValue(day, out var log))
        {
            result.Add(new LogDto
            {
                LogDate = log.LogDate,
                Weight = log.Weight
            });
        }
        else
        {
            result.Add(new LogDto
            {
                LogDate = new DateTime(
                    currentDate.Year,
                    currentDate.Month,
                    day,
                    0,
                    0,
                    0,
                    DateTimeKind.Utc),
                Weight = 0
            });
        }
    }

    return result;
}

private static IEnumerable<LogDto> BuildYearlyLogs(List<ProgressLog> logs)
{
    int currentYear = DateTime.UtcNow.Year;

    var groupedLogs = logs
        .Where(log => log.LogDate.Year == currentYear)
        .GroupBy(log => log.LogDate.Month)
        .ToDictionary(
            group => group.Key,
            group => group.Max(log => log.Weight));

    var result = new List<LogDto>();

    for (int month = 1; month <= 12; month++)
    {
        groupedLogs.TryGetValue(month, out var weight);

        result.Add(new LogDto
        {
            LogDate = new DateTime(
                currentYear,
                month,
                1,
                0,
                0,
                0,
                DateTimeKind.Utc),
            Weight = weight
        });
    }

    return result;
}
```

---

# 10. Файли з найбільшою кількістю проблем

## 10.1 `Tests/UnitTests/Controllers/AuthControllerTests.cs`

| Проблема             | Опис                                                    |
| ------------------- | -------------------------------------------------------------- |
| CS8625              | Cannot convert null literal to non-nullable reference type     |
| CA1816              | `Dispose()` should call `GC.SuppressFinalize()`                |
| Header access issue | `Set-Cookie` should be accessed using the `SetCookie` property |

Цей файл містить багато проблем із null references та проблем із керуванням ресурсами.

---

## 10.2 `WorkoutPlanner/Controllers/ProgressController.cs`

| Проблема              | Опис                                                   |
| -------------------- | ------------------------------------------------------------- |
| CA1861               | Prefer `static readonly` fields over constant array arguments |
| Cognitive Complexity | Складність методу перевищує дозволений поріг                  |
| DateTimeKind issue   | `DateTime` objects created without specifying `DateTimeKind`  |
| CA1854               | Prefer `TryGetValue()` instead of double dictionary lookup    |
| Unused variable      | Local variable `customer` is unused                           |

Цей контролер містить проблеми з продуктивністю, читабельністю та maintainability.

---

## 10.3 `WorkoutPlanner/Models/DTOs/TrainerDto.cs`

| Проблема | Опис                                 |
| ------- | ------------------------------------------- |
| CS8618  | Non-nullable properties are not initialized |

Моделі DTO бракує належної обробки nullable references та ініціалізації об'єктів.

---

## 10.4 `WorkoutPlanner/Controllers/CustomerController.cs`

| Проблема | Опис                        |
| ------- | ---------------------------------- |
| CS8601  | Possible null reference assignment |

---

## 10.5 `WorkoutPlanner/Controllers/AuthController.cs`

| Проблема          | Опис                            |
| ---------------- | -------------------------------------- |
| CS9113           | Parameter `roleManager` is unread      |
| Unused variable  | `createUserResult` is unused           |
| Unreachable code | Condition always evaluates to `False`  |
| CS8604           | Possible null reference argument       |
| CS8602           | Dereference of possibly null reference |

Цей контролер містить проблеми безпеки з можливістю використання null-значень, непрацюючий код та невикористані змінні.

---

# 11. Запропонований рефакторинг

Для покращення maintainability доцільно внести такі зміни:

| Проблемна зона               | Запропонований рефакторинг                                    | Очікуване покращення                     |
| --------------------------- | --------------------------------------------------------- | ---------------------------------------- |
| High Cognitive Complexity   | Розділити великі методи на менші цільові методи          | Покращена читабельність та maintainability |
| Nested Conditions           | Замінити вкладені оператори `if` на guard clauses         | Знижена когнітивна складність             |
| Duplicate Code              | Вилучення спільної логіки у сервіси повторного використання       | Зменення дублікованого коду           |
| Nullable Reference Issues   | Використати анотації, конструктори та валідацію з можливістю використання null-значень    | Підвищена надійність                     |
| Unused Variables/Parameters | Прибрати невикористані змінні та залежності                  | Чистіший та більш підтримуваний код       |
| Missing `DateTimeKind`      | Явно вказати `DateTimeKind.Utc` або `Local`          | Запобігання помилкам, пов'язаним з часовими поясами            |
| Dictionary Double Lookup    | Замінити `ContainsKey()` + індексатор на `TryGetValue()`    | Краща продуктивність                       |
| Utility Classes             | Зробити допоміжні класи "статичними" або додати приватні конструктори | Покращена узгодженість архітектури        |
| Missing Tests               | Додати тести                           | Підвищена надійність та maintainability |
| Low Documentation           | Додати XML-коментарі та документацію методів                 | Легший онбординг та обслуговування        |
| Dispose Pattern Issues      | Додати `GC.SuppressFinalize(this)` в `Dispose()`            | Правильне керування ресурсами              |
| Dead/Unreachable Code       | Видалити недосяжні умови та зайві гілки      | Чистіший control flow                     |

---

# 12. Очікуваний результат після рефакторингу

Після застосування рекомендованих рефакторингів очікується, що проєкт досягне таких покращень:

| Зона            | Очікуваний результат                                            |
| --------------- | ---------------------------------------------------------- |
| Maintainability | Зменшення кількості code smells та покращення читабельності     |
| Reliability     | Менше помилок, пов'язаниз з null            |
| Complexity      | Нижча когнітивна та цикломатична складність                  |
| Duplication     | Зменшення відсотка дублювання коду                         |
| Performance     | Покращений доступ до словника та оптимізоване використання об'єктів     |
| Architecture    | Чіткіше розподілення обов'язків та краща організація     |
| Testing         | Підвищена впевненість під час майбутніх змін та рефакторингу |
| Scalability     | Легша розробка та підтримка майбутніх функцій          |
| Documentation   | Покращена зрозумілість для розробників                  |
| Code Quality    | Вища довгострокова стабільність та maintainability             |

---


# 13. Висновки

Статичний аналіз показав, що проєкт загалом стабільний та безпечний, але він містить значну кількість проблем, пов'язаних з підтримкою.

Найважливішими недоліками, виявленими під час аналізу, є:

* висока складність коду
* відсутність автоматизованих тестів
* дубльована логіка
* проблеми з null reference types
* недостатня документація

Незважаючи на велику кількість недоліків коду, рейтинг підтримки залишається високим, оскільки більшість проблем є відносно невеликими та можуть бути виправлені поступово з обмеженим технічним боргом.

Запропонований рефакторинг значно покращить:

* читабельність
* підтримку
* масштабованість
* надійність
* довгострокову якість коду

Проєкт демонструє міцну основу, але виграв би від додаткового рефакторингу, покращених методів тестування та кращої організації коду.

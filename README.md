# Plumsail

Римский калькулятор, парсит строковое математическое выражение, считает, и возвращает ответ римским числом.

**Поддерживаемые платформы:**
* .NET 6
* .NET Standard 2.0

**Поддерживаемые римские символы:** IVXLCDM

**Поддерживаемые операции:**
* Сложение +
* Вычитание -
* Умножение * 
* Деление / 
* Изменить приоритет операций ()

**Поддерживаемые римские числа:** 
* Без знака минуса
* Со знаком минуса
* С плавающей точкой (округление до 3 знаков после запятой)

**Поддерживаемые опции в командной строке:**
* -e, --expr — римское математическое выражение
* -w — продолжить работу приложения после расчёта математического выражения

**Дополнительно:**

* Предусмотрено удобное добавление кастомных операций в калькулятор
* Предусмотрен метод регистрации калькулятора в DI контейнере
* Предусмотрены римские числа с плавающей точкой
* Предусмотрены тесты
* Предусмотрены негативные числа (со знаком минус)
* Предусмотрена операция / деления
* Предусмотрен запуск приложения из командной строки (с автозакрытием, либо продолжением работы)

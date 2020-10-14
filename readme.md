## Создание приложения при помощи Django

### 1. Создаём новый проект
В данном примере для управления виртуальными окружениями и зависимостями будет использоваться pipenv. Создаём в текущем каталоге виртуальное окружение, ставим в него Django и активируем его:
```
pipenv install django
pipenv shell
```
Теперь создадим проект под названием example в текущем каталоге:
```
django-admin startproject example .
```
Перед нами возникает набор файлов:
```
├── example
│   ├── __init__.py
│   ├── asgi.py
│   ├── settings.py
│   ├── urls.py
│   └── wsgi.py
└── manage.py
```
* `manage.py`: утилита командной строки для взаимодействия с проектом
* `example/`: каталог проекта
* `example/__init__.py`: чтобы Python рассматривал каталог как пакет
* `example/settings.py`: настройки проекта
* `example/urls.py`: объявления URL
* `example/asgi.py`: entry-point для ASGI-совместимого веб-сервера
* `example/wsgi.py`: entry-point для WSGI-совместимого веб-сервера

Уже сейчас можно запустить development-сервер и увидеть стандартную страницу пустого проекта:
```
python manage.py runserver
```

Проект содержит информацию о конфигурации. Для того, чтобы начать писать код теперь нужно добавить приложение
```
python manage.py startapp shop
```
При этом создаётся каталог shop со следующим содержимым:
```
└── shop
    ├── __init__.py
    ├── admin.py
    ├── apps.py
    ├── migrations
    │   └── __init__.py
    ├── models.py
    ├── tests.py
    └── views.py
```
Теперь всё готово, чтобы заняться приложением.

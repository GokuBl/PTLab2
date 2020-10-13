## Создание приложения на ASP.NET (C#)

### 1. Создадим новый проект ASP.NET Web Application.
При создании из доступных шаблонов выбираем MVC и отмечаем чекбокс Web API.

Изучим подробнее структуру созданного проекта. Каталог App_Data хранит все необходимые файлы и ресурсы, например, базы данных, используемые приложением. После развертывания приложения только непосредственно приложение может работать с этой папкой, доступ же простых пользователей в эту папку запрещен.

Каталог App_Start включает весь функционал конфигурации приложения,
который до версии 4.0 содержался в файле Global.asax, а теперь перенесен в набор
статичных классов, вызываемых в Global.asax. Эти статичные классы содержат
некоторую логику инициализации приложения, выполняющуюся при запуске.

Каталог Content содержит некоторые вспомогательные файлы, которые не
включают код на c# или javascript, и которые развертываются вместе с
приложением. В частности, здесь могут размещаться файлы стилей css. Так, в этой
папке вы увидите файл Site.css, который содержит стили приложения, а также папку
с темами, включающую стили css и изображения для определенных тем.

Каталог Controllers содержит контроллеры - классы, отвечающие за работу
приложения. По умолчанию здесь находятся всего один контроллер -
HomeController отвечающий за домашнюю страницу.

Каталог Models cодержит модели, используемые приложением. По умолчанию
здесь пусто.

В каталоге Views размещаются представления сгруппированые по папкам,
каждая из которых соответствует одному контроллеру. После получения и
обработки запроса контроллер, отправляет одно из этих представлений,
заполненных некоторыми данными, клиенту. Кроме того, имеется папка общих для
контроллеров представлений - папка Shared.

Каталоги Images и Scripts содержат соответственно изображения и скрипты на
JavaScript, используемые в приложении. По умолчанию эти папки уже содержат
файлы, в частности, в папку Scripts уже помещены файлы библиотеки jQuery.

В корне проекта также находится Файл Web.config - файл конфигурации
приложения.

### 2. Добавим модели
В каталог Models добавим 2 класса моделей:
1. модель товара
```c#
public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
}
```
2. модель покупки 
```c#
public class Purchase
{
    public int ID { get; set; }
    public int ProductID { get; set; }
    public string Person { get; set; }
    public string Address { get; set; }
    public DateTime Date { get; set; }
}
```

### 3. Добавление базы данных
Для хранения данных будем использовать БД, а для доступа к ней EntityFramework. Добавим библиотеку через менеджер пакетов.

Создадим контекст базы данных. Для этого создадим в проекте новый каталог DAL и добавим класс ShopContext:
```c#
using System;
using System.Data.Entity;
using example.Models;

namespace example.DAL
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }
}
```
EntityFramework выполнит все необходимые действия для создания таблиц в
базе данных по коду классов-сущностей. Такой подход принято называть Code First.

В файле Web.config необходимо указать строку подключения к базе данных.
```xml
<configuration>
    ...
    <connectionStrings>
        <add name="ShopContext" connectionString="Data Source=(LocalDb)\MSSQLLocalDB;Integrated Security=True;AttachDBFilename=|DataDirectory|\Products.mdf" providerName="System.Data.SqlClient"/>
    </connectionStrings>
    ...
</configuration>
```
Выражение |DataDirectory| представляет заместитель, который указывает, что
база данных будет создаваться в проекте в папке App_Data.

### 4. Контроллер и представление
В контроллере домашней страницы выполняем чтение данных из БД и их
передачу в вид для отображения в браузере.

```c#
public class HomeController : Controller
{
    private ShopContext db = new ShopContext();
    public ActionResult Index()
    {
        IEnumerable<Product> products = db.Products;
        ViewBag.Products = products;
        return View();
    }
}
```

Далее приводится код представления для отображения на домашней странице списка
доступных товаров:

```html
@{
    Layout = null;
}
<!DOCTYPE html>
<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Товары</title>
</head>
<body>
    <div>
        <h3>Список</h3>
        <table>
            <tr>
                <td><p>Наименование</p></td>
                <td><p>Цена</p></td>
                <td></td>
            </tr>
            @foreach (var p in ViewBag.Products)
            {
                <tr>
                    <td><p>@p.Name</p></td>
                    <td><p>@p.Price</p></td>
                    <td><p><a href="/Home/Buy/@p.ID">Купить</a></p></td>
                </tr>
            }
        </table>
    </div>
</body>
</html>
```

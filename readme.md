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

### 5. Инициализируем базу тестовыми данными
Чтобы база данных не была пустой при запуске приложения создадим специальный класс для её наполнения. В каталоге DAL создадим класс ShopInitializer:
```c#
public class ShopInitializer : DropCreateDatabaseAlways<ShopContext>
{
    protected override void Seed(ShopContext context)
    {
        var products = new List<Product>
        {
        new Product { Name = "Стол", Price = 2000 },
        new Product { Name = "Стул", Price = 1000 },
        new Product { Name = "Табурет", Price = 500 },
        };
        products.ForEach(p => context.Products.Add(p));
        context.SaveChanges();
        base.Seed(context);
    }
}
```

Затем собщим EntityFramework о том, что мы хотим использовать наш инициализатор. Для этого в файле Web.config добавим следующие строчки:
```xml
<entityFramework>
...
    <contexts>
        <context type="example.DAL.ShopContext, example">
            <databaseInitializer type="example.DAL.ShopInitializer, example" />
        </context>
    </contexts>
...
</entityFramework>
```

### 6. Добавляем недостающие страницы
Приложение можно запустить и увидеть в браузере, как выглядит
домашняя страница. На данной странице уже есть ссылки для покупки товаров, однако если нажать на любую из них, то будет выведена ошибка, т. к. пока что нет соответствующей страницы. Исправим это дополнив код контроллера домашней страницы двумя методами:
```c#
[HttpGet]
public ActionResult Buy(int id)
{
    ViewBag.ProductId = id;
    return View();
}

[HttpPost]
public string Buy(Purchase purchase)
{
    purchase.Date = DateTime.Now;
    db.Purchases.Add(purchase);
    db.SaveChanges();
    return "Спасибо за покупку, " + purchase.Person + "!";
}
```
Здесь мы определили одно действие Buy, однако в одном случае оно выполняется при получении запроса GET, а во втором случае - при получении запроса POST, что мы и определили с помощью атрибутов `[HttpGet]` и `[HttpPost]`.

Первый метод принимает id выбранного товара и возвращает для него соответствующее представление. Второй принимает переданную ему в запросе POST модель purchase и добавляет ее в базу данных. В конце мы возвращаем строку сообщения. Также необходимо добавить представление Buy. Это можно сделать через контекстное меню для соответствующего метода контроллера.

Код представления будет следующим:
```html
@{
    Layout = null;
}
<!DOCTYPE html>
<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Покупка</title>
</head>
<body>
    <div>
        <h3>Покупка</h3>
        <form method="post">
            <input type="hidden" value="@ViewBag.ProductId" name="ProductID" />
            <table>
                <tr>
                    <td><p>Введите свое имя </p></td>
                    <td><input type="text" name="Person" /> </td>
                </tr>
                <tr>
                    <td><p>Введите адрес доставки:</p></td>
                    <td>
                        <input type="text" name="Address" />
                    </td>
                </tr>
                <tr><td><input type="submit" value="Отправить" /> </td><td></td></tr>
            </table>
        </form>
    </div>
</body>
</html>
```
Снова запускаем и проверяем.

### 6. Добавляем контроллер Web API
Теперь добавим контроллер Web API, чтобы иметь возможность взаимодействовать с сервисом через REST API. Для этого счёлкаем правий кнопкой мыши в обозревателе решений на каталоге с Controllers и выбираем Добавить контроллер > Контроллер Web API с действиями, использующий Entity Framework. В качестве класса модели выбираем Product, а контекста ShopContext. Запускаем и проверяем /api/Products.

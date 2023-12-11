// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var list = []
var cart = ""
function AddProduct(event) {
    
    list.push(event);
    cart = JSON.stringify(list);

    document.getElementById("ProductIDList").setAttribute("value", cart);
    console.log("AddProductBTN " + event);
    document.getElementById("AddProductBTN " + event).setAttribute("disabled", true);
}


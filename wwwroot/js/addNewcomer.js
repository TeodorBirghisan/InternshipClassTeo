function addNewcomer() {
    var list = document.getElementById("list");
    var newcomer = document.getElementById("newcomer");
    var node = document.createElement("li");
    node.innerText = newcomer.value;
    list.appendChild(node);
}

document.getElementById("add").onclick = function () { addNewcomer() };
var minusBtn = document.getElementById("minus-btn");
var plusBtn = document.getElementById("plus-btn");
var inputField = document.querySelector(".form-control");

minusBtn.addEventListener("click", function () {
    var currentValue = parseInt(inputField.value);
    if (currentValue > 1) {
        inputField.value = currentValue - 1;
    }
});

plusBtn.addEventListener("click", function () {
    var currentValue = parseInt(inputField.value);
    if (currentValue < 10) {
        inputField.value = currentValue + 1;
    }
});
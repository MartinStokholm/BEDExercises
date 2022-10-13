"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/expensehub").build();

connection.on("NewExpense", function (expense) {

    var exepenseStr = JSON.stringify(expense);
    console.log("NewExpense: " + exepenseStr);

    var ModelId = JSON.stringify(expense.modelId);
    var JobId = JSON.stringify(expense.jobId);
    var Date = JSON.stringify(expense.date);
    var Text = JSON.stringify(expense.text);
    var Amount = JSON.stringify(expense.amount);

    var newList = document.createElement("li");
    var now = new Date();
    newList.textContent = `${now.toLocaleTimeString()} - New expense logged: `;

    var expenseItemsList = document.createElement("ul");

    newList.appendChild(expenseItemsList);

    var modelId = document.createElement("li");
    var jobId = document.createElement("li");
    var date = document.createElement("li");
    var text = document.createElement("li");
    var amount = document.createElement("li");

    expenseItemsList.append(modelId);
    expenseItemsList.appendChild(jobId);
    expenseItemsList.appendChild(date);
    expenseItemsList.appendChild(text);
    expenseItemsList.appendChild(amount);

    expenseItemsList.style.fontSize = "12px";
    
    modelId.textContent = ModelId;
    jobId.textContent = `JobId: ${JobId}`;
    date.textContent = `Date: ${Date}`;
    text.textContent = `Text: ${Text}`;
    amount.textContent = `Amount: ${Amount}`

    document.getElementById("expensesList").appendChild(newList);
});

connection.start().then(function () {})
    .catch(function (err)
    {
        return console.error(err.toString());
    });
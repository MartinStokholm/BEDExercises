var connection = new signalR.HubConnectionBuilder().withUrl("/expensehub").build();
connection.on("expenseadded", function (expense) {
    
    console.log("New Expense Added");
    var expenseObject = JSON.stringify(expense);
    
    var expenseItem = document.createElement("li");
    
    expenseItem.textContent = expenseObject;

    document.getElementById("expensesList").appendChild(expenseItem);
    
});

connection.start().then(function () {})
    .catch(function (err)
    {
        return console.error(err.toString());
    });
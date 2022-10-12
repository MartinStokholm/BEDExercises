"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/messageHub").build();

connection.on("ReceiveMessage", function (expense, modelname, jobname) {
    var li = document.createElement("li");
    var messageContainer = document.getElementById("messageContainer");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${expense.date.toString()} - New expense logged: `;
    var modelP = document.createElement("p");
    var customerP = document.createElement("p");
    var amountP = document.createElement("p");
    var commentP = document.createElement("p");
    
    messageContainer.appendChild(modelP);
    messageContainer.appendChild(customerP);
    messageContainer.appendChild(amountP);
    messageContainer.appendChild(commentP);
    
    modelP.textContent = `Model: ${modelname}`;
    customerP.textContent = `Customer: ${jobname}`;
    amountP.textContent = `Amount: ${expense.amount}`;
    commentP.textContent = `Comment: ${expense.text}`;


});

connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});
;


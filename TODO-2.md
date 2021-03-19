Sprint 2:

In this second “sprint” of the mini-project, you are required to:

1. declare a function dtgVAT: int -> float such that the value dtgVAT n x is obtained by increasing x by n percent. Use any percent value for your VAT. // done

2. Implement a function to order a drink or leave a comment to in a concurrent way using dtgVAT function to charge VAT when the drink is of type coffee.

   Hints: The OrderDrink message processing should use the Price calculation function that returns the price for the specified drink multiplied by the given quantity:

   F# has both shared-memory concurrency and message-passing concurrency. As discussed during the lessons,
   F# has a built-in mailbox processor concept that is popular in Erlang and Akka Scala.

   This built-in mailbox processor is defined in the F# library as a type called MailboxProcessor and usually referred to as an Agent or Actor.

   type DTGCafeMessage = | OrderDrink of AllDrinks \* int // Drink, qty
   | LeaveAComment of string // ”Comment-super!”

   Implement an agent (call it dtgCafeAgent) that receives a DTGCafeMessage declared above and prints a message with the price of the drink to the sender (for OrderDrink).
   Further, a second message LeaveAComment of string (ie. “Comment”) to leave a string comment to the drink to go operators. Acknowledge the comments with your own ideas.

   Example:> dtgCafeAgent.Post(OrderDrink(Coffee{type=Latte; Size=Small}, 2));;

   Should give for instance: > Please pay DKK34.00 for your 2 Latte coffee drinks. Thanks!

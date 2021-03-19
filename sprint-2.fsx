// Sprint 1

// TODO 1: Create different types of drinks (coffee, tea, juice, soda, alcohol)
// TODO 2: Add corresponding sizes (Small, Medium, and Large) small = price*1; medium = price*1.5; large = price*2
// TODO 3: Add different prices.

open System

type DrinkType =
    | Coffee
    | Tea
    | Juice
    | Soda
    | Milk

type Size =
    | Small
    | Medium
    | Large


type Drink = { type': DrinkType; size: Size }

type MenuForSmallDrinks =
    { Coffee: float
      Tea: float
      Juice: float
      Soda: float
      Milk: float }

let menu =
    { Coffee = 20.00
      Tea = 10.50
      Juice = 15.00
      Soda = 8.50
      Milk = 12.50 }

let price_list =
    [ for size in [ Small; Medium; Large ] do
          match size with
          | Small ->
              yield ({ type' = Coffee; size = Small }, menu.Coffee)
              yield ({ type' = Tea; size = Small }, menu.Tea)
              yield ({ type' = Juice; size = Small }, menu.Juice)
              yield ({ type' = Soda; size = Small }, menu.Soda)
              yield ({ type' = Milk; size = Small }, menu.Milk)
          | Medium ->
              yield ({ type' = Coffee; size = Medium }, menu.Coffee * 1.5)
              yield ({ type' = Tea; size = Medium }, menu.Tea * 1.5)
              yield ({ type' = Juice; size = Medium }, menu.Juice * 1.5)
              yield ({ type' = Soda; size = Medium }, menu.Soda * 1.5)
              yield ({ type' = Milk; size = Medium }, menu.Milk * 1.5)
          | Large ->
              yield ({ type' = Coffee; size = Large }, menu.Coffee * 2.0)
              yield ({ type' = Tea; size = Large }, menu.Tea * 2.0)
              yield ({ type' = Juice; size = Large }, menu.Juice * 2.0)
              yield ({ type' = Soda; size = Large }, menu.Soda * 2.0)
              yield ({ type' = Milk; size = Large }, menu.Milk * 2.0) ]

price_list

let rec search dict key =
    match dict with
    | [] -> 0.0
    | (k, v: float) :: _ when k = key -> v
    | _ :: tl -> search tl key

let getPrice drink =
    match drink with
    | { type' = t; size = size } -> search price_list { type' = t; size = size }


getPrice { type' = Juice; size = Large }


// Sprint 2

let VAT = 20.0

let dgtVAT (x: float) n = x + ((x * n) / 100.0)

dgtVAT 200.0 VAT // testing

type DTGCafeMessage =
    | OrderDrink of Drink * float // Drink, qty
    | LeaveAComment of string // ”Comment-super!”


type Bar() =
    let dtgCafeAgent =
        MailboxProcessor.Start
            (fun inbox ->
                let rec messageLoop () =
                    async {
                        let! msg = inbox.Receive()

                        match msg with
                        | (drink, amount) ->
                            let drinkPrice : float = getPrice drink
                            let mutable totalPrice = ((drinkPrice: float) * (amount: float))

                            if drink.type' = Coffee then
                                totalPrice <- dgtVAT totalPrice VAT

                            printfn
                                "Please pay DKK%d for your %d %A drinks. %s!"
                                (Convert.ToInt32(totalPrice))
                                (Convert.ToInt32(amount))
                                (string drink.type')
                                 "Thanks!"
                        return! messageLoop ()
                    }

                messageLoop ())

    member this.Order msg = dtgCafeAgent.Post msg



let bar = Bar()
let testDrink = { type' = Coffee; size = Small }

//let orderMsg : (DTGCafeMessage, DTGCafeMessage) = ((testDrink, 2.0) , "Thanks")

bar.Order ({type'=Coffee; size=Small}, 2.0)




let listOfDrinks =
    [ (({ type' = Coffee; size = Small }, 2.0))
      (({ type' = Coffee; size = Medium }, 3.0))
      (({ type' = Soda; size = Large }, 1.0))
      (({ type' = Juice; size = Small }, 2.0))
      (({ type' = Juice; size = Large }, 4.0)) ]

let simulateOrders =
    let bar = Bar()
    listOfDrinks
        |> List.map (fun o -> bar.Order o)


let task = 
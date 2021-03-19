// TODO 1: Create different types of drinks (coffee, tea, juice, soda, alcohol)
// TODO 2: Add corresponding sizes (Small, Medium, and Large) small = price*1; medium = price*1.5; large = price*2
// TODO 3: Add different prices.

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
    { Coffee = 20.0
      Tea = 10.5
      Juice = 15.0
      Soda = 8.5
      Milk = 12.5 }

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
    | [] -> None
    | (k, v) :: _ when k = key -> Some v
    | _ :: tl -> search tl key

let getPrice drink =
    match drink with
    | { type' = t; size = size } -> search price_list { type' = t; size = size }


getPrice { type' = Juice; size = Large }

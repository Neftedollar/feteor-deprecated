module TstsClient
open Fable.Core
open Fable.Import.Meteor.Meteor
open MyCollections
open Fable.Core.JsInterop

let private func = (fun _ ->
    let count = todos.find().count()
    if count = 0 then
        printfn "zero client!"
        for x in 1..10 do
                let todo = {
                    Todos.Title = "title"
                    Todos.Done = false
                } 
                let id = todos.insert todo
                printfn "%A" id
    else
        printfn "exist client"
        for x in todos.find().fetch() do
            printfn "%A _id %A" x x?_id)

Meteor.setTimeout func 1000 |> ignore
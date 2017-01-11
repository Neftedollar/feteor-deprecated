module TstsServer
open Fable.Core
open Fable.Import.Meteor
open MyCollections

let count = todos.find().count()
if count = 0 then
     printfn "zero!"
     for x in 1..10 do
            
            let todo = Todos ((sprintf "title %d" x), false)
            let id = todos.insert todo
            printfn "%A" id
else
    printfn "exist"
    for x in todos.find().fetch() do
        printfn "%A" x
        
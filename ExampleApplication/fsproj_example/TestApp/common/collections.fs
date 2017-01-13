module MyCollections

open Fable.Core
open Fable.Import.Meteor.Mongo
open Fable.Import.Meteor.Meteor
open Fable.Core.JsInterop
open System

type Todos = 
    { Title : string
      Done : bool }
    static member Create title isDone = 
        { Title = title
          Done = isDone }

let todos = Mongo.Collection.Create<Todos>("todos")

if Meteor.isServer && todos.find().count() = 0 then 
    Todos.Create "TODO1" false
    |> todos.insert
    |> ignore


    ignore <| todos.find().observe [
        Action<Todos, Todos>(fun n o -> 
            printfn "o %A n %A" o n 
        )  |> Changed
    ] 
    

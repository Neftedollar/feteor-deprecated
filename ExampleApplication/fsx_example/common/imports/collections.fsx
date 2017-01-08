#r "../../node_modules/fable-core/Fable.Core.dll"
#load "../../../../Feteor/lib/Fable.Import.Meteor.fs"
open Fable
open Fable.Core
open Fable.Import.Meteor.Meteor
open Fable.Import.Meteor.Mongo

//[<Import("*", "Collections")>]
module CommonColelctions =

type Todo = {
    Title : string
    Done : bool
}

let Todos = Mongo.Collection.Create<Todo>("todo")

if Meteor.isServer && Todos.find().count() = 0 then
    Todos.insert {
        Title = "TODO1"
        Done = false
    } |> ignore
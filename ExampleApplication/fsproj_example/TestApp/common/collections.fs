module MyCollections 
open Fable.Core
open Fable.Import.Meteor.Mongo
open Fable.Import.Meteor.Meteor
open Fable.Core.JsInterop
type Todos = {
     Title : System.String
     Done : bool
}

let todos =  Mongo.Collection.Create<Todos>("todos")

if Meteor.isServer && todos.find().count() = 0 then
    todos.insert {
        Title = "TODO1"
        Done = false
    } |> ignore
module MyCollections 
open Fable.Core
open Fable.Import.Meteor.Mongo
open Fable.Import.Meteor.Meteor
open Fable.Core.JsInterop

type Todos(title:string, ``done``:bool) = 
    member val Title = title with get,set
    member val Done = ``done`` with get,set
    member x._id() = jsNative
    

let todos =  Mongo.Collection.Create<Todos>("todos")

if Meteor.isServer && todos.find().count() = 0 then
    Todos("TODO1",  false )
    |> todos.insert  
    |> ignore
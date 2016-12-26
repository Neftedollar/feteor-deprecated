module MyCollections 
open Fable.Core
open Fable.Meteor
open Fable.Core.JsInterop
type Todos = {
     Title : System.String
     Done : bool
}

let todos =  Mongo.Globals.Collection.Create<Todos>("todos")

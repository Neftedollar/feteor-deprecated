module TodoComponent
open Fable.Core
open Fable.Import.Meteor.Meteor
open Fable.Import.Meteor.Mongo
open MyCollections
open Fable.Core.JsInterop

open Fable.Arch
open Fable.Arch.App
open Fable.Arch.Html

type Model = MyCollections.Todos

type Actions = 
    | TodoDone of newDone:bool

let update (model:Todos) msg =
    match msg with
    | TodoDone dn -> 
                printfn "%A is done? %b" model dn
                model.Done <- dn
                model

let view (model:Todos) =
        let ``checked`` = if model.Done then "checked" else "notchecked"
        printfn "isChecked %A" model.Done
        div [] [
            label [] [
                text model.Title
            ]
            
            input [ property  "type" "checkbox" 
                    attribute ``checked`` ``checked`` 
                    onMouseClick (fun x -> TodoDone (not model.Done)) ]
        ]


module ServerMain
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Meteor.Meteor


if Meteor.isServer then
    Meteor.startup  (fun _ -> 
        printfn __SOURCE_FILE__
    )
else 
    Meteor.startup 
    $ fun _ -> printfn __SOURCE_FILE__
    |> ignore

printfn "count %i" TstsServer.count
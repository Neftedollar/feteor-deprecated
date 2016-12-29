module ServerMain
open Fable.Core
open Fable.Core.JsInterop
open Fable.Meteor


if Meteor.Globals.isServer then
    Meteor.Globals.startup  (fun _ -> 
        printfn __SOURCE_FILE__
    )
else 
    Meteor.Globals.startup 
    $ fun _ -> printfn __SOURCE_FILE__
    |> ignore

printfn "count %f" TstsServer.count
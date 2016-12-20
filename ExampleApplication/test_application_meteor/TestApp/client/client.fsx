#r "../../node_modules/fable-core/Fable.Core.dll"
#r "../../../../dist/Feteor.dll"

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
// #r "../../node_modules/fable-core/Fable.Core.dll"
// #r "../../../../dist/Feteor.dll"
module ClientMain
open Fable.Core
open Fable.Core.JsInterop
open Fable.Meteor
open TstsClient



if Meteor.Globals.isServer then
    Meteor.Globals.startup  (fun _ -> 
        printfn __SOURCE_FILE__
    )
else 
    Meteor.Globals.startup 
    $ fun _ -> printfn __SOURCE_FILE__
    |> ignore

printfn "count %f" count
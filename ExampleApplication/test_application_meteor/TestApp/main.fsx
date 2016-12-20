#r "../../test_application_meteor/node_modules/fable-core/Fable.Core.dll"
#r "../../../dist/Feteor.dll"


open Fable.Core
open Fable.Core.JsInterop
open Fable.Meteor

if Meteor.Globals.isServer then
    Meteor.Globals.startup  (fun _ -> 
        printfn ("METEOR FABLE SERVER")
    )
else 
    Meteor.Globals.startup 
    $ fun _ -> printfn ("METEOR FABLE BROWSER")
    |> ignore
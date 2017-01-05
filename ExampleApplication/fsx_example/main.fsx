#r "node_modules/fable-core/Fable.Core.dll"
#load "../../Feteor/lib/Fable.Import.Meteor.fs"
open Fable
open Fable.Import.JS
open Fable.Core.JsInterop
open System
open Fable.Import.Meteor.Meteor


Meteor.startup (fun _ -> 
    printfn "Meteor.isServer %A" Meteor.isServer
    printfn "Meteor.isClient %A" Meteor.isClient
    printfn "Meteor.isCordova %A" Meteor.isCordova
    printfn "Meteor.isDevelopment %A" Meteor.isDevelopment
    printfn "Meteor.isProduction %A" Meteor.isProduction
    printfn "Meteor.isTest %A" Meteor.isTest
    printfn "Meteor.release %A" Meteor.release
    printfn "Meteor.settings %A" Meteor.settings
    // let methods = createObj [
    //     "testMethod" ==> JsFunc0(fun () -> printfn "%A" jsThis?userId)
    //     "anotherTestMethod" ==> JsFunc1(fun a -> printfn "%A" a)
    // ]
    // methods?antotherTestMethod <- fun a -> printfn "%A" a
    let another_methods = createObj [
        "new_method" ==> JsFunc0(fun () -> printfn "%A" jsThis?userId)
        "anotherTestMethod" ==> JsFunc1(fun a -> printfn "%A" a)
    ]
    printfn "ok %A" another_methods
    Meteor.methods another_methods
    )

if Meteor.isServer then
    Meteor.onConnection(fun x ->    x.onClose (fun _ -> printfn "%s closed" x.id ) 
                                    printfn "%A" x)

Meteor.absoluteUrl() |> printfn "%s"
Meteor.absoluteUrl("path") |> printfn "%s"
Meteor.absoluteUrl("path", [
    Secure
    ReplaceLocalhost

]) |> printfn "%s"
Meteor.absoluteUrl("path", [
    Secure
    RootUrl("http://neftedollar.com")
]) |> printfn "%s"
    
Meteor.defer(fun () -> 
    if Meteor.isServer then
        printfn "%A" Meteor.methods
    Meteor.call("new_method") |> ignore
    Meteor.call("anotherTestMethod", "lol") |> ignore
    Meteor.call("anotherTestMethod", 3) |> ignore
    Meteor.call("anotherTestMethod", createObj [ "lol" ==> 3 ]) |> ignore
)
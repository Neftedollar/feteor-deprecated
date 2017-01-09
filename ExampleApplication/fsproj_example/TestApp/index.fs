module Index
open Fable.Core
open Fable.Import.Meteor.Mongo
open Fable.Core.JsInterop

open Fable
open Fable.Import.JS
open Fable.Core.JsInterop
open System
open Fable.Import.Meteor.Meteor
open Fable.Import.Meteor.Mongo
open MyCollections





let s = todos.find().count()
printfn "Count %i" s
type TestType = {
    Value : string
} 

type TestType1() = 
    member x.Ok = "localhost"   
    member val No = "127.0.0.1" with get,set


Meteor.startup (fun _ -> 
    printfn "Meteor.isServer %A" Meteor.isServer
    printfn "Meteor.isClient %A" Meteor.isClient
    printfn "Meteor.isCordova %A" Meteor.isCordova
    printfn "Meteor.isDevelopment %A" Meteor.isDevelopment
    printfn "Meteor.isProduction %A" Meteor.isProduction
    printfn "Meteor.isTest %A" Meteor.isTest
    printfn "Meteor.release %A" Meteor.release
    printfn "Meteor.settings %A" Meteor.settings

    let methods = createObj [
        "new_method" ==> JsFunc0(fun () -> printfn "%A" jsThis?userId)
        "anotherTestMethod" ==> JsFunc1(fun a -> printfn "%A" a)
    ]
    Meteor.methods methods
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
    let tt = { Value = "HI all" }
    let tt1 = TestType1()
    tt1.No <- tt1.No + tt1.No
    let tt1s = JSON.stringify(tt1)
    let tts = JSON.stringify(tt)
    if Meteor.isServer then
        printfn "%A" Meteor.methods
    Meteor.call("new_method") |> ignore
    Meteor.call("anotherTestMethod", "lol") |> ignore
    Meteor.call("anotherTestMethod", 3) |> ignore
    Meteor.call("anotherTestMethod", createObj [ "lol" ==> 3 ]) |> ignore
    Meteor.call("anotherTestMethod", tt) |> ignore
    Meteor.call("anotherTestMethod", tt1 ) |> ignore
    Meteor.call("anotherTestMethod", tts) |> ignore
    Meteor.call("anotherTestMethod", tt1s ) |> ignore
    todos.find().count() |> printfn "Todos.find().count() %A"
)

let mutable private IntervalId = 0.0
let mutable private counter = 0
IntervalId <- Meteor.setInterval (fun () ->  printfn "Interval is 1500 counter is %i" counter
                                             counter <- counter + 1
                                             if counter >= 4 then
                                                Meteor.clearInterval IntervalId
                                            ) 1500
Meteor.setTimeout (fun _ -> printfn "TIMEOUT!!!") 1000 |> ignore
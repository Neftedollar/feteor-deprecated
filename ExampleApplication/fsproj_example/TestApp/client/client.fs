// #r "../../node_modules/fable-core/Fable.Core.dll"
// #r "../../../../dist/Feteor.dll"
module ClientMain
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Meteor.Meteor
open Fable.Import.Meteor.Mongo
 open Fable.Arch.App
 open Fable.Arch
open MyCollections           

let f = System.Action<Computation>(fun x ->
                        let initModel = todos.findOne(U3.Case1([]))
                        printfn "%A" initModel
                        initModel?Done <- true
                        initModel?Done <- false
                        initModel?Title<- "LOOOOL"
//                        let initModel1 = initModel |> inflate<Todos>
//                        printfn "%A" initModel1
//                        initModel?Done <- false
//                        printfn "%A" initModel1
//                        let am = Todos.Create "ANOTHER"  false
//                        printfn "%A" am
//                        am?Done <- true
//                        printfn "%A" am
                        createSimpleApp initModel TodoComponent.view TodoComponent.update Virtualdom.createRender
                        |> withStartNodeSelector "#app"
                        |> start
                        |> ignore)

Meteor.setTimeout(fun _ ->
                    Tracker.autorun(f)) 1500
                    |> ignore
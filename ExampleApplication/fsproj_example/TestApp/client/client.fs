// #r "../../node_modules/fable-core/Fable.Core.dll"
// #r "../../../../dist/Feteor.dll"
module ClientMain
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Meteor.Meteor
open Fable.Import.Meteor.Mongo
// open Fable.Arch.App
// open Fable.Arch
open MyCollections           

Meteor.setTimeout(fun _ ->
                    let initModel = todos.findOne(U3.Case1([]))
                    printfn "%A" initModel
                    let initModel1 = initModel |> inflate<Todos>
                    printfn "%A" initModel1
                    initModel.Done <- false
                    printfn "%A" initModel1
                    let am = Todos("ANOTHER", false)
                    printfn "%A" am
                    am.Done <- true
                    printfn "%A" am
                    // createSimpleApp initModel TodoComponent.view TodoComponent.update Virtualdom.createRender
                    // |> withStartNodeSelector "#app"
                    // |> start
                    |> ignore) 1500
                    |> ignore
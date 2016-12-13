namespace Fable.Import
open System
open System.Text.RegularExpressions
open Fable.Core
open Fable.Import.JS

module Match =
    type [<Import("*","Match")>] Globals =
        static member Maybe(pattern: obj): bool = jsNative



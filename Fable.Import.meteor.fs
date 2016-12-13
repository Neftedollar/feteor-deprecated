namespace Fable.Import
open System
open System.Text.RegularExpressions
open Fable.Core
open Fable.Import.JS

module Meteor =
    type [<Import("*","Meteor")>] Globals =
        static member isDevelopment with get(): bool = jsNative and set(v: bool): unit = jsNative
        static member isTest with get(): bool = jsNative and set(v: bool): unit = jsNative



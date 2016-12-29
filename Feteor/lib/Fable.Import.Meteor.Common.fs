namespace Fable.Import.Meteor.Common
open System
open System.Text.RegularExpressions
open Fable.Core
open Fable.Import.JS



type SettingsType() =
        //abstract ``public``: obj with get, set
        [<Emit("$0[$1]{{=$2}}")>]
        member this.``public`` with get():obj = jsNative and set(v:obj) = jsNative
        [<Emit("$0[$1]{{=$2}}")>] 
        member this.Item with get():obj = jsNative and set(v:obj) = jsNative

///     secure Boolean Create an HTTPS URL.
///     replaceLocalhost Boolean  Replace localhost with 127.0.0.1. Useful for services that don't recognize localhost as a domain name.
///     rootUrl String   Override the default ROOT_URL from the server environment. For example: "http://foo.example.com"
type absoluteUrlOptions = {

    ///  Create an HTTPS URL.
    mutable secure :bool
    ///  Replace localhost with 127.0.0.1. Useful for services that don't recognize localhost as a domain name.
    mutable replaceLocalhost :bool
    ///  Override the default ROOT_URL from the server environment. For example: "http://foo.example.com"  
    mutable rootUrl :string
}

type [<Import("Meteor","meteor/meteor")>] Meteor =
    ///Boolean variable. True if running in client environment.
    static member isClient with get(): bool = jsNative
    ///Boolean variable. True if running in server environment.
    static member isServer with get(): bool = jsNative
    ///Boolean variable. True if running in a Cordova mobile environment.
    static member isCordova:bool = jsNative
    ///Boolean variable. True if running in production environment.
    static member isProduction:bool = jsNative 
    ///Boolean variable. True if running in development environment.
    static member isDevelopment:bool = jsNative
    static member isTest:bool = jsNative
    
    ///`Meteor.release` is a string containing the name of the release with which the project was built (for example, "1.2.3"). It is `undefined` if the project was built using a git checkout of Meteor.
    static member release:string = jsNative
    
    /// <summary>
    ///  Run code when a client or a server starts.
    /// </summary>
    /// <remarks>
    ///  On a server, the function will run as soon as the server process is finished starting. On a client, the function will run as soon as the DOM is ready. Code wrapped in Meteor.startup always runs after all app files have loaded, so you should put code here if you want to access shared variables from other files.
    ///  The startup callbacks are called in the same order as the calls to Meteor.startup were made.
    ///  On a client, startup callbacks from packages will be called first, followed by "&lt;body&gt;" templates from your .html files, followed by your application code.
    /// </remarks>
    /// <example>
    /// <code>
    ///     if Meteor.isServer then
    ///         Meteor.startup (fun _ -> 
    ///             if Rooms.find().count() = 0 then
    ///                 //...    
    ///         )
    /// </code>
    /// </example>
    /// <param name="func">
    ///   A function to run on startup.  
    /// </param>
    static member startup(func: System.Action): unit = jsNative
    
    /// <summary>
    ///    Wrap a function that takes a callback function as its final parameter. The signature of the callback of the wrapped function should be function(error, result){}. On the server, the wrapped function can be used either synchronously (without passing a callback) or asynchronously (when a callback is passed). On the client, a callback is always required; errors will be logged if there is no callback. If a callback is provided, the environment captured when the original function was called will be restored in the callback. 
    /// </summary>
    /// <param name="func">
    ///       A function that takes a callback as it's final parameter
    /// </param>
    static member wrapAsync (func:Function, ?context:obj) :Function = jsNative
    
    ///   <summary>
    ///     Defer execution of a function to run asynchronously in the background (similar to Meteor.setTimeout(func, 0))
    ///   </summary>
    ///   <param name="func">
    ///       The function to run
    ///    </param>
    static member defer(func: Function): unit = jsNative
    
    ///<summary>
    ///Generate an absolute URL pointing to the application. The server reads from the ROOT_URL environment variable to determine where it is running. This is taken care of automatically for apps deployed to Galaxy, but must be provided when using meteor build.
    ///</summary>
    /// <param name="path">
    ///    A path to append to the root URL. Do not include a leading "/".
    /// </param>
    ///  <param name="options">
    ///     <see cref="absoluteUrlOptions"/>
    /// </param>
    static member absoluteUrl(?path: string, ?options: absoluteUrlOptions): string = jsNative
    /// <summary>
    ///     Meteor.settings contains deployment-specific configuration options. You can initialize settings by passing the --settings option (which takes the name of a file containing JSON data) to meteor run or meteor deploy. When running your server directly (e.g. from a bundle), you instead specify settings by putting the JSON directly into the METEOR_SETTINGS environment variable. If the settings object contains a key named public, then Meteor.settings.public will be available on the client as well as the server. All other properties of Meteor.settings are only defined on the server. You can rely on Meteor.settings and Meteor.settings.public being defined objects (not undefined) on both client and server even if there are no settings specified. Changes to Meteor.settings.public at runtime will be picked up by new client connections.
    /// </summary>
    static member settings with get(): SettingsType = jsNative
    /// <summary>
    ///     Defines functions that can be invoked over the network by clients.
    /// </summary>
    static member methods(methods: obj): unit = jsNative
    

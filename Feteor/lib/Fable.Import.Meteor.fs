namespace Fable.Import.Meteor
open System
open System.Text.RegularExpressions
open Fable.Core
open Fable.Import.JS


module Mongo = 
    open Fable.Core.JsInterop
     
    
    type LiveQueryHandle =
        abstract stop: unit -> unit
    [<KeyValueList>]
    type Selector = 
        | [<Erase>]Select of string * obj

    and [<KeyValueList>]Modifier =
        interface end
    and [<KeyValueList>]SortSpecifier =
        | [<Erase>] S of fieldname:string * int
    and [<KeyValueList>]FieldSpecifier =
        | [<Erase>] Field of fieldname:string * int
    [<KeyValueList>]
    type FindOneOptions<'T> =
        ///Number of results to skip at the beginning
        | Skip of int
        /// (Client only) Default true; pass false to disable reactivity
        | Reactive of bool
        ///Overrides transform on the Collection for this cursor. Pass null(None) to disable transformation.
        | Transform  of JsFunc1<'T, obj> option
        /// Dictionary of fields to return or exclude.
        | Fields of FieldSpecifier list
        ///Sort order (default: natural order)
        | Sort of   SortSpecifier list
    [<KeyValueList>]
    type FindOptions<'T> =
        ///Number of results to skip at the beginning
        | Skip of int
        /// (Client only) Default true; pass false to disable reactivity
        | Reactive of bool
        ///Overrides transform on the Collection for this cursor. Pass null(None) to disable transformation.
        | Transform  of JsFunc1<'T, obj> option
        /// Dictionary of fields to return or exclude.
        | Fields of FieldSpecifier list
        ///Sort order (default: natural order)
        | Sort of   SortSpecifier list
        ///Maximum number of results to return 
        | Limit of int
        ///(Server only) Pass true to disable oplog-tailing on this query. This affects the way server processes calls to observe on this query. Disabling the oplog can be useful when working with data that updates in large batches.
        | [<CompiledName("disableOplog")>]DisableOplog of bool 
        ///(Server only) When oplog is disabled (through the use of disableOplog or when otherwise not available), the frequency (in milliseconds) of how often to poll this query when observing on the server. Defaults to 10000ms (10 seconds).
        | [<CompiledName("pollingIntervalMs")>]PollingIntervalMs of int
        ///(Server only) When oplog is disabled (through the use of disableOplog or when otherwise not available), the minimum time (in milliseconds) to allow between re-polling when observing on the server. Increasing this will save CPU and mongo load at the expense of slower updates to users. Decreasing this is not recommended. Defaults to 50ms.
        | [<CompiledName("pollingThrottleMs")>]PollingThrottleMs  of int
    [<StringEnum>]
    type IdGeneration = 
        |  [<CompiledName("STRING")>] STRING
        |  [<CompiledName("MONGO")>] MONGO
    [<KeyValueList>]
    type CreateCollectonOptions<'T> = 
        | [<CompiledName("idGeneration")>]IdGeneration of  IdGeneration
        | Transform of JsFunc1<'T,obj>
        | [<CompiledName("defineMutationMethods")>]DefineMutationMethods of bool
    type CollectionStatic =
        [<Emit("new $0($1...)")>] abstract Create: name: string * ?options: CreateCollectonOptions<'T> list -> Collection<'T>
    and Collection<'T> =
        abstract allow: options: obj -> bool
        abstract deny: options: obj -> bool
        abstract find: ?selector: U3<Selector list, ObjectID, string> * ?options: FindOptions<'T> list -> Cursor<'T>
        abstract findOne: ?selector: U3<Selector list, ObjectID, string> * ?options: FindOneOptions<'T> list -> 'T
        abstract insert: doc: 'T * ?callback: System.Action<Option<Object>,Option<string>> -> string
        abstract rawCollection: unit -> obj
        abstract rawDatabase: unit -> obj
        abstract remove: selector: U3<Selector list, ObjectID, string> * ?callback: Function -> float
        abstract update: selector: U3<Selector list, ObjectID, string> * modifier: Modifier * ?options: obj * ?callback: Function -> float
        abstract upsert: selector: U3<Selector list, ObjectID, string> * modifier: Modifier * ?options: obj * ?callback: Function -> obj
        abstract _ensureIndex: keys: U2<obj, string> * ?options: obj -> unit
        abstract _dropIndex: keys: U2<obj, string> -> unit
    and CursorStatic =
        [<Emit("new $0($1...)")>] abstract Create: unit -> Cursor<'T>
        
    and [<KeyValueList>] ObserveCallbacks<'T> =
        | Added of Action<'T>
        | [<CompiledName("addedAt")>]AddedAt of Action<'T, int, 'T>
        | Changed of  Action<'T,'T>
        | [<CompiledName("changedAt")>]ChangedAt of Action<'T,'T, int> 
        | Removed of Action<'T>
        | [<CompiledName("removedAt")>]RemovedAt of Action<'T, int>
        | [<CompiledName("movedTo")>]MovedTo of Action<'T, int,int,'T>

    and ObserveChangesCallbacks =
        abstract added: id: string * fields: obj -> unit
        abstract addedBefore: id: string * fields: obj * before: obj -> unit
        abstract changed: id: string * fields: obj -> unit
        abstract movedBefore: id: string * before: obj -> unit
        abstract removed: id: string -> unit
    and Cursor<'T> =
        abstract count: ?applySkipLimit: bool -> int
        abstract fetch: unit -> ResizeArray<'T>
        abstract forEach: callback: Func<'T, float, Cursor<'T>, unit> * ?thisArg: obj -> unit
        abstract map: callback: Func<'T, float, Cursor<'T>, 'U> * ?thisArg: obj -> ResizeArray<'U>
        abstract observe: callbacks: ObserveCallbacks<'T> list -> LiveQueryHandle
        abstract observeChanges: callbacks: ObserveChangesCallbacks -> LiveQueryHandle
    and ObjectIDStatic =
        [<Emit("new $0($1...)")>] abstract Create: ?hexString: string -> ObjectID
    and ObjectID =
        interface end

    type [<Import("Mongo","meteor/mongo")>] Mongo =
        static member Collection with get(): CollectionStatic = jsNative and set(v: CollectionStatic): unit = jsNative
        static member Cursor with get(): CursorStatic = jsNative and set(v: CursorStatic): unit = jsNative
        static member ObjectID with get(): ObjectIDStatic = jsNative and set(v: ObjectIDStatic): unit = jsNative

module EJSON =
     type  JSONable =
            [<Emit("$0[$1]{{=$2}}")>] abstract Item: key: string -> obj with get, set

     type  EJSONable =
            [<Emit("$0[$1]{{=$2}}")>] abstract Item: key: string -> obj with get, set

     type  EJSONableCustomType =
            abstract clone: unit -> EJSONableCustomType
            abstract equals: other: obj -> bool
            abstract toJSONValue: unit -> JSONable
            abstract typeName: unit -> string

     type EJSON =
            inherit EJSONable    


module Meteor =
    [<KeyValueList>]
    type AbsoluteUrlOptions =
        ///  Create an HTTPS URL.
        | Secure
        ///  Replace localhost with 127.0.0.1. Useful for services that don't recognize localhost as a domain name.
        | ReplaceLocalhost
        ///Override the default ROOT_URL from the server environment. For example: "http://foo.example.com"  
        | RootUrl of string
        
    type settingsType =
        abstract ``public``: obj with get, set
        [<Emit("$0[$1]{{=$2}}")>] abstract Item: id: string -> obj with get, set
    type MeteorConnection = 
        abstract id:string
        abstract close: unit->unit
        abstract onClose: System.Action -> unit
        abstract clientAddres:string
        abstract httpHeaders:obj 
    type [<Import("Meteor","meteor/meteor")>] Meteor =
        ///Boolean variable. True if running in development environment.
        static member isDevelopment with get(): bool = jsNative and set(v: bool): unit = jsNative
        static member isTest with get(): bool = jsNative and set(v: bool): unit = jsNative
        static member onConnection(callback: System.Action<MeteorConnection>): unit = jsNative
        ///Publish a record set.
        ///name : If String, name of the record set. If Object, publications Dictionary of publish functions by name. If null, the set has no name, and the record set is automatically sent to all connected clients.
        ///func : (JsFunc) Function called on the server each time a client subscribes. Inside the function, this is the publish handler object, described below. If the client passed arguments to subscribe, the function is called with the same arguments.
        static member publish(name: string, func: Object): unit = jsNative
        ///Boolean variable. True if running in client environment.
        static member isClient with get(): bool = jsNative and set(v: bool): unit = jsNative
        ///Boolean variable. True if running in a Cordova mobile environment.
        static member isCordova with get(): bool = jsNative and set(v: bool): unit = jsNative
        ///Boolean variable. True if running in server environment.
        static member isServer with get(): bool = jsNative and set(v: bool): unit = jsNative
        ///Boolean variable. True if running in production environment.
        static member isProduction with get(): bool = jsNative and set(v: bool): unit = jsNative
        ///Meteor.release is a string containing the name of the release with which the project was built (for example, "1.2.3"). It is undefined if the project was built using a git checkout of Meteor.
        static member release with get(): string = jsNative and set(v: string): unit = jsNative
        ///Meteor.settings contains deployment-specific configuration options. You can initialize settings by passing the --settings option (which takes the name of a file containing JSON data) to meteor run or meteor deploy. When running your server directly (e.g. from a bundle), you instead specify settings by putting the JSON directly into the METEOR_SETTINGS environment variable. If the settings object contains a key named public, then Meteor.settings.public will be available on the client as well as the server. All other properties of Meteor.settings are only defined on the server. You can rely on Meteor.settings and Meteor.settings.public being defined objects (not undefined) on both client and server even if there are no settings specified. Changes to Meteor.settings.public at runtime will be picked up by new client connections.
        static member settings with get(): settingsType = jsNative and set(v: settingsType): unit = jsNative
        static member users with get(): Mongo.Collection<User> = jsNative and set(v: Mongo.Collection<User>): unit = jsNative
        static member Error with get(): ErrorStatic = jsNative and set(v: ErrorStatic): unit = jsNative
        static member user(): User = jsNative
        static member userId(): string = jsNative
        static member methods(methods: obj): unit = jsNative
        static member call(name: string, [<ParamArray>] args: obj[]): obj = jsNative
        static member apply(name: string, args: ResizeArray<EJSON.EJSONable>, ?options: obj, ?asyncCallback: Function): obj = jsNative
        ///Generate an absolute URL pointing to the application. The server reads from the ROOT_URL environment variable to determine where it is running. This is taken care of automatically for apps deployed to Galaxy, but must be provided when using meteor build.
        ///path : A path to append to the root URL. Do not include a leading "/".
        static member absoluteUrl(?path: string, ?options: AbsoluteUrlOptions list): string = jsNative
        [<Emit("Meteor.setInterval($0,$1)")>]
        static member setInterval (func: unit -> unit)  (delay: int): float = jsNative
        [<Emit("Meteor.setTimeout($0,$1)")>]
        static member setTimeout (func: unit-> unit)  (delay: int): float = jsNative
        static member clearInterval(id: float): unit = jsNative
        static member clearTimeout(id: float): unit = jsNative
        ///Defer execution of a function to run asynchronously in the background (similar to Meteor.setTimeout(func, 0).
        /// func : The function to run
        [<Emit("Meteor.defer($0)")>]
        static member defer(func: unit -> unit): unit = jsNative
        ///Run code when a client or a server starts.
        ///func : A function to run on startup.
        [<Emit("Meteor.startup($0)")>]
        static member startup(func: unit -> unit): unit = jsNative
        ///Wrap a function that takes a callback function as its final parameter. The signature of the callback of the wrapped function should be function(error, result){}. On the server, the wrapped function can be used either synchronously (without passing a callback) or asynchronously (when a callback is passed). On the client, a callback is always required; errors will be logged if there is no callback. If a callback is provided, the environment captured when the original function was called will be restored in the callback.
        ///func : A function that takes a callback as its final parameter 
        ///context :  Optional this object against which the original function will be invoked
        static member wrapAsync(func: obj, ?context: obj): obj = jsNative

    and ErrorStatic =
        [<Emit("new $0($1...)")>] abstract Create: error: U2<string, float> * ?reason: string * ?details: string -> Error

    and [<AllowNullLiteral>] Error =
        abstract error: U2<string, float> with get, set
        abstract reason: string option with get, set
        abstract details: string option with get, set

    and UserEmail =
        abstract address: string with get, set
        abstract verified: bool with get, set

    and User =
        abstract _id: string option with get, set
        abstract username: string option with get, set
        abstract emails: ResizeArray<UserEmail> option with get, set
        abstract createdAt: float option with get, set
        abstract profile: obj option with get, set
        abstract services: obj option with get, set

    type Subscription =
        abstract connection: MeteorConnection with get, set
        abstract userId: string with get, set
        abstract added: collection: string * id: string * fields: obj -> unit
        abstract changed: collection: string * id: string * fields: obj -> unit
        abstract error: error: Error -> unit
        abstract onStop: func: Function -> unit
        abstract ready: unit -> unit
        abstract removed: collection: string * id: string -> unit
        abstract stop: unit -> unit
    type Computation =
        ///Prevents this computation from rerunning.
        abstract  stop : unit -> unit
        ///Invalidates this computation so that it will be rerun.
        abstract invalidate : unit -> unit
        ///Registers callback to run when this computation is next invalidated, or runs it immediately if the computation is already invalidated. The callback is run exactly once and not upon future invalidations unless onInvalidate is called again after the computation becomes valid again.
        abstract onInvalidate : System.Action<Computation> -> unit
        ///Registers callback to run when this computation is stopped, or runs it immediately if the computation is already stopped. The callback is run after any onInvalidate callbacks.
        abstract onStop : System.Action<Computation> -> unit
        ///True if this computation has been stopped.
        abstract stopped : bool
        ///True if this computation has been invalidated (and not yet rerun), or if it has been stopped.
        abstract invalidated :  bool
        ///True during the initial run of the computation at the time Tracker.autorun is called, and false on subsequent reruns and at other times.
        abstract firstRun : bool
    type Dependency =
        ///Invalidate all dependent computations immediately and remove them as dependents.
        abstract changed : unit -> unit
        ///Declares that the current computation (or fromComputation if given) depends on dependency. The computation will be invalidated the next time dependency changes.
        abstract depend: Computation -> unit
        ///True if this Dependency has one or more dependent Computations, which would be invalidated if this Dependency were to change.
        abstract hasDependents : unit -> bool

    [<Import("Tracker","meteor/tracker")>]
    type Tracker =
        ///Run a function now and rerun it later whenever its dependencies change. Returns a Computation object that can be used to stop or observe the rerunning.
        static member autorun(runFunc:System.Action<Computation> , ?onError:System.Action<Error>) = jsNative
        //Process all reactive updates immediately and ensure that all invalidated computations are rerun.
        static member flush:unit -> unit = jsNative
        static member nonreactive: System.Action -> unit = jsNative
        ///True if there is a current computation, meaning that dependencies on reactive data sources will be tracked and potentially cause the current computation to be rerun.
        static member active : bool  = jsNative
        ///The current computation, or null if there isn't one. The current computation is the Tracker.Computation object created by the innermost active call to Tracker.autorun, and it's the computation that gains dependencies when reactive data sources are accessed.
        static member currentComputation: Computation option = jsNative
        ///Registers a new onInvalidate callback on the current computation (which must exist), to be called immediately when the current computation is invalidated or stopped.
        static member onInvalidate : System.Action<Computation> -> unit = jsNative
        ///hedules a function to be called during the next flush, or later in the current flush if one is in progress, after all invalidated computations have been rerun. The function will be run once and not on subsequent flushes unless afterFlush is called again.
        static member afterFlush: System.Action -> unit = jsNative

module Accounts = 
     type [<Import("Accounts","meteor/accounts-base")>] Globals =
            static member urls with get(): URLS = jsNative and set(v: URLS): unit = jsNative
            static member user(): Meteor.User = jsNative
            static member userId(): string = jsNative
            static member createUser(options: obj, ?callback: Function): string = jsNative
            static member config(options: obj): unit = jsNative
            static member onLogin(func: Function): obj = jsNative
            static member onLoginFailure(func: Function): obj = jsNative
            static member loginServicesConfigured(): bool = jsNative
            static member onPageLoadLogin(func: Function): unit = jsNative
     and URLS = 
            abstract resetPassword: Func<string, string> with get, set
            abstract verifyEmail: Func<string, string> with get, set
            abstract enrollAccount: Func<string, string> with get, set

     type Header =
            [<Emit("$0[$1]{{=$2}}")>] abstract Item: id: string -> string with get, set

     type EmailFields =
            abstract from: Func<unit, string> option with get, set
            abstract subject: Func<Meteor.User, string> option with get, set
            abstract text: Func<Meteor.User, string, string> option with get, set
            abstract html: Func<Meteor.User, string, string> option with get, set

     type EmailTemplates =
            abstract from: string with get, set
            abstract siteName: string with get, set
            abstract headers: Header option with get, set
            abstract resetPassword: EmailFields with get, set
            abstract enrollAccount: EmailFields with get, set
            abstract verifyEmail: EmailFields with get, set



module Email =
     type MailComposerOptions =
            abstract escapeSMTP: bool with get, set
            abstract encoding: string with get, set
            abstract charset: string with get, set
            abstract keepBcc: bool with get, set
            abstract forceEmbeddedImages: bool with get, set

     type  MailComposer =
            abstract addHeader: name: string * value: string -> unit
            abstract setMessageOption: from: string * ``to``: string * body: string * html: string -> unit
            abstract streamMessage: unit -> unit
            abstract pipe: stream: obj -> unit

     type  MailComposerStatic =
            [<Emit("new $0($1...)")>] abstract Create: options: MailComposerOptions -> MailComposer
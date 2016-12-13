namespace Fable.Import
open System
open System.Text.RegularExpressions
open Fable.Core
open Fable.Import.JS

type [<AllowNullLiteral>] URLS =
    abstract resetPassword: Func<string, string> with get, set
    abstract verifyEmail: Func<string, string> with get, set
    abstract enrollAccount: Func<string, string> with get, set

and [<AllowNullLiteral>] EmailFields =
    abstract from: Func<unit, string> option with get, set
    abstract subject: Func<Meteor.User, string> option with get, set
    abstract text: Func<Meteor.User, string, string> option with get, set
    abstract html: Func<Meteor.User, string, string> option with get, set

and [<AllowNullLiteral>] Header =
    [<Emit("$0[$1]{{=$2}}")>] abstract Item: id: string -> string with get, set

and [<AllowNullLiteral>] EmailTemplates =
    abstract from: string with get, set
    abstract siteName: string with get, set
    abstract headers: Header option with get, set
    abstract resetPassword: EmailFields with get, set
    abstract enrollAccount: EmailFields with get, set
    abstract verifyEmail: EmailFields with get, set

and [<AllowNullLiteral>] EJSONableCustomType =
    abstract clone: unit -> EJSONableCustomType
    abstract equals: other: obj -> bool
    abstract toJSONValue: unit -> JSONable
    abstract typeName: unit -> string

and [<AllowNullLiteral>] EJSONable =
    [<Emit("$0[$1]{{=$2}}")>] abstract Item: key: string -> obj with get, set

and [<AllowNullLiteral>] JSONable =
    [<Emit("$0[$1]{{=$2}}")>] abstract Item: key: string -> obj with get, set

and [<AllowNullLiteral>] EJSON =
    inherit EJSONable


and [<AllowNullLiteral>] MailComposerOptions =
    abstract escapeSMTP: bool with get, set
    abstract encoding: string with get, set
    abstract charset: string with get, set
    abstract keepBcc: bool with get, set
    abstract forceEmbeddedImages: bool with get, set

and [<AllowNullLiteral>] MailComposerStatic =
    [<Emit("new $0($1...)")>] abstract Create: options: MailComposerOptions -> MailComposer

and [<AllowNullLiteral>] MailComposer =
    abstract addHeader: name: string * value: string -> unit
    abstract setMessageOption: from: string * ``to``: string * body: string * html: string -> unit
    abstract streamMessage: unit -> unit
    abstract pipe: stream: obj -> unit

and [<AllowNullLiteral>] Subscription =
    abstract connection: Meteor.Connection with get, set
    abstract userId: string with get, set
    abstract added: collection: string * id: string * fields: obj -> unit
    abstract changed: collection: string * id: string * fields: obj -> unit
    abstract error: error: Error -> unit
    abstract onStop: func: Function -> unit
    abstract ready: unit -> unit
    abstract removed: collection: string * id: string -> unit
    abstract stop: unit -> unit

and [<AllowNullLiteral>] ReactiveVarStatic =
    [<Emit("new $0($1...)")>] abstract Create: initialValue: 'T * ?equalsFunc: Function -> ReactiveVar<'T>

and [<AllowNullLiteral>] ReactiveVar<'T> =
    abstract get: unit -> 'T
    abstract set: newValue: 'T -> unit

and [<AllowNullLiteral>] TemplateStatic =
    inherit Blaze.TemplateStatic
    abstract body: Blaze.Template with get, set
    [<Emit("$0[$1]{{=$2}}")>] abstract Item: index: string -> U2<obj, Blaze.Template> with get, set
    [<Emit("new $0($1...)")>] abstract Create: ?viewName: string * ?renderFunction: Function -> Blaze.Template

and [<AllowNullLiteral>] ILengthAble =
    abstract length: float with get, set

and [<AllowNullLiteral>] ITinytestAssertions =
    abstract ok: doc: obj -> unit
    abstract expect_fail: unit -> unit
    abstract fail: doc: obj -> unit
    abstract runId: unit -> string
    abstract equal: actual: 'T * expected: 'T * ?message: string * ?not: bool -> unit
    abstract notEqual: actual: 'T * expected: 'T * ?message: string -> unit
    abstract instanceOf: obj: obj * klass: Function * ?message: string -> unit
    abstract notInstanceOf: obj: obj * klass: Function * ?message: string -> unit
    abstract matches: actual: obj * regexp: Regex * ?message: string -> unit
    abstract notMatches: actual: obj * regexp: Regex * ?message: string -> unit
    abstract throws: f: Function * ?expected: U2<string, Regex> -> unit
    abstract isTrue: v: bool * ?msg: string -> unit
    abstract isFalse: v: bool * ?msg: string -> unit
    abstract isNull: v: obj * ?msg: string -> unit
    abstract isNotNull: v: obj * ?msg: string -> unit
    abstract isUndefined: v: obj * ?msg: string -> unit
    abstract isNotUndefined: v: obj * ?msg: string -> unit
    abstract isNan: v: obj * ?msg: string -> unit
    abstract isNotNan: v: obj * ?msg: string -> unit
    abstract ``include``: s: U3<ResizeArray<'T>, obj, string> * value: obj * ?msg: string * ?not: bool -> unit
    abstract notInclude: s: U3<ResizeArray<'T>, obj, string> * value: obj * ?msg: string * ?not: bool -> unit
    abstract length: obj: ILengthAble * expected_length: float * ?msg: string -> unit
    abstract _stringEqual: actual: string * expected: string * ?msg: string -> unit

type [<Erase>]Globals =
    [<Global>] static member MailComposer with get(): MailComposerStatic = jsNative and set(v: MailComposerStatic): unit = jsNative
    [<Global>] static member ReactiveVar with get(): ReactiveVarStatic = jsNative and set(v: ReactiveVarStatic): unit = jsNative
    [<Global>] static member Template with get(): TemplateStatic = jsNative and set(v: TemplateStatic): unit = jsNative

module Accounts =
    type [<Import("*","Accounts")>] Globals =
        static member urls with get(): URLS = jsNative and set(v: URLS): unit = jsNative
        static member user(): Meteor.User = jsNative
        static member userId(): string = jsNative
        static member createUser(options: obj, ?callback: Function): string = jsNative
        static member config(options: obj): unit = jsNative
        static member onLogin(func: Function): obj = jsNative
        static member onLoginFailure(func: Function): obj = jsNative
        static member loginServicesConfigured(): bool = jsNative
        static member onPageLoadLogin(func: Function): unit = jsNative



module ``meteor/accounts-base`` =
    type [<AllowNullLiteral>] URLS =
        abstract resetPassword: Func<string, string> with get, set
        abstract verifyEmail: Func<string, string> with get, set
        abstract enrollAccount: Func<string, string> with get, set

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



module Accounts =
    type [<AllowNullLiteral>] uiType =
        abstract config: options: obj -> unit

    type [<Import("*","Accounts")>] Globals =
        static member ui with get(): uiType = jsNative and set(v: uiType): unit = jsNative
        static member changePassword(oldPassword: string, newPassword: string, ?callback: Function): unit = jsNative
        static member forgotPassword(options: obj, ?callback: Function): unit = jsNative
        static member resetPassword(token: string, newPassword: string, ?callback: Function): unit = jsNative
        static member verifyEmail(token: string, ?callback: Function): unit = jsNative
        static member onEmailVerificationLink(callback: Function): unit = jsNative
        static member onEnrollmentLink(callback: Function): unit = jsNative
        static member onResetPasswordLink(callback: Function): unit = jsNative
        static member loggingIn(): bool = jsNative
        static member logout(?callback: Function): unit = jsNative
        static member logoutOtherClients(?callback: Function): unit = jsNative




module Accounts =
    type [<AllowNullLiteral>] IValidateLoginAttemptCbOpts =
        abstract ``type``: string with get, set
        abstract allowed: bool with get, set
        abstract error: Meteor.Error with get, set
        abstract user: Meteor.User with get, set
        abstract connection: Meteor.Connection with get, set
        abstract methodName: string with get, set
        abstract methodArguments: ResizeArray<obj> with get, set

    type [<Import("*","Accounts")>] Globals =
        static member emailTemplates with get(): EmailTemplates = jsNative and set(v: EmailTemplates): unit = jsNative
        static member addEmail(userId: string, newEmail: string, ?verified: bool): unit = jsNative
        static member removeEmail(userId: string, email: string): unit = jsNative
        static member onCreateUser(func: Function): unit = jsNative
        static member findUserByEmail(email: string): obj = jsNative
        static member findUserByUsername(username: string): obj = jsNative
        static member sendEnrollmentEmail(userId: string, ?email: string): unit = jsNative
        static member sendResetPasswordEmail(userId: string, ?email: string): unit = jsNative
        static member sendVerificationEmail(userId: string, ?email: string): unit = jsNative
        static member setUsername(userId: string, newUsername: string): unit = jsNative
        static member setPassword(userId: string, newPassword: string, ?options: obj): unit = jsNative
        static member validateNewUser(func: Function): bool = jsNative
        static member validateLoginAttempt(func: Function): obj = jsNative


module Blaze =
    type [<AllowNullLiteral>] ViewStatic =
        [<Emit("new $0($1...)")>] abstract Create: ?name: string * ?renderFunction: Function -> View

    and [<AllowNullLiteral>] View =
        abstract name: string with get, set
        abstract parentView: View with get, set
        abstract isCreated: bool with get, set
        abstract isRendered: bool with get, set
        abstract isDestroyed: bool with get, set
        abstract renderCount: float with get, set
        abstract template: Template with get, set
        abstract autorun: runFunc: Function -> unit
        abstract onViewCreated: func: Function -> unit
        abstract onViewReady: func: Function -> unit
        abstract onViewDestroyed: func: Function -> unit
        abstract firstNode: unit -> Node
        abstract lastNode: unit -> Node
        abstract templateInstance: unit -> TemplateInstance

    and [<AllowNullLiteral>] HelpersMap =
        [<Emit("$0[$1]{{=$2}}")>] abstract Item: key: string -> Function with get, set

    and [<AllowNullLiteral>] EventsMap =
        [<Emit("$0[$1]{{=$2}}")>] abstract Item: key: string -> Function with get, set

    and [<AllowNullLiteral>] TemplateStatic =
        [<Emit("new $0($1...)")>] abstract Create: ?viewName: string * ?renderFunction: Function -> Template
        abstract registerHelper: name: string * func: Function -> unit
        abstract instance: unit -> TemplateInstance
        abstract currentData: unit -> obj
        abstract parentData: numLevels: float -> obj

    and [<AllowNullLiteral>] Template =
        abstract viewName: string with get, set
        abstract renderFunction: Function with get, set
        abstract head: Template with get, set
        abstract ``abstract `: obj with get, set
        abstract created: Function with get, set
        abstract rendered: Function with get, set
        abstract destroyed: Function with get, set
        abstract constructView: unit -> View
        abstract find: selector: string -> HTMLElement
        abstract findAll: selector: string -> ResizeArray<HTMLElement>
        abstract onCreated: cb: Function -> unit
        abstract onRendered: cb: Function -> unit
        abstract onDestroyed: cb: Function -> unit
        abstract helpers: helpersMap: HelpersMap -> unit
        abstract events: eventsMap: EventsMap -> unit

    and [<AllowNullLiteral>] TemplateInstanceStatic =
        [<Emit("new $0($1...)")>] abstract Create: view: View -> TemplateInstance

    and [<AllowNullLiteral>] TemplateInstance =
        abstract data: obj with get, set
        abstract firstNode: obj with get, set
        abstract lastNode: obj with get, set
        abstract view: obj with get, set
        abstract ``abstract `: selector: string -> obj
        abstract autorun: runFunc: Function -> obj
        abstract find: selector: string -> HTMLElement
        abstract findAll: selector: string -> ResizeArray<HTMLElement>
        abstract subscribe: name: string * [<ParamArray>] args: obj[] -> Meteor.SubscriptionHandle
        abstract subscriptionsReady: unit -> bool

    type [<Import("*","Blaze")>] Globals =
        static member View with get(): ViewStatic = jsNative and set(v: ViewStatic): unit = jsNative
        static member currentView with get(): View = jsNative and set(v: View): unit = jsNative
        static member Template with get(): TemplateStatic = jsNative and set(v: TemplateStatic): unit = jsNative
        static member TemplateInstance with get(): TemplateInstanceStatic = jsNative and set(v: TemplateInstanceStatic): unit = jsNative
        static member isTemplate(value: obj): bool = jsNative
        static member Each(argFunc: Function, contentFunc: Function, ?elseFunc: Function): View = jsNative
        static member Unless(conditionFunc: Function, contentFunc: Function, ?elseFunc: Function): View = jsNative
        static member If(conditionFunc: Function, contentFunc: Function, ?elseFunc: Function): View = jsNative
        static member Let(bindings: Function, contentFunc: Function): View = jsNative
        static member With(data: U2<obj, Function>, contentFunc: Function): View = jsNative
        static member getData(?elementOrView: U2<HTMLElement, View>): obj = jsNative
        static member getView(?element: HTMLElement): View = jsNative
        static member remove(renderedView: View): unit = jsNative
        static member render(templateOrView: U2<Template, View>, parentNode: Node, ?nextNode: Node, ?parentView: View): View = jsNative
        static member renderWithData(templateOrView: U2<Template, View>, data: U2<obj, Function>, parentNode: Node, ?nextNode: Node, ?parentView: View): View = jsNative
        static member toHTML(templateOrView: U2<Template, View>): string = jsNative
        static member toHTMLWithData(templateOrView: U2<Template, View>, data: U2<obj, Function>): string = jsNative



module ``meteor/blaze`` =
    module Blaze =
        type [<AllowNullLiteral>] ViewStatic =
            [<Emit("new $0($1...)")>] abstract Create: ?name: string * ?renderFunction: Function -> View

        and [<AllowNullLiteral>] View =
            abstract name: string with get, set
            abstract parentView: View with get, set
            abstract isCreated: bool with get, set
            abstract isRendered: bool with get, set
            abstract isDestroyed: bool with get, set
            abstract renderCount: float with get, set
            abstract template: Template with get, set
            abstract autorun: runFunc: Function -> unit
            abstract onViewCreated: func: Function -> unit
            abstract onViewReady: func: Function -> unit
            abstract onViewDestroyed: func: Function -> unit
            abstract firstNode: unit -> Node
            abstract lastNode: unit -> Node
            abstract templateInstance: unit -> TemplateInstance

        and [<AllowNullLiteral>] HelpersMap =
            [<Emit("$0[$1]{{=$2}}")>] abstract Item: key: string -> Function with get, set

        and [<AllowNullLiteral>] EventsMap =
            [<Emit("$0[$1]{{=$2}}")>] abstract Item: key: string -> Function with get, set

        and [<AllowNullLiteral>] TemplateStatic =
            [<Emit("new $0($1...)")>] abstract Create: ?viewName: string * ?renderFunction: Function -> Template
            abstract registerHelper: name: string * func: Function -> unit
            abstract instance: unit -> TemplateInstance
            abstract currentData: unit -> obj
            abstract parentData: numLevels: float -> obj

        and [<AllowNullLiteral>] Template =
            abstract viewName: string with get, set
            abstract renderFunction: Function with get, set
            abstract head: Template with get, set
            abstract ``abstract``: obj with get, set
            abstract created: Function with get, set
            abstract rendered: Function with get, set
            abstract destroyed: Function with get, set
            abstract constructView: unit -> View
            abstract find: selector: string -> HTMLElement
            abstract findAll: selector: string -> ResizeArray<HTMLElement>
            abstract onCreated: cb: Function -> unit
            abstract onRendered: cb: Function -> unit
            abstract onDestroyed: cb: Function -> unit
            abstract helpers: helpersMap: HelpersMap -> unit
            abstract events: eventsMap: EventsMap -> unit

        and [<AllowNullLiteral>] TemplateInstanceStatic =
            [<Emit("new $0($1...)")>] abstract Create: view: View -> TemplateInstance

        and [<AllowNullLiteral>] TemplateInstance =
            abstract data: obj with get, set
            abstract firstNode: obj with get, set
            abstract lastNode: obj with get, set
            abstract view: obj with get, set
            abstract ``abstract `: selector: string -> obj
            abstract autorun: runFunc: Function -> obj
            abstract find: selector: string -> HTMLElement
            abstract findAll: selector: string -> ResizeArray<HTMLElement>
            abstract subscribe: name: string * [<ParamArray>] args: obj[] -> Meteor.SubscriptionHandle
            abstract subscriptionsReady: unit -> bool

        type [<Import("Blaze","meteor/blaze")>] Globals =
            static member View with get(): ViewStatic = jsNative and set(v: ViewStatic): unit = jsNative
            static member currentView with get(): View = jsNative and set(v: View): unit = jsNative
            static member Template with get(): TemplateStatic = jsNative and set(v: TemplateStatic): unit = jsNative
            static member TemplateInstance with get(): TemplateInstanceStatic = jsNative and set(v: TemplateInstanceStatic): unit = jsNative
            static member isTemplate(value: obj): bool = jsNative
            static member Each(argFunc: Function, contentFunc: Function, ?elseFunc: Function): View = jsNative
            static member Unless(conditionFunc: Function, contentFunc: Function, ?elseFunc: Function): View = jsNative
            static member If(conditionFunc: Function, contentFunc: Function, ?elseFunc: Function): View = jsNative
            static member Let(bindings: Function, contentFunc: Function): View = jsNative
            static member With(data: U2<obj, Function>, contentFunc: Function): View = jsNative
            static member getData(?elementOrView: U2<HTMLElement, View>): obj = jsNative
            static member getView(?element: HTMLElement): View = jsNative
            static member remove(renderedView: View): unit = jsNative
            static member render(templateOrView: U2<Template, View>, parentNode: Node, ?nextNode: Node, ?parentView: View): View = jsNative
            static member renderWithData(templateOrView: U2<Template, View>, data: U2<obj, Function>, parentNode: Node, ?nextNode: Node, ?parentView: View): View = jsNative
            static member toHTML(templateOrView: U2<Template, View>): string = jsNative
            static member toHTMLWithData(templateOrView: U2<Template, View>, data: U2<obj, Function>): string = jsNative



module BrowserPolicy =
    type [<AllowNullLiteral>] framingType =
        abstract disallow: unit -> unit
        abstract restrictToOrigin: origin: string -> unit
        abstract allowAll: unit -> unit

    and [<AllowNullLiteral>] contentType =
        abstract allowEval: unit -> unit
        abstract allowInlineStyles: unit -> unit
        abstract allowInlineScripts: unit -> unit
        abstract allowSameOriginForAll: unit -> unit
        abstract allowDataUrlForAll: unit -> unit
        abstract allowOriginForAll: origin: string -> unit
        abstract allowImageOrigin: origin: string -> unit
        abstract allowMediaOrigin: origin: string -> unit
        abstract allowFontOrigin: origin: string -> unit
        abstract allowStyleOrigin: origin: string -> unit
        abstract allowScriptOrigin: origin: string -> unit
        abstract allowFrameOrigin: origin: string -> unit
        abstract allowContentTypeSniffing: unit -> unit
        abstract allowAllContentOrigin: unit -> unit
        abstract allowAllContentDataUrl: unit -> unit
        abstract allowAllContentSameOrigin: unit -> unit
        abstract disallowAll: unit -> unit
        abstract disallowInlineStyles: unit -> unit
        abstract disallowEval: unit -> unit
        abstract disallowInlineScripts: unit -> unit
        abstract disallowFont: unit -> unit
        abstract disallowObject: unit -> unit
        abstract disallowAllContent: unit -> unit

    type [<Import("*","BrowserPolicy")>] Globals =
        static member framing with get(): framingType = jsNative and set(v: framingType): unit = jsNative
        static member content with get(): contentType = jsNative and set(v: contentType): unit = jsNative



module ``meteor/browser-policy-common`` =
    module BrowserPolicy =
        type [<AllowNullLiteral>] framingType =
            abstract disallow: unit -> unit
            abstract restrictToOrigin: origin: string -> unit
            abstract allowAll: unit -> unit

        and [<AllowNullLiteral>] contentType =
            abstract allowEval: unit -> unit
            abstract allowInlineStyles: unit -> unit
            abstract allowInlineScripts: unit -> unit
            abstract allowSameOriginForAll: unit -> unit
            abstract allowDataUrlForAll: unit -> unit
            abstract allowOriginForAll: origin: string -> unit
            abstract allowImageOrigin: origin: string -> unit
            abstract allowMediaOrigin: origin: string -> unit
            abstract allowFontOrigin: origin: string -> unit
            abstract allowStyleOrigin: origin: string -> unit
            abstract allowScriptOrigin: origin: string -> unit
            abstract allowFrameOrigin: origin: string -> unit
            abstract allowContentTypeSniffing: unit -> unit
            abstract allowAllContentOrigin: unit -> unit
            abstract allowAllContentDataUrl: unit -> unit
            abstract allowAllContentSameOrigin: unit -> unit
            abstract disallowAll: unit -> unit
            abstract disallowInlineStyles: unit -> unit
            abstract disallowEval: unit -> unit
            abstract disallowInlineScripts: unit -> unit
            abstract disallowFont: unit -> unit
            abstract disallowObject: unit -> unit
            abstract disallowAllContent: unit -> unit

        type [<Import("BrowserPolicy","meteor/browser-policy-common")>] Globals =
            static member framing with get(): framingType = jsNative and set(v: framingType): unit = jsNative
            static member content with get(): contentType = jsNative and set(v: contentType): unit = jsNative



module Match =
    type [<Import("*","Match")>] Globals =
        static member Any with get(): obj = jsNative and set(v: obj): unit = jsNative
        static member String with get(): obj = jsNative and set(v: obj): unit = jsNative
        static member Integer with get(): obj = jsNative and set(v: obj): unit = jsNative
        static member Boolean with get(): obj = jsNative and set(v: obj): unit = jsNative
        static member undefined with get(): obj = jsNative and set(v: obj): unit = jsNative
        static member Object with get(): obj = jsNative and set(v: obj): unit = jsNative
        static member Optional(pattern: obj): bool = jsNative
        static member ObjectIncluding(dico: obj): bool = jsNative
        static member OneOf([<ParamArray>] patterns: obj[]): obj = jsNative
        static member Where(condition: obj): obj = jsNative
        static member test(value: obj, pattern: obj): bool = jsNative



module ``meteor/check`` =
    type [<Import("*","meteor/check")>] Globals =
        static member check(value: obj, pattern: obj): unit = jsNative

    module Match =
        type [<Import("Match","meteor/check")>] Globals =
            static member Any with get(): obj = jsNative and set(v: obj): unit = jsNative
            static member String with get(): obj = jsNative and set(v: obj): unit = jsNative
            static member Integer with get(): obj = jsNative and set(v: obj): unit = jsNative
            static member Boolean with get(): obj = jsNative and set(v: obj): unit = jsNative
            static member undefined with get(): obj = jsNative and set(v: obj): unit = jsNative
            static member Object with get(): obj = jsNative and set(v: obj): unit = jsNative
            static member Optional(pattern: obj): bool = jsNative
            static member ObjectIncluding(dico: obj): bool = jsNative
            static member OneOf([<ParamArray>] patterns: obj[]): obj = jsNative
            static member Where(condition: obj): obj = jsNative
            static member test(value: obj, pattern: obj): bool = jsNative



module DDPRateLimiter =
    type [<AllowNullLiteral>] Matcher =
        abstract ``type``: U2<string, Func<string, bool>> option with get, set
        abstract name: U2<string, Func<string, bool>> option with get, set
        abstract userId: U2<string, Func<string, bool>> option with get, set
        abstract connectionId: U2<string, Func<string, bool>> option with get, set
        abstract clientAddress: U2<string, Func<string, bool>> option with get, set

    type [<Import("*","DDPRateLimiter")>] Globals =
        static member addRule(matcher: Matcher, numRequests: float, timeInterval: float): string = jsNative
        static member removeRule(ruleId: string): bool = jsNative



module ``meteor/ddp-rate-limiter`` =
    module DDPRateLimiter =
        type [<AllowNullLiteral>] Matcher =
            abstract ``type``: U2<string, Func<string, bool>> option with get, set
            abstract name: U2<string, Func<string, bool>> option with get, set
            abstract userId: U2<string, Func<string, bool>> option with get, set
            abstract connectionId: U2<string, Func<string, bool>> option with get, set
            abstract clientAddress: U2<string, Func<string, bool>> option with get, set

        type [<Import("DDPRateLimiter","meteor/ddp-rate-limiter")>] Globals =
            static member addRule(matcher: Matcher, numRequests: float, timeInterval: float): string = jsNative
            static member removeRule(ruleId: string): bool = jsNative



module DDP =
    type [<AllowNullLiteral>] DDPStatic =
        abstract subscribe: name: string * [<ParamArray>] rest: obj[] -> Meteor.SubscriptionHandle
        abstract call: ``method``: string * [<ParamArray>] parameters: obj[] -> unit
        abstract apply: ``method``: string * [<ParamArray>] parameters: obj[] -> unit
        abstract methods: IMeteorMethodsDictionary: obj -> obj
        abstract status: unit -> DDPStatus
        abstract reconnect: unit -> unit
        abstract disconnect: unit -> unit
        abstract onReconnect: unit -> unit

    and [<AllowNullLiteral>] [<StringEnum>] Status =
        | Connected | Connecting | Failed | Waiting | Offline

    and [<AllowNullLiteral>] DDPStatus =
        abstract connected: bool with get, set
        abstract status: Status with get, set
        abstract retryCount: float with get, set
        abstract retryTime: float option with get, set
        abstract reason: string option with get, set

    type [<Import("*","DDP")>] Globals =
        static member _allSubscriptionsReady(): bool = jsNative
        static member connect(url: string): DDPStatic = jsNative



module DDPCommon =
    type [<AllowNullLiteral>] MethodInvocation =
        [<Emit("new $0($1...)")>] abstract Create: options: obj -> MethodInvocation
        abstract unblock: unit -> unit
        abstract setUserId: userId: float -> unit



module ``meteor/ddp`` =
    module DDP =
        type [<AllowNullLiteral>] DDPStatic =
            abstract subscribe: name: string * [<ParamArray>] rest: obj[] -> Meteor.SubscriptionHandle
            abstract call: ``method``: string * [<ParamArray>] parameters: obj[] -> unit
            abstract apply: ``method``: string * [<ParamArray>] parameters: obj[] -> unit
            abstract methods: IMeteorMethodsDictionary: obj -> obj
            abstract status: unit -> DDPStatus
            abstract reconnect: unit -> unit
            abstract disconnect: unit -> unit
            abstract onReconnect: unit -> unit

        and [<AllowNullLiteral>] [<StringEnum>] Status =
                | Connected | Connecting | Failed | Waiting | Offline

        and [<AllowNullLiteral>] DDPStatus =
            abstract connected: bool with get, set
            abstract status: Status with get, set
            abstract retryCount: float with get, set
            abstract retryTime: float option with get, set
            abstract reason: string option with get, set

        type [<Import("DDP","meteor/ddp")>] Globals =
            static member _allSubscriptionsReady(): bool = jsNative
            static member connect(url: string): DDPStatic = jsNative



    module DDPCommon =
        type [<AllowNullLiteral>] MethodInvocation =
            [<Emit("new $0($1...)")>] abstract Create: options: obj -> MethodInvocation
            abstract unblock: unit -> unit
            abstract setUserId: userId: float -> unit



module EJSON =
    type [<Import("*","EJSON")>] Globals =
        static member newBinary with get(): obj = jsNative and set(v: obj): unit = jsNative
        static member addType(name: string, factory: Func<JSONable, EJSONableCustomType>): unit = jsNative
        static member clone(``val``: 'T): 'T = jsNative
        static member equals(a: EJSON, b: EJSON, ?options: obj): bool = jsNative
        static member fromJSONValue(``val``: JSONable): obj = jsNative
        static member isBinary(x: obj): bool = jsNative
        static member parse(str: string): EJSON = jsNative
        static member stringify(``val``: EJSON, ?options: obj): string = jsNative
        static member toJSONValue(``val``: EJSON): JSONable = jsNative



module ``meteor/ejson`` =
    type [<AllowNullLiteral>] EJSONableCustomType =
        abstract clone: unit -> EJSONableCustomType
        abstract equals: other: obj -> bool
        abstract toJSONValue: unit -> JSONable
        abstract typeName: unit -> string

    and [<AllowNullLiteral>] EJSONable =
        [<Emit("$0[$1]{{=$2}}")>] abstract Item: key: string -> obj with get, set

    and [<AllowNullLiteral>] JSONable =
        [<Emit("$0[$1]{{=$2}}")>] abstract Item: key: string -> obj with get, set

    and [<AllowNullLiteral>] EJSON =
        inherit EJSONable


    module EJSON =
        type [<Import("EJSON","meteor/ejson")>] Globals =
            static member newBinary with get(): obj = jsNative and set(v: obj): unit = jsNative
            static member addType(name: string, factory: Func<JSONable, EJSONableCustomType>): unit = jsNative
            static member clone(``val``: 'T): 'T = jsNative
            static member equals(a: EJSON, b: EJSON, ?options: obj): bool = jsNative
            static member fromJSONValue(``val``: JSONable): obj = jsNative
            static member isBinary(x: obj): bool = jsNative
            static member parse(str: string): EJSON = jsNative
            static member stringify(``val``: EJSON, ?options: obj): string = jsNative
            static member toJSONValue(``val``: EJSON): JSONable = jsNative



module Email =
    type [<Import("*","Email")>] Globals =
        static member send(options: obj): unit = jsNative



module ``meteor/email`` =
    type [<AllowNullLiteral>] MailComposerOptions =
        abstract escapeSMTP: bool with get, set
        abstract encoding: string with get, set
        abstract charset: string with get, set
        abstract keepBcc: bool with get, set
        abstract forceEmbeddedImages: bool with get, set

    and [<AllowNullLiteral>] MailComposerStatic =
        [<Emit("new $0($1...)")>] abstract Create: options: MailComposerOptions -> MailComposer

    and [<AllowNullLiteral>] MailComposer =
        abstract addHeader: name: string * value: string -> unit
        abstract setMessageOption: from: string * ``to``: string * body: string * html: string -> unit
        abstract streamMessage: unit -> unit
        abstract pipe: stream: obj -> unit

    type [<Import("*","meteor/email")>] Globals =
        static member MailComposer with get(): MailComposerStatic = jsNative and set(v: MailComposerStatic): unit = jsNative

    module Email =
        type [<Import("Email","meteor/email")>] Globals =
            static member send(options: obj): unit = jsNative



module HTTP =
    type [<AllowNullLiteral>] HTTPRequest =
        abstract content: string option with get, set
        abstract data: obj option with get, set
        abstract query: string option with get, set
        abstract ``params``: obj option with get, set
        abstract auth: string option with get, set
        abstract headers: obj option with get, set
        abstract timeout: float option with get, set
        abstract followRedirects: bool option with get, set

    and [<AllowNullLiteral>] HTTPResponse =
        abstract statusCode: float option with get, set
        abstract headers: obj option with get, set
        abstract content: string option with get, set
        abstract data: obj option with get, set

    type [<Import("*","HTTP")>] Globals =
        static member call(``method``: string, url: string, ?options: HTTP.HTTPRequest, ?asyncCallback: Function): HTTP.HTTPResponse = jsNative
        static member del(url: string, ?callOptions: HTTP.HTTPRequest, ?asyncCallback: Function): HTTP.HTTPResponse = jsNative
        static member get(url: string, ?callOptions: HTTP.HTTPRequest, ?asyncCallback: Function): HTTP.HTTPResponse = jsNative
        static member post(url: string, ?callOptions: HTTP.HTTPRequest, ?asyncCallback: Function): HTTP.HTTPResponse = jsNative
        static member put(url: string, ?callOptions: HTTP.HTTPRequest, ?asyncCallback: Function): HTTP.HTTPResponse = jsNative
        static member call(``method``: string, url: string, ?options: obj, ?asyncCallback: Function): HTTP.HTTPResponse = jsNative



module ``meteor/http`` =
    module HTTP =
        type [<AllowNullLiteral>] HTTPRequest =
            abstract content: string option with get, set
            abstract data: obj option with get, set
            abstract query: string option with get, set
            abstract ``params``: obj option with get, set
            abstract auth: string option with get, set
            abstract headers: obj option with get, set
            abstract timeout: float option with get, set
            abstract followRedirects: bool option with get, set

        and [<AllowNullLiteral>] HTTPResponse =
            abstract statusCode: float option with get, set
            abstract headers: obj option with get, set
            abstract content: string option with get, set
            abstract data: obj option with get, set

        type [<Import("HTTP","meteor/http")>] Globals =
            static member call(``method``: string, url: string, ?options: HTTP.HTTPRequest, ?asyncCallback: Function): HTTP.HTTPResponse = jsNative
            static member del(url: string, ?callOptions: HTTP.HTTPRequest, ?asyncCallback: Function): HTTP.HTTPResponse = jsNative
            static member get(url: string, ?callOptions: HTTP.HTTPRequest, ?asyncCallback: Function): HTTP.HTTPResponse = jsNative
            static member post(url: string, ?callOptions: HTTP.HTTPRequest, ?asyncCallback: Function): HTTP.HTTPResponse = jsNative
            static member put(url: string, ?callOptions: HTTP.HTTPRequest, ?asyncCallback: Function): HTTP.HTTPResponse = jsNative
            static member call(``method``: string, url: string, ?options: obj, ?asyncCallback: Function): HTTP.HTTPResponse = jsNative



module Meteor =
    type [<AllowNullLiteral>] settingsType =
        abstract ``public``: obj with get, set
        [<Emit("$0[$1]{{=$2}}")>] abstract Item: id: string -> obj with get, set

    and [<AllowNullLiteral>] UserEmail =
        abstract address: string with get, set
        abstract verified: bool with get, set

    and [<AllowNullLiteral>] User =
        abstract _id: string option with get, set
        abstract username: string option with get, set
        abstract emails: ResizeArray<UserEmail> option with get, set
        abstract createdAt: float option with get, set
        abstract profile: obj option with get, set
        abstract services: obj option with get, set

    and [<AllowNullLiteral>] ErrorStatic =
        [<Emit("new $0($1...)")>] abstract Create: error: U2<string, float> * ?reason: string * ?details: string -> Error

    and [<AllowNullLiteral>] Error =
        abstract error: U2<string, float> with get, set
        abstract reason: string option with get, set
        abstract details: string option with get, set

    and [<AllowNullLiteral>] SubscriptionHandle =
        abstract stop: unit -> unit
        abstract ready: unit -> bool

    and [<AllowNullLiteral>] LiveQueryHandle =
        abstract stop: unit -> unit

    type [<Import("*","Meteor")>] Globals =
        static member isClient with get(): bool = jsNative and set(v: bool): unit = jsNative
        static member isCordova with get(): bool = jsNative and set(v: bool): unit = jsNative
        static member isServer with get(): bool = jsNative and set(v: bool): unit = jsNative
        static member isProduction with get(): bool = jsNative and set(v: bool): unit = jsNative
        static member release with get(): string = jsNative and set(v: string): unit = jsNative
        static member settings with get(): settingsType = jsNative and set(v: settingsType): unit = jsNative
        static member users with get(): Mongo.Collection<User> = jsNative and set(v: Mongo.Collection<User>): unit = jsNative
        static member Error with get(): ErrorStatic = jsNative and set(v: ErrorStatic): unit = jsNative
        static member user(): User = jsNative
        static member userId(): string = jsNative
        static member methods(methods: obj): unit = jsNative
        static member call(name: string, [<ParamArray>] args: obj[]): obj = jsNative
        static member apply(name: string, args: ResizeArray<EJSONable>, ?options: obj, ?asyncCallback: Function): obj = jsNative
        static member absoluteUrl(?path: string, ?options: obj): string = jsNative
        static member setInterval(func: Function, delay: float): float = jsNative
        static member setTimeout(func: Function, delay: float): float = jsNative
        static member clearInterval(id: float): unit = jsNative
        static member clearTimeout(id: float): unit = jsNative
        static member defer(func: Function): unit = jsNative
        static member startup(func: Function): unit = jsNative
        static member wrapAsync(func: Function, ?context: obj): obj = jsNative



module ``meteor/meteor`` =
    module Meteor =
        type [<AllowNullLiteral>] settingsType =
            abstract ``public``: obj with get, set
            [<Emit("$0[$1]{{=$2}}")>] abstract Item: id: string -> obj with get, set

        and [<AllowNullLiteral>] UserEmail =
            abstract address: string with get, set
            abstract verified: bool with get, set

        and [<AllowNullLiteral>] User =
            abstract _id: string option with get, set
            abstract username: string option with get, set
            abstract emails: ResizeArray<UserEmail> option with get, set
            abstract createdAt: float option with get, set
            abstract profile: obj option with get, set
            abstract services: obj option with get, set

        and [<AllowNullLiteral>] ErrorStatic =
            [<Emit("new $0($1...)")>] abstract Create: error: U2<string, float> * ?reason: string * ?details: string -> Error

        and [<AllowNullLiteral>] Error =
            abstract error: U2<string, float> with get, set
            abstract reason: string option with get, set
            abstract details: string option with get, set

        and [<AllowNullLiteral>] SubscriptionHandle =
            abstract stop: unit -> unit
            abstract ready: unit -> bool

        and [<AllowNullLiteral>] LiveQueryHandle =
            abstract stop: unit -> unit

        type [<Import("Meteor","meteor/meteor")>] Globals =
            static member isClient with get(): bool = jsNative and set(v: bool): unit = jsNative
            static member isCordova with get(): bool = jsNative and set(v: bool): unit = jsNative
            static member isServer with get(): bool = jsNative and set(v: bool): unit = jsNative
            static member isProduction with get(): bool = jsNative and set(v: bool): unit = jsNative
            static member release with get(): string = jsNative and set(v: string): unit = jsNative
            static member settings with get(): settingsType = jsNative and set(v: settingsType): unit = jsNative
            static member users with get(): Mongo.Collection<User> = jsNative and set(v: Mongo.Collection<User>): unit = jsNative
            static member Error with get(): ErrorStatic = jsNative and set(v: ErrorStatic): unit = jsNative
            static member user(): User = jsNative
            static member userId(): string = jsNative
            static member methods(methods: obj): unit = jsNative
            static member call(name: string, [<ParamArray>] args: obj[]): obj = jsNative
            static member apply(name: string, args: ResizeArray<EJSONable>, ?options: obj, ?asyncCallback: Function): obj = jsNative
            static member absoluteUrl(?path: string, ?options: obj): string = jsNative
            static member setInterval(func: Function, delay: float): float = jsNative
            static member setTimeout(func: Function, delay: float): float = jsNative
            static member clearInterval(id: float): unit = jsNative
            static member clearTimeout(id: float): unit = jsNative
            static member defer(func: Function): unit = jsNative
            static member startup(func: Function): unit = jsNative
            static member wrapAsync(func: Function, ?context: obj): obj = jsNative



module Meteor =
    type [<AllowNullLiteral>] LoginWithExternalServiceOptions =
        abstract requestPermissions: ResizeArray<string> option with get, set
        abstract requestOfflineToken: Boolean option with get, set
        abstract forceApprovalPrompt: Boolean option with get, set
        abstract loginUrlParameters: obj option with get, set
        abstract redirectUrl: string option with get, set
        abstract loginHint: string option with get, set
        abstract loginStyle: string option with get, set

    and [<AllowNullLiteral>] Event =
        abstract ``type``: string with get, set
        abstract target: HTMLElement with get, set
        abstract currentTarget: HTMLElement with get, set
        abstract which: float with get, set
        abstract stopPropagation: unit -> unit
        abstract stopImmediatePropagation: unit -> unit
        abstract preventDefault: unit -> unit
        abstract isPropagationStopped: unit -> bool
        abstract isImmediatePropagationStopped: unit -> bool
        abstract isDefaultPrevented: unit -> bool

    and [<AllowNullLiteral>] EventHandlerFunction =
        inherit Function
        [<Emit("$0($1...)")>] abstract Invoke: ?``event``: Meteor.Event * ?templateInstance: Blaze.TemplateInstance -> unit

    and [<AllowNullLiteral>] EventMap =
        [<Emit("$0[$1]{{=$2}}")>] abstract Item: id: string -> Meteor.EventHandlerFunction with get, set

    type [<Import("*","Meteor")>] Globals =
        static member loginWithMeteorDeveloperAccount(?options: Meteor.LoginWithExternalServiceOptions, ?callback: Function): unit = jsNative
        static member loginWithFacebook(?options: Meteor.LoginWithExternalServiceOptions, ?callback: Function): unit = jsNative
        static member loginWithGithub(?options: Meteor.LoginWithExternalServiceOptions, ?callback: Function): unit = jsNative
        static member loginWithGoogle(?options: Meteor.LoginWithExternalServiceOptions, ?callback: Function): unit = jsNative
        static member loginWithMeetup(?options: Meteor.LoginWithExternalServiceOptions, ?callback: Function): unit = jsNative
        static member loginWithTwitter(?options: Meteor.LoginWithExternalServiceOptions, ?callback: Function): unit = jsNative
        static member loginWithWeibo(?options: Meteor.LoginWithExternalServiceOptions, ?callback: Function): unit = jsNative
        static member loggingIn(): bool = jsNative
        static member loginWith(?options: obj, ?callback: Function): unit = jsNative
        static member loginWithPassword(user: U2<obj, string>, password: string, ?callback: Function): unit = jsNative
        static member loginWithToken(token: string, ?callback: Function): unit = jsNative
        static member logout(?callback: Function): unit = jsNative
        static member logoutOtherClients(?callback: Function): unit = jsNative
        static member reconnect(): unit = jsNative
        static member disconnect(): unit = jsNative
        static member status(): DDP.DDPStatus = jsNative
        static member subscribe(name: string, [<ParamArray>] args: obj[]): Meteor.SubscriptionHandle = jsNative



module ``meteor/meteor`` =
    module Meteor =
        type [<AllowNullLiteral>] LoginWithExternalServiceOptions =
            abstract requestPermissions: ResizeArray<string> option with get, set
            abstract requestOfflineToken: Boolean option with get, set
            abstract forceApprovalPrompt: Boolean option with get, set
            abstract loginUrlParameters: obj option with get, set
            abstract redirectUrl: string option with get, set
            abstract loginHint: string option with get, set
            abstract loginStyle: string option with get, set

        and [<AllowNullLiteral>] Event =
            abstract ``type``: string with get, set
            abstract target: HTMLElement with get, set
            abstract currentTarget: HTMLElement with get, set
            abstract which: float with get, set
            abstract stopPropagation: unit -> unit
            abstract stopImmediatePropagation: unit -> unit
            abstract preventDefault: unit -> unit
            abstract isPropagationStopped: unit -> bool
            abstract isImmediatePropagationStopped: unit -> bool
            abstract isDefaultPrevented: unit -> bool

        and [<AllowNullLiteral>] EventHandlerFunction =
            inherit Function
            [<Emit("$0($1...)")>] abstract Invoke: ?``event``: Meteor.Event * ?templateInstance: Blaze.TemplateInstance -> unit

        and [<AllowNullLiteral>] EventMap =
            [<Emit("$0[$1]{{=$2}}")>] abstract Item: id: string -> Meteor.EventHandlerFunction with get, set

        type [<Import("Meteor","meteor/meteor")>] Globals =
            static member loginWithMeteorDeveloperAccount(?options: Meteor.LoginWithExternalServiceOptions, ?callback: Function): unit = jsNative
            static member loginWithFacebook(?options: Meteor.LoginWithExternalServiceOptions, ?callback: Function): unit = jsNative
            static member loginWithGithub(?options: Meteor.LoginWithExternalServiceOptions, ?callback: Function): unit = jsNative
            static member loginWithGoogle(?options: Meteor.LoginWithExternalServiceOptions, ?callback: Function): unit = jsNative
            static member loginWithMeetup(?options: Meteor.LoginWithExternalServiceOptions, ?callback: Function): unit = jsNative
            static member loginWithTwitter(?options: Meteor.LoginWithExternalServiceOptions, ?callback: Function): unit = jsNative
            static member loginWithWeibo(?options: Meteor.LoginWithExternalServiceOptions, ?callback: Function): unit = jsNative
            static member loggingIn(): bool = jsNative
            static member loginWith(?options: obj, ?callback: Function): unit = jsNative
            static member loginWithPassword(user: U2<obj, string>, password: string, ?callback: Function): unit = jsNative
            static member loginWithToken(token: string, ?callback: Function): unit = jsNative
            static member logout(?callback: Function): unit = jsNative
            static member logoutOtherClients(?callback: Function): unit = jsNative
            static member reconnect(): unit = jsNative
            static member disconnect(): unit = jsNative
            static member status(): DDP.DDPStatus = jsNative
            static member subscribe(name: string, [<ParamArray>] args: obj[]): Meteor.SubscriptionHandle = jsNative



module Meteor =
    type [<AllowNullLiteral>] Connection =
        abstract id: string with get, set
        abstract close: Function with get, set
        abstract onClose: Function with get, set
        abstract clientAddress: string with get, set
        abstract httpHeaders: obj with get, set

    type [<Import("*","Meteor")>] Globals =
        static member onConnection(callback: Function): unit = jsNative
        static member publish(name: string, func: Function): unit = jsNative



module ``meteor/meteor`` =
    type [<AllowNullLiteral>] Subscription =
        abstract connection: Meteor.Connection with get, set
        abstract userId: string with get, set
        abstract added: collection: string * id: string * fields: obj -> unit
        abstract changed: collection: string * id: string * fields: obj -> unit
        abstract error: error: Error -> unit
        abstract onStop: func: Function -> unit
        abstract ready: unit -> unit
        abstract removed: collection: string * id: string -> unit
        abstract stop: unit -> unit

    module Meteor =
        type [<AllowNullLiteral>] Connection =
            abstract id: string with get, set
            abstract close: Function with get, set
            abstract onClose: Function with get, set
            abstract clientAddress: string with get, set
            abstract httpHeaders: obj with get, set

        type [<Import("Meteor","meteor/meteor")>] Globals =
            static member onConnection(callback: Function): unit = jsNative
            static member publish(name: string, func: Function): unit = jsNative



module Mongo =
    type [<AllowNullLiteral>] Selector =
        [<Emit("$0[$1]{{=$2}}")>] abstract Item: key: string -> obj with get, set

    and [<AllowNullLiteral>] Selector =
        interface end

    and [<AllowNullLiteral>] Modifier =
        interface end

    and [<AllowNullLiteral>] SortSpecifier =
        interface end

    and [<AllowNullLiteral>] FieldSpecifier =
        [<Emit("$0[$1]{{=$2}}")>] abstract Item: id: string -> float with get, set

    and [<AllowNullLiteral>] CollectionStatic =
        [<Emit("new $0($1...)")>] abstract Create: name: string * ?options: obj -> Collection<'T>

    and [<AllowNullLiteral>] Collection<'T> =
        abstract allow: options: obj -> bool
        abstract deny: options: obj -> bool
        abstract find: ?selector: U3<Selector, ObjectID, string> * ?options: obj -> Cursor<'T>
        abstract findOne: ?selector: U3<Selector, ObjectID, string> * ?options: obj -> 'T
        abstract insert: doc: 'T * ?callback: Function -> string
        abstract rawCollection: unit -> obj
        abstract rawDatabase: unit -> obj
        abstract remove: selector: U3<Selector, ObjectID, string> * ?callback: Function -> float
        abstract update: selector: U3<Selector, ObjectID, string> * modifier: Modifier * ?options: obj * ?callback: Function -> float
        abstract upsert: selector: U3<Selector, ObjectID, string> * modifier: Modifier * ?options: obj * ?callback: Function -> obj
        abstract _ensureIndex: keys: U2<obj, string> * ?options: obj -> unit
        abstract _dropIndex: keys: U2<obj, string> -> unit

    and [<AllowNullLiteral>] CursorStatic =
        [<Emit("new $0($1...)")>] abstract Create: unit -> Cursor<'T>

    and [<AllowNullLiteral>] ObserveCallbacks =
        abstract added: document: obj -> unit
        abstract addedAt: document: obj * atIndex: float * before: obj -> unit
        abstract changed: newDocument: obj * oldDocument: obj -> unit
        abstract changedAt: newDocument: obj * oldDocument: obj * indexAt: float -> unit
        abstract removed: oldDocument: obj -> unit
        abstract removedAt: oldDocument: obj * atIndex: float -> unit
        abstract movedTo: document: obj * fromIndex: float * toIndex: float * before: obj -> unit

    and [<AllowNullLiteral>] ObserveChangesCallbacks =
        abstract added: id: string * fields: obj -> unit
        abstract addedBefore: id: string * fields: obj * before: obj -> unit
        abstract changed: id: string * fields: obj -> unit
        abstract movedBefore: id: string * before: obj -> unit
        abstract removed: id: string -> unit

    and [<AllowNullLiteral>] Cursor<'T> =
        abstract count: ?applySkipLimit: bool -> float
        abstract fetch: unit -> ResizeArray<'T>
        abstract forEach: callback: Func<'T, float, Cursor<'T>, unit> * ?thisArg: obj -> unit
        abstract map: callback: Func<'T, float, Cursor<'T>, 'U> * ?thisArg: obj -> ResizeArray<'U>
        abstract observe: callbacks: ObserveCallbacks -> Meteor.LiveQueryHandle
        abstract observeChanges: callbacks: ObserveChangesCallbacks -> Meteor.LiveQueryHandle

    and [<AllowNullLiteral>] ObjectIDStatic =
        [<Emit("new $0($1...)")>] abstract Create: ?hexString: string -> ObjectID

    and [<AllowNullLiteral>] ObjectID =
        interface end

    type [<Import("*","Mongo")>] Globals =
        static member Collection with get(): CollectionStatic = jsNative and set(v: CollectionStatic): unit = jsNative
        static member Cursor with get(): CursorStatic = jsNative and set(v: CursorStatic): unit = jsNative
        static member ObjectID with get(): ObjectIDStatic = jsNative and set(v: ObjectIDStatic): unit = jsNative



module ``meteor/mongo`` =
    module Mongo =
        type [<AllowNullLiteral>] Selector =
            [<Emit("$0[$1]{{=$2}}")>] abstract Item: key: string -> obj with get, set

        and [<AllowNullLiteral>] Selector =
            interface end

        and [<AllowNullLiteral>] Modifier =
            interface end

        and [<AllowNullLiteral>] SortSpecifier =
            interface end

        and [<AllowNullLiteral>] FieldSpecifier =
            [<Emit("$0[$1]{{=$2}}")>] abstract Item: id: string -> float with get, set

        and [<AllowNullLiteral>] CollectionStatic =
            [<Emit("new $0($1...)")>] abstract Create: name: string * ?options: obj -> Collection<'T>

        and [<AllowNullLiteral>] Collection<'T> =
            abstract allow: options: obj -> bool
            abstract deny: options: obj -> bool
            abstract find: ?selector: U3<Selector, ObjectID, string> * ?options: obj -> Cursor<'T>
            abstract findOne: ?selector: U3<Selector, ObjectID, string> * ?options: obj -> 'T
            abstract insert: doc: 'T * ?callback: Function -> string
            abstract rawCollection: unit -> obj
            abstract rawDatabase: unit -> obj
            abstract remove: selector: U3<Selector, ObjectID, string> * ?callback: Function -> float
            abstract update: selector: U3<Selector, ObjectID, string> * modifier: Modifier * ?options: obj * ?callback: Function -> float
            abstract upsert: selector: U3<Selector, ObjectID, string> * modifier: Modifier * ?options: obj * ?callback: Function -> obj
            abstract _ensureIndex: keys: U2<obj, string> * ?options: obj -> unit
            abstract _dropIndex: keys: U2<obj, string> -> unit

        and [<AllowNullLiteral>] CursorStatic =
            [<Emit("new $0($1...)")>] abstract Create: unit -> Cursor<'T>

        and [<AllowNullLiteral>] ObserveCallbacks =
            abstract added: document: obj -> unit
            abstract addedAt: document: obj * atIndex: float * before: obj -> unit
            abstract changed: newDocument: obj * oldDocument: obj -> unit
            abstract changedAt: newDocument: obj * oldDocument: obj * indexAt: float -> unit
            abstract removed: oldDocument: obj -> unit
            abstract removedAt: oldDocument: obj * atIndex: float -> unit
            abstract movedTo: document: obj * fromIndex: float * toIndex: float * before: obj -> unit

        and [<AllowNullLiteral>] ObserveChangesCallbacks =
            abstract added: id: string * fields: obj -> unit
            abstract addedBefore: id: string * fields: obj * before: obj -> unit
            abstract changed: id: string * fields: obj -> unit
            abstract movedBefore: id: string * before: obj -> unit
            abstract removed: id: string -> unit

        and [<AllowNullLiteral>] Cursor<'T> =
            abstract count: ?applySkipLimit: bool -> float
            abstract fetch: unit -> ResizeArray<'T>
            abstract forEach: callback: Func<'T, float, Cursor<'T>, unit> * ?thisArg: obj -> unit
            abstract map: callback: Func<'T, float, Cursor<'T>, 'U> * ?thisArg: obj -> ResizeArray<'U>
            abstract observe: callbacks: ObserveCallbacks -> Meteor.LiveQueryHandle
            abstract observeChanges: callbacks: ObserveChangesCallbacks -> Meteor.LiveQueryHandle

        and [<AllowNullLiteral>] ObjectIDStatic =
            [<Emit("new $0($1...)")>] abstract Create: ?hexString: string -> ObjectID

        and [<AllowNullLiteral>] ObjectID =
            interface end

        type [<Import("Mongo","meteor/mongo")>] Globals =
            static member Collection with get(): CollectionStatic = jsNative and set(v: CollectionStatic): unit = jsNative
            static member Cursor with get(): CursorStatic = jsNative and set(v: CursorStatic): unit = jsNative
            static member ObjectID with get(): ObjectIDStatic = jsNative and set(v: ObjectIDStatic): unit = jsNative



module Mongo =
    type [<AllowNullLiteral>] AllowDenyOptions =
        abstract insert: Func<string, obj, bool> option with get, set
        abstract update: Func<string, obj, ResizeArray<string>, obj, bool> option with get, set
        abstract remove: Func<string, obj, bool> option with get, set
        abstract fetch: ResizeArray<string> option with get, set
        abstract transform: Function option with get, set



module ``meteor/mongo`` =
    module Mongo =
        type [<AllowNullLiteral>] AllowDenyOptions =
            abstract insert: Func<string, obj, bool> option with get, set
            abstract update: Func<string, obj, ResizeArray<string>, obj, bool> option with get, set
            abstract remove: Func<string, obj, bool> option with get, set
            abstract fetch: ResizeArray<string> option with get, set
            abstract transform: Function option with get, set



module Random =
    type [<Import("*","Random")>] Globals =
        static member id(?numberOfChars: float): string = jsNative
        static member secret(?numberOfChars: float): string = jsNative
        static member fraction(): float = jsNative
        static member hexString(numberOfDigits: float): string = jsNative
        static member choice(array: ResizeArray<obj>): string = jsNative
        static member choice(str: string): string = jsNative



module ``meteor/random`` =
    module Random =
        type [<Import("Random","meteor/random")>] Globals =
            static member id(?numberOfChars: float): string = jsNative
            static member secret(?numberOfChars: float): string = jsNative
            static member fraction(): float = jsNative
            static member hexString(numberOfDigits: float): string = jsNative
            static member choice(array: ResizeArray<obj>): string = jsNative
            static member choice(str: string): string = jsNative



module ``meteor/reactive-var`` =
    type [<AllowNullLiteral>] ReactiveVarStatic =
        [<Emit("new $0($1...)")>] abstract Create: initialValue: 'T * ?equalsFunc: Function -> ReactiveVar<'T>

    and [<AllowNullLiteral>] ReactiveVar<'T> =
        abstract get: unit -> 'T
        abstract set: newValue: 'T -> unit

    type [<Import("*","meteor/reactive-var")>] Globals =
        static member ReactiveVar with get(): ReactiveVarStatic = jsNative and set(v: ReactiveVarStatic): unit = jsNative



module Session =
    type [<Import("*","Session")>] Globals =
        static member equals(key: string, value: U4<string, float, bool, obj>): bool = jsNative
        static member get(key: string): obj = jsNative
        static member set(key: string, value: U2<EJSONable, obj>): unit = jsNative
        static member setDefault(key: string, value: U2<EJSONable, obj>): unit = jsNative



module ``meteor/session`` =
    module Session =
        type [<Import("Session","meteor/session")>] Globals =
            static member equals(key: string, value: U4<string, float, bool, obj>): bool = jsNative
            static member get(key: string): obj = jsNative
            static member set(key: string, value: U2<EJSONable, obj>): unit = jsNative
            static member setDefault(key: string, value: U2<EJSONable, obj>): unit = jsNative



module ``meteor/templating`` =
    type [<AllowNullLiteral>] TemplateStatic =
        inherit Blaze.TemplateStatic
        abstract body: Blaze.Template with get, set
        [<Emit("$0[$1]{{=$2}}")>] abstract Item: index: string -> U2<obj, Blaze.Template> with get, set
        [<Emit("new $0($1...)")>] abstract Create: ?viewName: string * ?renderFunction: Function -> Blaze.Template

    type [<Import("*","meteor/templating")>] Globals =
        static member Template with get(): TemplateStatic = jsNative and set(v: TemplateStatic): unit = jsNative



module Tinytest =
    type [<Import("*","Tinytest")>] Globals =
        static member add(description: string, func: Func<ITinytestAssertions, unit>): unit = jsNative
        static member addAsync(description: string, func: Func<ITinytestAssertions, unit>): unit = jsNative



module ``meteor/tiny-test`` =
    type [<AllowNullLiteral>] ILengthAble =
        abstract length: float with get, set

    and [<AllowNullLiteral>] ITinytestAssertions =
        abstract ok: doc: obj -> unit
        abstract expect_fail: unit -> unit
        abstract fail: doc: obj -> unit
        abstract runId: unit -> string
        abstract equal: actual: 'T * expected: 'T * ?message: string * ?not: bool -> unit
        abstract notEqual: actual: 'T * expected: 'T * ?message: string -> unit
        abstract instanceOf: obj: obj * klass: Function * ?message: string -> unit
        abstract notInstanceOf: obj: obj * klass: Function * ?message: string -> unit
        abstract matches: actual: obj * regexp: Regex * ?message: string -> unit
        abstract notMatches: actual: obj * regexp: Regex * ?message: string -> unit
        abstract throws: f: Function * ?expected: U2<string, Regex> -> unit
        abstract isTrue: v: bool * ?msg: string -> unit
        abstract isFalse: v: bool * ?msg: string -> unit
        abstract isNull: v: obj * ?msg: string -> unit
        abstract isNotNull: v: obj * ?msg: string -> unit
        abstract isUndefined: v: obj * ?msg: string -> unit
        abstract isNotUndefined: v: obj * ?msg: string -> unit
        abstract isNan: v: obj * ?msg: string -> unit
        abstract isNotNan: v: obj * ?msg: string -> unit
        abstract ``include``: s: U3<ResizeArray<'T>, obj, string> * value: obj * ?msg: string * ?not: bool -> unit
        abstract notInclude: s: U3<ResizeArray<'T>, obj, string> * value: obj * ?msg: string * ?not: bool -> unit
        abstract length: obj: ILengthAble * expected_length: float * ?msg: string -> unit
        abstract _stringEqual: actual: string * expected: string * ?msg: string -> unit

    module Tinytest =
        type [<Import("Tinytest","meteor/tiny-test")>] Globals =
            static member add(description: string, func: Func<ITinytestAssertions, unit>): unit = jsNative
            static member addAsync(description: string, func: Func<ITinytestAssertions, unit>): unit = jsNative



module Tracker =
    type [<AllowNullLiteral>] Computation =
        abstract firstRun: bool with get, set
        abstract invalidated: bool with get, set
        abstract stopped: bool with get, set
        abstract invalidate: unit -> unit
        abstract onInvalidate: callback: Function -> unit
        abstract onStop: callback: Function -> unit
        abstract stop: unit -> unit

    and [<AllowNullLiteral>] DependencyStatic =
        [<Emit("new $0($1...)")>] abstract Create: unit -> Dependency

    and [<AllowNullLiteral>] Dependency =
        abstract changed: unit -> unit
        abstract depend: ?fromComputation: Computation -> bool
        abstract hasDependents: unit -> bool

    type [<Import("*","Tracker")>] Globals =
        static member currentComputation with get(): Computation = jsNative and set(v: Computation): unit = jsNative
        static member Dependency with get(): DependencyStatic = jsNative and set(v: DependencyStatic): unit = jsNative
        static member active with get(): bool = jsNative and set(v: bool): unit = jsNative
        static member Computation(): unit = jsNative
        static member afterFlush(callback: Function): unit = jsNative
        static member autorun(runFunc: Func<Computation, unit>, ?options: obj): Computation = jsNative
        static member flush(): unit = jsNative
        static member nonreactive(func: Function): unit = jsNative
        static member onInvalidate(callback: Function): unit = jsNative



module ``meteor/tracker`` =
    module Tracker =
        type [<AllowNullLiteral>] Computation =
            abstract firstRun: bool with get, set
            abstract invalidated: bool with get, set
            abstract stopped: bool with get, set
            abstract invalidate: unit -> unit
            abstract onInvalidate: callback: Function -> unit
            abstract onStop: callback: Function -> unit
            abstract stop: unit -> unit

        and [<AllowNullLiteral>] DependencyStatic =
            [<Emit("new $0($1...)")>] abstract Create: unit -> Dependency

        and [<AllowNullLiteral>] Dependency =
            abstract changed: unit -> unit
            abstract depend: ?fromComputation: Computation -> bool
            abstract hasDependents: unit -> bool

        type [<Import("Tracker","meteor/tracker")>] Globals =
            static member currentComputation with get(): Computation = jsNative and set(v: Computation): unit = jsNative
            static member Dependency with get(): DependencyStatic = jsNative and set(v: DependencyStatic): unit = jsNative
            static member active with get(): bool = jsNative and set(v: bool): unit = jsNative
            static member Computation(): unit = jsNative
            static member afterFlush(callback: Function): unit = jsNative
            static member autorun(runFunc: Func<Computation, unit>, ?options: obj): Computation = jsNative
            static member flush(): unit = jsNative
            static member nonreactive(func: Function): unit = jsNative
            static member onInvalidate(callback: Function): unit = jsNative



module Match =
    type [<Import("*","Match")>] Globals =
        static member Maybe(pattern: obj): bool = jsNative



module ``meteor/check`` =
    module Match =
        type [<Import("Match","meteor/check")>] Globals =
            static member Maybe(pattern: obj): bool = jsNative

            

module Meteor =
    type [<Import("*","Meteor")>] Globals =
        static member isDevelopment with get(): bool = jsNative and set(v: bool): unit = jsNative
        static member isTest with get(): bool = jsNative and set(v: bool): unit = jsNative



module ``meteor/meteor`` =
    module Meteor =
        type [<Import("Meteor","meteor/meteor")>] Globals =
            static member isDevelopment with get(): bool = jsNative and set(v: bool): unit = jsNative
            static member isTest with get(): bool = jsNative and set(v: bool): unit = jsNative



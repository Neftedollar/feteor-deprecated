Package.describe({
    name: "fable-meteor-compiler",
    summary: "F# Fable compiler for meteor",
    version: "0.0.1"
})
Package.registerBuildPlugin({
    name: "compilerFsharpFable",
    use: ['caching-compiler'],
    sources: ['plugin/fable-plugin.js'],
    npmDependencies: {
        "fable-compiler": "0.7.23"
    }
})

Package.onUse(function (api) {
    api.use('isobuild:compiler-plugin@1.0.0');
    api.use('babel-compiler');
})
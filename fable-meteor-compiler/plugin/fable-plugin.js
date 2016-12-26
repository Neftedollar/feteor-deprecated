const exec = require('child_process').exec

Plugin.registerCompiler({
    extensions: ["fsproj", "fsx", "fs"]
}, () => new FableCompiler());

export class FableCompiler extends CachingCompiler {
    constructor() {
        super({
            compilerName: 'fable',
            defaultCacheSize: 1024 * 1024 * 10
        })
        exec('fable --help')
    }

    processFilesForTarget (files) {
        files.forEach(f => console.log(f));
    }
}
// Import Tinytest from the tinytest Meteor package.
import { Tinytest } from "meteor/tinytest";

// Import and rename a variable exported by fable-meteor-compiler.js.
import { name as packageName } from "meteor/neftedollar:fable-meteor-compiler";

// Write your tests here!
// Here is an example.
Tinytest.add('fable-meteor-compiler - example', function (test) {
  test.equal(packageName, "fable-meteor-compiler");
});

# Contract Versioning

This is a simple experiment to play around with the new `Span<T>` type in .NET Core to slice up a stream of bytes for efficient parsing.

The idea is to make it easy to use a custom binary format to represent contracts between services that can be versioned.
Why? Because being able to upgrade a service in a cluster without affecting dependent applications means less downtime.
Maybe. The goal of this is to be part of a wider experiment to build and deploy Service Fabric (or Mesos + Marathon) apps with a rolling deployment strategy.

# Some thoughts

- Use Roslyn for compile-time code generation. Basically everything in `Genned.cs` should be generated. Making this useful and tunable will be a challenge.
- Add some conventions around requiring a contract version implements `From<V-1>`
- Responses will be trickier, if a service makes a request as v, then it expects a response as v. This suggests the version should be carried all the way from the contract through the implementation
- To keep things simple, just use ASCII encoded JSON (or maybe BSON) as the representation for the data sans version
- Another thing to consider is routing, so supporting multiple req/res pairs in a single service. Changes to one req/res type should not affect others

This should go hand-in-hand with making [easy-mesos](https://github.com/KodrAus/easy-mesos) a useful thing for local Mesosphere clusters.

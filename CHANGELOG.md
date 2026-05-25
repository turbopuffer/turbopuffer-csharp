# Changelog

## 0.1.0 (2026-05-25)

Full Changelog: [v0.1.0-alpha.7...v0.1.0](https://github.com/turbopuffer/turbopuffer-csharp/compare/v0.1.0-alpha.7...v0.1.0)

### Features

* add typed Get/TryGet to Row and Columns ([#19](https://github.com/turbopuffer/turbopuffer-csharp/issues/19)) ([4ae2e30](https://github.com/turbopuffer/turbopuffer-csharp/commit/4ae2e300929ce478a597c5247b88aa30b3d560f0))
* emit generic factories for `Filter.In` and similar variants ([#21](https://github.com/turbopuffer/turbopuffer-csharp/issues/21)) ([f6f1afc](https://github.com/turbopuffer/turbopuffer-csharp/commit/f6f1afcd5425ac2fc3fceea935717fcdcceedbf3))
* spec: force generation of FuzzyParams stainless models ([86b3cc9](https://github.com/turbopuffer/turbopuffer-csharp/commit/86b3cc96763035051ba0751bf125dc64579b7bfe))
* use Row for PatchByFilter.Patch ([#22](https://github.com/turbopuffer/turbopuffer-csharp/issues/22)) ([54b5f13](https://github.com/turbopuffer/turbopuffer-csharp/commit/54b5f13c3ebb4f8ca56c595661a1a044ba99d4ff))

## 0.1.0-alpha.7 (2026-05-19)

Full Changelog: [v0.1.0-alpha.6...v0.1.0-alpha.7](https://github.com/turbopuffer/turbopuffer-csharp/compare/v0.1.0-alpha.6...v0.1.0-alpha.7)

### Bug Fixes

* wrap CopyFrom request body in copy_from_namespace ([#17](https://github.com/turbopuffer/turbopuffer-csharp/issues/17)) ([a581ad3](https://github.com/turbopuffer/turbopuffer-csharp/commit/a581ad3e0c2ff3ad0d76f781687aaf1af462efdd))

## 0.1.0-alpha.6 (2026-05-18)

Full Changelog: [v0.1.0-alpha.5...v0.1.0-alpha.6](https://github.com/turbopuffer/turbopuffer-csharp/compare/v0.1.0-alpha.5...v0.1.0-alpha.6)

### Features

* add toggleable request and response compression ([#13](https://github.com/turbopuffer/turbopuffer-csharp/issues/13)) ([05b4c40](https://github.com/turbopuffer/turbopuffer-csharp/commit/05b4c40787c90e56078beaf55a0bb266f36ec69f))
* spec: rename csharp SDK package to Turbopuffer ([29fe3cd](https://github.com/turbopuffer/turbopuffer-csharp/commit/29fe3cd9722e1449227279b216e85080dd6d89cc))


### Bug Fixes

* serialize boxed apigen subtypes correctly ([#16](https://github.com/turbopuffer/turbopuffer-csharp/issues/16)) ([604552c](https://github.com/turbopuffer/turbopuffer-csharp/commit/604552c5b27e9fbcc398689ebdf4d808ec0c330c))
* wrap BranchFrom request body in branch_from_namespace ([#14](https://github.com/turbopuffer/turbopuffer-csharp/issues/14)) ([cef1e6c](https://github.com/turbopuffer/turbopuffer-csharp/commit/cef1e6c17de8dc273b76ed04a84fc4accff869c1))

## 0.1.0-alpha.5 (2026-05-18)

Full Changelog: [v0.1.0-alpha.4...v0.1.0-alpha.5](https://github.com/turbopuffer/turbopuffer-csharp/compare/v0.1.0-alpha.4...v0.1.0-alpha.5)

### Bug Fixes

* type write filter/condition params as Filter instead of JsonElement ([af0a133](https://github.com/turbopuffer/turbopuffer-csharp/commit/af0a13399b06328fbb06d5ebf2ebb2bfaa7067bc))

## 0.1.0-alpha.4 (2026-05-18)

Full Changelog: [v0.1.0-alpha.3...v0.1.0-alpha.4](https://github.com/turbopuffer/turbopuffer-csharp/compare/v0.1.0-alpha.3...v0.1.0-alpha.4)

### Bug Fixes

* improve ergonomics of custom types ([7165003](https://github.com/turbopuffer/turbopuffer-csharp/commit/71650031d5dd97bfb8a65801d523816c3c5dbea5))


### Documentation

* fix README API usage and strip header tag for NuGet ([#10](https://github.com/turbopuffer/turbopuffer-csharp/issues/10)) ([e07d83b](https://github.com/turbopuffer/turbopuffer-csharp/commit/e07d83bc9f60240288cb56943cbf1869ecdea87c))

## 0.1.0-alpha.3 (2026-05-18)

Full Changelog: [v0.1.0-alpha.2...v0.1.0-alpha.3](https://github.com/turbopuffer/turbopuffer-csharp/compare/v0.1.0-alpha.2...v0.1.0-alpha.3)

### Features

* spec: rename C# package to Turbopuffer.Client ([f749a74](https://github.com/turbopuffer/turbopuffer-csharp/commit/f749a749a72d08b41074c24df15deb8a54d16753))

## 0.1.0-alpha.2 (2026-05-18)

Full Changelog: [v0.1.0-alpha.1...v0.1.0-alpha.2](https://github.com/turbopuffer/turbopuffer-csharp/compare/v0.1.0-alpha.1...v0.1.0-alpha.2)

### Features

* publish csharp ([b2b5204](https://github.com/turbopuffer/turbopuffer-csharp/commit/b2b52043d550cfe3f783675d4e76ad68191f6aee))

## 0.1.0-alpha.1 (2026-05-18)

Full Changelog: [v0.0.1...v0.1.0-alpha.1](https://github.com/turbopuffer/turbopuffer-csharp/compare/v0.0.1...v0.1.0-alpha.1)

### Features

* spec: add C# stainless target ([2c15956](https://github.com/turbopuffer/turbopuffer-csharp/commit/2c15956e1e677c26acac66fbc5b6c2d0438e35ff))


### Bug Fixes

* add C# ports of the Java examples ([c3badb3](https://github.com/turbopuffer/turbopuffer-csharp/commit/c3badb32fdb05c21c406ce9d09185416497f7f27))
* bring to parity with other languages' client libraries ([3e159ae](https://github.com/turbopuffer/turbopuffer-csharp/commit/3e159ae3be32e97c1efe79afbbdb23facdf32057))

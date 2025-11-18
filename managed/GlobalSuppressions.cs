// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Style",
    "IDE0052:Remove unread private members",
    Justification = "<Pending>",
    Scope = "member",
    Target = "~P:SwiftlyS2.Core.Events.EventSubscriber._Id"
)]

// Compiler warnings - Pointers and unsafe code
[assembly: SuppressMessage(
    "Compiler",
    "CS8500:This takes the address of, gets the size of, or declares a pointer to a managed type",
    Justification = "Required for interop with native code"
)]

// Nullability warnings - General
[assembly: SuppressMessage(
    "Compiler",
    "CS8600:Converting null literal or possible null value to non-nullable type",
    Justification = "Nullable handling reviewed for interop scenarios"
)]
[assembly: SuppressMessage(
    "Compiler",
    "CS8601:Possible null reference assignment",
    Justification = "Generated code and nullable handling reviewed"
)]
[assembly: SuppressMessage(
    "Compiler",
    "CS8602:Dereference of a possibly null reference",
    Justification = "Null checks handled at runtime where needed"
)]
[assembly: SuppressMessage(
    "Compiler",
    "CS8603:Possible null reference return",
    Justification = "Null returns handled by API contracts"
)]
[assembly: SuppressMessage(
    "Compiler",
    "CS8604:Possible null reference argument",
    Justification = "Null arguments validated by interop layer"
)]
[assembly: SuppressMessage(
    "Compiler",
    "CS8618:Non-nullable field must contain a non-null value when exiting constructor",
    Justification = "Fields initialized through dependency injection or factory methods"
)]
[assembly: SuppressMessage(
    "Compiler",
    "CS8625:Cannot convert null literal to non-nullable reference type",
    Justification = "Generated SteamAPI code"
)]
[assembly: SuppressMessage(
    "Compiler",
    "CS8765:Nullability of type of parameter doesn't match overridden member",
    Justification = "Generated SteamAPI code with different nullability annotations"
)]
[assembly: SuppressMessage(
    "Compiler",
    "CS8766:Nullability of reference types in return type doesn't match implicitly implemented member",
    Justification = "Generated schema code with different nullability annotations"
)]

// XML Documentation warnings - Mostly in generated code
[assembly: SuppressMessage(
    "Documentation",
    "CS1570:XML comment has badly formed XML",
    Justification = "Generated SteamAPI documentation with XML formatting issues"
)]
[assembly: SuppressMessage(
    "Documentation",
    "CS1572:XML comment has a param tag but there is no parameter by that name",
    Justification = "Documentation mismatch in API definitions"
)]
[assembly: SuppressMessage(
    "Documentation",
    "CS1573:Parameter has no matching param tag in the XML comment",
    Justification = "Incomplete XML documentation"
)]
[assembly: SuppressMessage(
    "Documentation",
    "CS1587:XML comment is not placed on a valid language element",
    Justification = "Documentation placement issues in templates"
)]
[assembly: SuppressMessage(
    "Compiler",
    "CS0419:Ambiguous reference in cref attribute",
    Justification = "Method overloads in XML documentation"
)]

// Build warnings
[assembly: SuppressMessage(
    "Build",
    "MSB3270:There was a mismatch between the processor architecture",
    Justification = "Cross-platform compatibility - architecture mismatch expected"
)]

// Code quality - TestPlugin specific
[assembly: SuppressMessage(
    "Compiler",
    "CS0219:The variable is assigned but its value is never used",
    Justification = "Test/example code",
    Scope = "namespaceanddescendants",
    Target = "~N:TestPlugin"
)]
[assembly: SuppressMessage(
    "Compiler",
    "CS0649:Field is never assigned to and will always have its default value",
    Justification = "Test/example code",
    Scope = "namespaceanddescendants",
    Target = "~N:TestPlugin"
)]

// Obsolete API usage
[assembly: SuppressMessage(
    "Compiler",
    "CS0618:Member is obsolete",
    Justification = "Transitional code using deprecated APIs",
    Scope = "member",
    Target = "~M:SwiftlyS2.Core.Services.CoreCommandService"
)]

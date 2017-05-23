Since Smalltalk is a GUI oriented language, several changes have been made to Talkative for a better compiler language experience.

## Functions

In Smalltalk, there isn't a way to write functions in code. Although there is a way to write this using exclamation marks, I'm not a big fan of it, hence I am introducint the following pattern.

```Smalltalk
Object subclass: #SomeClass
SomeClass class instanceVariableNames: 'x'
SomeClass class instanceMethodNames: 'add'

SomeClass class methodFor:'add' = [ x: y: |
    ^ x + y.
]
```

The declaration of a function will be accomplished by setting a block to a variable, much like a function object in Javascript or a lambda in C#.


## Using declarations

Smalltalk doesn't have any using declarations, but for the ease of using C# frameworks, the following declarations may be used.

```Talkative
using System.
using System.Collections.Generic.
```

The usage of periods to connect identifiers will only be permitted upon importing C# libraries.

## Using the C# libraries

Any classes in the C# library may be used like so:

```Talkative
using System.

Console writeLine:'Hello World!'.
Console WriteLine:'Hello World!'.

```

Starting method names in lower case is permitted when using C# libraries, as all functions written in C# start with a capital letter.

I am doing this, because Talkative (and Smalltalk) should try to obey the English language at its best.
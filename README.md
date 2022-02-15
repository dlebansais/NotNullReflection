# Easly-Language

Easly language AST description and helpers.

<img src="https://github.com/dlebansais/Easly-Draw/blob/master/Easly-Draw/Resources/icon.png?raw=true" width="16" height="16"/> [![Build status](https://ci.appveyor.com/api/projects/status/4m9q4r5b6hxd7imp?svg=true)](https://ci.appveyor.com/project/dlebansais/easly-language)

This assembly contains definitions for all nodes of the [Easly](https://www.easly.org) language, as well as helper classes to create these nodes.

TODO:

- [ ] Make all nodes with a "Text" property inherit from a ITextNode interface.
- [ ] Get rid of the hack in IOptionalReference
- [X] Add a new keyword: Indexer   
- [X] Add a new node: keyword entity expression.   

## Hack in IOptionalReference

The specification for optional node is that if the node is unassigned, the value of `Item` is undefined. In practice, trying to read or write it gives an exception.
Now, it's particularly useful in the editor to be able to keep a "hidden" node because when the user expand or collapse the optional node, cells and other graphic info don't get replaced.
Hence the hack, which consist of replacing `Item` with a default construct for the node (depending on its type) when the file is loaded. When the file is saved, if the default construct is still unassigned it simply ignored and not serialized.

A solution to this problem could be maintaining "virtual" nodes in the controller, and assigning the virtual node to the real node when the optional reference is expanded.


# Kerbal Space Program: Crowd Control
A Kerbal Space Program 1.9 mod by [Rin](https://github.com/ry00001).

## Powered by
This mod is powered by the *awesome* [FullSerializer](https://github.com/jacobdufault/FullSerializer) library.

## Why?
I don't know.

## How to use
Well, first of all, you really shouldn't use this unless you have knowledge of C#, Node.js and how to run a server, 
as this mod has no configuration files and the only way to change the address of the server is by recompiling the DLL. 
As such, no releases will be offered.
  
If you must, set up KSPCC_Server the same way you'd set up any other Node.js application, then recompile the DLL, 
making sure to change url in KerbalCrowdControl.cs to the address of your server. Then drop the DLL, along with FullSerializer and websocket-sharp, 
into a folder in your KSP GameData. I assume you know how to do this.
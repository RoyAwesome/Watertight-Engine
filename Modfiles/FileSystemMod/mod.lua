local Mod = {};
Mod.Name = "FileSystemMod";
Mod.Author = {"RoyAwesome"};
Mod.Version = "0.0.1";
Mod.ServerMain = "sv_main.lua";
Mod.ClientMain = "cl_main.lua";

Mod.IncludeFiles = { "script://FileSystemMod/components/testcomponent.lua" }


RegisterMod(Mod);
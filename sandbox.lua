
--remove access to LuaInterface
luanet = nil;
package.loaded.luanet = nil;

--prevent future packages from being loaded
require = nil;
package.loadlib = nil;

--Remove Unsafe lualib code
collectgarbage = nil;
dofile = nil;
getfenv = nil;
load = nil;
loadfile = nil;
loadstring = nil;

--These aren't safe in certain situations
rawequal = nil;
rawget = nil;
rawset = nil;

--This changes global environment settings.  Don't want a mod to do this
setfenv = nil;

--Used to load other libraries 
module = nil;
package = nil;

--Access OS level and FileIO.  Dangerous for untrusted code
io = nil;
os = nil;

--Debug functions that let you access things you shouldn't
debug = nil;
newproxy = nil;

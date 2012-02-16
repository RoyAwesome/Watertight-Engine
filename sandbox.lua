
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

rawequal = nil;
rawget = nil;
rawset = nil;

setfenv = nil;

module = nil;

package = nil;

io = nil;
os = nil;

debug = nil;
newproxy = nil;

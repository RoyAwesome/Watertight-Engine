m = GetMod("FileSystemMod");




function OnInit()
  print("Mod is " .. m:GetName());
end
m:RegisterHook("Init", OnInit);


function OnTick(dt)
   print("tick tock");
end
m:RegisterHook("OnTick", OnTick);
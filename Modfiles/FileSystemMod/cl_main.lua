m = GetMod("FileSystemMod");


local shader = nil;

function OnInit()
  print("Mod is " .. m:GetName());
  print("Is this working at all?");
  render1 = 0
  render2 = 0
  render3 = 0
  rotation = 0
  render1Forward = false
  render2Forward = false
  render3Forward = false
  
  ent = Entity()  
  print("Entity named "..ent.Name)
  local t = ent:GetTransform()
  print("transform: "..t.Position:ToString().. " type: "..type(t))
  
end
m:RegisterHook("Init", OnInit);


function ResourceLoad()
  shader = FS.LoadEffect("shader://FileSystemMod/effects/basic30.effect" );
  
end
m:RegisterHook("ResourceLoad", ResourceLoad);

function OnTick(dt)
  
end
m:RegisterHook("OnTick", OnTick);


function PreRender(renderer)
	renderer.ActiveShader = shader;
	renderer.ActiveShader["Proj"] = Math.Matrix4.Identity;
	renderer.ActiveShader["View"] = Math.Matrix4.Identity;

end
m:RegisterHook("PreRender", PreRender);

function OnRender(dt, renderer)
  

  if (render1Forward) then
    render1 = render1 + 1
    render1Forward = render1 < 255
  else
    render1 = render1 - 1
    render1Forward = render2 < 0
  end
  if (render2Forward) then
    render2 = render2 + 1
    render2Forward = render2 < 255
  else
    render2 = render2 - 1
    render2Forward = render2 < 0
  end
  if (render3Forward) then
    render3 = render3 + 1
    render3Forward = render3 < 255
  else
    render3 = render3 - 1
    render3Forward = render3 < 0
  end
  renderer:AddColor(render1 / 255, 1, render1 / 255);
    renderer:AddVertex(0, 1);
  renderer:AddColor(.5, render2 / 255, render2 / 255);
  renderer:AddVertex(-1, -1);
  renderer:AddColor(1, 0, render3 / 255);
  renderer:AddVertex(1, -1);
 
end
m:RegisterHook("OnRender", OnRender);

--Test("Author is" .. WTMod.Author);
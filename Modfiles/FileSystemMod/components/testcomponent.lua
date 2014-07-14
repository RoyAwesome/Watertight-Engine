
local cmp = {}

function cmp:Awake()
	print("tab = "..tostring(self))
    self.i = self.i or 1
	print("Test Component for is working: " .. tostring(self.i))	
end

function cmp:Tick(dt)
	self.i = dt
end

RegisterComponent("TestLuaComponent", cmp)
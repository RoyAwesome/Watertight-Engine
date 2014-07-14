--rebind print to use the Game Console Messaging system
print = Msg

function Instantiate(mt)
    o = {}
    setmetatable(o, mt)    
    return o
end
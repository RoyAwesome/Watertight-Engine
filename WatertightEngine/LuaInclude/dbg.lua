function dbg(tab)
    print("-----BEGIN DBG-----")
    for k, v in pairs(tab) do
        print("\t K: "..k.." , v: "..v.." t:"..type(v))
    end
    print("mt")
    for k, v in pairs(getmetatable(tab)) do
        print("\t K: "..k.." , v: "..type(v))
    end
    print("-----END DBG------")
end
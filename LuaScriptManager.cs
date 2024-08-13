using MoonSharp.Interpreter;
using System;
using System.IO;

public class LuaScriptManager
{
    private Script _lua;

    public LuaScriptManager()
    {
        _lua = new Script();
    }

    public void LoadScript(string scriptPath)
    {
        if (File.Exists(scriptPath))
        {
            try
            {
                _lua.DoFile(scriptPath);
                Console.WriteLine($"Loaded script: {scriptPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading script {scriptPath}: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"Script not found: {scriptPath}");
        }
    }

    public DynValue GetGlobal(string globalName)
    {
        return _lua.Globals.Get(globalName);
    }
}

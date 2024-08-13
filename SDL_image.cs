using System;
using System.Runtime.InteropServices;

public static class SDL_image
{
    [DllImport("SDL2_image.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr IMG_LoadTexture(IntPtr renderer, string file);

    [DllImport("SDL2_image.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr IMG_Load(string file);
}

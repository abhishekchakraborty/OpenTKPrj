using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace OpenTK1
{
    public class MainWindow: GameWindow
    {
        public MainWindow(): base(720, 720, OpenTK.Graphics.GraphicsMode.Default, 
                                  "dreamstatecoding", GameWindowFlags.Default, 
                                  DisplayDevice.Default, 4, 0, 
                                  OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible )
        {
            Title += ": OpenGL Version: " + GL.GetString(StringName.Version);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnLoad(EventArgs e)
        {
            CursorVisible = true;
        }

        //protected override void OnUpdateFrame(FrameEventArgs e)
        //{
        //    HandleKeyboard();
        //}

        //private void HandleKeyboard()
        //{
        //    var keyState = Keyboard.GetState();

        //    if (keyState.IsKeyDown(Key.Escape))
        //    {
        //        Exit();
        //    }
        //}

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            Title = $"(Vsync: {VSync}) FPS: {1f / e.Time:0}";

            OpenTK.Graphics.Color4 backColor;

            backColor.A = 1.0f;
            backColor.R = 0.1f;
            backColor.G = 0.1f;
            backColor.B = 0.3f;
            GL.ClearColor(backColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            SwapBuffers();
        }
    }
}

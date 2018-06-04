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

        private int _program;
        private int _vertexArray;

        protected override void OnLoad(EventArgs e)
        {
            CursorVisible = true;
            _program = CompileShaders();

            GL.GenVertexArrays(1, out _vertexArray);
            GL.BindVertexArray(_vertexArray);

            Closed += OnClosed;
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            Exit();
        }

        public override void Exit()
        {
            GL.DeleteVertexArrays(1, ref _vertexArray);

            GL.DeleteProgram(_program);
            base.Exit();
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

            GL.UseProgram(_program);

            GL.DrawArrays(PrimitiveType.Points, 0, 1);
            GL.PointSize(10);

            SwapBuffers();
        }


        private int CompileShaders()
        {
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, System.IO.File.ReadAllText(@"..\..\vertexShader.vert") );
            GL.CompileShader(vertexShader);

            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, System.IO.File.ReadAllText(@"..\..\fragmentShader.frag"));
            GL.CompileShader(fragmentShader);

            var program = GL.CreateProgram();
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);

            GL.DetachShader(program, vertexShader);
            GL.DetachShader(program, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            return program;
        }
    }
}

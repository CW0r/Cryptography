using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Diagnostics;
using WeGUI.Shaders;

namespace WeGUI
{
    public class Window : GameWindow
    {
        /*   0 ---- 1
         *   |     /|
         *   |    / |
         *   |   /  |
         *   |  /   |
         *   | /    |
         *   |/     |
         *   2 ---- 3
         *   
         *   2 ---- 3
         *   |     /|
         *   |    / |
         *   |   /  |
         *   |  /   |
         *   | /    |
         *   |/     |
         *   4 ---- 5
         */
        private readonly float[] _verticesRectangleUpper =
        {
            -0.6f,  0.6f, 0.0f,  0.65f, 0.65f, 0.65f, // Top Left     - 0  - Red
             0.6f,  0.6f, 0.0f,  0.65f, 0.65f, 0.65f, // Top right    - 1  - Red
            -0.6f, -0.1f, 0.0f,  0.65f, 0.65f, 0.65f, // Middle left  - 2  - Green
             0.6f, -0.1f, 0.0f,  0.65f, 0.65f, 0.65f, // Middle right - 3  - Green
        };

        private readonly float[] _verticesRectangleLower =
        {
            -0.6f, -0.1f, 0.0f,  0.4f, 0.4f, 0.4f, // Middle left  - 2  - Green
             0.6f, -0.1f, 0.0f,  0.4f, 0.4f, 0.4f, // Middle right - 3  - Green
            -0.6f, -0.6f, 0.0f,  0.4f, 0.4f, 0.4f, // Bottom left  - 4  - Blue
             0.6f, -0.6f, 0.0f,  0.4f, 0.4f, 0.4f  // Bottom right - 5  - Blue
        };

        private float[] _verticesTriangle =
        {
             0.0f,  0.4f, 0.0f, // Top centre
            -0.4f, -0.4f, 0.0f, // Bottom left
             0.4f, -0.4f, 0.0f  // Bottom right
        };

        private readonly uint[] _indicesRectangleUpper =
        {
            0, 1, 2,
            1, 2, 3
        };

        private readonly uint[] _indicesRectangleLower =
        {
            0, 1, 2,
            1, 2, 3
        };

        private int _vertexBufferObjectRectangleUpper;
        private int _vertexBufferObjectRectangleLower;
        private int _vertexBufferObjectTriangle;

        private int _vertexArrayObjectRectangleUpper;
        private int _vertexArrayObjectRectangleLower;
        private int _vertexArrayObjectTriangle;

        private Shader _shaderRectangle;
        private Shader _shaderTriangle;

        private int _elementBufferObjectRectangleUpper;
        private int _elementBufferObjectRectangleLower;

        private Stopwatch _stopwatch;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {            
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            // White background
            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);

            // -----------------------------------------------------RECTANGLES---------------------------------------------------------------------
            // --------------------------------------------------------UPPER------------------------------------------------------------------------
            _vertexBufferObjectRectangleUpper = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObjectRectangleUpper);

            GL.BufferData(BufferTarget.ArrayBuffer, _verticesRectangleUpper.Length * sizeof(float), _verticesRectangleUpper, BufferUsageHint.StaticDraw);

            _vertexArrayObjectRectangleUpper = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObjectRectangleUpper);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);  

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.GetInteger(GetPName.MaxVertexAttribs, out int maxAttributeCount);
            Debug.WriteLine($"Maximum numbers of vertex attributes supported: {maxAttributeCount}");

            _elementBufferObjectRectangleUpper = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObjectRectangleUpper);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indicesRectangleUpper.Length * sizeof(uint), _indicesRectangleUpper, BufferUsageHint.StaticDraw);
            // ------------------------------------------------------------------------------------------------------------------------------------

            // --------------------------------------------------------LOWER------------------------------------------------------------------------
            _vertexBufferObjectRectangleLower = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObjectRectangleLower);

            GL.BufferData(BufferTarget.ArrayBuffer, _verticesRectangleLower.Length * sizeof(float), _verticesRectangleLower, BufferUsageHint.StaticDraw);

            _vertexArrayObjectRectangleLower = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObjectRectangleLower);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 6, 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, sizeof(float) * 6, sizeof(float) * 3);
            GL.EnableVertexAttribArray(1);

            _elementBufferObjectRectangleLower = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObjectRectangleLower);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indicesRectangleLower.Length * sizeof(uint), _indicesRectangleLower, BufferUsageHint.StaticDraw);

            // ------------------------------------------------------------------------------------------------------------------------------------

            // ------------------------------------------------------TRIANGLE----------------------------------------------------------------------
            _vertexBufferObjectTriangle = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObjectTriangle);

            GL.BufferData(BufferTarget.ArrayBuffer, _verticesTriangle.Length * sizeof(float), _verticesTriangle, BufferUsageHint.StaticDraw);

            _vertexArrayObjectTriangle = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObjectTriangle);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            // ------------------------------------------------------------------------------------------------------------------------------------

            _shaderRectangle = new Shader("Shaders/shaderRect.vert", "Shaders/shaderRect.frag");
            _shaderTriangle = new Shader("Shaders/shaderTri.vert", "Shaders/shaderTri.frag");

            _shaderRectangle.Use();
            _shaderTriangle.Use();

            _stopwatch = new Stopwatch();

            _stopwatch.Start();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            _shaderRectangle.Use();
            Debug.WriteLine("Rectangle");

            // Rectangle
            GL.BindVertexArray(_vertexArrayObjectRectangleUpper);

            GL.DrawElements(PrimitiveType.Triangles, _indicesRectangleUpper.Length, DrawElementsType.UnsignedInt, 0);
            Debug.WriteLine("Rectangle 2");

            GL.BindVertexArray(_vertexArrayObjectRectangleLower);
            GL.DrawElements(PrimitiveType.Triangles, _indicesRectangleLower.Length, DrawElementsType.UnsignedInt, 0);

            // Triangle

            _shaderTriangle.Use();
            Debug.WriteLine("Triangle");
            double timeValue = _stopwatch.Elapsed.TotalSeconds;
            float redValue = (float)Math.Sin(timeValue) / 2.0f + 0.5f;
            float greenValue = (float)Math.Sin(timeValue) / 2.0f + 0.5f;
            float blueValue = (float)Math.Sin(timeValue) / 2.0f + 0.5f;
            Debug.WriteLine(redValue);
            Debug.WriteLine(greenValue);
            Debug.WriteLine(blueValue);
            int vertexColorLocation = GL.GetUniformLocation(_shaderTriangle.Handle, "colorIn");
            GL.Uniform4(vertexColorLocation, redValue, greenValue, blueValue, 0.25f);

            GL.BindVertexArray(_vertexArrayObjectTriangle);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            _shaderRectangle.Dispose();
            _shaderTriangle.Dispose();
        }
    }
}

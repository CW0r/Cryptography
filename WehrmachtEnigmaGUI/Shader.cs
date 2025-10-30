using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace WeGUI.Shaders
{
    public class Shader
    {
        // Program reference - Full Scope
        public readonly int Handle;

        // Uniform key/locations - Full Scope
        private readonly Dictionary<string, int> _uniformLocations;

        private bool disposed = false;

        public Shader(string vertPath, string fragPath)
        {
            // Vertex Shader intialisation - Constructor Scope
            string? shaderSource = File.ReadAllText(vertPath);
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);

            // Shader source linking
            GL.ShaderSource(vertexShader, shaderSource);

            // Shader compilation
            CompileShader(vertexShader);

            // Fragment Shader initialisation - Constructor Scope
            shaderSource = File.ReadAllText(fragPath);
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

            // Shader source linking
            GL.ShaderSource(fragmentShader, shaderSource);

            // Shader compilation
            CompileShader(fragmentShader);

            // Program creation and reference linking
            Handle = GL.CreateProgram();

            // Attaching shaders to created program
            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);

            // Program linking for shader use
            LinkProgram(Handle);

            // Shader memory cleanup
            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);

            // Getting number of shader uniforms in use
            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out int numberOfUniforms);

            _uniformLocations = new Dictionary<string, int>();

            // Add existing uniforms to a dictionary of key and location
            for (int i = 0; i < numberOfUniforms; i++)
            {
                string? key = GL.GetActiveUniform(Handle, i, out _, out _);
                int location = GL.GetUniformLocation(Handle, key);

                _uniformLocations.Add(key, location);
            }
        }

        private static void CompileShader(int shader)
        {
            // Shader compiling
            GL.CompileShader(shader);

            // Shader compile status check and error output
            GL.GetShader(shader, ShaderParameter.CompileStatus, out int status);
            if (status != (int)All.True)
            {
                string? infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }
        }

        private static void LinkProgram(int program)
        {
            // Program linking
            GL.LinkProgram(program);

            // Program link status check and error output
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int status);
            if (status != (int)All.True)
            {
                throw new Exception($"Error occured whilst linking Program({program})");
            }
        }

        public void Use()
        {
            // Program use command
            GL.UseProgram(Handle);
            Debug.WriteLine("Shader used");
        }

        public int GetAttribLocation(string attribName)
        {
            // Get attribute location from program
            return GL.GetAttribLocation(Handle, attribName);
        }

        // Uniform value manipulation
        public void SetInt(string name, int data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(_uniformLocations[name], data);
        }

        public void SetFloat(string name, float data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(_uniformLocations[name], data);
        }

        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(Handle);
            GL.UniformMatrix4(_uniformLocations[name], true, ref data);
        }

        public void SetVector3(string name, Vector3 data)
        {
            GL.UseProgram(Handle);
            GL.Uniform3(_uniformLocations[name], data);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                GL.DeleteProgram(Handle);

                disposed = true;
            }
        }

        ~Shader()
        {
            if (disposed == false)
            {
                Console.WriteLine("GPU Memory Leak Detected");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ModChart.Wall
{

    //good color
    public class Color
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
        public float A { get; set; }
        public bool Equals(Color color, float tolerance)
        {
            if (color == null) return false;
            if (Math.Abs(color.R - R) > tolerance) return false;
            if (Math.Abs(color.G - G) > tolerance) return false;
            if (Math.Abs(color.B - B) > tolerance) return false;
            if (Math.Abs(color.A - A) > tolerance) return false;
            return true;
        }
        public object[] ToObjArray()
        {
            return new object[] { R, G, B, A };
        }
        public object[] ToObjArray(float alpha)
        {
            return new object[] { R, G, B, alpha };
        }
        public Color Clone()
        {
            return new Color()
            {
                R = R,
                G = G,
                B = B,
                A = A
            };
        }
        public static Color ColorFromObjArray(object[] array)
        {
            return new Color() { R = array[0].toFloat(), G = array[1].toFloat(), B = array[2].toFloat(), A = array[3].toFloat() };
        }
        public bool isBlackOrEmpty(float tolerance)
        {
            if (R + B + G < tolerance) return true;
            // if (A == 0f) return true;
            return false;
        }
        public override string ToString()
        {
            return $"{R} {G} {B} {A}";
        }
    }
    /// <summary>
    /// This class is made specifically for a beatsaber wall, 
    /// technically it can be used generically
    /// </summary>
    public class Cube
    {
        /// <summary>
        /// the name of the blender object
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// An array of all the frames of this cube,
        /// Nulled if there is no animation.
        /// Properties in this array can also be nulled if they are not animated.
        /// </summary>
        public Frame[] Frames { get; set; }

        /// <summary>
        /// The static decomposed transformation of this cube
        /// </summary>
        public Transformation Transformation { get; set; }

        /// <summary>
        /// The static matrix transformation of this cube
        /// </summary>
        public Matrix4x4? Matrix { get; set; }

        /// <summary>
        /// who left the camera in the scene *BARF*
        /// </summary>
        public bool isCamera { get; set; }

        /// <summary>
        /// shpere
        /// </summary>
        public bool isBomb { get; set; }

        public string Track { get; set; }


        /// <summary>
        /// The span of this cubes lifetime,
        /// Set by `hide viewport` in blender.
        /// </summary>
        public DoubleInt FrameSpan { get; set; }

        /// <summary>
        /// The static color
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// override alpha value
        /// from material IOR
        /// </summary>
        public float? IOR { get; set; }

        /// <summary>
        /// How many total frames there are
        /// </summary>
        public int? Count { get; set; }

        /// <summary>
        /// bro wtf
        /// </summary>
        public bool isNote { get; set; }

        /// <summary>
        /// the name of the materials that its on
        /// </summary>
        public string[] Material { get; set; }
        public class Frame
        {
            public int Number { get; set; }
            public Transformation Transformation { get; set; }
            public Matrix4x4? Matrix { get; set; }
            public float? Dissolve { get; set; }
            public Color Color { get; set; }

            /// <summary>
            /// Indicates if on this frame this cube is active in heirarchy.
            /// Used to create multiple cubes by the collada parser.
            /// </summary>
            public bool? Active { get; set; }
            public Frame Clone()
            {
                var newFrame = new Frame()
                {
                    Number = Number,
                    Matrix = Matrix,
                    Dissolve = Dissolve,
                    Active = Active
                };
                if (Transformation != null) newFrame.Transformation = Transformation.Clone();
                if (Color != null) newFrame.Color = Color.Clone();
                return newFrame;
            }

        }
        public void Decompose()
        {
            if ((Matrix.HasValue))
            {
                Transformation = Transformation.fromMatrix(Matrix.Value);
            }
            if (Frames != null && Frames.All(f => f.Matrix.HasValue))
            {
                Frames = Frames.Select(frame =>
                {
                    frame.Transformation = Transformation.fromMatrix(frame.Matrix.Value);
                    return frame;
                }).ToArray();
                Transformation = Frames.First().Transformation;
                Matrix = Frames.First().Matrix;
            }
        }
        public Cube[] InstantiateMultiples()
        {
            if (Frames != null && Frames.Any(f => f.Active != Frames.First().Active))
            {
                List<DoubleInt> framespan = new List<DoubleInt>();
                DoubleInt current = null;
                bool? lastactive = null;
                for (int i = 0; i < Frames.Length; i++)
                {
                    if (lastactive.HasValue && current != null && lastactive.Value == Frames[i].Active.Value)
                    {
                        current.Val2++;
                    }
                    else
                    {
                        if (current != null && lastactive == false) framespan.Add(current);
                        current = new DoubleInt(i, i + 1);
                    }
                    lastactive = Frames[i].Active.Value;
                }
                //Console.WriteLine();

                return framespan.Select(f =>
                {
                    var Newcube = this.Clone();
                    Newcube.FrameSpan = f;
                    Newcube.Frames = Newcube.Frames.Slice(f.Val1, f.Val2);
                    return Newcube;
                }).ToArray();
            }
            return new Cube[] { this };
        }
        public Cube Clone()
        {
            var newCube = new Cube()
            {
                Count = Count,
                IOR = IOR,
                isBomb = isBomb,
                isNote = isNote,
                Track = Track,
                Name = Name,
                isCamera = isCamera,
                Matrix = Matrix
            };
            if (Transformation != null) newCube.Transformation = Transformation.Clone();
            if (Frames != null && Frames.Any()) newCube.Frames = Frames.Select(f => f.Clone()).ToArray();
            if (Color != null) newCube.Color = Color.Clone();
            if (FrameSpan != null) newCube.FrameSpan = FrameSpan.Clone();
            if (Material != null) newCube.Material = (string[])Material.Clone();

            return newCube;
        }

        public static IEnumerable<Cube> TransformCollection(IEnumerable<Cube> cubes, Vector3 Position, Vector3 Rotation, float Scale)
        {
            var newCubes = cubes.Select(c => c);

            Transformation boundingbox = newCubes.Select(n => n.Matrix.Value).ToArray().GetBoundingBox(new Vector3(1,1,1)).Main;

            
            //center
            newCubes = newCubes.Select(c =>
            {
                var mat = c.Matrix.Value;
                mat.Translation = mat.Translation - boundingbox.Position;
                c.Matrix = mat;
                return c;
            });
            
            
            //scale
            newCubes = newCubes.Select(cube =>
            {
                cube.Matrix = Matrix4x4.Multiply(cube.Matrix.Value,Matrix4x4.CreateScale(Scale));
                return cube;
            });
            
            
            //rotate
            newCubes = newCubes.Select(c =>
            {
                c.Matrix = Matrix4x4.Transform(c.Matrix.Value, Rotation.ToQuaternion());
                return c;
            });
            
            
            //translate
            newCubes = newCubes.Select(c =>
            {
                var mat = c.Matrix.Value;
                mat.Translation = mat.Translation + boundingbox.Position + Position;
                c.Matrix = mat;
                return c;
            });

            //decompose
            foreach (var c in newCubes) c.Decompose();

            return newCubes;
        }
    }


}

using System;
using UnityEngine;

namespace Utils
{
    public static class DMath
    {
        public static int Mod(int n, int m) 
        {
            int r = n%m;
            return r <0 ? r +m : r;
        }
        
        public static float Mod(float n, float m) 
        {
            float r = n%m;
            return r <0 ? r +m : r;
        }
        
        public static bool IsDivisible(float n, float m, float p = 0.01f) 
        {
            float r = n%m;
            return Math.Abs(r) < p || Math.Abs(r-m) < p || Math.Abs(r+m) < p;
        }
        
        public static Vector2 RotateVector(Vector2 vector, float rotationAngle)
        {
            return Quaternion.Euler(Vector3.forward * rotationAngle) * vector;
        }

        public static Vector3 RotateVector(Vector3 vector, float rotationAngle, Vector3 axis)
        {
            return Quaternion.Euler(axis.normalized * rotationAngle) * vector;
        }

        public static Bounds GetBounds(GameObject target, bool superAccurate = true)
        {
            Bounds bounds;
            Renderer renderer = GetRenderer(target);
            bool hasRenderer = (renderer != null);
            if (hasRenderer)
            {
                if (superAccurate && renderer is MeshRenderer)
                {
                    bounds = GetMeshBounds((MeshRenderer)renderer);
                }
                else
                {
                    bounds = renderer.bounds;
                }
            }
            else // Don't need this. But compiler is stupid.
            {
                bounds = new Bounds(target.transform.position, Vector3.zero);
            }

            int childCount = target.transform.childCount;
            for (int i = 0; i < childCount; ++i)
            {
                Transform child = target.transform.GetChild(i);
                if (!child.gameObject.activeSelf)
                {
                    continue;
                }
                Bounds childBounds = GetBounds(child.gameObject, superAccurate);
                if (childBounds.size.magnitude > 0f)
                {
                    if (hasRenderer)
                    {
                        bounds.Encapsulate(childBounds);
                    }
                    else
                    {
                        bounds = childBounds;
                        hasRenderer = true;
                    }
                }
            }

            return bounds;
        }

        private static Renderer GetRenderer(GameObject target)
        {
            Renderer renderer = target.GetComponent<Renderer>();
            if (renderer is ParticleSystemRenderer)
            {
                return null;
            }
            return renderer;
        }

        private static Bounds GetMeshBounds(MeshRenderer renderer)
        {
            Mesh mesh = renderer.GetComponent<MeshFilter>().sharedMesh;
            if (mesh == null)
            {
                return renderer.bounds;
            }
            var vertices = mesh.vertices;
            if (vertices.Length <= 0)
            {
                return renderer.bounds;
            }

            var min = renderer.transform.TransformPoint(vertices[0]);
            var max = min;

            for (var i = 1; i < vertices.Length; i++)
            {
                var V = renderer.transform.TransformPoint(vertices[i]);

                for (var n = 0; n < 3; n++)
                {
                    max[n] = Mathf.Max(V[n], max[n]);
                    min[n] = Mathf.Min(V[n], min[n]);
                }
            }

            var bounds = new Bounds();
            bounds.SetMinMax(min, max);

            return bounds;
        }

        public static Vector3 GetFitScale(GameObject source, GameObject targetBoundsObject)
        {
            return source.transform.localScale * GetFitScaleMultiplier(GetBounds(source), GetBounds(targetBoundsObject));
        }

        public static float GetFitScaleMultiplier(Bounds source, Bounds target)
        {
            float targetSize = Mathf.Min(target.size.x, target.size.y, target.size.z);

            float sourceSize = Mathf.Max(source.size.x, source.size.y, source.size.z);
            if (sourceSize == 0f) // Safeguard against scaling to infinity
            {
                sourceSize = 1f;
            }
            return targetSize / sourceSize;
        }

        public static Vector3 GetCenteredPosition(GameObject source, Vector3 targetPosition)
        {
            return source.transform.position + (targetPosition - GetBounds(source).center);
        }

        public static Vector2 GetPerspectiveViewSize(Camera camera, float depth)
        {
            float halfFieldOfView = camera.fieldOfView* 0.5f* Mathf.Deg2Rad;
            float halfHeightAtDepth = depth * Mathf.Tan(halfFieldOfView);
            float halfWidthAtDepth = camera.aspect * halfHeightAtDepth;
            return new Vector2(halfWidthAtDepth *2, halfHeightAtDepth*2);
        }

        public static Vector2 WorldToUi(Vector3 worldPosition, Camera worldCamera, Camera uiCamera)
        {
            Vector2 viewportPosition = worldCamera.WorldToViewportPoint(worldPosition);
            Vector2 result = new Vector2(
                uiCamera.transform.position.x + (viewportPosition.x - 0.5f) * uiCamera.orthographicSize * 2f * uiCamera.aspect,
                uiCamera.transform.position.y + (viewportPosition.y - 0.5f) * uiCamera.orthographicSize * 2f);

            return result;
        }

        public static bool Equals(float a, float b, float threshold = 0.001f)
        {
            return Mathf.Abs(a - b) < threshold;
        }
        
        public static float AddVariation(float source, float variationFactor, VariationSign sign = VariationSign.BOTH)
        {
            if (sign == VariationSign.BOTH)
            {
                return source * (1f + (UnityEngine.Random.value - 0.5f) * variationFactor);
            }
            else if (sign == VariationSign.POSITIVE)
            {
                return source * (1f + UnityEngine.Random.value * variationFactor);
            }
            else
            {
                return source * (1f - UnityEngine.Random.value * variationFactor);
            }
        }

        public enum VariationSign
        {
            BOTH,
            POSITIVE,
            NEGATIVE
        }
    }
}

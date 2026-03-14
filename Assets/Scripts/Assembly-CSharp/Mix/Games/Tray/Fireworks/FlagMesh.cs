using System;
using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class FlagMesh : MonoBehaviour
	{
		private class Vert
		{
			public Vector3 originalPosition;

			public Vector2 positionInterp;
		}

		private Mesh mesh;

		public MeshFilter meshFilter;

		private Vector3[] originalVerts;

		private Vector3[] cacheVerts;

		private Vert[] adjVerts;

		private Vector3 sizeDelta = Vector3.one;

		private Mesh bMesh;

		public MeshFilter bMeshFilter;

		private float animT;

		public float minSpeed = 0.66f;

		public float speed = 1f;

		public float speedVarianceStrength = 1f;

		public float timeStrength = 5f;

		public AnimationCurve polePowerCurve;

		public float gravityStrength = 1f;

		public AnimationCurve gravityCurve;

		public float heightTimeStrength = 0.1f;

		public AnimationCurve heightTimeCurve;

		public float lengthFactor = 1.33f;

		public Vector3 offsetFactor = Vector3.one;

		public int xInterpDirection = 1;

		public int yInterpDirection = 1;

		public AnimationCurve xInterpCurve;

		public AnimationCurve yInterpCurve;

		private Vector3[] cachedNormals;

		private void Start()
		{
			mesh = meshFilter.mesh;
			bMesh = new Mesh();
			bMeshFilter.mesh = bMesh;
			bMesh.vertices = mesh.vertices;
			bMesh.uv = mesh.uv;
			int[] triangles = mesh.triangles;
			int[] array = new int[triangles.Length];
			for (int i = 0; i < triangles.Length; i++)
			{
				array[i] = triangles[triangles.Length - 1 - i];
			}
			bMesh.triangles = array;
			bMeshFilter.transform.position = meshFilter.transform.position;
			bMeshFilter.transform.rotation = meshFilter.transform.rotation;
			bMeshFilter.transform.localScale = meshFilter.transform.localScale;
			bMeshFilter.GetComponent<Renderer>().material = meshFilter.GetComponent<Renderer>().material;
			originalVerts = mesh.vertices;
			Vector3 center = meshFilter.GetComponent<Renderer>().bounds.center;
			Vector3 direction = meshFilter.transform.parent.TransformDirection(meshFilter.GetComponent<Renderer>().bounds.size);
			Vector3 vector = meshFilter.transform.parent.TransformDirection(direction);
			Vector3 position = center + vector * 0.5f;
			Vector3 position2 = center - vector * 0.5f;
			Vector3 vector2 = meshFilter.transform.InverseTransformPoint(position);
			Vector3 vector3 = meshFilter.transform.InverseTransformPoint(position2);
			sizeDelta = vector2 - vector3;
			cacheVerts = new Vector3[originalVerts.Length];
			adjVerts = new Vert[originalVerts.Length];
			for (int j = 0; j < adjVerts.Length; j++)
			{
				Vert vert = new Vert();
				vert.originalPosition = originalVerts[j];
				Vector3 vector4 = originalVerts[j] - vector3;
				Vector2 positionInterp = new Vector2(Mathf.Clamp(vector4.x / sizeDelta.x, 0f, 1f), Mathf.Clamp(vector4.z / sizeDelta.z, 0f, 1f));
				vert.positionInterp = positionInterp;
				if (xInterpDirection < 0)
				{
					vert.positionInterp.x = 1f - vert.positionInterp.x;
				}
				if (yInterpDirection < 0)
				{
					vert.positionInterp.y = 1f - vert.positionInterp.y;
				}
				if (xInterpCurve.length > 0)
				{
					vert.positionInterp.x = xInterpCurve.Evaluate(vert.positionInterp.x);
				}
				if (yInterpCurve.length > 0)
				{
					vert.positionInterp.y = yInterpCurve.Evaluate(vert.positionInterp.y);
				}
				adjVerts[j] = vert;
			}
			animT = UnityEngine.Random.Range(0f, (float)Math.PI * 2f);
			RegenMesh();
		}

		private void Update()
		{
			float f = Mathf.Lerp(minSpeed, speed, (Mathf.Sin(Time.time * speedVarianceStrength) + 1f) / 2f);
			animT += Time.deltaTime * Mathf.Abs(f);
			while (animT >= (float)Math.PI * 2f)
			{
				animT -= (float)Math.PI * 2f;
			}
			for (int i = 0; i < adjVerts.Length; i++)
			{
				Vector3 zero = Vector3.zero;
				float num = adjVerts[i].positionInterp.x;
				if (polePowerCurve.length > 0)
				{
					num = polePowerCurve.Evaluate(num);
				}
				float num2 = animT * Mathf.Sign(speed);
				float num3 = 1f + (timeStrength - 1f);
				num2 += (float)Math.PI * num3 * adjVerts[i].positionInterp.x;
				float num4 = adjVerts[i].positionInterp.y;
				if (heightTimeCurve.length > 0)
				{
					num4 = heightTimeCurve.Evaluate(num4);
				}
				num2 += num4 * heightTimeStrength;
				zero.y += Mathf.Sin(num2) * num;
				zero.z += Mathf.Sin(num2) * num;
				Vector3 a = zero;
				a *= Mathf.Lerp(1f, lengthFactor, adjVerts[i].positionInterp.x);
				a = Vector3.Scale(a, offsetFactor);
				float num5 = adjVerts[i].positionInterp.x;
				if (gravityCurve.length > 0)
				{
					num5 = gravityCurve.Evaluate(num5);
				}
				a.z += num5 * gravityStrength;
				cacheVerts[i] = adjVerts[i].originalPosition + a;
			}
			RegenMesh();
		}

		private void RegenMesh()
		{
			mesh.vertices = cacheVerts;
			mesh.RecalculateNormals();
			bMesh.vertices = mesh.vertices;
			if (cachedNormals != null)
			{
				cachedNormals = new Vector3[mesh.normals.Length];
			}
			cachedNormals = mesh.normals;
			for (int i = 0; i < cachedNormals.Length; i++)
			{
				cachedNormals[i] *= -1f;
			}
			bMesh.normals = cachedNormals;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beats : MonoBehaviour
{
    private Vector3[] cleanVerts;
    float scale;
    private Mesh mesh;
    float waveSpeed = 1f;
    float waveHeight = 2f;
    int direction = 0;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        cleanVerts = mesh.vertices;

    }

    private void FixedUpdate()
    {
        direction = Random.Range(0, 2);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3[] vertices = mesh.vertices;
        scale = AudioSpectrum._frequencyData;

        for (int i = 0; i < vertices.Length; i++)
        {
            if (direction == 0)
            {
                float x = (vertices[i].x * scale) + (Time.time * waveSpeed);
                float z = (vertices[i].z * scale) + (Time.time * waveSpeed);
                vertices[i].y = cleanVerts[i].y + (Mathf.PerlinNoise(x, z) - 0.5f) * waveHeight;
            }
            if (direction == 1)
            {
                float y = (vertices[i].y * scale) + (Time.time * waveSpeed);
                float z = (vertices[i].z * scale) + (Time.time * waveSpeed);
                vertices[i].x = cleanVerts[i].x + (Mathf.PerlinNoise(y, z) - 0.5f) * waveHeight;
            }
            if (direction == 2)
            {
                float x = (vertices[i].x * scale) + (Time.time * waveSpeed);
                float y = (vertices[i].y * scale) + (Time.time * waveSpeed);
                vertices[i].z = cleanVerts[i].z + (Mathf.PerlinNoise(x, y) - 0.5f) * waveHeight;
            }
        }

        mesh.vertices = vertices;
    }
}
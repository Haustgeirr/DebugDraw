using UnityEngine;
using System.Collections;

public static class DebugDraw
{
    public static void Point2D(Vector2 point, float size, Color colour, float duration = 0f, bool depthTest = true)
    {
        Vector2 up = Vector2.up * size;
        Vector2 right = Vector2.right * size;

        Debug.DrawLine(point - up, point + up, colour, duration, depthTest);
        Debug.DrawLine(point - right, point + right, colour, duration, depthTest);
    }

    public static void Arrow2D(Vector2 point, Vector2 direction, float scale, Color colour, bool arrowTowardsPoint = true, float duration = 0f, bool depthTest = true)
    {
        float angle = 15.0f;

        Vector2 forward = (direction - point).normalized;
        Vector2 right = new Vector2(forward.y, -forward.x).normalized;
        Vector2 endPoint = forward * scale + point;

        Debug.DrawLine(point, endPoint, colour, duration, depthTest);

        if (arrowTowardsPoint)
        {
            Debug.DrawLine(point, Vector3.Slerp(forward, right, angle / 90.0f) + (Vector3)point, colour, duration, depthTest);
            Debug.DrawLine(point, Vector3.Slerp(forward, -right, angle / 90.0f) + (Vector3)point, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(forward, right, angle / 90.0f) + (Vector3)point, Vector3.Slerp(forward, -right, angle / 90.0f) + (Vector3)point, colour, duration, depthTest);
        }
        else
        {
            Debug.DrawLine(endPoint, Vector3.Slerp(-forward, right, angle / 90.0f) + (Vector3)endPoint, colour, duration, depthTest);
            Debug.DrawLine(endPoint, Vector3.Slerp(-forward, -right, angle / 90.0f) + (Vector3)endPoint, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(-forward, right, angle / 90.0f) + (Vector3)endPoint, Vector3.Slerp(-forward, -right, angle / 90.0f) + (Vector3)endPoint, colour, duration, depthTest);
        }

    }

    public static void Circle2D(Vector2 point, float radius, Color colour, float duration = 0f, bool depthTest = true)
    {
        Vector2 up = point.normalized * radius;
        Vector2 right = new Vector2(up.y, -up.x);

        for (int i = 0; i < 26; i++)
        {
            Debug.DrawLine(Vector3.Slerp(-right, up, i / 25.0f) + (Vector3)point, Vector3.Slerp(-right, up, (i - 1) / 25.0f) + (Vector3)point, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(up, right, i / 25.0f) + (Vector3)point, Vector3.Slerp(up, right, (i - 1) / 25.0f) + (Vector3)point, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(right, -up, i / 25.0f) + (Vector3)point, Vector3.Slerp(right, -up, (i - 1) / 25.0f) + (Vector3)point, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(-up, -right, i / 25.0f) + (Vector3)point, Vector3.Slerp(-up, -right, (i - 1) / 25.0f) + (Vector3)point, colour, duration, depthTest);
        }
    }

    public static void Capsule2D(Vector2 start, Vector2 end, float radius, Color colour, float duration = 0f, bool depthTest = true)
    {
        Vector2 up = (end - start).normalized * radius;
        Vector2 right = new Vector2(up.y, -up.x);

        // Draw End caps
        for (int i = 1; i < 26; i++)
        {
            Debug.DrawLine(Vector3.Slerp(right, -up, i / 25.0f) + (Vector3)start, Vector3.Slerp(right, -up, (i - 1) / 25.0f) + (Vector3)start, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(-right, -up, i / 25.0f) + (Vector3)start, Vector3.Slerp(-right, -up, (i - 1) / 25.0f) + (Vector3)start, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(right, up, i / 25.0f) + (Vector3)end, Vector3.Slerp(right, up, (i - 1) / 25.0f) + (Vector3)end, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(-right, up, i / 25.0f) + (Vector3)end, Vector3.Slerp(-right, up, (i - 1) / 25.0f) + (Vector3)end, colour, duration, depthTest);
        }

        // Draw Side Lines
        Debug.DrawLine(start + right, end + right, colour, duration, depthTest);
        Debug.DrawLine(start - right, end - right, colour, duration, depthTest);
    }

    public static void Point(Vector3 point, float size, Color colour, float duration = 0f, bool depthTest = true)
    {
        Vector3 up = Vector3.up * size;
        Vector3 right = Vector3.right * size;
        Vector3 forward = Vector3.forward * size;

        Debug.DrawLine(point - up, point + up, colour, duration, depthTest);
        Debug.DrawLine(point - right, point + right, colour, duration, depthTest);
        Debug.DrawLine(point - forward, point + forward, colour, duration, depthTest);
    }

    public static void Arrow(Vector3 point, Vector3 direction, float scale, Color colour, bool arrowTowardsPoint = true, float duration = 0f, bool depthTest = true)
    {
        float angle = 15.0f;

        scale = Mathf.Clamp(scale, 1f, Mathf.Infinity);
        float arrowScale = Mathf.Clamp(scale * 0.2f, 0.1f, 1f);

        Vector3 forward = direction.normalized;
        Vector3 right = Vector3.Slerp(-forward, forward, 0.5f);
        Vector3 up = Vector3.Cross(right, forward);

        if (arrowTowardsPoint)
        {
            Vector3 centre = Vector3.Lerp(Vector3.Slerp(forward, right, angle / 90.0f), Vector3.Slerp(forward, -right, angle / 90.0f), 0.5f) * arrowScale;
            float radius = Vector3.Distance(Vector3.Slerp(forward, right, angle / 90.0f) * arrowScale, centre);
            Vector3 offset = centre + point;

            // Main Line
            Debug.DrawLine(point, forward * scale + point, colour, duration, depthTest);

            // Arrow edges
            Debug.DrawLine(point, Vector3.Slerp(forward, right, angle / 90.0f) * arrowScale + point, colour, duration, depthTest);
            Debug.DrawLine(point, Vector3.Slerp(forward, -right, angle / 90.0f) * arrowScale + point, colour, duration, depthTest);
            Debug.DrawLine(point, Vector3.Slerp(forward, up, angle / 90.0f) * arrowScale + point, colour, duration, depthTest);
            Debug.DrawLine(point, Vector3.Slerp(forward, -up, angle / 90.0f) * arrowScale + point, colour, duration, depthTest);

            for (int i = 0; i < 26; i++)
            {
                Debug.DrawLine(Vector3.Slerp(-right, up, i / 25.0f) * radius + offset, Vector3.Slerp(-right, up, (i - 1) / 25.0f) * radius + offset, colour, duration, depthTest);
                Debug.DrawLine(Vector3.Slerp(up, right, i / 25.0f) * radius + offset, Vector3.Slerp(up, right, (i - 1) / 25.0f) * radius + offset, colour, duration, depthTest);
                Debug.DrawLine(Vector3.Slerp(right, -up, i / 25.0f) * radius + offset, Vector3.Slerp(right, -up, (i - 1) / 25.0f) * radius + offset, colour, duration, depthTest);
                Debug.DrawLine(Vector3.Slerp(-up, -right, i / 25.0f) * radius + offset, Vector3.Slerp(-up, -right, (i - 1) / 25.0f) * radius + offset, colour, duration, depthTest);
            }
        }
        else
        {
            Vector3 centre = Vector3.Lerp(Vector3.Slerp(forward, right, angle / 90.0f), Vector3.Slerp(forward, -right, angle / 90.0f), 0.5f) * arrowScale;
            float radius = Vector3.Distance(Vector3.Slerp(forward, right, angle / 90.0f) * arrowScale, centre);
            Vector3 endPoint = forward * scale + point;
            Vector3 offset = -centre + endPoint;

            // Main Line
            Debug.DrawLine(point, endPoint, colour, duration, depthTest);

            // Arrow edges
            Debug.DrawLine(endPoint, Vector3.Slerp(-forward, right, angle / 90.0f) * arrowScale + endPoint, colour, duration, depthTest);
            Debug.DrawLine(endPoint, Vector3.Slerp(-forward, -right, angle / 90.0f) * arrowScale + endPoint, colour, duration, depthTest);
            Debug.DrawLine(endPoint, Vector3.Slerp(-forward, up, angle / 90.0f) * arrowScale + endPoint, colour, duration, depthTest);
            Debug.DrawLine(endPoint, Vector3.Slerp(-forward, -up, angle / 90.0f) * arrowScale + endPoint, colour, duration, depthTest);

            for (int i = 0; i < 26; i++)
            {
                Debug.DrawLine(Vector3.Slerp(-right, up, i / 25.0f) * radius + offset, Vector3.Slerp(-right, up, (i - 1) / 25.0f) * radius + offset, colour, duration, depthTest);
                Debug.DrawLine(Vector3.Slerp(up, right, i / 25.0f) * radius + offset, Vector3.Slerp(up, right, (i - 1) / 25.0f) * radius + offset, colour, duration, depthTest);
                Debug.DrawLine(Vector3.Slerp(right, -up, i / 25.0f) * radius + offset, Vector3.Slerp(right, -up, (i - 1) / 25.0f) * radius + offset, colour, duration, depthTest);
                Debug.DrawLine(Vector3.Slerp(-up, -right, i / 25.0f) * radius + offset, Vector3.Slerp(-up, -right, (i - 1) / 25.0f) * radius + offset, colour, duration, depthTest);
            }
        }
    }

    public static void Sphere(Vector3 point, float radius, Color colour, float duration = 0f, bool depthTest = true)
    {

        Vector3 up = Vector3.up * radius;
        Vector3 right = Vector3.Cross(up, Vector3.forward);
        Vector3 forward = Vector3.forward * radius;

        // Draw End caps
        for (int i = 1; i < 26; i++)
        {
            Debug.DrawLine(Vector3.Slerp(-right, up, i / 25.0f) + (Vector3)point, Vector3.Slerp(-right, up, (i - 1) / 25.0f) + (Vector3)point, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(up, right, i / 25.0f) + (Vector3)point, Vector3.Slerp(up, right, (i - 1) / 25.0f) + (Vector3)point, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(right, -up, i / 25.0f) + (Vector3)point, Vector3.Slerp(right, -up, (i - 1) / 25.0f) + (Vector3)point, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(-up, -right, i / 25.0f) + (Vector3)point, Vector3.Slerp(-up, -right, (i - 1) / 25.0f) + (Vector3)point, colour, duration, depthTest);

            Debug.DrawLine(Vector3.Slerp(forward, -up, i / 25.0f) + (Vector3)point, Vector3.Slerp(forward, -up, (i - 1) / 25.0f) + (Vector3)point, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(-forward, -up, i / 25.0f) + (Vector3)point, Vector3.Slerp(-forward, -up, (i - 1) / 25.0f) + (Vector3)point, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(forward, up, i / 25.0f) + (Vector3)point, Vector3.Slerp(forward, up, (i - 1) / 25.0f) + (Vector3)point, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(-forward, up, i / 25.0f) + (Vector3)point, Vector3.Slerp(-forward, up, (i - 1) / 25.0f) + (Vector3)point, colour, duration, depthTest);

            Debug.DrawLine(Vector3.Slerp(-right, forward, i / 25.0f) + (Vector3)point, Vector3.Slerp(-right, forward, (i - 1) / 25.0f) + (Vector3)point, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(forward, right, i / 25.0f) + (Vector3)point, Vector3.Slerp(forward, right, (i - 1) / 25.0f) + (Vector3)point, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(right, -forward, i / 25.0f) + (Vector3)point, Vector3.Slerp(right, -forward, (i - 1) / 25.0f) + (Vector3)point, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(-forward, -right, i / 25.0f) + (Vector3)point, Vector3.Slerp(-forward, -right, (i - 1) / 25.0f) + (Vector3)point, colour, duration, depthTest);
        }
    }

    public static void Capsule(Vector3 start, Vector3 end, float radius, Color colour, float duration = 0f, bool depthTest = true)
    {
        // If for some reason there is zero-distance between start/end point, draw a sphere instead
        if (start == end)
        {
            Sphere(start, radius, colour, duration, depthTest);
            return;
        }

        Vector3 up = (end - start).normalized * radius;
        Vector3 right = Vector3.Cross(up, Vector3.forward);
        Vector3 forward = Vector3.forward * radius;

        // Draw End caps
        for (int i = 1; i < 26; i++)
        {
            // Right End Caps
            Debug.DrawLine(Vector3.Slerp(right, -up, i / 25.0f) + (Vector3)start, Vector3.Slerp(right, -up, (i - 1) / 25.0f) + (Vector3)start, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(-right, -up, i / 25.0f) + (Vector3)start, Vector3.Slerp(-right, -up, (i - 1) / 25.0f) + (Vector3)start, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(right, up, i / 25.0f) + (Vector3)end, Vector3.Slerp(right, up, (i - 1) / 25.0f) + (Vector3)end, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(-right, up, i / 25.0f) + (Vector3)end, Vector3.Slerp(-right, up, (i - 1) / 25.0f) + (Vector3)end, colour, duration, depthTest);

            // Forward End Caps
            Debug.DrawLine(Vector3.Slerp(forward, -up, i / 25.0f) + (Vector3)start, Vector3.Slerp(forward, -up, (i - 1) / 25.0f) + (Vector3)start, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(-forward, -up, i / 25.0f) + (Vector3)start, Vector3.Slerp(-forward, -up, (i - 1) / 25.0f) + (Vector3)start, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(forward, up, i / 25.0f) + (Vector3)end, Vector3.Slerp(forward, up, (i - 1) / 25.0f) + (Vector3)end, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(-forward, up, i / 25.0f) + (Vector3)end, Vector3.Slerp(-forward, up, (i - 1) / 25.0f) + (Vector3)end, colour, duration, depthTest);

            // Start Point Circle
            Debug.DrawLine(Vector3.Slerp(-right, forward, i / 25.0f) + (Vector3)start, Vector3.Slerp(-right, forward, (i - 1) / 25.0f) + (Vector3)start, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(forward, right, i / 25.0f) + (Vector3)start, Vector3.Slerp(forward, right, (i - 1) / 25.0f) + (Vector3)start, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(right, -forward, i / 25.0f) + (Vector3)start, Vector3.Slerp(right, -forward, (i - 1) / 25.0f) + (Vector3)start, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(-forward, -right, i / 25.0f) + (Vector3)start, Vector3.Slerp(-forward, -right, (i - 1) / 25.0f) + (Vector3)start, colour, duration, depthTest);

            // End Point Circle
            Debug.DrawLine(Vector3.Slerp(-right, forward, i / 25.0f) + (Vector3)end, Vector3.Slerp(-right, forward, (i - 1) / 25.0f) + (Vector3)end, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(forward, right, i / 25.0f) + (Vector3)end, Vector3.Slerp(forward, right, (i - 1) / 25.0f) + (Vector3)end, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(right, -forward, i / 25.0f) + (Vector3)end, Vector3.Slerp(right, -forward, (i - 1) / 25.0f) + (Vector3)end, colour, duration, depthTest);
            Debug.DrawLine(Vector3.Slerp(-forward, -right, i / 25.0f) + (Vector3)end, Vector3.Slerp(-forward, -right, (i - 1) / 25.0f) + (Vector3)end, colour, duration, depthTest);
        }

        // Draw Side Lines
        Debug.DrawLine(start + right, end + right, colour, duration, depthTest);
        Debug.DrawLine(start - right, end - right, colour, duration, depthTest);
        Debug.DrawLine(start + forward, end + forward, colour, duration, depthTest);
        Debug.DrawLine(start - forward, end - forward, colour, duration, depthTest);
    }

    // Thanks to RPG Character Animation Pack FREE by Erik Ross http://roystanross.wordpress.com/
    public static void Marker(Vector3 position, float size, Color color, float duration = 0f, bool depthTest = true)
    {
        Vector3 line1PosA = position + Vector3.up * size * 0.5f;
        Vector3 line1PosB = position - Vector3.up * size * 0.5f;

        Vector3 line2PosA = position + Vector3.right * size * 0.5f;
        Vector3 line2PosB = position - Vector3.right * size * 0.5f;

        Vector3 line3PosA = position + Vector3.forward * size * 0.5f;
        Vector3 line3PosB = position - Vector3.forward * size * 0.5f;

        Debug.DrawLine(line1PosA, line1PosB, color, duration, depthTest);
        Debug.DrawLine(line2PosA, line2PosB, color, duration, depthTest);
        Debug.DrawLine(line3PosA, line3PosB, color, duration, depthTest);
    }

    // Thanks to RPG Character Animation Pack FREE by Erik Ross http://roystanross.wordpress.com/
    public static void Vector(Vector3 position, Vector3 direction, float raySize, float markerSize, Color color, float duration = 0f, bool depthTest = true)
    {
        Debug.DrawRay(position, direction * raySize, color, 0, false);
        DebugDraw.Marker(position + direction * raySize, markerSize, color, 0, false);
    }

    // Thanks to RPG Character Animation Pack FREE by Erik Ross http://roystanross.wordpress.com/
    // Courtesy of robertbu
    public static void Plane(Vector3 position, Vector3 normal, float size, Color color, float duration = 0f, bool depthTest = true)
    {
        Vector3 v3;

        if (normal.normalized != Vector3.forward)
            v3 = Vector3.Cross(normal, Vector3.forward).normalized * normal.magnitude;
        else
            v3 = Vector3.Cross(normal, Vector3.up).normalized * normal.magnitude; ;

        Vector3 corner0 = position + v3 * size;
        Vector3 corner2 = position - v3 * size;

        Quaternion q = Quaternion.AngleAxis(90.0f, normal);
        v3 = q * v3;
        Vector3 corner1 = position + v3 * size;
        Vector3 corner3 = position - v3 * size;

        Debug.DrawLine(corner0, corner2, color, duration, depthTest);
        Debug.DrawLine(corner1, corner3, color, duration, depthTest);
        Debug.DrawLine(corner0, corner1, color, duration, depthTest);
        Debug.DrawLine(corner1, corner2, color, duration, depthTest);
        Debug.DrawLine(corner2, corner3, color, duration, depthTest);
        Debug.DrawLine(corner3, corner0, color, duration, depthTest);
        Debug.DrawRay(position, normal * size, color, duration, depthTest);
    }

    // Thanks to RPG Character Animation Pack FREE by Erik Ross http://roystanross.wordpress.com/
    public static void Triangle(Vector3 a, Vector3 b, Vector3 c, Color color)
    {
        Debug.DrawLine(a, b, color);
        Debug.DrawLine(b, c, color);
        Debug.DrawLine(c, a, color);
    }

    // Thanks to RPG Character Animation Pack FREE by Erik Ross http://roystanross.wordpress.com/
    public static void Triangle(Vector3 a, Vector3 b, Vector3 c, Color color, Transform t)
    {
        a = t.TransformPoint(a);
        b = t.TransformPoint(b);
        c = t.TransformPoint(c);

        Debug.DrawLine(a, b, color);
        Debug.DrawLine(b, c, color);
        Debug.DrawLine(c, a, color);
    }

    // Thanks to RPG Character Animation Pack FREE by Erik Ross http://roystanross.wordpress.com/
    public static void Mesh(Mesh mesh, Color color, Transform t)
    {
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Triangle(mesh.vertices[mesh.triangles[i]], mesh.vertices[mesh.triangles[i + 1]], mesh.vertices[mesh.triangles[i + 2]], color, t);
        }
    }

    // Thanks to RPG Character Animation Pack FREE by Erik Ross http://roystanross.wordpress.com/
    public static Color RandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }

    public static Color RandomColor(float alpha)
    {
        return new Color(Random.value, Random.value, Random.value, alpha);
    }
}

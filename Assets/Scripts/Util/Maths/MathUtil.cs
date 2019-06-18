using UnityEngine;

public static class MathUtil
{
	// GNTODO: all the other overloads of this! :D
	public static uint Clamp01(int iValue)
	{
		if (iValue < 0) return 0;
		if (iValue > 1) return 1;
		return (uint)iValue;
	}


	public static uint Clamp01(uint iValue)
	{
		if (iValue < 0) return 0;
		if (iValue > 1) return 1;
		return (uint)iValue;
	}


	public static uint Clamp(int iValue, uint min, uint max)
	{
		if (iValue < min) return (uint)min;
		if (iValue > max) return (uint)max;
		return (uint)iValue;
	}


	public static uint Clamp(uint iValue, uint min, uint max)
	{
		if (iValue < min) return min;
		if (iValue > max) return max;
		return (uint)iValue;
	}


	public static ulong Clamp(ulong iValue, ulong min, ulong max)
	{
		if (iValue < min) return min;
		if (iValue > max) return max;
		return (ulong)iValue;
	}


	public static Vector3 GetCurvePoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
	{
		return Vector3.Lerp(Vector3.Lerp(p0, p1, t), Vector3.Lerp(p1, p2, t), t);
	}


	public static Vector3 GetBezierCurvePoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		float fT = Mathf.Clamp(t, 0.0f, 1.0f);
		float u = 1 - fT;
		float tt = fT * fT;
		float uu = u * u;
		float uuu = uu * u;
		float ttt = tt * t;

		Vector3 p = uuu * p0;
		p += 3 * uu * t * p1;
		p += 3 * u * tt * p2;
		p += ttt * p3;

		return p;
	}

}

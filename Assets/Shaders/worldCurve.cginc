#ifndef WORLD_CURVE_FUNC
#define WORLD_CURVE_FUNC

uniform float4 _CurveDirection = float4(0,0,0,0);
uniform float _CurveDistance = 50;

void curveWorld(inout float4 vertex)
{
	float4 vPos = mul(UNITY_MATRIX_MV, vertex);
	float zOff = vPos.z/_CurveDistance;
	vertex += _CurveDirection*zOff*zOff;
}

#endif
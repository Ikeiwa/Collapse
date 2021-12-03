#ifndef WORLD_CURVE_FUNC
#define WORLD_CURVE_FUNC

uniform float4 _CurveDirection = float4(0,0,0,0);
uniform float _CurveDistance = 50;

void curveWorld(inout float4 vertex)
{
	float4 wPos = mul(unity_ObjectToWorld, vertex);
	float zOff = (wPos.z-_WorldSpaceCameraPos.z)/_CurveDistance;
	wPos += _CurveDirection*zOff*zOff;
	vertex = mul(unity_WorldToObject, wPos);
}

#endif
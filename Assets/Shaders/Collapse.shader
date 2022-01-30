Shader "Unlit/Collapse"
{
    Properties
    {
        [NoScaleOffset] _Tex("Cubemap   (HDR)", Cube) = "grey" {}
    	_Progress("Progress",Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Overlay"}
        LOD 100
    	ZTest Off
    	ZWrite Off
    	
        GrabPass
        {
            "_BackgroundTexture"
        } 
    	
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float3 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = normalize(UnityWorldSpaceViewDir(worldPos));
                return o;
            }

            samplerCUBE _Tex;

            fixed4 frag(v2f i) : SV_Target
            {
                return texCUBE(_Tex, i.uv);
            }
            ENDCG
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            float _Progress;

            void RotateAboutAxis(float3 In, float3 Axis, float Rotation, out float3 Out)
            {
                float s = sin(Rotation);
                float c = cos(Rotation);
                float one_minus_c = 1.0 - c;

                Axis = normalize(Axis);
                float3x3 rot_mat =
                { one_minus_c * Axis.x * Axis.x + c, one_minus_c * Axis.x * Axis.y - Axis.z * s, one_minus_c * Axis.z * Axis.x + Axis.y * s,
                    one_minus_c * Axis.x * Axis.y + Axis.z * s, one_minus_c * Axis.y * Axis.y + c, one_minus_c * Axis.y * Axis.z - Axis.x * s,
                    one_minus_c * Axis.z * Axis.x - Axis.y * s, one_minus_c * Axis.y * Axis.z + Axis.x * s, one_minus_c * Axis.z * Axis.z + c
                };
                Out = mul(rot_mat, In);
            }

            float3 RotateAroundZXYInDegrees(float3 vertex, float3 degrees)
            {
                float3 sina, cosa;
                sincos(degrees, sina, cosa);
                // Create a rotation matrix around each axis ...
                float3x3 rx = float3x3 (
                    1.0, 0.0, 0.0,
                    0.0, cosa.x, -sina.x,
                    0.0, sina.x, cosa.x);
                float3x3 ry = float3x3 (
                    cosa.y, 0.0, sina.y,
                    0.0, 1.0, 0.0,
                    -sina.y, 0.0, cosa.y);
                float3x3 rz = float3x3 (
                    cosa.z, -sina.z, 0.0,
                    sina.z, cosa.z, 0.0,
                    0.0, 0.0, 1.0);
                // Composite rotation in order of Z axis, X axis, Y axis ...
                float3x3 m = mul(ry, mul(rx, rz));
                // apply rotation and return
                return mul(m, vertex);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.uv = ComputeGrabScreenPos(UnityObjectToClipPos(v.vertex));

                float vertProg = saturate((1 - v.color.a) - _Progress);
                v.vertex.y += vertProg*30*(v.color.r+1);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                return o;
            }

            sampler2D _BackgroundTexture;

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2Dproj(_BackgroundTexture, i.uv);
            }
            ENDCG
        }
    }
}

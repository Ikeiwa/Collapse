// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Projector/Projector"
{
    Properties
    {
        _ShadowTex("Cookie", 2D) = "white" {}
        _Attenuation("Falloff", Range(0.0, 1.0)) = 0.2
    }

	Subshader
    {
        Tags { "RenderType" = "Transparent"  "Queue" = "Transparent+100"}
        Pass
        {
            ZWrite Off
            Offset -1, -1

            Fog { Mode Off }

            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha


            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_fog_exp2
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"

			struct v2f {
				float4 uvShadow : TEXCOORD0;
				float4 uvClip : TEXCOORD1;
				float4 pos : SV_POSITION;
			};

			float4x4 unity_Projector;
			float4x4 unity_ProjectorClip;

			v2f vert(float4 vertex : POSITION)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(vertex);
				o.uvShadow = mul(unity_Projector, vertex);
				o.uvClip = mul(unity_ProjectorClip, vertex);
				return o;
			}

			sampler2D _ShadowTex;
            float _Attenuation;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 texS = tex2Dproj(_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));

				float2 uvMask = abs(floor(i.uvShadow.xy / i.uvShadow.w));
                float mask = 1-max(uvMask.x,uvMask.y);

                mask *= 1 - abs(floor(i.uvClip.z));

                clip(mask - 0.01f);

                float maskFar = 1 - smoothstep(1 - _Attenuation, 1, i.uvClip.z);
                float maskNear = smoothstep(0, _Attenuation, i.uvClip.z);

                texS.a *= saturate(maskFar * maskNear);

                return texS;
			}
            ENDCG

        }
    }
}
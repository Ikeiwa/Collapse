Shader "Unlit/Clouds"
{
    Properties
    {
    	_CloudColor("Clouds Color",Color) = (0,0,0,0)
        _MainTex ("Texture", 2D) = "white" {}
    	_Tiling("Tiling",Vector) = (1,1,0,0)
    	_FadeDist("Fade Distance",Float) = 1000
    }
    SubShader
    {
        Tags { "Queue" = "Background+1" "RenderType" = "Background" "IgnoreProjector" = "true"}
        LOD 100
    	Offset 1, 0
    	

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"
			#include "./noiseSimplex.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float distance : TEXCOORD1;
            };

            sampler2D _MainTex;
            float2 _Tiling;
            float _FadeDist;

            uniform sampler2D _MusicSpectrumTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = ((v.vertex*10000) + unity_ObjectToWorld._m03_m13_m23).xy* _Tiling;
                o.distance = mul(unity_ObjectToWorld, v.vertex).z - _WorldSpaceCameraPos.z;
                return o;
            }

            float4 _CloudColor;

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uvFloor = floor(i.uv);


                float3 tries = tex2D(_MainTex, i.uv).rgb;
                float2 triUV = uvFloor + tries.xy;

                float fade = saturate(1-pow(saturate(i.distance / _FadeDist),4));

                float triMask = tries.b;

                float noise = saturate(snoise(triUV*0.1) + snoise(triUV * 0.1 + float2(0, _Time.x)));

                noise *= 1+tex2D(_MusicSpectrumTex, float2(0.1, 0))/50.0f;

                float maskedTri = step(triMask, noise* fade);

                clip(maskedTri - 0.01f);

                return _CloudColor;
            }
            ENDCG
        }
    }
}

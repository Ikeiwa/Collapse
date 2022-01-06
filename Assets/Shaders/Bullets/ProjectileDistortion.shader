Shader "Collapse/Projectiles/Distortion"
{
    Properties
    {
        _Color("Color",Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
    	
    	GrabPass{"_SceneTexture"}

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 screenUV : TEXCOORD1;
                float3 worldViewDir : TEXCOORD2;
                float3 worldNormal : TEXCOORD3;
                float4 worldTangent : TEXCOORD4;
                float3 worldBitangent : TEXCOORD5;
                float4 vertex : SV_POSITION;
            };

            float4 _Color;
            sampler2D _SceneTexture;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldTangent = mul(unity_ObjectToWorld, v.tangent);
                o.worldBitangent = cross(o.worldNormal.xyz, o.worldTangent.xyz) * o.worldTangent.w;

                o.screenUV = ComputeGrabScreenPos(o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 refractDir = refract(-i.worldViewDir, i.worldNormal, 1.5f);


                refractDir = normalize(float3(
                    dot(refractDir, i.worldTangent.xyz),
                    dot(refractDir, i.worldBitangent.xyz),
                    dot(refractDir, i.worldNormal.xyz)
                    ));


                fixed4 grab = tex2Dproj(_SceneTexture, i.screenUV + float4(refractDir.xy,0,0));
                return grab*_Color;
            }
            ENDCG
        }
    }
}

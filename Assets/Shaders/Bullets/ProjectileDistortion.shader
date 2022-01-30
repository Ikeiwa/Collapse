Shader "Collapse/Projectiles/Distortion"
{
    Properties
    {
        [HDR] _Color("Color",Color) = (1,1,1,1)
        _Refraction("Refraction",Float) = 1
        _RefractionDistance("Refraction Scale",Float) = 1
        _FresnelPower("Fresnel Power", Float) = 1
        [Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull", Float) = 2
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		LOD 100

		ZWrite Off
        Cull[_Cull]
    	
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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 screenUV : TEXCOORD1;
                float4 vertex : SV_POSITION;
                float fresnel : TEXCOORD2;
            };

            float4 _Color;
            sampler2D _SceneTexture;
            float _Refraction;
            float _RefractionDistance;
            float _FresnelPower;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                //o.screenUV = ComputeGrabScreenPos(o.vertex);

                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                float3 objToEye = WorldSpaceViewDir(v.vertex);
                float3 refraction = normalize(refract(-objToEye, worldNormal, 1.0 / _Refraction));
                float3 objRefraction = mul(unity_WorldToObject, refraction) * _RefractionDistance;
                float4 newvertex = UnityObjectToClipPos(float4(objRefraction, v.vertex.w));

                o.fresnel = pow(1 - saturate(abs(dot(worldNormal, normalize(objToEye)))), _FresnelPower);
                o.screenUV = ComputeGrabScreenPos(newvertex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 grab = tex2Dproj(_SceneTexture, UNITY_PROJ_COORD(i.screenUV));
                return grab*lerp(1,_Color,i.fresnel*_Color.a);
            }
            ENDCG
        }
    }
}

Shader "Collapse/Projectiles/Field"
{
    Properties
    {
        [HDR]_Color("Color",Color) = (1,1,1,1)
    	_FresnelPower("Fresnel Power", Float) = 1
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

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
                float4 vertex : SV_POSITION;
                float fresnel : TEXCOORD1;
            };

            float4 _Color;
            float _FresnelPower;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
                float3 worldNormal = UnityObjectToWorldNormal(v.normal);

                o.fresnel = pow(1-saturate(dot(worldNormal, worldViewDir)),_FresnelPower);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                _Color.a = i.fresnel;

                return _Color;
            }
            ENDCG
        }
    }
}

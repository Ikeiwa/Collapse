Shader "Unlit/Sky"
{
    Properties
    {
        _ColorHorizon("Color Horizon",Color) = (0,0,0,1)
        _ColorZenith("Color Zenith",Color) = (1,1,1,1)
    	_GradientPower("Gradient Faloff",Float) = 1
    	_GradientOffset("Gradient Offset",Float) = 0
    }
    SubShader
    {
        Tags { "Queue" = "Background" "RenderType" = "Background" "PreviewType" = "Skybox" }
		Cull Off ZWrite Off

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
                float4 vertex : SV_POSITION;
                float3 viewDir : TEXCOORD0;
            };



            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
                return o;
            }

            float4 _ColorHorizon;
            float4 _ColorZenith;
            float _GradientPower;
            float _GradientOffset;

            fixed4 frag(v2f i) : SV_Target
            {
                return _ColorHorizon;
                float gradient = pow(saturate(dot(i.viewDir,float3(0,-1,0))+_GradientOffset),_GradientPower);

                return lerp(_ColorHorizon,_ColorZenith, gradient);
            }
            ENDCG
        }
    }
}

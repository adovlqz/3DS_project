Shader "Custom/DitherTextureTransparent"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _DitherTex ("Dither Pattern", 2D) = "white" {}
        _FadeAmount ("Fade Amount", Range(0,1)) = 1
        _Color ("Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _DitherTex;
            fixed4 _Color;
            float _FadeAmount;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float2 screenUV : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.screenUV = o.vertex.xy / o.vertex.w;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 ditherUV = frac(i.screenUV * 8); // 8 = tamaño del patrón
                float ditherSample = tex2D(_DitherTex, ditherUV).r;

                fixed4 col = tex2D(_MainTex, i.uv) * _Color;

                if (_FadeAmount < ditherSample)
                    col.a = 0;

                return col;
            }
            ENDCG
        }
    }
}

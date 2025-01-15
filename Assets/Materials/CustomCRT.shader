Shader "Custom/CustomCRT" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {} // Main texture
        _Transparency ("Transparency", Range(0, 1)) = 1.0 // Transparency level
    }
    SubShader {
        Tags { "Queue"="Overlay" "RenderType"="Transparent" }
        LOD 100

        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float _Transparency;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Sample the main texture
                fixed4 color = tex2D(_MainTex, i.uv);

                // Add scanlines (example)
                float scanline = sin(i.uv.y * 800.0) * 0.05;
                color.rgb *= (1.0 - scanline);

                // Apply transparency
                color.a *= _Transparency;

                return color;
            }
            ENDCG
        }
    }
}
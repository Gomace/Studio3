Shader "Unlit/Hologram"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //We added a new public property so that we can see and edit it in Unity.
        _TintColor("Tint Color", Color) = (1,1,1,1)
            _CutoutThresh("Cutout Threshold", Range(0.0,1.0)) = 0.2
            //Transparency, and the range of it. Meaning its lowest is 0.25 and its highest is 0.5
            _Transparency ("Transparency", Range(0.0,0.5)) = 0.25
            _Distance("Distance", Float) = 1
            _Amplitude("Amplitude", Float) = 1
            _Speed("Speed", Float) = 1
            _Amount("Amount", Range(0.0,1.0)) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
            //Switch to Zwrite off for transparent objects, it decides
            //if objects are written to the depth buffer or not.
            Zwrite Off
            //Render these things in order and at a certain point we need to blend them together
            //In this case we are blending them in our alpha channel.
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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            //We added a variable
            float4 _TintColor;
            float _CutoutThresh;
            float _Transparency;
            float _Distance;
                float _Amplitude;
                float _Speed;
                float _Amount;

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.x += sin(_Time.y * _Speed + v.vertex.y * _Amplitude) * _Distance * _Amount;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //We added that color in the fragment function to the color of our texture. You can also divide, multiply etc to get other effects.
                fixed4 col = tex2D(_MainTex, i.uv) + _TintColor;
                // apply fog
            col.a = _Transparency;
            clip(col.r - _CutoutThresh);
                return col;
            }
            ENDCG
        }
    }
}
